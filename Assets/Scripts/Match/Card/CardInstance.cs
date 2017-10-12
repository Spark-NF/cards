using System.Collections.Generic;

/// <summary>
/// Model of a card that is currently in game.
/// </summary>
public class CardInstance
{
	public Card Card;
	public int Life;
	public List<CardInstance> Equipments = new List<CardInstance>();
	public bool IsTapped { get; private set; }

	private MatchSide _side;

	public CardInstance(Card card, MatchSide side)
	{
		Card = card;
		Life = card.Life;
		_side = side;
	}

	public void Equip(CardInstance equipment)
	{
		Equipments.Add(equipment);
	}

	public void Attack(CardInstance other)
	{
		if (IsTapped)
			return;

		other.Life -= Card.Strength;
		if (other.Life <= 0)
			other.Die();

		Tap();
	}

	public void Die()
	{
		_side.RemoveCardInstance(this);
	}

	public void Tap()
	{
		IsTapped = true;
	}

	public void Untap()
	{
		IsTapped = false;
	}
}
