using UnityEngine;

public class PlayerMovement : CharacterMovement
{
	public GameObject Front;

	private void Update()
	{
		// Move player according to input (keyboard and mobile)
		var movementVector = new Vector3(
			CnControls.CnInputManager.GetAxisRaw("Horizontal"),
			0,
			CnControls.CnInputManager.GetAxisRaw("Vertical")
		);
		Move(movementVector);
	}
}
