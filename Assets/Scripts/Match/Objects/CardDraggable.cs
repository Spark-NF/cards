using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public float DraggingHeight = 0.05f;
	[NonSerialized] public DropArea DropArea;

	private CardObject _cardObject;
	private bool _dragging = false;
	private float _oldPositionDamp;
	private Color _oldColor;
	private Vector3 _startPosition;
	private Plane _dragPlane;

	private void Awake()
	{
		enabled = false;

		_cardObject = GetComponent<CardObject>();
		_dragPlane = new Plane(Vector3.up, Vector3.up * DraggingHeight);
	}

	private void OnMouseEnter()
	{
		if (!enabled || _dragging)
			return;

		var material = transform.Find("Front").GetComponent<Renderer>().material;
		_oldColor = material.color;
		material.color = Color.red;
	}

	private void OnMouseExit()
	{
		if (!enabled)
			return;

		RestoreColor();
	}

	private void RestoreColor()
	{
		transform.Find("Front").GetComponent<Renderer>().material.color = _oldColor;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!enabled)
			return;

		_dragging = true;
		DropArea = null;
		_startPosition = transform.position;
		_oldPositionDamp = _cardObject.PositionDamp;
		_cardObject.PositionDamp = 0.1f;

		RestoreColor();
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!enabled)
			return;

		// Move card to mouse
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance;
		if (_dragPlane.Raycast(ray, out distance))
			_cardObject.TargetTransform.position = ray.GetPoint(distance);

		// Drop area management
		int layerMask = LayerMask.GetMask("DropArea");
		RaycastHit hit;
		DropArea area = null;
		if (Physics.Raycast(ray, out hit, 10, layerMask))
		{
			area = hit.transform.gameObject.GetComponent<DropArea>();
		}
		SetDropArea(area);
	}

	private void SetDropArea(DropArea newArea)
	{
		DropArea oldArea = DropArea;

		if (newArea == oldArea)
			return;

		if (oldArea != null)
			oldArea.OnExit();

		if (newArea != null)
			newArea.OnEnter();

		DropArea = newArea;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (!enabled)
			return;

		_dragging = false;
		_cardObject.PositionDamp = _oldPositionDamp;

		if (DropArea != null)
		{
			DropArea.CardSlot.AddCard(_cardObject);
			_cardObject.TargetTransform.rotation = Quaternion.Euler(90, 0, 0);

			DropArea.OnExit();
			DropArea = null;
		}
		else
		{
			_cardObject.TargetTransform.position = _startPosition;
		}
	}
}
