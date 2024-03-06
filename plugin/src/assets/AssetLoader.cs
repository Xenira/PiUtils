using System.Collections;
using System.IO;
using BepInEx;
using PiUtils.Util;
using UnityEngine;

namespace PiUtils.Assets;

public class AssetLoader
{
	private static PluginLogger Logger = PluginLogger.GetLogger<AssetLoader>();

	private string assetPath = "assets";

	public AssetLoader()
	{
	}

	public AssetLoader(string assetPath)
	{
		this.assetPath = assetPath;
	}


	public IEnumerator Load<T>(AssetBundle bundle, string prefabName) where T : Object
	{
		var asset = LoadAsset<T>(bundle, prefabName);
		if (asset)
			yield return asset;
		else
		{
			Logger.LogError($"Failed to load asset {prefabName}");
			yield return null;
		}
	}

	public T LoadAsset<T>(AssetBundle bundle, string prefabName) where T : Object
	{
		var asset = bundle.LoadAsset<T>($"Assets/{prefabName}");
		if (asset)
			return asset;
		else
		{
			Logger.LogError($"Failed to load asset {prefabName}");
			return null;
		}

	}

	public AssetBundle LoadBundle(string assetName)
	{
		var bundle =
				AssetBundle.LoadFromFile(GetAssetPath(assetName));
		if (bundle == null)
		{
			Logger.LogError($"Failed to load AssetBundle {assetName}");
			return null;
		}

		return bundle;
	}

	private string GetAssetPath(string assetName)
	{
		return Path.Combine(Paths.PluginPath, Path.Combine(assetPath, assetName));
	}
}
