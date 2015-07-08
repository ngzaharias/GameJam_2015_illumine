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
			return m_instance;
		}
	}

	public void PlaySoundEffect(int typeInt)
	{
		SoundEffect type = (SoundEffect)typeInt;
		AudioClip soundEffect = SoundDatabase.Instance.GetSoundEffectFromType(type);
		if (soundEffect != null)
		{
			GameObject obj = new GameObject();
			obj.name = soundEffect.name;
			AudioSource source = obj.AddComponent<AudioSource>();
			source.clip = soundEffect;
			source.Play();

			Destroy(obj, soundEffect.length);
		}
		else 
		{
			Debug.LogWarning("PlaySoundEffect: AudioClip " + soundEffect.name + " doesn't exist");
		}
	}
}
