namespace PiUtils.Input;

public class BitState
{
	public static long state = 0;
	private static long initialState = 0;

	private static long nextNewFlag = 1;

	public static long GetNextFlag()
	{
		var result = nextNewFlag;
		nextNewFlag <<= 1;
		return result;
	}

	internal static void reset()
	{
		state = initialState;
	}

	public static void setState(long newState)
	{
		state = newState;
	}

	public static void addState(long newState)
	{
		state |= newState;
	}

	public static void removeState(long newState)
	{
		state &= ~newState;
	}

	public static void toggleState(long newState)
	{
		state ^= newState;
	}

	public static bool hasState(long newState)
	{
		return (state & newState) != 0;
	}

	public static bool hasAnyState(long newState)
	{
		return (state & newState) != 0;
	}
}
