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
		Vector3 delta = new Vector3(0, 0, Input.GetAxis("Mouse Y") + -Input.GetAxis("Mouse X"));
		delta = delta * Time.deltaTime;
		transform.Rotate(delta * speed, Space.World);
	}
}
