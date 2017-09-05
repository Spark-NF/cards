using System.Collections.Generic;
using UnityEngine;

public class Card
{
	public int Id;
	public Dictionary<CardColor, int> Price;
	public CardAttribute[] Attributes;
	public CardType Type;
	public string Name;
	public Sprite Image;
	public string Description;
	public int Strength;
	public int Life;
}
