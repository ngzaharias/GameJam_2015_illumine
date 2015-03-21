using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIComponentColours : MonoBehaviour 
{
    protected Button[] m_buttons = null;
    protected Text[] m_texts = null;
	protected InputField[] m_inputFields = null;
	protected Image[] m_images = null;

    void Awake()
    {
        m_buttons = GetComponentsInChildren<Button>();
		m_texts = GetComponentsInChildren<Text>();
		m_inputFields = GetComponentsInChildren<InputField>();
		m_images = GetComponentsInChildren<Image>();
    }

    void Start()
    {
		UpdateUIColours(GameParameters.Instance.m_UIColours);
    }

	void UpdateUIColours(UIColours colours)
    {
        for (int i = 0; i < m_buttons.Length; ++i)
        {
            ColorBlock colourBlock = m_buttons[i].colors;

            colourBlock.normalColor         = colours.button_Normal;
            colourBlock.highlightedColor    = colours.button_Highlighted;
            colourBlock.pressedColor        = colours.button_Pressed;
            colourBlock.disabledColor       = colours.button_Disabled;

            m_buttons[i].colors = colourBlock;
        }

        for (int i = 0; i < m_texts.Length; ++i)
        {
            m_texts[i].color = colours.text;
        }

		for (int i = 0; i < m_inputFields.Length; ++i)
		{
			ColorBlock colourBlock = m_inputFields[i].colors;

			colourBlock.normalColor = colours.button_Normal;
			colourBlock.highlightedColor = colours.button_Highlighted;
			colourBlock.pressedColor = colours.button_Pressed;
			colourBlock.disabledColor = colours.button_Disabled;

			m_inputFields[i].colors = colourBlock;
			m_inputFields[i].selectionColor = colours.button_Normal;
		}

		for (int i = 0; i < m_images.Length; ++i)
		{
			if (m_images[i].GetComponent<Button>() == null &&
				m_images[i].GetComponent<InputField>() == null)
			{
				m_images[i].color = colours.image;
			}
		}
    }
}
