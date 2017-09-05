using UnityEngine;

public class NPCMovement : CharacterMovement
{
	public bool CanMove = true;
	public float MoveDuration = 2.0f;
	public float WaitDuration = 4.0f;
	public float MoveDistance = 2.0f;
	public float RandomizeDuration = 1.0f;

	private bool _isMoving = true;
	private float _lastMove = 0f;
	private float _nextMove = 0f;
	private Vector2 _destination;

	public void LateUpdate()
	{
		var player = GameObject.FindWithTag("Player");
		bool above = player.transform.position.y > transform.position.y;
		GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder + (above ? 1 : -1);
	}

	public void Update()
	{
		if (!CanMove)
			return;

		if (Time.time > _lastMove + _nextMove)
		{
			if (!_isMoving)
			{
				bool x = Random.value > 0.5f;
				float dist = (Random.value * 2f - 1f) * MoveDistance;

				_destination = new Vector2(x ? dist : 0, x ? 0 : dist);
			}
			else
			{
				Move(Vector2.zero);
			}

			_lastMove = Time.time;
			_isMoving = !_isMoving;
			_nextMove = (_isMoving ? MoveDuration : WaitDuration) + (Random.value - 0.5f) * RandomizeDuration;
		}

		if (_isMoving)
		{
			Move(_destination);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		// Stop moving if we are close to the player
		if (other.CompareTag("Player"))
		{
			_isMoving = false;
			Move(Vector2.zero);
		}
	}
}
