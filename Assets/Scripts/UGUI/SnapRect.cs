using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SnapRect : MonoBehaviour 
{

	[SerializeField]
	private bool m_horizontal = true;
	[SerializeField]
	private bool m_vertical = true;
	[SerializeField]
	private RectTransform m_content = null;
	[SerializeField]
	private RectTransform[] m_positions = null;
	[SerializeField]
	private AnimationCurve m_curve = AnimationCurve.Linear(0.0f, 1.0f, 0.0f, 1.0f);
	[SerializeField]
	private float m_duration = 1.0f;

	private Vector2 m_previousPosition;
	private Vector2 m_nextPosition;
	private int m_currentIndex = 0;
	private float m_timer = 0.0f;

	void Awake()
	{
		if (m_positions.Length == 0)
		{
			Debug.LogWarning("SnapRect doesn't have any positions");
		}
	}

	void Start () 
	{
		
	}
	
	void Update () 
	{
		InterpolateContent();
	}

	private void InterpolateContent()
	{
		if (m_positions.Length > 0)
		{
			m_timer += Time.deltaTime / m_duration;

			float eval = m_curve.Evaluate(m_timer);
			Vector2 current = m_content.anchoredPosition;
			if (m_horizontal) current.x = -Mathf.SmoothStep(m_previousPosition.x, m_nextPosition.x, eval);
			if (m_vertical) current.y = -Mathf.Lerp(m_previousPosition.y, m_nextPosition.y, eval);
			m_content.anchoredPosition = new Vector2(current.x, current.y);
		}
	}

	public void IncrementIndex(int amount)
	{
		int previousIndex = m_currentIndex;
		m_currentIndex += amount;

		if (m_currentIndex < 0)
		{
			m_currentIndex = 0;
		}
		else if (m_currentIndex >= m_positions.Length)
		{
			m_currentIndex = m_positions.Length - 1;
		}

		if (m_positions[m_currentIndex].gameObject.activeSelf == false)
		{
			m_currentIndex = previousIndex;
		}

		if (m_timer > 1.0f)
		{
			m_timer = 0.0f;
		}
		else
		{
			m_timer *= 0.5f;
		}
		m_previousPosition = -m_content.anchoredPosition;
		m_nextPosition = m_positions[m_currentIndex].anchoredPosition;
	}
}
