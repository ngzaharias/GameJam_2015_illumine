using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class UIDroppable : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	private Image m_image = null;

	private Color normalColor;
	public Color highlightColor = Color.yellow;

	void Awake()
	{
		m_image = GetComponent<Image>();
	}

	void Start()
	{
		normalColor = m_image.color;
	}

	public void OnDrop(PointerEventData data)
	{
		m_image.color = normalColor;
	}

	public void OnPointerEnter(PointerEventData data)
	{
		Sprite dropSprite = GetDropSprite(data);
		if (dropSprite != null)
			m_image.color = highlightColor;
	}

	public void OnPointerExit(PointerEventData data)
	{
		m_image.color = normalColor;
	}

	private Sprite GetDropSprite(PointerEventData data)
	{
		var originalObj = data.pointerDrag;
		if (originalObj == null)
			return null;

		var srcImage = originalObj.GetComponent<Image>();
		if (srcImage == null)
			return null;

		return srcImage.sprite;
	}
}
