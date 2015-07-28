using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SoundDatabase))]
public class SoundDatabaseEditor : Editor
{
	SoundDatabase gp = null;

	void OnEnable()
	{
		gp = target as SoundDatabase;
	}

	void OnDisable()
	{
		gp = null;
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		//gp.GameName = EditorGUILayout.TextField("Name:", gp.GameName);
		//gp.Version = EditorGUILayout.TextField("Version:", gp.Version);
	}

	// Creation of the Asset
	[MenuItem("Assets/Create/Sound Database")]
	public static void CreateAsset()
	{
		ScriptableObjectUtility.CreateAsset<SoundDatabase>();
	}

	// Selecting the Asset
	[MenuItem("Assets/Sound Database")]
	public static void SelectAsset()
	{
		Selection.activeObject = AssetDatabase.LoadAssetAtPath("Assets/Resources/SoundDatabase.asset", typeof(ScriptableObject));
	}
}
