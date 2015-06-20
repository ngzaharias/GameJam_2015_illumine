using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private Vector3 m_offset = Vector3.zero;

	public void OnBeginDrag(PointerEventData eventData)
	{
		Vector3 mouse;
		RectTransform plane = eventData.pointerDrag.transform as RectTransform;
		RectTransform rectTransform = transform as RectTransform;
		RectTransformUtility.ScreenPointToWorldPointInRectangle(plane, eventData.position, eventData.pressEventCamera, out mouse);

		m_offset = rectTransform.position - mouse;
		SetDraggedPosition(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		SetDraggedPosition(eventData);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
	}

	private void SetDraggedPosition(PointerEventData eventData)
	{
		Vector3 mouse;
		RectTransform plane = eventData.pointerDrag.transform as RectTransform;
		RectTransform rectTransform = transform as RectTransform;
		RectTransformUtility.ScreenPointToWorldPointInRectangle(plane, eventData.position, eventData.pressEventCamera, out mouse);

		rectTransform.position = mouse + m_offset;
	}
}
