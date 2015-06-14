using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour
{
	void Update()
	{
#if UNITY_STANDALONE
		RotateAxis(MouseDelta());
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WINRT

#endif
	}

	Vector3 MouseDelta()
	{
		Vector3 delta = Vector3.zero;
		if (Input.GetMouseButton(0))
		{
			delta = new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0);
		}
		if (Input.GetMouseButton(1))
		{
			Vector2 position = Camera.main.WorldToScreenPoint(transform.position);

			float x = Input.GetAxis("Mouse X");
			float y = Input.GetAxis("Mouse Y");

			x = (Input.mousePosition.y > position.y) ? x : -x;
			y = (Input.mousePosition.x > position.x) ? y : -y;

			delta = new Vector3(0, 0, y - x);
		}
		return delta;
	}

	Vector3 TouchDelta()
	{
		Vector3 delta = Vector3.zero;

		return delta;
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