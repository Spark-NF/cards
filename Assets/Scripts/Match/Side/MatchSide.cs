using System.Collections.Generic;

public class MatchSide
{
	public Player Player;
	public int MaxLife;
	public int CurrentLife;
	public int CurrentEnergy;

	public Stack<Card> Pick = new Stack<Card>();
	public Stack<Card> Trash = new Stack<Card>();
	public List<Card> Hand = new List<Card>();
	public List<CardInstance> Front = new List<CardInstance>();
	public List<CardInstance> Back = new List<CardInstance>();

	private bool _hasPutResource = false;

	public MatchSide(Player player, Deck deck, int life)
	{
		Player = player;
		MaxLife = life;
		CurrentLife = MaxLife;

		// We have to make a copy of the cards first because Shuffle() operates in place
		var cards = new List<Card>(deck.Cards);
		cards.Shuffle();

		Pick = new Stack<Card>(cards);
	}

	public void NewTurn()
	{
		_hasPutResource = false;
	}

	public Card PickCard()
	{
		if (Pick.Count == 0)
			return null;

		Card card = Pick.Pop();
		Hand.Add(card);
		return card;
	}

	public bool CanPutInRow(Card card, CardType row)
	{
		// Ensure we're not trying to put a card that we do not own
		if (!Hand.Contains(card))
			return false;

		// You can only put cards in their row
		if (card.Type != row)
			return false;

		// You can only put one resource per turn
		if (row == CardType.Resource && _hasPutResource)
			return false;

		return true;
	}

	public CardInstance PutInRow(Card card, CardType row)
	{
		if (!CanPutInRow(card, row))
			return null;

		Hand.Remove(card);
		var instance = new CardInstance(card, this);
		var list = row == CardType.Resource ? Back : Front;
		list.Add(instance);

		if (row == CardType.Resource)
			_hasPutResource = true;

		return instance;
	}

	public void RemoveCardInstance(CardInstance cardInstance)
	{
		Front.Remove(cardInstance);
		Back.Remove(cardInstance);

		Trash.Push(cardInstance.Card);
	}
}
