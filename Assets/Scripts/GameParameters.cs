using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TargetModel
{
    public string key;
    public GameObject model;
    public string[] names;
}

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

    public Texture2D[] m_Textures = null;
	public TargetModel m_DefaultModel = null;
	public TargetModel[] m_LevelModels = null;

    public UIColours m_UIColours;

	public float m_TutorialStateTime = 5.0f;
	public float m_TutorialDragDistance = 100.0f;
}
