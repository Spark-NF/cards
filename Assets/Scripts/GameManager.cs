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
		// FIXME: multi-scene loading and streaming, the player should be cross-scene and set on load/new game
		Game.Current.Player = GameObject.FindWithTag("Player").GetComponent<GamePlayer>();

		// Load test data
		Game.Current.Player.Inventory.Money = 1000;
		// TODO: Use card manager.
		//var cardManager = new CardManager();
		var cards = new CardList
		{
			//cardManager.GetById(0),
			new Card { Id = 0 },
			new Card { Id = 1 },
			new Card { Id = 1 },
			new Card { Id = 2 },
			new Card { Id = 3 },
			new Card { Id = 4 },
			new Card { Id = 5 },
			new Card { Id = 5 }
		};
		Game.Current.Player.Inventory.Cards = cards;

		// Should load data BEFORE level to prevent TPs
		if (SaveManager.MustLoad >= 0)
		{
			SaveManager.Load(SaveManager.MustLoad);
			SaveManager.MustLoad = -1;
		}
	}
}
