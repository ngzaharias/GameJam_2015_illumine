using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameParameters))]
public class GameParametersAsset : Editor 
{
    GameParameters gp = null;

	void OnEnable()
	{
    	gp = target as GameParameters;
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
    [MenuItem("Assets/Create/Game Parameters")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<GameParameters>();
    }

    // Selecting the Asset
    [MenuItem("Assets/Game Parameters")]
    public static void SelectAsset()
    {
        Selection.activeObject = AssetDatabase.LoadAssetAtPath("Assets/Resources/GameParameters.asset", typeof(ScriptableObject));
    }
}
