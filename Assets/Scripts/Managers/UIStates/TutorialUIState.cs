using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class TutorialUIState : UIState
{
	private int m_state = 0;
	private float m_timer = 0.0f;

	protected override void Start()
	{
		base.Start();
		m_timer = GameParameters.Instance.m_TutorialMinimumTime;
	}

	protected override void Update()
	{
		base.Update();

		m_timer = Mathf.Clamp(m_timer - Time.deltaTime, 0.0f, GameParameters.Instance.m_TutorialMinimumTime);

		switch (m_state)
		{
			case 0: State0(); break;
			case 1: State1(); break;
			case 2: State2(); break;
			case 3: State3(); break;
		}
		m_animator.SetInteger("State", m_state);
	}

	public override void Enable()
	{
		base.Enable();
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
		if (Input.GetMouseButtonDown(0))
		{
			m_state = 2;
		}
	}

	void State2()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			m_state = 3;
		}
	}

	void State3()
	{
		if (Input.GetMouseButtonDown(1))
		{
			m_state = 4;
		}
	}
}