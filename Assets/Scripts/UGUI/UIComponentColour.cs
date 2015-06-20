using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIComponentColour : MonoBehaviour 
{
	private Button m_button;
	private Image m_image;
	private InputField m_inputField;
	private Text m_text;

	void Awake()
	{
		m_button = GetComponent<Button>();
		m_inputField = GetComponent<InputField>();
		m_image = GetComponent<Image>();
		m_text = GetComponent<Text>();

		if (m_inputField != null)
		{
			UpdateColoursInputField();
		}
		else if (m_button != null)
		{
			UpdateColoursButton();
		}
		else if (m_text != null)
		{
			UpdateColoursText();
		}
		else if (m_image != null)
		{
			UpdateColoursImage();
		}
	}

	private void UpdateColoursButton()
	{
		UIColours colours = GameParameters.Instance.m_UIColours;
		ColorBlock colourBlock = m_button.colors;

		colourBlock.normalColor = colours.button_Normal;
		colourBlock.highlightedColor = colours.button_Highlighted;
		colourBlock.pressedColor = colours.button_Pressed;
		colourBlock.disabledColor = colours.button_Disabled;

		m_button.colors = colourBlock;
	}

	private void UpdateColoursImage()
	{
		UIColours colours = GameParameters.Instance.m_UIColours;
		m_image.color = new Color(colours.image.r, colours.image.g, colours.image.b, m_image.color.a);
	}

	private void UpdateColoursInputField()
	{
		UIColours colours = GameParameters.Instance.m_UIColours;
		ColorBlock colourBlock = m_inputField.colors;

		colourBlock.normalColor = colours.button_Normal;
		colourBlock.highlightedColor = colours.button_Highlighted;
		colourBlock.pressedColor = colours.button_Pressed;
		colourBlock.disabledColor = colours.button_Disabled;

		m_inputField.colors = colourBlock;
		m_inputField.selectionColor = colours.button_Normal;
	}

	private void UpdateColoursText()
	{
		UIColours colours = GameParameters.Instance.m_UIColours;
		m_text.color = new Color(colours.text.r, colours.text.g, colours.text.b, m_text.color.a);
	}
}
