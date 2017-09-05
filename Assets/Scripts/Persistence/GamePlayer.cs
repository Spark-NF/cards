using UnityEngine;

public class GamePlayer : MonoBehaviour
{
	public GamePlayer()
	{
		Game.Current.Player = this;
	}
}