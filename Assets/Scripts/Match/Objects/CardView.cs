using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
	public Text Title;
	public GameObject Cost;
	public Image Image;
	public Text Description;
	public Text Damage;
	public Text Life;

	public void SetCard(Card card)
	{
		Title.text = card.Name;
		Description.text = card.Description;
		Damage.text = card.Strength.ToString();
		Life.text = card.Life.ToString();

		foreach (var cost in card.Price)
		{
			// TODO: instantiate costs
		}
	}
}
