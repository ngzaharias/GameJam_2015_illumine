using UnityEngine;
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
	private Timer m_modelDestroyDelay = null;

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

		if (m_modelDestroyDelay != null && m_modelDestroyDelay.Finished())
		{
			DestroyModel();
			m_modelDestroyDelay = null;
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
			if (delay > 0.0f)
			{
				m_modelDestroyDelay = new Timer();
				m_modelDestroyDelay.Start(delay);
			}
			else
			{
				Destroy(m_model);
			}
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

	public void StartLevel(LevelData data)
	{
		SpawnModel(data.model, Utility.RandomQuaternion(), 1.0f);
		CurrentLevel = data;
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
			ExitLevel();
		}
	}

	public void ExitLevel()
	{
		UIStateManager.Instance.SetState("LEVEL_MENU");
		DestroyModel(1.0f);
	}
}
