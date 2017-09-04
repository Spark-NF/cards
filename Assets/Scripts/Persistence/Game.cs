using UnityEngine;

public class Game
{
	public static Game current = new Game();

	public Player player = null;

	public GameSave Save()
	{
		GameSave gs = new GameSave();

		// Player
		gs.playerPositionX = player.transform.position.x;
		gs.playerPositionY = player.transform.position.y;

		return gs;
	}

	public void Load(GameSave gs)
	{
		// Player
		Vector2 playerPos = new Vector2(gs.playerPositionX, gs.playerPositionY);
		player.transform.position = playerPos;
		Camera.main.transform.position = new Vector3(playerPos.x, playerPos.y, Camera.main.transform.position.z);
	}
}
