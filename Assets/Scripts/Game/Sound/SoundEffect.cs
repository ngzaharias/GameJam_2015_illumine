using UnityEngine;
using System.Collections;

public class SoundEffect : MonoBehaviour 
{
	public enum Type
	{
		SOUND_EFFECT_NONE = -1,

		SOUND_EFFECT_BUTTON_SELECT,
		SOUND_EFFECT_BUTTON_BACK,
		SOUND_EFFECT_BUTTON_ARROW,
		SOUND_EFFECT_BUTTON_SECTION,
		SOUND_EFFECT_BUTTON_LEVEL,
		SOUND_EFFECT_BUTTON_LETTER_SLOT,

		SOUND_EFFECT_LEVEL_START,
		SOUND_EFFECT_LEVEL_END,
		SOUND_EFFECT_LEVEL_SPAWN_LIGHT,
	};

	[SerializeField]
	private Type m_type = Type.SOUND_EFFECT_NONE;

	public void Play()
	{
		AudioClip clip = SoundDatabase.Instance.GetSoundEffect(m_type);
		SoundManager.Instance.PlayAudioClip(clip);
	}
}
