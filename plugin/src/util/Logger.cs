using System;
using System.Collections.Generic;
using BepInEx.Logging;
using UnityEngine;

namespace PiUtils.Util;

public class PluginLogger
{
	public static Dictionary<string, ManualLogSource> loggers = new Dictionary<string, ManualLogSource>();

	private string prefix;
	private ManualLogSource logger
	{
		get
		{
			if (loggers.ContainsKey(prefix))
			{
				return loggers[prefix];
			}

			var logger = BepInEx.Logging.Logger.CreateLogSource(prefix);
			loggers.Add(prefix, logger);
			return logger;
		}
	}

	public PluginLogger(string prefix)
	{
		this.prefix = prefix;
	}

	public PluginLogger(Type type) : this(type.FullName)
	{
	}

	public static PluginLogger GetLogger<T>()
	{
		return new PluginLogger(typeof(T));
	}

	public void LogInfo(string message)
	{
		logger.LogInfo($"<{Time.frameCount}> {message}");
	}

	public void LogDebug(string message)
	{
		logger.LogDebug($"<{Time.frameCount}> {message}");
	}

	public void LogWarning(string message)
	{
		logger.LogWarning($"<{Time.frameCount}> {message}");
	}

	public void LogError(string message)
	{
		logger.LogError($"<{Time.frameCount}> {message}");
	}

	public void LogTrace(string v)
	{
		if (!ModConfig.TraceLogEnabled())
		{
			return;
		}
		logger.LogDebug($"<{Time.frameCount}> {v}");
	}
}
