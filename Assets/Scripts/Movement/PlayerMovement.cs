using UnityEngine;

public class PlayerMovement : CharacterMovement
{
	public GameObject Front;

	private void Update()
	{
		// Move player according to input (keyboard and mobile)
		Vector2 movementVector = new Vector2(
			CnControls.CnInputManager.GetAxisRaw("Horizontal"),
			CnControls.CnInputManager.GetAxisRaw("Vertical")
		);
		Move(movementVector);

		// Update eyes
		if (movementVector != Vector2.zero && !Frozen)
		{
			int angle = Mathf.Abs(movementVector.x) > Mathf.Abs(movementVector.y)
				? movementVector.x > 0 ? 90 : -90
				: movementVector.y > 0 ? 180 : 0;

			Front.transform.rotation = Quaternion.Euler(0, 0, angle);
		}
	}
}
