using UnityEngine;

namespace PiUtils.Util;

public static class TransformUtils
{
	public static void DestroyAllChildren(this Transform transform)
	{
		for (int i = transform.childCount - 1; i >= 0; i--)
		{
			GameObject.Destroy(transform.GetChild(i).gameObject);
		}
	}
}
