using UnityEngine;

public class CardListSlot : CardSlot
{
	public float Spacing = 0.05f;
	public float MaxWidth = 0.8f;

	public override void AddCard(CardObject card)
	{
		base.AddCard(card);

		float cardWidth = card.GetComponent<BoxCollider>().size.x * card.transform.localScale.x;
		float absSpacing = cardWidth * Spacing;

		// Move previous cards in their right positions
		for (int i = 0; i < Cards.Count - 1; ++i)
		{
			Cards[i].TargetTransform.localPosition = GetCardPosition(i, Cards.Count, cardWidth, absSpacing);
		}

		// Add new card on the right side
		card.TargetTransform.localPosition = GetCardPosition(Cards.Count - 1, Cards.Count, cardWidth, absSpacing);
	}

	private Vector3 GetCardPosition(int index, int count, float width, float spacing)
	{
		float totalWidth = (count - 1) * (width + spacing);
		float x = index * (width + spacing) - totalWidth / 2f;
		return transform.position + new Vector3(x, 0, 0);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Extensions.GizmosDrawRect(transform.position, MaxWidth, 0.088f);
	}
}
