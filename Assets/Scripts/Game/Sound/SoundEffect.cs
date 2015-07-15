using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundEffect : MonoBehaviour 
{
	public enum Type
	{
		SOUND_EFFECT_NONE = -1,

		SOUND_EFFECT_BUTTON_ENTER,
		SOUND_EFFECT_BUTTON_PRESS,
		SOUND_EFFECT_BUTTON_PRESS_BACK,
		SOUND_EFFECT_BUTTON_PRESS_ARROW,
		SOUND_EFFECT_BUTTON_PRESS_TOGGLE,
		SOUND_EFFECT_BUTTON_PRESS_LETTER_ADD,
		SOUND_EFFECT_BUTTON_PRESS_LETTER_REMOVE,
		SOUND_EFFECT_BUTTON_PRESS_SECTION,
		SOUND_EFFECT_BUTTON_PRESS_LEVEL,

		SOUND_EFFECT_LEVEL_START,
		SOUND_EFFECT_LEVEL_COMPLETE,

		SOUND_EFFECT_LIGHT_SPAWN,
	};

	[SerializeField]
	private List<Type> m_types = new List<Type>(1);

	public void Play(int index)
	{
		AudioClip clip = SoundDatabase.Instance.GetSoundEffect(m_types[index]);
		SoundManager.Instance.PlayAudioClip(clip);
	}
}
