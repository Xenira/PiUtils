using UnityEngine;

namespace PiUtils.Util;

public class MathyStuff
{
	public static void PositionCanvasInWorld(GameObject tlc, Camera cam, Vector3 point, Vector3 lookAt = default)
	{
		var canvas = tlc.transform.GetComponentInParent<Canvas>();
		Vector3 screenPoint = cam.WorldToScreenPoint(point);
		Vector3 canvasPoint = canvas.worldCamera.ScreenToWorldPoint(screenPoint);
		tlc.transform.position = canvasPoint;

		if (lookAt != Vector3.zero)
		{
			Vector3 direction = point - lookAt;
			Quaternion rotation = Quaternion.LookRotation(direction);

			tlc.transform.rotation = Quaternion.Inverse(cam.transform.rotation) * rotation;
		}
	}
}
