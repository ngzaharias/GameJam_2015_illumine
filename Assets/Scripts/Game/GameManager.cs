using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	static protected GameManager m_instance = null;
	static public GameManager Instance
	{
		get
		{
			if (m_instance == null)
				m_instance = GameObject.FindObjectOfType<GameManager>();
			return m_instance;
		}
	}

	[SerializeField]
	private ParticleSystem m_victoryParticles = null;

	void Awake()
	{
		m_instance = this;
	}

	void Update()
	{

	}

	public void PlayVictoryParticles(out float duration)
	{
		if (m_victoryParticles != null)
		{
			m_victoryParticles.Play();
			duration = m_victoryParticles.duration;
		}
		else
		{
			duration = 0.0f;
		}
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
