using System.Diagnostics;
using System.IO;
using BepInEx;
using BepInEx.Logging;
using System.Reflection;
using HarmonyLib;
using System;
using PiUtils.Util;

namespace PiUtils;

[BepInPlugin("de.xenira.pi_utils", MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class PiUtils : BaseUnityPlugin
{
	public static new ManualLogSource Logger;
	private static PluginLogger logger;

	public static string gameExePath = Process.GetCurrentProcess().MainModule.FileName;
	public static string gamePath = Path.GetDirectoryName(gameExePath);

	private void Awake()
	{
		Logger = base.Logger;
		logger = PluginLogger.GetLogger<PiUtils>(Logger);

		logger.LogInfo($"Loading plugin {MyPluginInfo.PLUGIN_GUID} version {MyPluginInfo.PLUGIN_VERSION}...");
		License.LogLicense(logger, "Xenira");

		ModConfig.Init(Config);

		if (!ModConfig.ModEnabled())
		{
			logger.LogInfo("Mod is disabled, skipping...");
			return;
		}

		Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

		logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
	}
}
