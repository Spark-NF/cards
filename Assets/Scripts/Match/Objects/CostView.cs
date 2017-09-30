using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostView : MonoBehaviour
{
	public GameObject CostPrefab;

	public Sprite SpriteColorAny;
	public Sprite SpriteColorWhite;
	public Sprite SpriteColorBlue;
	public Sprite SpriteColorBlack;
	public Sprite SpriteColorRed;
	public Sprite SpriteColorGreen;

	private readonly Dictionary<CardColor, Sprite> _costSprites = new Dictionary<CardColor, Sprite>();
	private readonly List<GameObject> _costObjects = new List<GameObject>();

	private void Start()
	{
		_costSprites.Add(CardColor.Any, SpriteColorAny);
		_costSprites.Add(CardColor.White, SpriteColorWhite);
		_costSprites.Add(CardColor.Blue, SpriteColorBlue);
		_costSprites.Add(CardColor.Black, SpriteColorBlack);
		_costSprites.Add(CardColor.Red, SpriteColorRed);
		_costSprites.Add(CardColor.Green, SpriteColorGreen);
	}

	public void SetCost(Dictionary<CardColor, int> cost)
	{
		// Destroy previous objects if necessary
		foreach (var costObject in _costObjects)
		{
			Destroy(costObject);
		}

		// Spawn new cost objects
		int i = 0;
		foreach (var pair in cost)
		{
			for (int c = 0; c < pair.Value; ++c)
			{
				var obj = CreateCostObject(pair.Key);
				obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-50 * i++, 0);
				_costObjects.Add(obj);
			}
		}
	}

	private GameObject CreateCostObject(CardColor color)
	{
		var costObject = Instantiate(CostPrefab, gameObject.transform);

		var image = costObject.GetComponent<Image>();
		image.sprite = _costSprites[color];

		return costObject;
	}
}
