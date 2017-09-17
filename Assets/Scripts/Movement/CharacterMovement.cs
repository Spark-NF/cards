using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour
{
	public float Speed = 1.0f;

	protected bool Frozen = false;

	private Rigidbody2D _rigidbody;
	private Animator _animator;

	public bool IsFrozen
	{
		get { return Frozen; }
		set
		{
			Frozen = value;
			if (Frozen)
				_animator.SetBool("is_walking", false);
		}
	}

	public void Start()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
	}

	/// <summary>
	///     Adapts the animation according to the character direction.
	/// </summary>
	public void Animate(Vector2 movementVector)
	{
		// TODO: is_walking should be unnecessary in the animator as <> input_x && y == 0
		bool hasVector = movementVector != Vector2.zero;
		_animator.SetBool("is_walking", hasVector);
		if (!hasVector)
			return;

		// Character orientation
		_animator.SetFloat("input_x", movementVector.x);
		_animator.SetFloat("input_y", movementVector.y);
	}

	/// <summary>
	/// Shifts the character's position according to the specified vector and character speed.
	/// </summary>
	/// <param name="movementVector">The delta to apply to the current position.</param>
	protected void Move(Vector2 movementVector)
	{
		if (Frozen)
			return;

		Animate(movementVector);
		_rigidbody.MovePosition(_rigidbody.position + movementVector * Time.deltaTime * Speed);
	}
}
