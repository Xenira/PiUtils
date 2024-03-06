using System;
using System.Linq;
using UnityEngine;

namespace PiUtils.Util;

public class Callbacks
{
	public static Action<T> Debounce<T>(Action<T> callback, float seconds)
	{
		Coroutine timeout = null;
		T lastValue;

		return (T arg) =>
		{
			lastValue = arg;
			if (timeout != null)
			{
				return;
			}

			timeout = AsyncGameObject.Timeout(() =>
			{
				timeout = null;
				callback(lastValue);
			}, seconds);
		};
	}

	public static Action<T> Unique<T>(Action<T> callback)
	{
		Option<T> lastValue = Option<T>.None();

		return (T arg) =>
		{
			if (lastValue.Contains(arg))
			{
				return;
			}

			lastValue = Option<T>.Some(arg);
			callback(arg);
		};
	}
}
