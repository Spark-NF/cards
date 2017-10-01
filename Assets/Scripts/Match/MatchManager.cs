using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
	public static Combat CurrentMatch;

	public SideManager PlayerSideManager;
	public SideManager FoeSideManager;
	public TurnNotifier Notifier;

	private MatchSide _sidePlayer;
	private MatchSide _sideFoe;

	private void Start()
	{
		var cardManager = new CardManager();
		cardManager.LoadFromFile("Cards");

		var player = CreatePlayer("Player", cardManager);
		var foe = CreatePlayer("Foe", cardManager);

		PlayerSideManager.MatchSide = new MatchSide(player, player.Decks[0], 20);
		FoeSideManager.MatchSide = new MatchSide(foe, foe.Decks[0], 20);
		CurrentMatch = new Combat(new []
		{
			PlayerSideManager.MatchSide,
			FoeSideManager.MatchSide
		});

		StartCoroutine(StartGame());
	}

	private IEnumerator StartGame()
	{
		// Drop both decks
		var playerDrop = PlayerSideManager.DropCards();
		var foeDrop = FoeSideManager.DropCards();
		while (playerDrop.MoveNext() || foeDrop.MoveNext())
			yield return null;

		// Hand pick after a small delay
		yield return new WaitForSeconds(1);

		// Pick both hands
		var playerPickHand = PlayerSideManager.PickHand(true, 5);
		StartCoroutine(FoeSideManager.PickHand(false, 5));
		yield return playerPickHand;

		// Start first turn
		yield return NewTurn();
	}

	private IEnumerator NewTurn()
	{
		yield return Notifier.Notify("New turn");

		// Pick card
		yield return PlayerSideManager.PickCard(true, true);

		// Allow to drag hand cards
		PlayerSideManager.AllowDragDrop();
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
