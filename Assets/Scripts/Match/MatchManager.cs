using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
	public static MatchManager CurrentMatch;

	public SideManager PlayerSideManager;
	public SideManager FoeSideManager;
	public TurnNotifier Notifier;

	public MatchSide CurrentSide { get { return _sides[_currentSide]; } }

	private int _currentStep;
	private int _currentSide;
	private List<Step> _steps;
	private List<MatchSide> _sides;

	private void Start()
	{
		CurrentMatch = this;

		var cardManager = new CardManager();
		cardManager.LoadFromFile("Cards");

		var player = CreatePlayer("Player", cardManager);
		var foe = CreatePlayer("Foe", cardManager);

		_currentStep = 0;
		_currentSide = 0;
		_steps = new List<Step>
		{
			new UntapStep(),
			new DrawStep(),
			new SomeStep(),
		};
		_sides = new List<MatchSide>
		{
			new MatchSide(player, player.Decks[0], 20),
			new MatchSide(foe, foe.Decks[0], 20),
		};
		PlayerSideManager.MatchSide = _sides[0];
		FoeSideManager.MatchSide = _sides[1];

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
		StartCoroutine(NewTurn());
	}

	private IEnumerator NewTurn()
	{
		yield return Notifier.Notify("New turn");

		CurrentSide.NewTurn();

		StartCoroutine(RunCurrentStep());
	}

	private IEnumerator RunCurrentStep()
	{
		yield return _steps[_currentStep].Run(PlayerSideManager);

		_currentStep++;

		if (_currentStep >= _steps.Count)
		{
			_currentStep = 0;
			StartCoroutine(NewTurn());
		}
		else
		{
			StartCoroutine(RunCurrentStep());
		}
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
