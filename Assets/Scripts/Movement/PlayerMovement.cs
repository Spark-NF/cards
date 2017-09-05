using UnityEngine;

public class PlayerMovement : CharacterMovement
{
	public GameObject front;

	private void Update()
	{
		// Move player according to input (keyboard and mobile)
		Vector2 movementVector = new Vector2(
			CnControls.CnInputManager.GetAxisRaw("Horizontal"),
			CnControls.CnInputManager.GetAxisRaw("Vertical")
		);
		Move(movementVector);

		// Update eyes
		/*if (movementVector != Vector2.zero && !frozen)
		{
			int angle;
			if (Mathf.Abs(movementVector.x) > Mathf.Abs(movementVector.y))
			{
				angle = movementVector.x > 0 ? 90 : -90;
			}
			else
			{
				angle = movementVector.y > 0 ? 180 : 0;
			}

			front.transform.rotation = Quaternion.Euler(0, 0, angle);
		}*/
	}
}
