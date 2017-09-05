using UnityEngine;

public class Player : MonoBehaviour
{
	public Player()
	{
		Game.Current.Player = this;
	}
}