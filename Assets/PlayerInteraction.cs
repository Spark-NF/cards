using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	private Collider2D _currentCollider = null;
	private Flowchart _currentInteractible = null;

	void Update()
	{
		if (CnControls.CnInputManager.GetButtonDown("Action"))
		{
			if (_currentInteractible != null)
			{
				_currentInteractible.ExecuteBlock("Init");
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
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

	void OnTriggerExit2D(Collider2D other)
	{
		if (other == _currentCollider)
		{
			_currentCollider = null;
			_currentInteractible = null;
		}
	}
}
