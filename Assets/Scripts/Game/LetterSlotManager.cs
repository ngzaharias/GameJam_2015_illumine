using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LetterSlotManager : MonoBehaviour {

	static protected LetterSlotManager m_instance = null;
	static public LetterSlotManager Instance
	{
		get
		{
			return m_instance;
		}
	}

	[SerializeField]
	private Letter m_letterPrefab = null;
	[SerializeField]
	private LetterSlot m_slotPrefab = null;

	[SerializeField]
	private RectTransform m_answerSlotsParent = null;
	[SerializeField]
	private RectTransform m_letterSlotsParent = null;

	private List<LetterSlot> m_answerSlots = new List<LetterSlot>();
	private List<LetterSlot> m_lettersSlots = new List<LetterSlot>();

	private bool m_toggle = false;

	void Awake()
	{
		m_instance = this;
	}

	public string GetAnswer()
	{
		string answer = null;
		for (int i = 0; i < m_answerSlots.Count; ++i)
		{
			if (m_answerSlots[i].Letter != null)
			{
				answer += m_answerSlots[i].Letter.Text;
			}
		}
		return answer;
	}

	public LetterSlot GetAnswerSlot()
	{
		//	search through for an empty space OR
		//	switch with the last item
		for (int i = 0; i < m_answerSlots.Count; ++i)
		{
			if (m_answerSlots[i].Letter == null ||
				i == m_answerSlots.Count - 1)
			{
				return m_answerSlots[i];
			}
		}
		return null;
	}

	public LetterSlot GetLetterSlot()
	{
		//	search through for an empty space OR
		//	switch with the last item
		for (int i = 0; i < m_lettersSlots.Count; ++i)
		{
			if (m_lettersSlots[i].Letter == null ||
				i == m_lettersSlots.Count - 1)
			{
				return m_lettersSlots[i];
			}
		}
		return null;
	}

	public void ToggleSlots()
	{
		m_toggle = !m_toggle;
		m_answerSlotsParent.GetComponent<Animator>().SetBool("Toggle", m_toggle);
		m_letterSlotsParent.GetComponent<Animator>().SetBool("Toggle", m_toggle);
	}

	public void ToggleSlots(bool state)
	{
		if (m_toggle != state)
		{
			m_toggle = state;
			m_answerSlotsParent.GetComponent<Animator>().SetBool("Toggle", m_toggle);
			m_letterSlotsParent.GetComponent<Animator>().SetBool("Toggle", m_toggle);
		}
	}

	public void SetupSlots(LevelData data)
	{
		SetupAnswerSlots(data);
		SetupLetterSlots(data);
	}

	void SetupAnswerSlots(LevelData data)
	{
		for (int i = 0; i < m_answerSlots.Count; ++i)
		{
			Destroy(m_answerSlots[i].gameObject);
		}
		m_answerSlots.Clear();

		for (int i = 0; i < data.answer.Length; ++i)
		{
			LetterSlot slot = Instantiate<LetterSlot>(m_slotPrefab);
			slot.transform.SetParent(m_answerSlotsParent, false);
			slot.GetComponent<Button>().onClick.AddListener(slot.SwapLettersWithLetterSlot);
			m_answerSlots.Add(slot);
		}
	}

	void SetupLetterSlots(LevelData data)
	{
		// clear letterSlots
		for (int i = 0; i < m_lettersSlots.Count; ++i)
		{
			Destroy(m_lettersSlots[i].gameObject);
		}
		m_lettersSlots.Clear();

		//	add legitimate letters
		int count = data.answer.Length;
		List<char> letters = new List<char>();
		for (int i = 0; i < count; ++i)
		{
			letters.Add(data.answer[i]);
		}

		//	add random letters
		int subCount = (count / 3) + 1;
		for (int i = 0; i < subCount; ++i)
		{
			int rand = Random.Range(0, 26);
			letters.Add((char)(65 + rand));
		}

		//	spawn the letters
		for (int i = 0; i < count + subCount; ++i)
		{
			LetterSlot slot = Instantiate<LetterSlot>(m_slotPrefab);
			slot.transform.SetParent(m_letterSlotsParent, false);
			slot.GetComponent<Button>().onClick.AddListener(slot.SwapLettersWithAnswerSlot);
			m_lettersSlots.Add(slot);

			Letter letter = Instantiate<Letter>(m_letterPrefab);
			letter.transform.SetParent(slot.transform, false);

			slot.Letter = letter;

			int rand = Random.Range(0, letters.Count);
			letter.SetLetter(letters[rand]);
			letters.RemoveAt(rand);
		}
	}
}
