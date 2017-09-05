using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

[Serializable]
public class CardList : List<Card>, ISerializable
{
	public CardList()
	{}

	protected CardList(SerializationInfo info, StreamingContext context)
	{
		if (info == null)
			throw new ArgumentNullException("info");

		var ids = (int[])info.GetValue("Ids", typeof(int[]));
		var cardManager = Game.Current.CardManager;

		Clear();
		foreach (int id in ids)
			Add(cardManager.GetById(id));
	}

	public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		if (info == null)
			throw new ArgumentNullException("info");

		info.AddValue("Ids", this.Select(c => c.Id).ToArray());
	}
}
