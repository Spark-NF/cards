using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour
{
	public float Speed = 3.0f;
	public float RotationSpeed = 5.0f;

	private bool _frozen = false;
	private Rigidbody _rigidbody;

	public bool IsFrozen
	{
		get { return _frozen; }
		set { _frozen = value; }
	}

	public void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	/// <summary>
	/// Shifts the character's position according to the specified vector and character speed.
	/// </summary>
	/// <param name="movementVector">The delta to apply to the current position.</param>
	protected void Move(Vector3 movementVector)
	{
		if (IsFrozen || movementVector == Vector3.zero)
			return;

		_rigidbody.transform.Translate(movementVector * Time.deltaTime * Speed, Space.World);
		_rigidbody.transform.rotation = Quaternion.Lerp(_rigidbody.transform.rotation, Quaternion.LookRotation(movementVector), Time.fixedDeltaTime * RotationSpeed);
	}
}
