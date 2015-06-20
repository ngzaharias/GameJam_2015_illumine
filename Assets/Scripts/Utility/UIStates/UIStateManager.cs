﻿using UnityEngine;
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

	private string m_nextStateKey = null;

	private List<UIState> m_stateStack = new List<UIState>();
	private List<UIState> m_statePopStack = new List<UIState>();
	private Dictionary<string, UIState> m_statesDictionary = new Dictionary<string, UIState>();

	public UIState CurrentState
	{
		get
		{
			if (m_stateStack.Count > 0)
			{
				return m_stateStack[m_stateStack.Count - 1];
			}
			return null;
		}
	}

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
		if (m_statesDictionary.ContainsKey(state.m_key) == false)
		{
			m_statesDictionary[state.m_key] = state;
		}
	}

	public void UnRegisterState(UIState state)
	{
		if (m_statesDictionary.ContainsKey(state.m_key))
		{
			m_statesDictionary.Remove(state.m_key);
		}
	}

	public void ChangeState(string key)
	{
		if (m_statesDictionary.ContainsKey(key) && m_stateStack[m_stateStack.Count-1].m_key != key)
		{
			PopState();
			m_nextStateKey = key;
		}
	}

	public void PushState(string key)
	{
		if (m_statesDictionary.ContainsKey(key))
		{
			if (m_stateStack.Contains(m_statesDictionary[key]) == false)
			{
				UIState state = m_statesDictionary[key];
				state.gameObject.SetActive(true);
				m_stateStack.Add(state);
				state.Enable();
			}
		}
	}

	public void PopState()
	{
		if (m_stateStack.Count > 0)
		{
			UIState state = m_stateStack[m_stateStack.Count - 1];
			state.Disable();
			m_stateStack.Remove(state);
			m_statePopStack.Add(state);
		}
	}

	public void PopState_Finished(UIState state)
	{
		if (m_nextStateKey != null)
		{
			if (m_statePopStack.Count > 0)
			{
				m_statePopStack.Remove(state);
				if (m_statePopStack.Count == 0)
				{
					PushState(m_nextStateKey);
					m_nextStateKey = null;
				}
			}
		}
	}

	public void SetState(string key)
	{
		if (m_statesDictionary.ContainsKey(key))
		{
			if (m_stateStack.Count > 0)
			{
				for (int i = m_stateStack.Count - 1; i >= 0; --i)
				{
					PopState();
				}
				m_nextStateKey = key;
			}
			else
			{
				PushState(key);
			}
		}
	}

	//public void ToggleState(string key)
	//{
	//	UIState state = m_statesDictionary[key];
	//	int index = m_stateStack.IndexOf(state);

	//	// not recorded, push it on and record it
	//	if (index == -1)
	//	{
	//		PushState(key);
	//	}
	//	//	already recorded, pop everything above and including the state
	//	else
	//	{
	//		int count = m_stateStack.Count - index;
	//		for (int i = 0; i < count; ++i)
	//		{
	//			PopState();
	//		}
	//	}
	//}
}