using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonUnity.Stadium
{
	/// <summary>
	/// </summary>
	/// <remarks>
	/// https://www.youtube.com/watch?v=211t6r12XPQ
	/// </remarks>
	public class TabGroup : MonoBehaviour
	{
		/// <summary>
		/// Group of <see cref="GameObject"/> to cycle between when navigating <seealso cref="tabButtons"/>
		/// </summary>
		public List<GameObject> tabPages;
		public List<TabButton> tabButtons;
		public Color tabIdle;
		public Color tabActive;
		public Color tabHover;
		public TabButton selectedTab;

		public virtual void Subscribe(TabButton tab)
		{
			if (tabButtons == null)
			{
				tabButtons = new List<TabButton>();
			}

			tabButtons.Add(tab);
		}

		public virtual void OnTabEnter(TabButton tab)
		{
			if (!tab.selectable.interactable) return;
			ResetTabs();
			//if (selectedTab == null || tab != selectedTab)
			//{
			//	tab.background.color = tabHover;
			//}
		}

		public virtual void OnTabExit(TabButton tab)
		{
			if (!tab.selectable.interactable) return;
			ResetTabs();
		}

		public virtual void OnTabSelected(TabButton tab)
		{
			Core.Logger.Log($"TabGroup[{name}].OnTabSelect(tab[{tab.name}]);");
			if (!tab.selectable.interactable) return;
			if (selectedTab != null)
				selectedTab.Deselect();
			selectedTab = tab;
			selectedTab.Select();
			ResetTabs();
			//tab.background.color = tabActive;
			int index = tab.transform.GetSiblingIndex();
			for (int i = 0; i < tabPages.Count; i++)
				if (i == index)
					tabPages[i]?.SetActive(true);
				else
					tabPages[i]?.SetActive(false);
		}

		public virtual void ResetTabs()
		{
			foreach (TabButton tab in tabButtons)
			{
				if (selectedTab != null && selectedTab == tab) continue;
				//tab.background.color = tabIdle;
			}
		}
	}
}