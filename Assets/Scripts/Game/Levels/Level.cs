using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour 
{
	[SerializeField]
	private LevelData m_data;
	public LevelData Data { get { return m_data; } set { m_data = value; } }

	public void StartLevel(string key)
	{
		//	MUST DO THIS BEFORE STARTING LEVEL
		UIStateManager.Instance.SetState(key);
		LevelManager.Instance.StartLevel(m_data);
	}
}
