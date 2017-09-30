using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		Game.Current = Game.CreateNew();

		// FIXME: multi-scene loading and streaming, the player should be cross-scene and set on load/new game
		Game.Current.Player = GameObject.FindWithTag("Player").GetComponent<GamePlayer>();

		// Load test data
		var cardManager = Game.Current.CardManager;
		Game.Current.Player.Inventory.Money = 1000;
		Game.Current.Player.Inventory.Cards = new CardList
		{
			cardManager.GetById(1),
			cardManager.GetById(2),
			cardManager.GetById(2),
			cardManager.GetById(3),
			cardManager.GetById(4),
			cardManager.GetById(5),
			cardManager.GetById(6),
			cardManager.GetById(6)
		};

		// TODO: Should load data BEFORE level to prevent TPs
		if (SaveManager.MustLoad >= 0)
		{
			SaveManager.Load(SaveManager.MustLoad);
			SaveManager.MustLoad = -1;
		}
	}
}
