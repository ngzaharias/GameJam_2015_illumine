using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputAnswer : MonoBehaviour 
{
	private Animator m_animator = null;
	private InputField m_inputField = null;

	void Awake()
	{
		m_animator = GetComponent<Animator>();
		m_inputField = GetComponent<InputField>();
	}

	public void Submit(string answer)
	{
		answer = answer.ToLower();
		string name = LevelManager.Instance.CurrentLevel.answer.ToLower();
		if (name.CompareTo(answer) == 0)
		{
			CorrectAnswer();
		}
		else
		{
			IncorrectAnswer();
		}
	}

	private void CorrectAnswer()
	{
		float duration;
		GameManager.Instance.PlayVictoryParticles(out duration);
		LevelManager.Instance.NextLevel();
		m_inputField.text = "";
	}

	private void IncorrectAnswer()
	{
		if (m_animator != null)
		{
			m_animator.SetTrigger("Shake");
			m_inputField.text = "";
		}
	}
}
