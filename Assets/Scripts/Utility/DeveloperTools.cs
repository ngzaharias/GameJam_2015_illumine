using UnityEngine;
using System.Collections;

public class DeveloperTools : MonoBehaviour 
{
	[SerializeField]
	public bool m_timeScale = true;
	[SerializeField]
	public float m_speedSlow = 0.25f;
	[SerializeField]
	public float m_speedFast = 4.0f;

	void Update ()
	{
		TimeScale();
	}

	void TimeScale()
	{
		if (m_timeScale)
		{
			if (Input.GetKey(KeyCode.KeypadMinus))
			{
				Time.timeScale = m_speedSlow;
			}
			else if (Input.GetKey(KeyCode.KeypadPlus))
			{
				Time.timeScale = m_speedFast;
			}
			else
			{
				Time.timeScale = 1.0f;
			}
		}
	}
}
