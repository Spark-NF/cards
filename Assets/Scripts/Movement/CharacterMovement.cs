using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour
{
	protected Rigidbody2D rbody;
	protected Animator anim;
	protected bool frozen = false;
	public float speed = 1.0f;

	private void Start()
	{
		rbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	public void LookAt(Vector2 movementVector)
	{
		anim.SetFloat("input_x", movementVector.x);
		anim.SetFloat("input_y", movementVector.y);
	}

	protected void Move(Vector2 movementVector)
	{
		if (frozen)
			return;

		bool hasVector = movementVector != Vector2.zero;
		anim.SetBool("is_walking", hasVector);
		if (hasVector)
		{
			LookAt(movementVector);
		}

		rbody.MovePosition(rbody.position + movementVector * Time.deltaTime * speed);
	}

	public void SetFrozen(bool frz)
	{
		frozen = frz;

		if (frz)
		{
			anim.SetBool("is_walking", false);
		}
	}
}
