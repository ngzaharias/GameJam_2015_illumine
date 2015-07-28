using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SpawnLight : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerClickHandler
{
	private bool m_isDragging = false;

	public void OnPointerDown(PointerEventData eventData)
	{
		m_isDragging = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		m_isDragging = true;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (LightManager.Instance.SpawningEnabled && m_isDragging == false)
		{
			LightManager.Instance.SpawnPointLight();
		}
	}
}
