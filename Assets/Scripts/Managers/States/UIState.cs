using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class UIState : MonoBehaviour
{
	public string m_key = "NULL";
	[HideInInspector]
	public Animator m_animator = null;

	private bool m_enabled = false;

	void Awake()
	{
		if (m_key.CompareTo("NULL") == 0)
		{
			Debug.LogError("Scene Key for " + gameObject.name + " hasn't been set!");
		}

		m_animator = GetComponent<Animator>();
	}

	void Start()
	{
		UIStateManager.Instance.RegisterState(this);
	}

	void Update()
	{

	}

	public virtual void Enable()
	{
		m_enabled = true;
		m_animator.SetBool("Enabled", m_enabled);
	}

	public virtual void Disable()
	{
		m_enabled = false;
		m_animator.SetBool("Enabled", m_enabled);
	}
}