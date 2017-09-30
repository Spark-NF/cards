using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
	public Text Title;
	public CostView CostView;
	public Image Image;
	public Text Description;
	public Text Damage;
	public Text Life;


	public void SetCard(Card card)
	{
		Title.text = card.Name;
		Description.text = card.Description;

		// Disable useless game objects
		CostView.gameObject.SetActive(card.Type == CardType.Unit);
		Damage.gameObject.SetActive(card.Type == CardType.Unit);
		Life.gameObject.SetActive(card.Type == CardType.Unit);

		// Only show unit information if the card is an unit
		if (card.Type == CardType.Unit)
		{
			CostView.SetCost(card.Price);
			Damage.text = card.Strength.ToString();
			Life.text = card.Life.ToString();
		}
	}
}
