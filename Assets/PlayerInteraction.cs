using Fungus;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	private Collider2D _currentCollider = null;
	private Flowchart _currentInteractible = null;

	public void Update()
	{
		if (CnControls.CnInputManager.GetButtonDown("Action"))
		{
			if (_currentInteractible != null)
			{
				_currentInteractible.SendFungusMessage("Start");
			}
		}
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		var obj = other.gameObject;
		if (obj == null)
			return;

		var flowchart = obj.GetComponent<Flowchart>();
		if (flowchart == null)
			return;

		_currentCollider = other;
		_currentInteractible = flowchart;
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if (other == _currentCollider)
		{
			_currentCollider = null;
			_currentInteractible = null;
		}
	}
}
