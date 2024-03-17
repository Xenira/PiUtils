using System;
using PiUtils.Util;
using UnityEngine;

namespace PiUtils.Objects.Behaviours;

public class Interactable : MonoBehaviour
{
	private static PluginLogger Logger = PluginLogger.GetLogger<Interactable>();

	public Transform interactionTransform;
	public float radius = 0.1f;
	public event Action OnEnter;
	public event Action OnExit;

	public bool isHovered { get; private set; } = false;

	private void Update()
	{
		if (interactionTransform == null)
		{
			return;
		}

		float distance = Vector3.Distance(interactionTransform.position, transform.position);
		if (distance <= radius && !isHovered)
		{
			Logger.LogDebug($"Hovered {gameObject.name}");
			isHovered = true;
			OnEnter?.Invoke();
			return;
		}

		if (distance > radius && isHovered)
		{
			Logger.LogDebug($"Unhovered {gameObject.name}");
			isHovered = false;
			OnExit?.Invoke();
		}
	}
}
