using UnityEngine;

public class NPCMovement : CharacterMovement
{
	public bool canMove = true;
	public float moveDuration = 2.0f;
	public float waitDuration = 4.0f;
	public float moveDistance = 2.0f;
	public float randomizeDuration = 1.0f;

	private bool isMoving = true;
	private float lastMove = 0f;
	private float nextMove = 0f;
	private Vector2 destination;

	public void LateUpdate()
	{
		GameObject player = GameObject.FindWithTag("Player");
		bool above = player.transform.position.y > transform.position.y;
		GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder + (above ? 1 : -1);
	}

	public void Update()
	{
		if (!canMove)
			return;

		if (Time.time > lastMove + nextMove)
		{
			if (!isMoving)
			{
				bool x = Random.value > 0.5f;
				float dist = (Random.value * 2f - 1f) * moveDistance;

				destination = new Vector2(x ? dist : 0, x ? 0 : dist);
			}
			else
			{
				Move(Vector2.zero);
			}

			lastMove = Time.time;
			isMoving = !isMoving;
			nextMove = (isMoving ? moveDuration : waitDuration) + (Random.value - 0.5f) * randomizeDuration;
		}

		if (isMoving)
		{
			Move(destination);
		}
	}
}
