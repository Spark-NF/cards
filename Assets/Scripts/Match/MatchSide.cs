using System.Collections.Generic;

class MatchSide
{
	public Player Player;
	public int MaxLife;
	public int CurrentLife;
	public int CurrentEnergy;

	public Stack<Card> Pick = new Stack<Card>();
	public Stack<Card> Trash = new Stack<Card>();
	public List<Card> Hand = new List<Card>();
	public List<CardInstance> Front = new List<CardInstance>;
	public List<CardInstance> Back = new List<CardInstance>;

	private bool _hasPutResource = false;

	public MatchSide(Player player, Deck deck, int life)
	{
		Player = player;
		MaxLife = life;
		CurrentLife = maxLife;

		// We have to make a copy of the cards first because Shuffle() operates in place
		var cards = new List<Card>(deck.cards);
		cards.Shuffle();

		Pick = new Stack<Card>(cards);
	}

	public void BeginStep(MatchStep step)
	{
		CurrentEnergy = 0;

		switch (step)
		{
			case MatchStep.Untap:
				_hasPutResource = false;
				foreach (CardInstance card in Front)
					card.Untap();
				break;

			case MatchStep.Draw:
				PickCard();
				break;

			// TODO: manage next steps
		}
	}

	public void EndStep(MatchStep step)
	{
		// No-op
	}

	public void PickCard()
	{
		Hand.Append(Pick.Pop());
	}

	public void PutCard(Card card)
	{
		// Ensure we're not trying to put a card that we do not own
		if (!Hand.Contains(card))
			return;

		Hand.RemoveFirst(card);

		// You can only put one resource per turn
		if (card.Type == CardType.Resource && _hasPutResource)
			return;

		var cardInstance = new CardInstance(card, this);
		var row = card.Type == CardType.Resource ? Back : Front;

		row.Append(cardInstance);
	}

	public void RemoveCardInstance(CardInstance cardInstance)
	{
		Front.Remove(cardInstance);
		Back.Remove(cardInstance);

		Trash.Stack(cardInstance.Card);
	}
}
