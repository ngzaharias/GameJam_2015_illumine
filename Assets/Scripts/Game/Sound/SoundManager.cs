using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour 
{
	static protected SoundManager m_instance = null;
	static public SoundManager Instance
	{
		get
		{
			if (m_instance == null)
				m_instance = GameObject.FindObjectOfType<SoundManager>();
			return m_instance;
		}
	}

	public void PlayAudioClip(AudioClip clip)
	{
		if (clip != null)
		{
			GameObject obj = new GameObject();
			obj.name = clip.name;
			AudioSource source = obj.AddComponent<AudioSource>();
			source.clip = clip;
			source.Play();

			Destroy(obj, clip.length);
		}
		else
		{
			Debug.LogWarning("PlaySoundEffect: AudioClip doesn't exist");
		}
	}
}
