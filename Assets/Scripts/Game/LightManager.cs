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
}
