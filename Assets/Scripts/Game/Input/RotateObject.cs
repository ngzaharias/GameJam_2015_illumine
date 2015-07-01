using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class RotateObject : MonoBehaviour, IDragHandler, IPointerClickHandler
{
	public void OnDrag(PointerEventData eventData)
	{
		RotateAxis(new Vector3(eventData.delta.y, -eventData.delta.x, 0));
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		LightManager.Instance.SpawnPointLight();
	}

	void RotateAxis(Vector3 delta)
	{
		if (delta != Vector3.zero)
		{
			delta = delta * Time.deltaTime;
			transform.Rotate(delta * GameParameters.Instance.m_RotateObjectSpeed, Space.World);
		}
	}
}