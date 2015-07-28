using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
	public string key = null;
	public GameObject model = null;
	public string answer = null;

	public LevelData next = null;
}

[System.Serializable]
public class SectionData
{
	public string key = null;
	public List<LevelData> levels = new List<LevelData>();

	public SectionData next = null;

	public LevelData GetLevelByIndex(int index)
	{
		if (index < levels.Count)
		{
			return levels[index];
		}
		return null;
	}
}

public class LevelDatabase : ScriptableObject 
{
	static private LevelDatabase m_instance = null;
	static public LevelDatabase Instance
    {
        get
        {
            if (m_instance == null)
				m_instance = (LevelDatabase)Resources.Load("LevelDatabase", typeof(LevelDatabase));
            if (m_instance == null)
				Debug.LogError("GameParameters asset hasn't be created yet! Go to 'Assets/LevelDatabase'");
            return m_instance;
        }
    }

	public List<SectionData> m_sections = null;

	public SectionData GetSectionByIndex(int index)
	{
		if (index < m_sections.Count)
		{
			return m_sections[index];
		}
		return null;
	}

	public void AddSection(SectionData section)
	{
		m_sections.Add(section);
	}

	public void RemoveSection(SectionData section)
	{
		m_sections.Remove(section);
	}

	public void MoveSection(SectionData section, int direction)
	{
		int index = m_sections.IndexOf(section);
		if (index != -1)
		{
			m_sections.RemoveAt(index);
			m_sections.Insert(index + direction, section);
		}
	}
}
