using System.Collections.Generic;

namespace PiUtils.Util;

public class Option<T> : IEnumerable<T>
{
	private readonly T[] data;

	private Option(T[] data)
	{
		this.data = data;
	}

	public static Option<T> New(T value)
	{
		if (value == null)
		{
			return None();
		}

		return Some(value);
	}

	public static Option<T> Some(T value)
	{
		return new Option<T>([value]);
	}

	public static Option<T> None()
	{
		return new Option<T>([]);
	}

	public bool IsSome()
	{
		return data.Length > 0;
	}

	public IEnumerator<T> GetEnumerator()
	{
		return ((IEnumerable<T>)data).GetEnumerator();
	}

	System.Collections.IEnumerator
			System.Collections.IEnumerable.GetEnumerator()
	{
		return data.GetEnumerator();
	}
}
