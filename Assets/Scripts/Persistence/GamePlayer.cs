using UnityEngine;

public class GamePlayer : MonoBehaviour
{
	public Inventory Inventory = new Inventory();

	public GamePlayer()
	{
		Game.Current.Player = this;
	}
}
