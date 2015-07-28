using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class FadeLight : MonoBehaviour 
{
	private float m_start = 0.0f;
	private float m_end = 0.0f;
	private Light m_light = null;
	private Timer m_fadeTimer = null;
	private Timer m_delayTimer = null;

	void Awake()
	{
		m_fadeTimer = new Timer();
		m_light = GetComponent<Light>();
	}

	void OnDestroy()
	{
		LightManager.Instance.PointLights.Remove(this);
	}

	void Update () 
	{
		if (m_delayTimer != null)
		{
			if (m_delayTimer.Finished())
			{
				m_fadeTimer.Reset();
				m_delayTimer = null;
			}
		}
		else 
		{
			m_light.intensity = Mathf.Lerp(m_start, m_end, (float)m_fadeTimer.Percentage);
		}
	}

	public void Fade(float value, float duration, float delay = 0.0f)
	{
		m_fadeTimer.Start(duration);

		if (delay > 0.0f)
		{
			m_delayTimer = new Timer();
			m_delayTimer.Start(delay);
		}

		m_start = m_light.intensity;
		m_end = value;
	}
}
