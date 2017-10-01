using UnityEngine;
using UnityEngine.UI;

public class DropArea : MonoBehaviour
{
	public float Width = 0.5f;
	public float Height = 0.2f;
	public Image ImageOver;

	private void Awake()
	{
		enabled = false;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Extensions.GizmosDrawRect(transform.position, Width, Height);
	}
}
