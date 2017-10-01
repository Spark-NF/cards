using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
	public Dealer DealerPlayer;
	public Dealer DealerFoe;
	public CardSlot PlayerHand;
	public TurnNotifier Notifier;

	private Combat _combat;
	private MatchSide _sidePlayer;
	private MatchSide _sideFoe;

	private void Start()
	{
		var cardManager = new CardManager();
		cardManager.LoadFromFile("Cards");

		var player = CreatePlayer("Player", cardManager);
		var foe = CreatePlayer("Foe", cardManager);

		_sidePlayer = new MatchSide(player, player.Decks[0], 20);
		_sideFoe = new MatchSide(foe, foe.Decks[0], 20);
		_combat = new Combat(new [] { _sidePlayer, _sideFoe });

		StartCoroutine(StartGame());
	}

	private IEnumerator StartGame()
	{
		// Drop both decks
		var playerDrop = DealerPlayer.DropCards(_sidePlayer.Pick.Count);
		var foeDrop = DealerFoe.DropCards(_sideFoe.Pick.Count);
		while (playerDrop.MoveNext() || foeDrop.MoveNext())
			yield return null;

		// Hand pick after a small delay
		yield return new WaitForSeconds(2);
		yield return PickHand(5);

		// New turn
		yield return Notifier.Notify("New turn");

		// Pick card
		yield return PickCardAsync();
	}

	private IEnumerator PickHand(int count)
	{
		for (int i = 0; i < count; ++i)
			yield return PickCardAsync(1);
	}

	private IEnumerator PickCardAsync(float waitForSeconds = -1f)
	{
		var card = DealerPlayer.StackCardSlot.TopCard();
		if (card == null)
			yield break;

		// Link card object to actual match card
		Card cardInfo = _sidePlayer.PickCard();
		card.CardView.SetCard(cardInfo);

		// Show the card to the player
		card.TargetTransform.position = Camera.main.transform.position - new Vector3(0, 0.07f, -0.04f);
		card.TargetTransform.rotation = Camera.main.transform.rotation;

		// Wait for user interacton or a given amount of time
		if (waitForSeconds < -0.001f)
			while (!Input.GetMouseButtonDown(0))
				yield return null;
		else if (waitForSeconds > 0.001f)
			yield return new WaitForSeconds(waitForSeconds);

		// Move card to hand
		PlayerHand.AddCard(card);
		card.TargetTransform.rotation = Quaternion.Euler(90, 0, 0);
	}

	private static Player CreatePlayer(string name, CardManager cardManager)
	{
		return new Player
		{
			Name = name,
			Decks = new List<Deck>
			{
				new Deck
				{
					Name = "Basic deck",
					Cards = new CardList
					{
						cardManager.GetById(1),
						cardManager.GetById(2),
						cardManager.GetById(3),
						cardManager.GetById(4),
						cardManager.GetById(5),
						cardManager.GetById(6),
						cardManager.GetById(6),
						cardManager.GetById(6),
						cardManager.GetById(7),
						cardManager.GetById(7),
						cardManager.GetById(8)
					}
				}
			}
		};
	}
}
