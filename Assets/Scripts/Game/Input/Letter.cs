using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Text))]
public class Letter : MonoBehaviour 
{
	private Text m_text = null;

	void Awake()
	{
		m_text = GetComponent<Text>();
	}

	void Update () 
	{
		if (m_text.text.Length > 1)
		{
			m_text.text = m_text.text[0].ToString();
		}
	}
}
