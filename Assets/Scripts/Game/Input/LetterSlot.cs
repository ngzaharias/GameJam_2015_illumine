using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class LetterSlot : MonoBehaviour 
{
	private Letter m_letter = null;
	public Letter Letter { get { return m_letter; } set { m_letter = value; } }

	static private LetterSlot s_other = null;

	void Awake()
	{
		m_letter = GetComponentInChildren<Letter>();
	}

	public void SelectSlot()
	{
		if (s_other == null)
		{
			s_other = this;
		}
		else if (s_other != this)
		{
			SwapLetters(s_other);
			s_other = null;
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	public void SwapLetters(LetterSlot slot)
	{
		Letter temp = m_letter;
		m_letter = slot.Letter;
		if (m_letter != null)
		{
			m_letter.transform.SetParent(transform);
			RectTransform rt = m_letter.transform as RectTransform;
			rt.anchoredPosition = Vector2.zero;
			rt.sizeDelta = Vector2.zero;
		}

		slot.Letter = temp;
		if (slot.Letter != null)
		{
			slot.Letter.transform.SetParent(slot.transform);
			RectTransform rt = slot.Letter.transform as RectTransform;
			rt.anchoredPosition = Vector2.zero;
			rt.sizeDelta = Vector2.zero;
		}
	}

	public void SwapLettersWithAnswerSlot()
	{
		LetterSlot slot = LetterSlotManager.Instance.GetAnswerSlot();
		if (slot != null)
		{
			SwapLetters(slot);
		}

		string slotAnswer = LetterSlotManager.Instance.GetAnswer();
		string levelAnswer = LevelManager.Instance.CurrentLevel.answer;
		if (slotAnswer.ToLower() == levelAnswer.ToLower())
		{
			LevelManager.Instance.CompleteLevel();
		}
	}

	public void SwapLettersWithLetterSlot()
	{
		LetterSlot slot = LetterSlotManager.Instance.GetLetterSlot();
		if (slot != null)
		{
			SwapLetters(slot);
		}
	}
}
