using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour 
{
	private LevelData m_data;
	public LevelData Data { get { return m_data; } set { m_data = value; } }

	public void StartLevel()
	{
		LevelManager.Instance.StartLevel(m_data);
	}
}
