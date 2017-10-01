using UnityEngine;
using UnityEngine.UI;

public class DropArea : MonoBehaviour
{
	public CardSlot CardSlot;
	public Image ImageOver;

	private Color _oldColor;

	public void OnEnter()
	{
		_oldColor = ImageOver.color;
		ImageOver.color = Color.blue;
	}

	public void OnExit()
	{
		ImageOver.color = _oldColor;
	}
}
