using System;
using System.Linq;

[Serializable]
public struct CardDatabase
{
	[Serializable]
	public struct CardDatabaseCard
	{
		public int Id;
		public string[] Price;
		public string[] Attributes;
		public string Type;
		public string Name;
		public string Image;
		public string Description;
		public int Strength;
		public int Life;
		public string Rarity;

		public Card ToCard()
		{
			return new Card
			{
				Id = Id,
				Price = Price.Select(c => (CardColor)Enum.Parse(typeof(CardColor), c)).GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count()),
				Attributes = Attributes.Select(a => (CardAttribute)Enum.Parse(typeof(CardAttribute), a)).ToArray(),
				Type = (CardType)Enum.Parse(typeof(CardType), Type),
				Name = Name,
				Description = Description,
				Strength = Strength,
				Life = Life,
				Rarity = (CardRarity)Enum.Parse(typeof(CardRarity), Rarity)
			};
		}
	}

	public CardDatabaseCard[] Cards;
}
