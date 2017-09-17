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

		// Should load data BEFORE level to prevent TPs
		if (SaveManager.MustLoad >= 0)
		{
			SaveManager.Load(SaveManager.MustLoad);
			SaveManager.MustLoad = -1;
		}
	}
}
