using System.Collections;
using UnityEngine;

class UntapStep : Step
{
	public override IEnumerator Run(SideManager sideManager)
	{
		// Untap all cards
		foreach (var card in sideManager.MatchSide.Front)
		{
			if (!card.IsTapped)
				continue;

			card.Untap();
			card.CardObject.TargetTransform.Rotate(Vector3.forward, 90);
		}

		yield break;
	}
}
