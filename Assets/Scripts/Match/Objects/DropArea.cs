using UnityEngine;
using UnityEngine.UI;

public class DropArea : MonoBehaviour
{
	public CardSlot CardSlot;
	public Image ImageOver;
	public CardType CardType = CardType.Any;

	private Color _oldColor;

	public void OnEnter(bool ok)
	{
		_oldColor = ImageOver.color;
		ImageOver.color = ok ? Color.green : Color.red;
	}

	public void OnExit()
	{
		ImageOver.color = _oldColor;
	}

	public bool CanDrop(CardObject card)
	{
		return CardType == CardType.Any || card.Card.Type == CardType;
	}
}
