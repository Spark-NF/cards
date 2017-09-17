using System.Collections;
using UnityEngine;

public class NPCMovement : CharacterMovement
{
	public bool CanMove = true;
	public float MinWaitDuration = 0.2f;
	public float MaxWaitDuration = 2.0f;
	public float MinMoveDistance = 0.2f;
	public float MaxMoveDistance = 1.0f;

	private bool _isMoving = false;
	private bool _isTouchingPlayer = false;

	private GameObject _player;
	private SpriteRenderer _spriteRenderer;
	private SpriteRenderer _playerSpriteRenderer;
	private Coroutine _movement;

	public new void Start()
	{
		base.Start();

		_player = GameObject.FindWithTag("Player");
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_playerSpriteRenderer = _player.GetComponent<SpriteRenderer>();
	}

	public void Update()
	{
		if (!CanMove)
			return;

		if (!_isMoving && !_isTouchingPlayer)
			_movement = StartCoroutine(WaitAndMove());
	}

	public void LateUpdate()
	{
		bool above = _player.transform.position.y > transform.position.y;
		_spriteRenderer.sortingOrder = _playerSpriteRenderer.sortingOrder + (above ? 1 : -1);
	}

	private IEnumerator WaitAndMove()
	{
		_isMoving = true;

		// Wait for next move
		yield return new WaitForSeconds(Randomize(MinWaitDuration, MaxWaitDuration));

		// Randomize step direction
		int dir = (int) (Random.value * 4); // [0 - 3]
		var step = new Vector2(dir % 2 == 0 ? dir - 1 : 0, dir % 2 == 1 ? dir - 2 : 0);

		// Randomize distance
		float dist = Randomize(MinMoveDistance, MaxMoveDistance);

		// Determine duration according to distance
		float moveDuration = dist / Speed;

		// Move for duration or until moving is set to false
		float time = 0f;
		while (_isMoving && time < moveDuration)
		{
			time += Time.deltaTime;
			Move(step);
			yield return null;
		}

		// Stop movement
		Move(Vector2.zero);
		_isMoving = false;
	}

	private float Randomize(float min, float max)
	{
		return min + Random.value * (max - min);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		// Stop moving if we are close to the player
		if (other.CompareTag("Player"))
		{
			StopMovement();
			_isTouchingPlayer = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_isTouchingPlayer = false;
		}
	}

	private void StopMovement()
	{
		if (!_isMoving || _movement == null)
			return;

		Move(Vector2.zero);
		_isMoving = false;
		StopCoroutine(_movement);
		_movement = null;
	}
}
