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
				if (i > 0)
				{
					m_levels[i-1].Data.next = m_levels[i].Data;
				}
			}
			else
			{
				m_levels[i].gameObject.SetActive(false);
			}
		}
	}

	public void StartLevel(LevelData data)
	{
		LightManager.Instance.FadeDirectionalLight(0.0f, 0.5f);
		LightManager.Instance.FadePointLights(0.0f, 0.5f);
		
		SpawnModel(data.model, Utility.RandomQuaternion(), 1.0f);
		CurrentLevel = data;

		LetterSlotManager.Instance.SetupSlots(data);
		LightManager.Instance.SpawningEnabled = true;
	}

	public void CompleteLevel()
	{
		LightManager.Instance.FadePointLights(0.0f, 0.5f);
		LightManager.Instance.FadeDirectionalLight(1.0f, 0.5f);

		LetterSlotManager.Instance.ToggleSlots(false);
		UIStateManager.Instance.PushState("LEVEL_END_MENU");
	}

	public void NextLevel()
	{
		if (m_currentLevel.next != null)
		{
			UIStateManager.Instance.SetState("GAME_MENU");
			DestroyModel(1.0f);
			StartLevel(m_currentLevel.next);
		}
		else
		{
			ExitLevel("SECTION_MENU");
		}
	}

	public void ExitLevel(string key)
	{
		LightManager.Instance.FadePointLights(0.0f, 0.3f);
		LightManager.Instance.FadeDirectionalLight(0.0f, 0.3f);
		DestroyModel(0.5f);

		LetterSlotManager.Instance.ToggleSlots(false);
		UIStateManager.Instance.SetState(key);
		LightManager.Instance.SpawningEnabled = false;
	}
}
