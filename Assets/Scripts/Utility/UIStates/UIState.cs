using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class UIState : MonoBehaviour
{
	public string m_key = "NULL";
	[HideInInspector]
	public Animator m_animator = null;

	protected virtual void Awake()
	{
		if (m_key.CompareTo("NULL") == 0)
		{
			Debug.LogError("Scene Key for " + gameObject.name + " hasn't been set!");
		}

		m_animator = GetComponent<Animator>();
	}

	protected virtual void Start()
	{
		UIStateManager.Instance.RegisterState(this);
		this.gameObject.SetActive(false);
	}

	public virtual void Enable()
	{
		m_animator.SetTrigger("Enable");
	}

	public virtual void Disable()
	{
		m_animator.SetTrigger("Disable");
	}

	public virtual void DisableFinished()
	{
		UIStateManager.Instance.PopState_Finished(this);
	}
}