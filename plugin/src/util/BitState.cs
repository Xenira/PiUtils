namespace PiUtils.Input;

public class BitState
{
	public long state = 0;
	private long initialState = 0;

	private long nextNewFlag = 1;

	public long GetNextFlag()
	{
		var result = nextNewFlag;
		nextNewFlag <<= 1;
		return result;
	}

	internal void reset()
	{
		state = initialState;
	}

	public void setState(long newState)
	{
		state = newState;
	}

	public void addState(long newState)
	{
		state |= newState;
	}

	public void removeState(long newState)
	{
		state &= ~newState;
	}

	public void toggleState(long newState)
	{
		state ^= newState;
	}

	public bool hasState(long newState)
	{
		return (state & newState) != 0;
	}

	public bool hasAnyState(long newState)
	{
		return (state & newState) != 0;
	}
}
