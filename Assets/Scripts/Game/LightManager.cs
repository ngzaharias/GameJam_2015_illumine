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
			if (m_instance == null)
				m_instance = GameObject.FindObjectOfType<LightManager>();
			return m_instance;
		}
	}

	[SerializeField]
	private FadeLight m_directionalLight = null;
	private List<FadeLight> m_pointLights = new List<FadeLight>();
	public List<FadeLight> PointLights { get { return m_pointLights; } }

	public bool SpawningEnabled { get; set; }

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
		FadeLight light = Instantiate<FadeLight>(GameParameters.Instance.m_SpawnLightPrefab);
		light.transform.position = Camera.main.transform.position;
		light.GetComponent<Rigidbody>().AddForce(transform.forward * GameParameters.Instance.m_SpawnLightForce);
		light.Fade(4.0f, 1.0f);
		RegisterPointLight(light);

		// remove a light
		while (m_pointLights.Count > GameParameters.Instance.m_SpawnLightsMax)
		{
			if (m_pointLights[0] != null)
			{
				m_pointLights[0].Fade(0.0f, 1.0f);
				Destroy(m_pointLights[0].gameObject, 1.0f);
			}
			m_pointLights.RemoveAt(0);
		}

		AudioClip clip = SoundDatabase.Instance.GetSoundEffect(SoundEffect.Type.SOUND_EFFECT_LIGHT_SPAWN);
		SoundManager.Instance.PlayAudioClip(clip);
	}
}
