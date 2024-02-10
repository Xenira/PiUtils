using System;
using BepInEx.Logging;
using UnityEngine;

namespace PiUtils.Util;

public class PluginLogger
{
	private ManualLogSource logger;
	private string prefix;

	public PluginLogger(ManualLogSource logger, string prefix)
	{
		this.logger = logger;
		this.prefix = prefix;
	}
	public PluginLogger(ManualLogSource logger, Type type)
	{
		this.logger = logger;
		prefix = type.FullName;
	}

	public static PluginLogger GetLogger<T>(ManualLogSource logger)
	{
		return new PluginLogger(logger, typeof(T));
	}

	public void LogInfo(string message)
	{
		logger.LogInfo($"[{prefix}] ({Time.frameCount}) {message}");
	}

	public void LogDebug(string message)
	{
		logger.LogDebug($"[{prefix}] ({Time.frameCount}) {message}");
	}

	public void LogWarning(string message)
	{
		logger.LogWarning($"[{prefix}] ({Time.frameCount}) {message}");
	}

	public void LogError(string message)
	{
		logger.LogError($"[{prefix}] ({Time.frameCount}) {message}");
	}

	internal void LogTrace(string v)
	{
		if (!ModConfig.TraceLogEnabled())
		{
			return;
		}
		logger.LogDebug($"[{prefix}] ({Time.frameCount}) {v}");
	}
}
