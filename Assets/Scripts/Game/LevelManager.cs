using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour 
{
	static protected LevelManager m_instance = null;
	static public LevelManager Instance
	{
		get
		{
			return m_instance;
		}
	}

	private GameObject m_model = null;
	private Quaternion m_modelRotation = Quaternion.identity;
	private Timer m_modelSpawnDelay = null;

	[SerializeField]
	private Section[] m_sections;
	[SerializeField]
	private Level[] m_levels;

	[SerializeField]
	private Letter m_letter = null;
	[SerializeField]
	private LetterSlot m_letterSlot = null;

	[SerializeField]
	private RectTransform m_inputAnswer = null;
	private bool m_toggleAnswer = false;
	[SerializeField]
	private RectTransform m_inputLetters = null;
	private bool m_toggleLetters = false;

	private LevelData m_currentLevel;
	public LevelData CurrentLevel { get { return m_currentLevel; } set { m_currentLevel = value; } }

	void Awake()
	{
		m_instance = this;
	}

	void Start()
	{
		SetupSections(LevelDatabase.Instance.m_sections.ToArray());
	}

	void Update()
	{
		//	Check for a delayed spawn
		if (m_modelSpawnDelay != null && m_modelSpawnDelay.Finished())
		{
			SpawnModel(m_model, m_modelRotation);
			m_modelSpawnDelay = null;
		}
	}

	public void SpawnModel(GameObject model, Quaternion rotation, float delay = 0.0f)
	{
		if (model != null)
		{
			m_model = model;
			m_modelRotation = rotation;
			if (delay > 0.0f)
			{
				m_modelSpawnDelay = new Timer();
				m_modelSpawnDelay.Start(delay);
				return;
			}

			m_model = Instantiate(m_model, Vector3.zero, Quaternion.identity) as GameObject;
			m_model.transform.parent = GameManager.Instance.transform;
			m_model.transform.rotation = m_modelRotation;
		}
	}

	public void DestroyModel(float delay = 0.0f)
	{
		if (m_model != null)
		{
			Destroy(m_model, delay);
			m_model = null;
		}
	}

	public void SetupSections(SectionData[] sections)
	{
		for (int i = 0; i < m_sections.Length; ++i)
		{
			if (i < sections.Length)
			{
				m_sections[i].gameObject.SetActive(true);
				m_sections[i].Data = sections[i];
			}
			else
			{
				m_sections[i].gameObject.SetActive(false);
			}
		}
	}

	public void SetupLevels(LevelData[] levels)
	{
		for (int i = 0; i < m_levels.Length; ++i)
		{
			if (i < levels.Length)
			{
				m_levels[i].gameObject.SetActive(true);
				m_levels[i].Data = levels[i];
			}
			else
			{
				m_levels[i].gameObject.SetActive(false);
			}
		}
	}

	public void SetupAnswer(LevelData data)
	{
		LetterSlot[] items = m_inputAnswer.GetComponentsInChildren<LetterSlot>(true);
		for (int i = 0; i < items.Length; ++i)
		{
			if (items[i].gameObject != m_inputAnswer.gameObject)
			{
				Destroy(items[i].gameObject);
			}
		}

		for (int i = 0; i < data.answer.Length; ++i)
		{
			LetterSlot slot = Instantiate<LetterSlot>(m_letterSlot);
			slot.transform.SetParent(m_inputAnswer, false);
		}
	}

	public void SetupLetters(LevelData data)
	{
		// clear letterSlots
		LetterSlot[] slots = m_inputLetters.GetComponentsInChildren<LetterSlot>(true);
		for (int i = 0; i < slots.Length; ++i)
		{
			if (slots[i].gameObject != m_inputAnswer.gameObject)
			{
				Destroy(slots[i].gameObject);
			}
		}

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
			LetterSlot slot = Instantiate<LetterSlot>(m_letterSlot);
			slot.transform.SetParent(m_inputLetters, false);

			Letter letter = Instantiate<Letter>(m_letter);
			letter.transform.SetParent(slot.transform, false);

			slot.Letter = letter;

			int rand = Random.Range(0, letters.Count);
			letter.SetLetter(letters[rand]);
			letters.RemoveAt(rand);
		}
	}

	public void StartLevel(LevelData data)
	{
		SpawnModel(data.model, Utility.RandomQuaternion(), 1.0f);
		CurrentLevel = data;
		SetupAnswer(data);
		SetupLetters(data);
	}

	public void NextLevel()
	{
		if (m_currentLevel.next != null)
		{
			DestroyModel(1.0f);
			StartLevel(m_currentLevel.next);
		}
		else
		{
			ExitLevel("LEVEL_MENU");
		}
	}

	public void ExitLevel(string key)
	{
		if (m_toggleAnswer) ToggleAnswer();
		if (m_toggleLetters) ToggleLetters();

		UIStateManager.Instance.SetState(key);
		LightManager.Instance.FadePointLights(0.0f, 0.5f);
		DestroyModel(0.6f);
	}

	public void ToggleAnswer()
	{
		m_toggleAnswer = !m_toggleAnswer;
		m_inputAnswer.GetComponent<Animator>().SetBool("Toggle", m_toggleAnswer);
	}

	public void ToggleLetters()
	{
		m_toggleLetters = !m_toggleLetters;
		m_inputLetters.GetComponent<Animator>().SetBool("Toggle", m_toggleLetters);
	}
}
