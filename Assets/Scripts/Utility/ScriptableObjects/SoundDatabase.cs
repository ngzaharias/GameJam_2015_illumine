using UnityEngine;
using System.Collections;

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

	public AudioClip m_buttonSelect = null;
	public AudioClip m_buttonBack = null;
	public AudioClip m_buttonArrow = null;
	public AudioClip m_buttonSection = null;
	public AudioClip m_buttonLevel = null;
	public AudioClip m_buttonLetterSlot= null;

	public AudioClip m_levelStart = null;
	public AudioClip m_levelEnd = null;
	public AudioClip m_levelSpawnLight = null;

	public AudioClip GetSoundEffect(SoundEffect.Type type)
	{
		switch (type)
		{
			case SoundEffect.Type.SOUND_EFFECT_BUTTON_SELECT: return m_buttonSelect;
			case SoundEffect.Type.SOUND_EFFECT_BUTTON_BACK: return m_buttonBack;
			case SoundEffect.Type.SOUND_EFFECT_BUTTON_ARROW: return m_buttonArrow;
			case SoundEffect.Type.SOUND_EFFECT_BUTTON_SECTION: return m_buttonSection;
			case SoundEffect.Type.SOUND_EFFECT_BUTTON_LEVEL: return m_buttonLevel;
			case SoundEffect.Type.SOUND_EFFECT_BUTTON_LETTER_SLOT: return m_buttonLetterSlot;

			case SoundEffect.Type.SOUND_EFFECT_LEVEL_START: return m_levelStart;
			case SoundEffect.Type.SOUND_EFFECT_LEVEL_END: return m_levelEnd;
			case SoundEffect.Type.SOUND_EFFECT_LEVEL_SPAWN_LIGHT: return m_levelSpawnLight;
		}
		return null;
	}
}
