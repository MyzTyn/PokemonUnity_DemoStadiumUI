using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*[ExecuteInEditMode]
public class StorageTabGroup : MonoBehaviour
{
	public List<StorageTabButton> tabButtons;
	public Color tabIdle;
	public Color tabActive;
	public Color tabHover;

	public void Subscribe(StorageTabButton tab)
	{
		if (tabButtons == null)
		{
			tabButtons = new List<StorageTabButton>();
		}

		tabButtons.Add(tab);
	}

	public void OnTabEnter(StorageTabButton tab)
	{
		ResetTabs();
		tab.background.color = tabHover;
	}

	public void OnTabExit(StorageTabButton tab)
	{
		ResetTabs();
	}

	public void OnTabSelected(StorageTabButton tab)
	{
		ResetTabs();
		tab.background.color = tabActive;
	}

	public void ResetTabs()
	{
		foreach (StorageTabButton tab in tabButtons)
		{
			tab.background.color = tabIdle;
		}
	}
}*/