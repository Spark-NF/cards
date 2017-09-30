using UnityEngine;
using System.Collections.Generic;

public class CardSlot : MonoBehaviour
{
	private readonly List<CardObject> _cards = new List<CardObject>();

	public void AddCard(CardObject card)
	{
		// Take ownership of the card
		if (card.ParentCardSlot != null)
			card.ParentCardSlot.RemoveCard(card);
		card.ParentCardSlot = this;
		_cards.Add(card);

		// Give the card the same rotation of the slot, with a slight randomness
		card.TargetTransform.rotation = transform.rotation;
		card.TargetTransform.Rotate(card.TargetTransform.forward, Random.Range(-0.4f, 0.4f), Space.Self);

		// Stack the card on top of others
		float cardHeight = card.GetComponent<BoxCollider>().size.z;
		card.TargetTransform.position = transform.position;
		card.TargetTransform.Translate(new Vector3(0, 0, _cards.Count * cardHeight), Space.Self);
	}

	public void RemoveCard(CardObject card)
	{
		card.ParentCardSlot = null;
		_cards.Remove(card);
	}
}
