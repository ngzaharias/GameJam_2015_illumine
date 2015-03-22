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
	private static List<UIState> m_statesStack = new List<UIState>();
	private static List<UIState> m_toggleStack = new List<UIState>();
	private static Dictionary<string, UIState> m_statesDictionary = new Dictionary<string, UIState>();

	public UIState CurrentState
	{
		get
		{
			if (m_statesStack.Count > 0)
			{
				return m_statesStack[m_statesStack.Count - 1];
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

	public void UnregisterState(UIState state)
	{
		if (m_statesDictionary.ContainsKey(state.m_key))
		{
			m_statesDictionary.Remove(state.m_key);
		}
	}

	public void ChangeState(string key)
	{
		if (m_statesDictionary.ContainsKey(key) && m_statesStack[m_statesStack.Count-1].m_key != key)
		{
			PopState();
			PushState(key);
		}
	}

	public void PushState(string key)
	{
		if (m_statesDictionary.ContainsKey(key))
		{
			if (m_statesStack.Contains(m_statesDictionary[key]) == false)
			{
				m_statesStack.Add(m_statesDictionary[key]);
				m_statesStack[m_statesStack.Count - 1].Enable();
			}
		}
	}

	public void PopState()
	{
		if (m_statesStack.Count > 0)
		{
			m_statesStack[m_statesStack.Count - 1].Disable();
			m_statesStack.Remove(m_statesStack[m_statesStack.Count - 1]);
		}
	}

	public void SetState(string key)
	{
		if (m_statesDictionary.ContainsKey(key))
		{
			for (int i = m_statesStack.Count - 1; i >= 0; --i)
			{
				PopState();
			}
			PushState(key);
		}
	}

	public void ToggleState(string key)
	{
		UIState state = m_statesDictionary[key];
		int index = m_toggleStack.IndexOf(state);

		// not recorded, push it on and record it
		if (index == -1)
		{
			m_toggleStack.Add(state);
			PushState(key);
		}
		//	already recorded, pop everything above and including the state
		else
		{
			int count = m_statesStack.Count-1 - index;
			for (int i = 0; i < count; ++i)
			{
				m_toggleStack.Remove(state);
				PopState();
			}
		}
	}
}
