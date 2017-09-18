using System.Collections.Generic;
using System.Linq;
using Fungus;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	private List<GameObject> _interactibles = new List<GameObject>();

	public void Update()
	{
		// We always need something to interact with
		if (_interactibles.Count == 0)
			return;

		// Interact with the object in view when triggering the "Action" control
		if (CnControls.CnInputManager.GetButtonDown("Action"))
		{
			// If there are multiple objects in front of us, we sort them by distance to the player
			if (_interactibles.Count > 1)
			{
				_interactibles = _interactibles.OrderBy(
					x => Vector2.Distance(transform.parent.position, x.transform.position)
				).ToList();
			}

			// We try each object in front of us until we find an interactible one
			foreach (var obj in _interactibles)
			{
				// Interact using a Fungus Flowchart
				var flowchart = obj.GetComponent<Flowchart>();
				if (flowchart != null)
				{
					flowchart.SendFungusMessage("Start");
					return;
				}
			}
		}
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		_interactibles.Add(other.gameObject);
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		_interactibles.Remove(other.gameObject);
	}
}
