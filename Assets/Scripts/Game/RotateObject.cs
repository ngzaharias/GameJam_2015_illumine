using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour 
{
    float speed = 500.0f;

	void Update () 
    {
	    if (Input.GetMouseButton(0))
        {
			RotateAxisXY();
        }
		if (Input.GetMouseButton(1))
		{
			RotateAxisZ();
		}
	}

    void RotateAxisXY()
    {
        Vector3 delta = new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0);
        delta = delta * Time.deltaTime;
        transform.Rotate(delta * speed, Space.World);
    }

	void RotateAxisZ()
	{
		Vector2 position = Camera.main.WorldToScreenPoint(transform.position);

		float x = Input.GetAxis("Mouse X");
		float y = Input.GetAxis("Mouse Y");

		x = (Input.mousePosition.y > position.y) ? x : -x;
		y = (Input.mousePosition.x > position.x) ? y : -y;

		Vector3 delta = new Vector3(0, 0, y - x);
		delta = delta * Time.deltaTime;
		transform.Rotate(delta * speed, Space.World);
	}
}
