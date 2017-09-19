using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform Target;
	public float Lerp = 0.1f;

	private void Update()
	{
		if (!Target)
			return;

		// FIXME: should not lerp on the last part of the movement to ensure target = transform (instead of infinitely /2)
		Vector3 targetPos = Target.position + new Vector3(0, 10, -4);
		Vector3 targetCamera = Vector3.Lerp(transform.position, targetPos, Lerp);

		transform.position = targetCamera;
	}
}
