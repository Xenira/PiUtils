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
}

public class AsyncGameObject : MonoBehaviour
{
	private static PluginLogger Logger = PluginLogger.GetLogger<AsyncGameObject>(PiUtils.Logger);
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
