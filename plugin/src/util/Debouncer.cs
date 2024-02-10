using System;
using UnityEngine;

namespace PiUtils.Util;

public class Debouncer
{
	public static Action<T> Debounce<T>(Action<T> callback, float seconds)
	{
		Coroutine timeout = null;

		return (T arg) =>
		{
			if (timeout != null)
			{
				AsyncGameObject.Cancel(timeout);
			}

			timeout = AsyncGameObject.Timeout(() => callback(arg), seconds);
		};
	}
}
