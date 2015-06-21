using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class LevelDatabaseEditor : EditorWindow
{
	static private LevelDatabase target = null;
	static private LevelDatabaseEditor window = null;

    // Creation of the Asset
	[MenuItem("Assets/Create/Level Database")]
    public static void CreateAsset()
    {
		ScriptableObjectUtility.CreateAsset<LevelDatabase>();
    }

    // Selecting the Asset
	[MenuItem("Window/Level Database")]
	static void ShowWindow()
    {
		window = (LevelDatabaseEditor)EditorWindow.GetWindow(typeof(LevelDatabaseEditor), true, "Level Database");
		target = (LevelDatabase)AssetDatabase.LoadAssetAtPath("Assets/Resources/LevelDatabase.asset", typeof(ScriptableObject));

		window.minSize = new Vector2(512, 256);
	}

	Vector2 sectionScroll;
	void OnGUI()
	{
		EditorGUILayout.BeginVertical();
		{
			//	Add Section
			if (GUILayout.Button("Add Section"))
			{
				target.AddSection(new SectionData());
			}

			sectionScroll = EditorGUILayout.BeginScrollView(sectionScroll);
			{
				//	Sections
				List<SectionData> sections = target.m_sections;
				for (int i = 0; i < sections.Count; ++i)
				{
					DisplaySection(i, sections[i]);
					GUILayout.Space(8);
				}
			}
			EditorGUILayout.EndScrollView();
		}
		EditorGUILayout.EndVertical();

		EditorUtility.SetDirty(target);
	}

	bool[] sectionFoldout = new bool[256];
	void DisplaySection(int index, SectionData section)
	{
		EditorGUILayout.BeginVertical();
		{
			EditorGUILayout.BeginHorizontal();
			{
				//	Move Section
				EditorGUILayout.BeginVertical();
				if (GUILayout.Button("+") && index != 0)
				{
					target.MoveSection(section, -1);
				}
				if (GUILayout.Button("-") && index != target.m_sections.Count - 1)
				{
					target.MoveSection(section, 1);
				}
				EditorGUILayout.EndVertical();

				//	Details
				sectionFoldout[index] = EditorGUILayout.Foldout(sectionFoldout[index], "");
				GUILayout.Label("Key: ");
				section.key = EditorGUILayout.TextField(section.key);
				if (GUILayout.Button("Add Level"))
				{
					section.levels.Add(new LevelData());
				}
				if (GUILayout.Button("Remove Level"))
				{
					if (section.levels.Count > 0)
					{
						section.levels.RemoveAt(section.levels.Count - 1);
					}
				}

				GUILayout.FlexibleSpace();
				if (GUILayout.Button("Remove Section"))
				{
					target.RemoveSection(section);
				}
			}
			EditorGUILayout.EndHorizontal();

			//	Levels
			if (sectionFoldout[index])
			{
				levelScroll[index] = EditorGUILayout.BeginScrollView(levelScroll[index]);
				{
					EditorGUILayout.BeginHorizontal();
					{
						for (int i = 0; i < section.levels.Count; ++i)
						{
							DisplayLevel(i, section.levels[i]);
							GUILayout.Space(8);
						}
					}
					EditorGUILayout.EndHorizontal();
				}
				EditorGUILayout.EndScrollView();
			}
		}
		EditorGUILayout.EndVertical();
		EditorGUILayout.Separator();
	}

	Vector2[] levelScroll = new Vector2[256];
	void DisplayLevel(int index, LevelData level)
	{
		EditorGUILayout.BeginVertical();

		GUILayout.Label("Level " + (index+1).ToString() + ": ");

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Key: ");
		level.key = EditorGUILayout.TextField(level.key);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Model: ");
		GUILayoutOption[] options = { GUILayout.MinWidth(128) };//, GUILayout.MinHeight(32) };
		level.model = (GameObject)EditorGUILayout.ObjectField(level.model, typeof(GameObject), false, options);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Answer: ");
		level.answer = EditorGUILayout.TextField(level.answer);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.EndVertical();
	}
}
