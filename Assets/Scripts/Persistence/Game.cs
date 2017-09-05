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
		gs.PlayerInventory = Player.Inventory;

		return gs;
	}

	public void Load(GameSave gs)
	{
		// Player
		var playerPos = new Vector2(gs.PlayerPositionX, gs.PlayerPositionY);
		Player.transform.position = playerPos;
		Player.Inventory = gs.PlayerInventory;
		Camera.main.transform.position = new Vector3(playerPos.x, playerPos.y, Camera.main.transform.position.z);

		// All available cards
		CardManager = new CardManager();
	}
}
