using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager
{
	private Dictionary<int, Card> _cards = new Dictionary<int, Card>();

	public Card GetById(int id)
	{
		return _cards[id];
	}

	public Card RandomCard()
	{
        int index = (int)System.Math.Floor(Random.value * _cards.Count);
        var keys = _cards.Keys.ToArray();
        return _cards[keys[index]];
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
