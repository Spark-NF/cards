using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class CardSlot : MonoBehaviour
{
	public readonly List<CardObject> Cards = new List<CardObject>();

	public CardObject TopCard()
	{
		return Cards.LastOrDefault();
	}

	public virtual void AddCard(CardObject card)
	{
		// Take ownership of the card
		if (card.ParentCardSlot != null)
			card.ParentCardSlot.RemoveCard(card);
		card.ParentCardSlot = this;
		Cards.Add(card);

		// Put the card on the slot
		card.TargetTransform.rotation = transform.rotation;
		card.TargetTransform.position = transform.position;
	}

	public void RemoveCard(CardObject card)
	{
		card.ParentCardSlot = null;
		Cards.Remove(card);
	}
}
