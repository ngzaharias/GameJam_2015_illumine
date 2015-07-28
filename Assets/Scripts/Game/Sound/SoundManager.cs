using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct AudioClipInfo
{
	public AudioClip clip;
	public Timer timer;
};

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

	private List<AudioClipInfo> m_clipsDelayed = new List<AudioClipInfo>();

	void Update()
	{
		PlayAudioClipDelayed();
	}

	public void PlayAudioClip(AudioClip clip, float delay = 0.0f)
	{
		if (clip != null)
		{
			if (delay <= 0.0f)
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
				AudioClipInfo clipInfo;
				clipInfo.clip = clip;
				clipInfo.timer = new Timer();
				clipInfo.timer.Start(delay);
				m_clipsDelayed.Add(clipInfo);
			}
		}
		else
		{
			Debug.LogWarning("PlaySoundEffect: AudioClip doesn't exist");
		}
	}

	private void PlayAudioClipDelayed()
	{
		for (int i = m_clipsDelayed.Count - 1; i >= 0; --i)
		{
			if (m_clipsDelayed[i].timer.Finished())
			{
				PlayAudioClip(m_clipsDelayed[i].clip);
				m_clipsDelayed.RemoveAt(i);
			}
		}
	}
}
