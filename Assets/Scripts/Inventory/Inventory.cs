using System;
using System.Collections.Generic;

[Serializable]
public class Inventory
{
    public int Money = 0;
	public CardList Cards = new CardList();
	public List<Deck> Decks = new List<Deck>();
}
