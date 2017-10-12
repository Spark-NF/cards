using System.Collections;

class DrawStep : Step
{
	public override IEnumerator Run(SideManager sideManager)
	{
		yield return sideManager.PickCard(true, true);
	}
}
