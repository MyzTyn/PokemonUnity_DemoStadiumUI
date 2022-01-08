using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// </summary>
/// <remarks>
/// https://www.youtube.com/watch?v=211t6r12XPQ
/// </remarks>
[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	public TabGroup tabGroup;
	public bool disabled;
	public Image background;
	public Color? tabIdleOverride;
	public Color? tabActiveOverride;
	public Color? tabHoverOverride;
	public Color? tabTextIdleOverride;
	public Color? tabTextActiveOverride;
	public Color? tabTextHoverOverride;
	public UnityEvent onTabSelected;
	public UnityEvent onTabDeselected;

	public virtual void Select() 
	{
		Debug.Log($"[LOG]: TabButton[{name}].Select();");
		if (onTabSelected != null)
		{
			Debug.Log($"[LOG]: TabButton[{name}].onTabSelected.Invoke();");
			onTabSelected.Invoke();
		}
	}
	public virtual void Deselect() 
	{
		if (onTabDeselected != null) onTabDeselected.Invoke();
	}
	public virtual void Select(UnityAction action) 
	{
		Debug.Log($"[LOG]: TabButton[{name}].Select(UnityAction);");
		if (action != null)
		{
			Debug.Log($"[LOG]: TabButton[{name}].onTabSelected.AddListener(UnityAction);");
			onTabSelected.AddListener(action);
		}
	}
	public virtual void Deselect(UnityAction action) 
	{
		if (action != null) onTabDeselected.AddListener(action);
	}

	private void Start()
	{
		background = GetComponent<Image>();
		tabGroup?.Subscribe(this);
	}

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		tabGroup?.OnTabEnter(this);
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		tabGroup?.OnTabExit(this);
	}

	public virtual void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log($"[LOG]: TabButton[{name}].OnPointerClick(PointerEventData);");
		tabGroup?.OnTabSelected(this);
	}
}