using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
			SwapLetters();
			s_other = null;
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	private void SwapLetters()
	{
		Letter temp = m_letter;
		m_letter = s_other.Letter;
		if (m_letter != null)
		{
			m_letter.transform.SetParent(transform);
			RectTransform rt = m_letter.transform as RectTransform;
			rt.anchoredPosition = Vector2.zero;
			rt.sizeDelta = Vector2.zero;
		}

		s_other.Letter = temp;
		if (s_other.Letter != null)
		{
			s_other.Letter.transform.SetParent(s_other.transform);
			RectTransform rt = s_other.Letter.transform as RectTransform;
			rt.anchoredPosition = Vector2.zero;
			rt.sizeDelta = Vector2.zero;
		}
	}
}
