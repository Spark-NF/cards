using System.Collections;
using UnityEngine;

public class NPCMovement : CharacterMovement
{
	public bool CanMove = true;
	public float MoveDuration = 2.0f;
	public float WaitDuration = 4.0f;
	public float MoveDistance = 2.0f;
	public float RandomizeDuration = 1.0f;

	private bool _isMoving = false;

	private GameObject _player;
	private SpriteRenderer _spriteRenderer;
	private SpriteRenderer _playerSpriteRenderer;

	public new void Start()
	{
		base.Start();

		_player = GameObject.FindWithTag("Player");
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_playerSpriteRenderer = _player.GetComponent<SpriteRenderer>();
	}

	public void LateUpdate()
	{
		bool above = _player.transform.position.y > transform.position.y;
		_spriteRenderer.sortingOrder = _playerSpriteRenderer.sortingOrder + (above ? 1 : -1);
	}

	public void Update()
	{
		if (!CanMove)
			return;

		if (!_isMoving)
			StartCoroutine(WaitAndMove());
	}

	private float Randomize(float duration)
	{
		return duration + (Random.value - 0.5f) * RandomizeDuration;
	}

	private IEnumerator WaitAndMove()
	{
		_isMoving = true;

		// Wait for next move
		yield return new WaitForSeconds(Randomize(WaitDuration));

		// Determine our next destination
		bool x = Random.value > 0.5f;
		float dist = (Random.value * 2f - 1f) * MoveDistance;
		var destination = new Vector2(x ? dist : 0, x ? 0 : dist);

		// Move to the distation
		float moveDuration = Randomize(MoveDuration);
		float time = 0f;
		while (time < moveDuration)
		{
			time += Time.deltaTime;
			Move(destination);
			yield return null;
		}

		Move(Vector2.zero);

		_isMoving = false;
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
