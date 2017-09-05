using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Deck
{
	public string Name;
	public CardList Cards;

	public bool IsValid(List<Card> cards)
	{
		Dictionary<int, int> cardCount = cards.GroupBy(x => x.Id).ToDictionary(g => g.Key, g => g.Count());

		foreach (Card card in Cards)
		{
			if (!cardCount.ContainsKey(card.Id) || --cardCount[card.Id] < 0)
				return false;
		}

		return true;
	}
}
