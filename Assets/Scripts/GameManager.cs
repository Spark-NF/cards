using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		Game.Current.Player = GameObject.FindWithTag("Player").GetComponent<GamePlayer>();

		if (SaveManager.MustLoad >= 0)
		{
			SaveManager.Load(SaveManager.MustLoad);
			SaveManager.MustLoad = -1;
		}
	}
}
