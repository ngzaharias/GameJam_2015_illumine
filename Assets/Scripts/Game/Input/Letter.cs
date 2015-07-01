using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Text))]
public class Letter : MonoBehaviour 
{
	private Text m_text = null;
	public string Text { get { return m_text.text; } }

	void Awake()
	{
		m_text = GetComponent<Text>();
	}

	public void SetLetter(char letter)
	{
		letter = char.ToUpper(letter);
		m_text.text = letter.ToString();
	}
}
