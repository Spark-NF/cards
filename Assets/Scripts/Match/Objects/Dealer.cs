using UnityEngine;
using System.Collections;

public class Dealer : MonoBehaviour
{
	public GameObject CardPrefab;
	public CardSlot StackCardSlot;
	public float CardStackDelay = .01f;

	private void Start()
	{
		StartCoroutine(StackCardOnSlot(20, StackCardSlot));
	}

	private IEnumerator StackCardOnSlot(int count, CardSlot cardSlot)
	{
		for (int i = 0; i < count; ++i)
		{
			GameObject cardInstance = Instantiate(CardPrefab);
			cardInstance.transform.parent = transform;
			cardInstance.transform.position = new Vector3(0, 10, 0);

			var cardObject = cardInstance.GetComponent<CardObject>();
			cardSlot.AddCard(cardObject);

			yield return new WaitForSeconds(CardStackDelay);
		}
	}
}
