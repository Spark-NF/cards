using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public float lerp = 0.1f;
	private float initZ;

	void Start()
	{
		initZ = transform.position.z;
	}

	void Update()
	{
		if (!target)
			return;

		Vector3 targetCamera = Vector3.Lerp(transform.position, target.position, lerp);
		targetCamera.z = initZ;

		transform.position = targetCamera;
	}
}
