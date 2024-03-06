using System.Linq;
using PiUtils.Util;
using UnityEngine;

namespace PiUtils.Objects.Behaviours;
public class Grabable : MonoBehaviour
{
	private static readonly PluginLogger Logger = PluginLogger.GetLogger<Grabable>();

	public bool IsGrabbed { get; private set; } = false;
	public bool IsHovered { get; private set; } = false;

	public float grabDistance = 0.2f;

	public bool snapToHand = false;
	public Option<Vector3> grabOffset = Option<Vector3>.None();
	public Option<Quaternion> grabRotation = Option<Quaternion>.None();

	public Transform originalParent { get; private set; }

	public Grabable() { }

	public Grabable(float? grabDistance, bool snapToHand, Option<Vector3> grabOffset, Option<Quaternion> grabRotation)
	{
		this.grabDistance = grabDistance ?? this.grabDistance;
		this.snapToHand = snapToHand;
		this.grabOffset = grabOffset;
		this.grabRotation = grabRotation;
	}

	public bool TryGrab(Transform grabbingTransform)
	{
		if (IsGrabbed)
		{
			Logger.LogDebug("Object already grabbed: " + gameObject.name);
			return false;
		}

		var distance = Vector3.Distance(transform.position, grabbingTransform.position);
		if (grabDistance >= 0 && distance >= grabDistance)
		{
			Logger.LogDebug($"Object too far to grab: {gameObject.name} ({distance} > {grabDistance})");
			return false;
		}

		Grab(grabbingTransform);
		return IsGrabbed;
	}

	public void Grab(Transform grabbingTransform)
	{
		if (IsGrabbed)
		{
			return;
		}

		Logger.LogDebug("Grabbing object: " + gameObject.name);

		IsGrabbed = true;
		originalParent = transform.parent;

		if (snapToHand)
		{
			transform.position = grabbingTransform.position + grabOffset.FirstOrDefault();
			transform.rotation = grabbingTransform.rotation * grabRotation.FirstOrDefault();
		}

		transform.SetParent(grabbingTransform, true);
	}

	public void Release()
	{
		if (!IsGrabbed)
		{
			return;
		}

		IsGrabbed = false;
		transform.SetParent(originalParent, true);

		originalParent = null;
	}
}
