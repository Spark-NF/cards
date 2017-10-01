using UnityEngine;

public class CardArea : MonoBehaviour
{
	public CardSlot CardSlot;
	public GameObject DropArea;

	public void ToggleDrag(bool enable)
	{
		foreach (CardObject card in CardSlot.Cards)
		{
			card.GetComponent<CardDraggable>().enabled = enable;
		}
	}

	public void ToggleDrop(bool enable)
	{
		DropArea.SetActive(enable);
	}
}
