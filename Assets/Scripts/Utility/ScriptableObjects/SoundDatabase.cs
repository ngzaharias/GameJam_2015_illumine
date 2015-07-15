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

	public AudioClip m_buttonEnter = null;
	public AudioClip m_buttonPress = null;

	public AudioClip m_buttonPressBack = null;
	public AudioClip m_buttonPressArrow = null;
	public AudioClip m_buttonPressToggle = null;
	public AudioClip m_buttonPressLetterAdd = null;
	public AudioClip m_buttonPressLetterRemove = null;
	public AudioClip m_buttonPressSection = null;
	public AudioClip m_buttonPressLevel = null;

	public AudioClip m_levelStart = null;
	public AudioClip m_levelComplete = null;

	public AudioClip m_lightSpawn = null;

	public AudioClip GetSoundEffect(SoundEffect.Type type)
	{
		switch (type)
		{
			case SoundEffect.Type.SOUND_EFFECT_BUTTON_ENTER: return m_buttonEnter;
			case SoundEffect.Type.SOUND_EFFECT_BUTTON_PRESS: return m_buttonPress;

			case SoundEffect.Type.SOUND_EFFECT_BUTTON_PRESS_BACK: return m_buttonPressBack;
			case SoundEffect.Type.SOUND_EFFECT_BUTTON_PRESS_ARROW: return m_buttonPressArrow;
			case SoundEffect.Type.SOUND_EFFECT_BUTTON_PRESS_TOGGLE: return m_buttonPressToggle;
			case SoundEffect.Type.SOUND_EFFECT_BUTTON_PRESS_LETTER_ADD: return m_buttonPressLetterAdd;
			case SoundEffect.Type.SOUND_EFFECT_BUTTON_PRESS_LETTER_REMOVE: return m_buttonPressLetterRemove;
			case SoundEffect.Type.SOUND_EFFECT_BUTTON_PRESS_SECTION: return m_buttonPressSection;
			case SoundEffect.Type.SOUND_EFFECT_BUTTON_PRESS_LEVEL: return m_buttonPressLevel;

			case SoundEffect.Type.SOUND_EFFECT_LEVEL_START: return m_levelStart;
			case SoundEffect.Type.SOUND_EFFECT_LEVEL_COMPLETE: return m_levelComplete;

			case SoundEffect.Type.SOUND_EFFECT_LIGHT_SPAWN: return m_lightSpawn;
		}
		return null;
	}
}
