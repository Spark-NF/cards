using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SideManager : MonoBehaviour
{
	public Dealer Dealer;
	public Text RemainingPickCountText;
	[NonSerialized] public MatchSide MatchSide;

	public GameObject Units;
	private CardSlot _unitsCardSlot;
	private DropArea _unitsDropArea;

	public GameObject Resources;
	private CardSlot _resourcesCardSlot;
	private DropArea _resourcesDropArea;

	public GameObject Hand;
	private CardSlot _handCardSlot;
	private DropArea _handDropArea;

	private void Awake()
	{
		_unitsCardSlot = Units.GetComponent<CardSlot>();
		_unitsDropArea = Units.GetComponent<DropArea>();

		_resourcesCardSlot = Resources.GetComponent<CardSlot>();
		_resourcesDropArea = Resources.GetComponent<DropArea>();

		_handCardSlot = Hand.GetComponent<CardSlot>();
		_handDropArea = Hand.GetComponent<DropArea>();
	}

	public void AllowDragDrop()
	{
		_unitsDropArea.enabled = true;
		_resourcesDropArea.enabled = true;

		foreach (CardObject card in _handCardSlot.Cards)
		{
			card.GetComponent<CardDraggable>().enabled = true;
		}
	}

	public IEnumerator DropCards()
	{
		UpdatePickCountText();
		return Dealer.DropCards(MatchSide.Pick.Count);
	}

	private void UpdatePickCountText()
	{
		RemainingPickCountText.text = MatchSide.Pick.Count.ToString();
	}

	public IEnumerator PickHand(bool show, int count)
	{
		for (int i = 0; i < count; ++i)
		{
			yield return PickCard(show, false);

			// Add small interval if we don't show the cards to prevent lightspeed dealing
			if (!show)
				yield return new WaitForSeconds(0.5f);
		}
	}

	public IEnumerator PickCard(bool show, bool waitForClick, float waitDuration = 1f)
	{
		// Try to pick a card
		Card cardInfo = MatchSide.PickCard();
		if (cardInfo == null)
			yield break;

		// Update card view to match card info
		UpdatePickCountText();
		var card = Dealer.StackCardSlot.TopCard();
		card.CardView.SetCard(cardInfo);

		if (show)
		{
			// Show the card to the player
			card.TargetTransform.position = Camera.main.transform.position - new Vector3(0, 0.07f, -0.04f);
			card.TargetTransform.rotation = Camera.main.transform.rotation;

			// Wait for user interacton or a given amount of time
			if (waitForClick)
				while (!Input.GetMouseButtonDown(0))
					yield return null;
			else if (waitDuration > 0.001f)
				yield return new WaitForSeconds(waitDuration);
		}

		// Move card to hand
		_handCardSlot.AddCard(card);
		card.TargetTransform.rotation = Quaternion.Euler(90, 0, 0);
	}
}
