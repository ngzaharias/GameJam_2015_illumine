using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIStateManager : MonoBehaviour 
{
	private static UIStateManager m_instance = null;
	public static UIStateManager Instance
	{
		get
		{
			return m_instance;
		}
	}

	public UIState m_startState = null;
	private static List<UIState> m_scenesStack = new List<UIState>();
	private static Dictionary<string, UIState> m_scenesDictionary = new Dictionary<string, UIState>();

	void Awake()
	{
		m_instance = this;
	}

	void Start()
	{
		if (m_startState != null)
		{
			PushState(m_startState.m_key);
		}
	}

	public void RegisterState(UIState state)
	{
		if (m_scenesDictionary.ContainsKey(state.m_key) == false)
		{
			m_scenesDictionary[state.m_key] = state;
		}
	}

	public void UnregisterState(UIState state)
	{
		if (m_scenesDictionary.ContainsKey(state.m_key))
		{
			m_scenesDictionary.Remove(state.m_key);
		}
	}

	public void ChangeState(string key)
	{
		if (m_scenesDictionary.ContainsKey(key) && m_scenesStack[m_scenesStack.Count-1].m_key != key)
		{
			PopState();
			PushState(key);
		}
	}

	public void PushState(string key)
	{
		if (m_scenesDictionary.ContainsKey(key))
		{
			if (m_scenesStack.Contains(m_scenesDictionary[key]) == false)
			{
				m_scenesStack.Add(m_scenesDictionary[key]);
				m_scenesStack[m_scenesStack.Count - 1].Enable();
			}
		}
	}

	public void PopState()
	{
		if (m_scenesStack.Count > 0)
		{
			m_scenesStack[m_scenesStack.Count - 1].Disable();
			m_scenesStack.Remove(m_scenesStack[m_scenesStack.Count - 1]);
		}
	}

	public void SetState(string key)
	{
		if (m_scenesStack.Count == 1 && m_scenesStack[0].m_key != key)
		{
			for (int i = m_scenesStack.Count - 1; i >= 0; --i)
			{
				PopState();
			}
			PushState(key);
		}
	}
}
