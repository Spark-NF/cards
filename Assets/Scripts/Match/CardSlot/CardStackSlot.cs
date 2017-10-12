using UnityEngine;

public class CardStackSlot : CardSlot
{
	public override void AddCard(CardObject card)
	{
		base.AddCard(card);

		// Give the card a slight randomness in its rotation
		card.TargetTransform.Rotate(card.TargetTransform.forward, Random.Range(-0.4f, 0.4f), Space.Self);

		// Stack the card on top of others
		float cardHeight = card.GetComponent<BoxCollider>().size.z;
		card.TargetTransform.Translate(new Vector3(0, 0, Cards.Count * cardHeight), Space.Self);
	}

	private void OnDrawGizmosSelected()
	{
		const float height = 0.04f;
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position + (height / 2f) * Vector3.up, new Vector3(0.063f, height, 0.088f));
	}
}
