using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour
{
	public float speed = 1.0f;

	protected bool Frozen = false;

	private Rigidbody2D _rigidbody;
	private Animator _animator;

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
	}

	public void LookAt(Vector2 movementVector)
	{
		_animator.SetFloat("input_x", movementVector.x);
		_animator.SetFloat("input_y", movementVector.y);
	}

	protected void Move(Vector2 movementVector)
	{
		if (Frozen)
			return;

		bool hasVector = movementVector != Vector2.zero;
		_animator.SetBool("is_walking", hasVector);
		if (hasVector)
		{
			LookAt(movementVector);

			_rigidbody.MovePosition(_rigidbody.position + movementVector * Time.deltaTime * speed);
		}
	}

	public void SetFrozen(bool frz)
	{
		Frozen = frz;

		if (Frozen)
		{
			_animator.SetBool("is_walking", false);
		}
	}
}
