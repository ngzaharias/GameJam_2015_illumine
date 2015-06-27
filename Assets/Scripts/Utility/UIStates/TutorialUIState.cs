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
			case  0: State0(); break;
			case  1: State1(); break;
			case  2: State2(); break;
			case  3: State3(); break;
		}
		m_animator.SetInteger("State", m_state);
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

	void State0()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			m_state = 1;
		}
	}

	void State1()
	{
		float dragDistance = CalculateMouseDragDistance(0);
		if (dragDistance >= GameParameters.Instance.m_TutorialDragDistance)
		{
			m_state = 2;
		}
	}

	void State2()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			m_state = 4;
		}
	}

	void State3()
	{
		float dragDistance = CalculateMouseDragDistance(1);
		if (dragDistance >= GameParameters.Instance.m_TutorialDragDistance)
		{
			m_state = 4;
		}
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