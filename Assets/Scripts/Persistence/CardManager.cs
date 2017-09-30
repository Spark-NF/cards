using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager
{
	private Dictionary<int, Card> _cards = new Dictionary<int, Card>();
	private Dictionary<CardRarity, List<Card>> _cardsByRarity = new Dictionary<CardRarity, List<Card>>();

	private Dictionary<CardRarity, int> _rarityWeights = new Dictionary<CardRarity, int>
	{
		{ CardRarity.Common, 90 },
		{ CardRarity.Uncommon, 9 },
		{ CardRarity.Rare, 1 },
	};
	private int[] _cumulativeRarityWeights;
	private int _cumulativeTotal;

	public CardManager()
	{
		int sum = 0;
		_cumulativeRarityWeights = _rarityWeights.Values.Select(x => (sum += x)).ToArray();
		_cumulativeTotal = sum;
	}

	public bool LoadFromFile(string path)
	{
		var jsonObj = Resources.Load(path) as TextAsset;
		if (jsonObj == null)
			return false;

		var cards = JsonUtility.FromJson<CardDatabase>(jsonObj.text);
		if (cards.Cards.Length == 0)
			return false;

		foreach (var card in cards.Cards)
			_cards.Add(card.Id, card.ToCard());

		return true;
	}

	public Card GetById(int id)
	{
		Card card;
		if (!_cards.TryGetValue(id, out card))
			return null;

		return card;
	}

	private Card RandomCard(CardRarity rarity)
	{
		int count = _cardsByRarity[rarity].Count;
		int index = (int)System.Math.Floor(Random.value * count);
		return _cardsByRarity[rarity][index];
	}

	private CardRarity RandomRarity()
	{
		int value = (int)System.Math.Floor(Random.value * _cumulativeTotal);
		int index = System.Array.BinarySearch(_cumulativeRarityWeights, value);
		if (index >= 0)
			index = ~index;

		var rarities = _rarityWeights.Keys.ToArray();
		return rarities[index];
	}

	private List<CardRarity> RandomRarities(int count)
	{
		var rarities = new List<CardRarity>();
		for (int i = 0; i < count; ++i)
		{
			rarities.Add(RandomRarity());
		}
		return rarities;
	}

	// TODO: verify rarity drops
	public List<Card> RandomCards(int count, List<CardRarity> rarities = null)
	{
		if (rarities != null && rarities.Count > count)
			throw new System.ArgumentException("rarities");


		// Generate weighted random card rarities
		if (rarities == null)
			rarities = new List<CardRarity>();
		if (rarities.Count < count)
			rarities.AddRange(RandomRarities(count - rarities.Count));

		var cards = new List<Card>();
		foreach (CardRarity rarity in rarities)
		{
			cards.Add(RandomCard(rarity));
		}
		return cards;
	}
}
