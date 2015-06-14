using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class SplashUIState : UIState
{
	[SerializeField]
	private string m_nextState;

	public void TransitionToNextState()
	{
		UIStateManager.Instance.SetState(m_nextState);
	}

	public void TransitionToNextSplash()
	{

	}
}