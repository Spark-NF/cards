using Fungus;
using UnityEngine;

[CommandInfo("Inventory",
	"Give Money",
	"Give a fixed amount of money to the player.")]
public class GiveMoney : Command
{
	public int Amount;

	public override void OnEnter()
	{
		Game.Current.Player.Inventory.Money += Amount;

		Continue();
	}

	public override string GetSummary()
	{
		return Amount.ToString();
	}

	public override Color GetButtonColor()
	{
		return new Color32(235, 191, 217, 255);
	}
}
