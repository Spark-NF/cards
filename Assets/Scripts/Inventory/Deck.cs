using System.Collections.Generic;

public class Deck
{
	public string Name;
	public List<Card> Cards;

	public bool IsValid(List<Card> cards)
	{
		Dictionnary<int, int> cardCount = cards.GroupBy(x => x.Id).ToDictionnary(g => g.Id, g.Count);

		foreach (Card card in Cards)
		{
			if (!cardCount.Contains(card.Id) || --cardCount[card.Id] < 0)
				return false;
		}

		return true;
	}
}
