using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
	public Text MoneyCounter;

	private void Update ()
	{
		MoneyCounter.text = Game.Current.Player.Inventory.Money.ToString();
	}
}
