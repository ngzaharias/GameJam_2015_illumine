using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Animator))]
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
		string[] names = LevelManager.Instance.CurrentModel.names;
		for (int i = 0; i < names.Length; ++i)
		{
			string name = names[i].ToLower();
			if (name.CompareTo(answer) == 0)
			{
				CorrectAnswer();
				return;
			}
		}
		IncorrectAnswer();
	}

	void CorrectAnswer()
	{
		float duration;
		GameManager.Instance.PlayVictoryParticles(out duration);
		LevelManager.Instance.NextLevel();
		m_inputField.text = "";
	}

	void IncorrectAnswer()
	{
		m_animator.SetTrigger("Shake");
		m_inputField.text = "";
	}
}
