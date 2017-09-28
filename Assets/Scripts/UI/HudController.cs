using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
	public Text Money;
	public GamePlayer Player;
	
	void Start()
	{
		SetMoney();
	}
	
	void Update()
	{
		SetMoney();
	}

	private void SetMoney()
	{
		Money.text = Player.Inventory.Money.ToString();
	}
}
