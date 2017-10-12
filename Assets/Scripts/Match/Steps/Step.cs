using System.Collections;

abstract class Step
{
	public abstract IEnumerator Run(SideManager sideManager);
}
