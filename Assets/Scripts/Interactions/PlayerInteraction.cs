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
	public float OutlineSize = 0.05f;
	public Color OutlineColor = Color.red;

	private List<GameObject> _interactibles = new List<GameObject>();
	private GameObject _activeInteractible;
	private Shader _oldShader;

	public void Update()
	{
		// We always need something to interact with
		if (_activeInteractible == null)
			return;

		// Interact with the object in view when triggering the "Action" control
		if (CnControls.CnInputManager.GetButtonDown("Action"))
		{
			// Interact using Fungus
			var fungusInteractible = _activeInteractible.GetComponent<FungusInteractible>();
			if (fungusInteractible != null)
			{
				if (!fungusInteractible.FullDialogBox)
				{
					SayDialog.ActiveSayDialog = SmallSayDialog;

					// Show small dialog above interactible
					SmallSayDialogTransform.position = _activeInteractible.transform.position + new Vector3(0, 2f, 0);
					SmallSayDialogTransform.pivot = new Vector2(0.5f, 0.5f);
				}
				else
				{
					SayDialog.ActiveSayDialog = FullSizeSayDialog;

					// Freeze player
					MovementController.IsFrozen = true;
					BlockSignals.OnBlockEnd += UnfreezePlayerWhenFlowchartEnds;
				}

				fungusInteractible.Flowchart.SendFungusMessage("Start");
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

	private void UpdateInteractible()
	{
		if (_activeInteractible != null)
			_activeInteractible.GetComponent<Renderer>().material.shader = _oldShader;

		_activeInteractible = RefreshInteractible();

		if (_activeInteractible != null)
		{
			var material = _activeInteractible.GetComponent<Renderer>().material;
			_oldShader = material.shader;
			material.shader = Shader.Find("Custom/Outline");
			material.SetFloat("_Outline", OutlineSize);
			material.SetColor("_OutlineColor", OutlineColor);
		}
	}

	private GameObject RefreshInteractible()
	{
		// If there are multiple objects in front of us, we sort them by distance to the player
		if (_interactibles.Count > 1)
		{
			_interactibles = _interactibles.OrderBy(
				x => Vector3.Distance(transform.parent.position, x.transform.position)
			).ToList();
		}

		// We try each object in front of us until we find an interactible one
		return _interactibles.FirstOrDefault(obj => obj.GetComponent<Interactible>() != null);
	}

	public void OnTriggerEnter(Collider other)
	{
		_interactibles.Add(other.gameObject);
		UpdateInteractible();
	}

	public void OnTriggerExit(Collider other)
	{
		_interactibles.Remove(other.gameObject);
		UpdateInteractible();
	}
}
