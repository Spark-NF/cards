using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	void OnLevelWasLoaded(int level)
	{
		//Game.current.player = GameObject.FindWithTag("Player");

		if (SaveManager.mustLoad >= 0)
		{
			SaveManager.Load(SaveManager.mustLoad);
			SaveManager.mustLoad = -1;
		}
	}
}
