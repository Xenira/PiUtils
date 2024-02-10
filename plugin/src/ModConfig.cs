using BepInEx.Configuration;
using UnityEngine;

namespace PiUtils;

public class ModConfig
{
	// General
	private static ConfigEntry<bool> modEnabled;

	// Debug
	private static ConfigEntry<bool> debugMode;
	private static ConfigEntry<bool> gizmoEnabled;
	private static ConfigEntry<bool> debugLineEnabled;
	private static ConfigEntry<bool> traceLogEnabled;

	public static void Init(ConfigFile config)
	{
		// General
		modEnabled = config.Bind("General", "Enabled", true, "Enable mod");

		// Debug
		debugMode = config.Bind("Debug", "Debug Mode", false, "Enable debug mode");
		gizmoEnabled = config.Bind("Debug", "Gizmo Enabled", false, "Enable gizmos");
		debugLineEnabled = config.Bind("Debug", "Debug Line Enabled", false, "Enable debug lines");
		traceLogEnabled = config.Bind("Debug", "Trace Log Enabled", false, "Enable trace logs");
	}

	public static bool ModEnabled()
	{
		return modEnabled.Value;
	}

	// Debug
	public static bool GizmoEnabled()
	{
		return debugMode.Value && gizmoEnabled.Value;
	}

	public static bool DebugEnabled()
	{
		return debugMode.Value;
	}

	public static bool DebugLineEnabled()
	{
		return debugMode.Value && debugLineEnabled.Value;
	}

	public static bool TraceLogEnabled()
	{
		return debugMode.Value && traceLogEnabled.Value;
	}
}
