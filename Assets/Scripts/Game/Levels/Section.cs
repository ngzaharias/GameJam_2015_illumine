using UnityEngine;
using System.Collections;

public class Section : MonoBehaviour 
{
	private SectionData m_data;
	public SectionData Data { get { return m_data; } set { m_data = value; } }

	public void StartSection()
	{
		LevelManager.Instance.SetupLevels(m_data.levels.ToArray());
		UIStateManager.Instance.SetState("LEVEL_MENU");
	}
}
