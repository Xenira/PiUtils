using System.IO;
using System.Reflection;
using PiUtils.Util;

namespace PiUtils.Assets;

public class DependencyLoader
{
	private static PluginLogger Logger = PluginLogger.GetLogger<DependencyLoader>();

	public static void LoadDirectory(string path)
	{
		string[] files = Directory.GetFiles(path, "*.dll");

		foreach (string file in files)
		{
			try
			{
				Assembly.LoadFile(file);
			}
			catch (System.Exception e)
			{
				Logger.LogError($"Failed to load assembly {file}", e);
			}
		}
	}
}
