using System.Collections;

class SomeStep : Step
{
	public override IEnumerator Run(SideManager sideManager)
	{
		sideManager.AllowDragDrop();

		while (true)
		{
			yield return null;
		}
	}
}
