using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightManager : MonoBehaviour 
{
	static protected LightManager m_instance = null;
	static public LightManager Instance
	{
		get
		{
			return m_instance;
		}
	}

	[SerializeField]
	private uint m_pointLightsMax = 3;
	[SerializeField]
	private float m_pointForceAmount = 100.0f;
	[SerializeField]
	private FadeLight m_pointLightPrefab = null;

	[SerializeField]
	private FadeLight m_directionalLight = null;
	private List<FadeLight> m_pointLights = new List<FadeLight>();
	public List<FadeLight> PointLights { get { return m_pointLights; } }

	void Awake()
	{
		m_instance = this;
	}

	public void FadeDirectionalLight(float value, float duration, float delay = 0.0f)
	{
		if (m_directionalLight != null)
		{
			m_directionalLight.Fade(value, duration, delay);
		}
	}

	public void FadePointLights(float value, float duration, float delay = 0.0f)
	{
		for (int i = 0; i < m_pointLights.Count; ++i)
		{
			m_pointLights[i].Fade(value, duration, delay);
		}
	}

	public void RegisterPointLight(FadeLight light)
	{
		if (m_pointLights.Contains(light) == false)
		{
			m_pointLights.Add(light);
		}
	}

	public void UnRegisterPointLight(FadeLight light)
	{
		m_pointLights.Remove(light);
	}

	public void SpawnPointLight()
	{
		// spawn a light
		FadeLight light = Instantiate<FadeLight>(m_pointLightPrefab);
		light.transform.position = Camera.main.transform.position;
		light.GetComponent<Rigidbody>().AddForce(transform.forward * m_pointForceAmount);
		light.Fade(4.0f, 1.0f);
		RegisterPointLight(light);

		// remove a light
		while (m_pointLights.Count > m_pointLightsMax)
		{
			if (m_pointLights[0] != null)
			{
				m_pointLights[0].Fade(0.0f, 1.0f);
				Destroy(m_pointLights[0].gameObject, 1.0f);
			}
			m_pointLights.RemoveAt(0);
		}
	}
}
