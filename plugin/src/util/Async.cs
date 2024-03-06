using System;
using System.Collections;
using UnityEngine;

namespace PiUtils.Util;

public class Async
{
	public static IEnumerator Timeout(Action callback, float seconds)
	{
		yield return new WaitForSeconds(seconds);
		callback();
	}

	public static IEnumerator TimeoutFrames(Action callback, int frames)
	{
		for (int i = 0; i < frames; i++)
		{
			yield return new WaitForEndOfFrame();
		}
		callback();
	}

	public static IEnumerator Interval(Action callback, float seconds, float startInSeconds, int? cnt = null)
	{
		yield return new WaitForSeconds(startInSeconds < 0 ? seconds : startInSeconds);
		while (true)
		{
			callback();

			if (cnt.HasValue)
			{
				if (--cnt <= 0)
				{
					break;
				}
			}

			yield return new WaitForSeconds(seconds);
		}
	}

	public static IEnumerator IntervalFrames(Action callback, int frames, int startInFrames, int? cnt = null)
	{
		for (int i = 0; i < startInFrames; i++)
		{
			yield return new WaitForEndOfFrame();
		}
		while (true)
		{
			callback();

			if (cnt.HasValue)
			{
				if (--cnt <= 0)
				{
					break;
				}
			}

			for (int i = 0; i < frames; i++)
			{
				yield return new WaitForEndOfFrame();
			}
		}
	}

	public static IEnumerator DelayUntilSet<T>(Action callback, T value)
	{
		yield return new WaitUntil(() => value != null);
		callback();
	}

	public static IEnumerator DelayUntil(IEnumerator condition, Action callback)
	{
		while (!condition.MoveNext())
		{
			yield return null;
		}
		callback();
	}

	public static IEnumerator DelayUntil(Action callback, Func<bool> condition)
	{
		yield return new WaitUntil(condition);
		callback();
	}
}

public class AsyncGameObject : MonoBehaviour
{
	private static PluginLogger Logger = PluginLogger.GetLogger<AsyncGameObject>();
	private static AsyncGameObject instance;

	private static AsyncGameObject Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameObject(nameof(AsyncGameObject)).AddComponent<AsyncGameObject>();
			}
			return instance;
		}
	}


	void Awake()
	{
		if (instance != null)
		{
			Logger.LogError("AsyncGameObject already exists, destroying this one");
			Destroy(this);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public static Coroutine Timeout(Action callback, float seconds)
	{
		return Instance.timeout(callback, seconds);
	}

	private Coroutine timeout(Action callback, float seconds)
	{
		return StartCoroutine(Async.Timeout(callback, seconds));
	}

	public static Coroutine TimeoutFrames(Action callback, int frames)
	{
		return Instance.timeoutFrames(callback, frames);
	}

	public static Coroutine NextFrame(Action callback)
	{
		return Instance.timeoutFrames(callback, 1);
	}

	private Coroutine timeoutFrames(Action callback, int frames)
	{
		return StartCoroutine(Async.TimeoutFrames(callback, frames));
	}

	public static Coroutine Interval(Action callback, float seconds, float startInSeconds, int? cnt = null)
	{
		return Instance.interval(callback, seconds, startInSeconds, cnt);
	}

	private Coroutine interval(Action callback, float seconds, float startInSeconds, int? cnt = null)
	{
		return StartCoroutine(Async.Interval(callback, seconds, startInSeconds, cnt));
	}

	public static Coroutine DelayUntilSet<T>(Action callback, T value)
	{
		return Instance.delayUntilSet(callback, value);
	}

	private Coroutine delayUntilSet<T>(Action callback, T value)
	{
		return StartCoroutine(Async.DelayUntilSet(callback, value));
	}

	public static Coroutine DelayUntil(Action callback, Func<bool> condition)
	{
		return Instance.delayUntil(callback, condition);
	}

	private Coroutine delayUntil(Action callback, Func<bool> condition)
	{
		return StartCoroutine(Async.DelayUntil(callback, condition));
	}

	public static void Cancel(Coroutine coroutine)
	{
		Instance.cancel(coroutine);
	}

	private void cancel(Coroutine coroutine)
	{
		if (coroutine != null)
		{
			StopCoroutine(coroutine);
		}
	}
}
