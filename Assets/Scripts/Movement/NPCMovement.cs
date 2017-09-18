using System.Collections;
using UnityEngine;

public class NPCMovement : CharacterMovement
{
	public bool CanMove = true;
	public float MinWaitDuration = 0.2f;
	public float MaxWaitDuration = 2.0f;
	public float MinMoveDistance = 0.2f;
	public float MaxMoveDistance = 1.0f;
	public Rect Bounds = new Rect(-1, -1, 2, 2);

	private bool _isMoving = false;
	private bool _isTouchingPlayer = false;

	private GameObject _player;
	private SpriteRenderer _spriteRenderer;
	private SpriteRenderer _playerSpriteRenderer;
	private Coroutine _movement;
	private Vector2 _startPos;

	public new void Start()
	{
		base.Start();

		_player = GameObject.FindWithTag("Player");
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_playerSpriteRenderer = _player.GetComponent<SpriteRenderer>();
		_startPos = transform.position;
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

		// Debug.
		DrawSquare(Color.yellow, _startPos + Bounds.position, Bounds.size);
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
		while (_isMoving && IsGoodDirection(step) && time < moveDuration)
		{
			time += Time.deltaTime;
			Move(step);
			yield return null;
		}

		// Stop movement
		Move(Vector2.zero);
		_isMoving = false;
	}

	private bool IsGoodDirection(Vector2 step)
	{
		if (step.x > 0 && transform.position.x > _startPos.x + Bounds.x + Bounds.width)
			return false;
		if (step.x < 0 && transform.position.x < _startPos.x + Bounds.x)
			return false;
		if (step.y > 0 && transform.position.y > _startPos.y + Bounds.y + Bounds.height)
			return false;
		if (step.y < 0 && transform.position.y < _startPos.y + Bounds.y)
			return false;
		return true;
	}

	/// <summary>
	///		Draws a debug square.
	/// </summary>
	/// <param name="color">The color to paint in.</param>
	/// <param name="position">The top left corner of the square.</param>
	/// <param name="size">The size of the square.</param>
	private static void DrawSquare(Color color, Vector2 position, Vector2 size)
	{
		Debug.DrawLine(position, position + new Vector2(size.x, 0), color);
		Debug.DrawLine(position + new Vector2(size.x, 0), position + size, color);
		Debug.DrawLine(position + size, position + new Vector2(0, size.y), color);
		Debug.DrawLine(position + new Vector2(0, size.y), position, color);
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
