using System.Collections;

class UntapStep : Step
{
	public override IEnumerator Run(SideManager sideManager)
	{
		// Untap all cards
		foreach (var card in sideManager.MatchSide.Front)
			if (card.IsTapped)
				card.Untap();

		yield break;
	}
}
