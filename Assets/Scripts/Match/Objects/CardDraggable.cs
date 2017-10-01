using UnityEngine;
using UnityEngine.EventSystems;

public class CardDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public float DraggingHeight = 0.05f;

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
		_startPosition = transform.position;
		_oldPositionDamp = _cardObject.PositionDamp;
		_cardObject.PositionDamp = 0.1f;

		RestoreColor();
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!enabled)
			return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance;
		if (_dragPlane.Raycast(ray, out distance))
			_cardObject.TargetTransform.position = ray.GetPoint(distance);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (!enabled)
			return;

		_dragging = false;
		_cardObject.TargetTransform.position = _startPosition;
		_cardObject.PositionDamp = _oldPositionDamp;
	}
}
