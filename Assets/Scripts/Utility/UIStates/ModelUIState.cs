using UnityEngine;
using System.Collections;

public class ModelUIState : UIState
{
	[SerializeField]
	protected GameObject m_model = null;

	public override void Enable()
	{
		base.Enable();
		LevelManager.Instance.SpawnModel(m_model, Quaternion.identity);
		LightManager.Instance.FadeDirectionalLight(1.0f, 1.0f);
	}

	public override void Disable()
	{
		base.Disable();
		LevelManager.Instance.DestroyModel(1.0f);
		LightManager.Instance.FadeDirectionalLight(0.0f, 1.0f);
	}
}