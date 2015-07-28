using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DeveloperTools))]
public class DeveloperToolsEditor : Editor 
{
	DeveloperTools dt = null;

	void OnEnable()
	{
		dt = target as DeveloperTools;
	}

	void OnDisable()
	{
		dt = null;
	}

	public override void OnInspectorGUI()
	{

	}
}
