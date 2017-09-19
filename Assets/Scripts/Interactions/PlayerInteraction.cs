using System.Collections.Generic;
using System.Linq;
using Fungus;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	public CharacterMovement MovementController;
	public SayDialog SmallSayDialog;
	public RectTransform SmallSayDialogTransform;
	public SayDialog FullSizeSayDialog;

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
					x => Vector3.Distance(transform.parent.position, x.transform.position)
				).ToList();
			}

			// We try each object in front of us until we find an interactible one
			foreach (var obj in _interactibles)
			{
				// Interact using a Fungus Flowchart
				var flowchart = obj.GetComponent<Flowchart>();
				if (flowchart != null)
				{
					bool basic = Random.value > 0.5f;

					if (basic)
					{
						SayDialog.ActiveSayDialog = SmallSayDialog;

						// Show small dialog above interactible
						SmallSayDialogTransform.position = obj.transform.position + new Vector3(0, 0.16f, 0);
						SmallSayDialogTransform.pivot = new Vector2(0.5f, -1);
					}
					else
					{
						SayDialog.ActiveSayDialog = FullSizeSayDialog;

						// Freeze player
						MovementController.IsFrozen = true;
						BlockSignals.OnBlockEnd += UnfreezePlayerWhenFlowchartEnds;
					}

					flowchart.SendFungusMessage("Start");
					return;
				}
			}
		}
	}

	private void UnfreezePlayerWhenFlowchartEnds(Block block)
	{
		var flowchart = block.GetFlowchart();
		if (!flowchart.HasExecutingBlocks() && !MenuDialog.GetMenuDialog().IsActive())
		{
			MovementController.IsFrozen = false;
			BlockSignals.OnBlockEnd -= UnfreezePlayerWhenFlowchartEnds;
			flowchart.SendFungusMessage("End");
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		_interactibles.Add(other.gameObject);
	}

	public void OnTriggerExit(Collider other)
	{
		_interactibles.Remove(other.gameObject);
	}
}
