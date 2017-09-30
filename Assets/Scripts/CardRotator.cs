using UnityEngine;

public class CardRotator : MonoBehaviour
{
	public CardView CardView;
	public float Interval = 2f;

	private CardManager _cardManager;
	private int _current;
	private int _count;

	private void Start()
	{
		_cardManager = new CardManager();
		_cardManager.LoadFromFile("Cards");

		_current = 0;
		_count = 8;

		InvokeRepeating("RotateImage", 0, Interval);
	}

	private void RotateImage()
	{
		Card card = _cardManager.GetById(_current + 1);
		CardView.SetCard(card);

		_current = (_current + 1) % _count;
	}
}
