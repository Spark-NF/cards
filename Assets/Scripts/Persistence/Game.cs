using UnityEngine;

public class Game
{
	public static Game Current = new Game();

	public GamePlayer Player = null;
	public CardManager CardManager = null;

	public GameSave Save()
	{
		var gs = new GameSave();

		// Player
		gs.PlayerPositionX = Player.transform.position.x;
		gs.PlayerPositionY = Player.transform.position.y;
		gs.PlayerPositionZ = Player.transform.position.z;
		gs.PlayerInventory = Player.Inventory;

		return gs;
	}

	public void Load(GameSave gs)
	{
		// Player
		var playerPos = new Vector3(gs.PlayerPositionX, gs.PlayerPositionY, gs.PlayerPositionZ);
		Player.transform.position = playerPos;
		Player.Inventory = gs.PlayerInventory;
		Camera.main.transform.position = playerPos + new Vector3(0, 10, -4);

		// All available cards
		CardManager = new CardManager();
	}
}
