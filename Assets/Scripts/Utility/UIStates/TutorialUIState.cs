using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class TutorialUIState : UIState
{
	private int m_state = 0;
	private Vector2 m_dragStartPos = Vector2.zero;

	protected override void Start()
	{
		base.Start();
	}

	void Update()
	{
		switch (m_state)
		{
			case 0: State0(); break;
			case 1: State1(); break;
			case 2: State2(); break;
			case 3: State3(); break;
		}
	}

	public override void Enable()
	{
		base.Enable();
		UIStateManager.Instance.PushState("GAME_MENU");
	}

	public override void Disable()
	{
		base.Disable();
	}

	private void State0()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			NextState();
		}
	}

	private void State1()
	{
		float dragDistance = CalculateMouseDragDistance(0);
		if (dragDistance >= GameParameters.Instance.m_TutorialDragDistance)
		{
			NextState();
		}
	}

	private void State2()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			NextState();
			NextState();
		}
	}

	private void State3()
	{
		float dragDistance = CalculateMouseDragDistance(1);
		if (dragDistance >= GameParameters.Instance.m_TutorialDragDistance)
		{
			NextState();
		}
	}

	private void NextState()
	{
		++m_state;
		m_animator.SetInteger("State", m_state);
		m_animator.SetTrigger("NextState");
	}

	private float CalculateMouseDragDistance(int mouseButton)
	{
		if (Input.GetMouseButtonDown(mouseButton))
		{
			m_dragStartPos = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp(mouseButton))
		{
			return Vector2.Distance(Input.mousePosition, m_dragStartPos);
		}
		return 0.0f;
	}
}