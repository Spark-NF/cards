using System.Collections.Generic;

public class CardManager
{
	private Dictionary<int, Card> _cards = new Dictionary<int, Card>();

	public Card GetById(int id)
	{
		return _cards[id];
	}

	public Card RandomCard()
	{
		return cards[(int)System.Math.Floor(Random.value * cards.Length)];
	}

	public List<Card> RandomCards(int count)
	{
		var cards = new List<Card>();
		for (int i = 0; i < count; ++i)
		{
			cards.Add(RandomCard());
		}
		return cards;
	}
}
