using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

[Serializable]
public class CardList : List<Card>, ISerializable
{
    public CardList()
    { }

    protected CardList(SerializationInfo info, StreamingContext context)
    {
        if (info == null)
            throw new ArgumentNullException(nameof(info));

        var ids = (int[])info.GetValue("Ids", typeof(int[]));
		var cardManager = Game.current.CardManager;

        Clear();
        foreach (int id in ids)
            Add(cardManager.GetById(id));
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        if (info == null)
            throw new ArgumentNullException(nameof(info));

        info.AddValue("Ids", this.Select(c => c.Id).ToArray());
    }
}
