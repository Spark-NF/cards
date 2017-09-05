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
		Game.Current.Player = GameObject.FindWithTag("Player").GetComponent<GamePlayer>();

		if (SaveManager.MustLoad >= 0)
		{
			SaveManager.Load(SaveManager.MustLoad);
			SaveManager.MustLoad = -1;
		}
	}
}
