using System.Collections.Generic;

class CardInstance
{
	public Card Card;
	public int Life;
	public List<CardInstance> Equipments = new List<CardInstance>();

	private MatchSide _side;
	private bool _isTapped = false;

	public CardInstance(Card card, MatchSide side)
	{
		Card = card;
		Life = card.Life;
		_side = side;
	}

	public void Equip(CardInstance equipment)
	{
		Equipments.Append(equipment);
	}

	public void Attack(CardInstance other)
	{
		if (_isTapped)
			return;

		other.Life -= Card.strength;
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
		_isTapped = true;
	}

	public void Untap()
	{
		_isTapped = false;
	}
}
