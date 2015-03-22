using UnityEngine;
using System.Collections;

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

	private Animator m_animator = null;
	private TargetModel m_currentModel = null;
	public TargetModel CurrentModel { get { return m_currentModel; } }
	private int m_currentLevel = -1;

	void Awake()
	{
		m_instance = this;
		m_animator = GetComponent<Animator>();
	}
	void Start()
	{
		SpawnDefaultModel();
	}

	public void NextLevel()
	{
		++m_currentLevel;
		if (m_currentLevel < GameParameters.Instance.m_LevelModels.Length)
		{
			m_currentModel = GameParameters.Instance.m_LevelModels[m_currentLevel];
			SpawnLevelModel();
			UIStateManager.Instance.SetState("GAME_MENU");
		}
		else
		{

		}
	}

	void SpawnLevelModel()
	{
		DestroyModel();

		m_currentModel = GameParameters.Instance.m_LevelModels[m_currentLevel];

		m_model = Instantiate(m_currentModel.model, Vector3.zero, Quaternion.identity) as GameObject;

		m_model.transform.parent = GameManager.Instance.transform;
		m_model.transform.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
	}

	void SpawnDefaultModel()
	{
		DestroyModel();

		m_currentModel = GameParameters.Instance.m_DefaultModel;

		m_model = Instantiate(m_currentModel.model, Vector3.zero, Quaternion.identity) as GameObject;
		m_model.transform.parent = GameManager.Instance.transform;
	}

	void DestroyModel()
	{
		if (m_model != null)
		{
			Destroy(m_model);
		}
	}
}
