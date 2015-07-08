using UnityEngine;
using System.Collections;

public enum SoundEffect
{
	SOUND_EFFECT_NONE = -1,

	SOUND_EFFECT_BUTTON_BACK,
	SOUND_EFFECT_BUTTON_LETTER_SLOT,
	SOUND_EFFECT_BUTTON_SECTION,
	SOUND_EFFECT_BUTTON_LEVEL,
	SOUND_EFFECT_LEVEL_START,
	SOUND_EFFECT_LEVEL_END,

	SOUND_EFFECT_COUNT
};

public class SoundDatabase : ScriptableObject 
{
    private static SoundDatabase m_instance = null;
    public static SoundDatabase Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = (SoundDatabase)Resources.Load("SoundDatabase", typeof(SoundDatabase));
            if (m_instance == null)
				Debug.LogError("SoundDatabase asset hasn't be created yet! Go to 'Assets/SoundDatabase'");
            return m_instance;
        }
    }

	public AudioClip m_buttonBack = null;
	public AudioClip m_buttonLetterSlot= null;
	public AudioClip m_buttonSection = null;
	public AudioClip m_buttonLevel = null;
	public AudioClip m_levelStart = null;
	public AudioClip m_levelEnd = null;

	public AudioClip GetSoundEffectFromType(SoundEffect type)
	{
		switch (type)
		{
			case SoundEffect.SOUND_EFFECT_BUTTON_BACK: return m_buttonBack;
			case SoundEffect.SOUND_EFFECT_BUTTON_LETTER_SLOT: return m_buttonLetterSlot;
			case SoundEffect.SOUND_EFFECT_BUTTON_SECTION: return m_buttonSection;
			case SoundEffect.SOUND_EFFECT_BUTTON_LEVEL: return m_buttonLevel;
			case SoundEffect.SOUND_EFFECT_LEVEL_START: return m_levelStart;
			case SoundEffect.SOUND_EFFECT_LEVEL_END: return m_levelEnd;
		}
		return null;
	}
}
