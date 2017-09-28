using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
	public GamePlayer Player;
	public GameObject CardSlotPrefab;

	private GameObject _cardGrid;
	private List<CardSlot> _cardSlots;

	private void Start()
	{
		_cardGrid = transform.Find("CardGrid").gameObject;
		_cardSlots = new List<CardSlot>();

		// Add the player's cards.
		foreach (Card card in Player.Inventory.Cards)
		{
			var cardSlot = _cardSlots.FirstOrDefault(c => c.Card.Id == card.Id);
			if (cardSlot != null)
			{
				cardSlot.CardCount++;
				continue;
			}
			GameObject slot = Instantiate(CardSlotPrefab, _cardGrid.transform);
			_cardSlots.Add(new CardSlot(card, slot));
		}
	}

	private void Update ()
	{
	}

	private class CardSlot
	{
		public readonly Card Card;
		public readonly GameObject Slot;
		public int CardCount;

		public CardSlot(Card card, GameObject slot)
		{
			Card = card;
			Slot = slot;
			CardCount = 1;
		}
	}
}
