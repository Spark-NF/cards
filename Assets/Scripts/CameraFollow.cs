using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public float lerp = 0.1f;
	private float initZ;

	private void Start()
	{
		initZ = transform.position.z;
	}

	private void Update()
	{
		if (!target)
			return;

		// FIXME: should not lerp on the last part of the movement to ensure target = transform (instead of infinitely /2)
		Vector3 targetCamera = Vector3.Lerp(transform.position, target.position, lerp);
		targetCamera.z = initZ;

		transform.position = targetCamera;
	}
}
