using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct UIColours
{
    public Color text;
    public Color button_Normal;
    public Color button_Highlighted;
    public Color button_Pressed;
    public Color button_Disabled;
	public Color image;
}

public class GameParameters : ScriptableObject 
{
    private static GameParameters m_instance = null;
    public static GameParameters Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = (GameParameters)Resources.Load("GameParameters", typeof(GameParameters));
            if (m_instance == null)
                Debug.LogError("GameParameters asset hasn't be created yet! Go to 'Assets/GameParameters'");
            return m_instance;
        }
    }

    public string m_GameName = "";
    public string m_Version = "alpha.1";

	public bool m_connectSections = false;

    public Texture2D[] m_Textures = null;

    public UIColours m_UIColours;

	public uint m_RotateXYTouchCount = 1;
	public uint m_RotateZTouchCount = 2;
	public float m_RotateObjectSpeed = 500.0f;

	public float m_TutorialStateTime = 5.0f;
	public float m_TutorialDragDistance = 100.0f;
}
