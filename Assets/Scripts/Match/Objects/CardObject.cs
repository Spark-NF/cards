using System;
using UnityEngine;

public class CardObject : MonoBehaviour
{
	public GameObject Canvas;
	public GameObject Borders;
	public CardView CardView;
	[NonSerialized] public Transform TargetTransform;
	[NonSerialized] public CardSlot ParentCardSlot;
	[NonSerialized] public float PositionDamp = 0.2f;
	[NonSerialized] public float RotationDamp = 0.2f;

	private Vector3 _smoothVelocity;
	private Vector4 _smoothRotationVelocity;

	private void Awake()
	{
		CheckVisibility();
		Borders.SetActive(false);

		// Instantiate target transform, which requires a GameObject
		var obj = new GameObject("Target");
		obj.transform.position = transform.position;
		obj.transform.forward = transform.forward;
		TargetTransform = obj.transform;
	}

	private void Update()
	{
		if (TargetTransform != null && (TargetTransform.position != transform.position || TargetTransform.eulerAngles != transform.eulerAngles))
		{
			transform.position = Vector3.SmoothDamp(transform.position, TargetTransform.position, ref _smoothVelocity, PositionDamp);

			Quaternion newRotation;
			newRotation.x = Mathf.SmoothDamp(transform.rotation.x, TargetTransform.rotation.x, ref _smoothRotationVelocity.x, RotationDamp);
			newRotation.y = Mathf.SmoothDamp(transform.rotation.y, TargetTransform.rotation.y, ref _smoothRotationVelocity.y, RotationDamp);
			newRotation.z = Mathf.SmoothDamp(transform.rotation.z, TargetTransform.rotation.z, ref _smoothRotationVelocity.z, RotationDamp);
			newRotation.w = Mathf.SmoothDamp(transform.rotation.w, TargetTransform.rotation.w, ref _smoothRotationVelocity.w, RotationDamp);
			transform.rotation = newRotation;

			CheckVisibility();
		}
	}

	private void CheckVisibility()
	{
		// Only enable canvas if the card is front-facing
		float forwardAngle = Vector3.Angle(Camera.main.transform.forward, transform.forward);
		Canvas.SetActive(forwardAngle < 90);
	}
}
