using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
//[RequireComponent(typeof(TabGroup))]
public class StorageBoxesTabGroupPanel : TabGroup
{
	//public TabGroup pokemonStorageBoxesTabGroup;
	//public TabButton[] pokemonStorageBoxesTabButtons;
	public StorageBoxTabButton pokemonStorageBoxesTabButtonPrefab;
	//public GameObject selectedCursor;
	bool refreshInUpdate;

	#region Unity Monobehavior
	void Awake()
	{
		refreshInUpdate = true;
		//Clear child objects
		//var children = GetComponentsInChildren<Transform>(true);
		//foreach (var child in children) Destroy(child.gameObject);
		//selectedCursor = GetComponentsInChildren<Transform>(false)[1].gameObject;
		//selectedCursor.name = "SelectCursor";
		Destroy(GetComponentsInChildren<Transform>(false)[1].gameObject);

		//pokemonStorageBoxesTabGroup = GetComponent<StorageBoxesTabGroupPanel>() as TabGroup;
		//pokemonStorageBoxesTabButtons = new TabButton[13];
		//for (int i = 0; i < pokemonStorageBoxesTabButtons.Length; i++)
		for (int i = 0; i < 14; i++)
		{
			//Instantiate new Prefab to Scene
			var tabButton = Instantiate<StorageBoxTabButton>(pokemonStorageBoxesTabButtonPrefab, transform);
			tabButton.tabGroup = this;
			tabButton.selectable.enabled = true;
			tabButton.background.color = tabIdle;
			if (tabButton.selectable is Toggle t) //|| tabButton.selectable is Button
			{
				t.isOn = false;
				t.group = GetComponent<ToggleGroup>();
				t.targetGraphic.color = tabActive;
				//tabButton.GetComponent<Toggle>().colors = new ColorBlock()
				//{
				//	normalColor = tabActive,
				//	highlightedColor = tabHover,
				//	pressedColor = tabActive,
				//	disabledColor = tabIdle
				//};
			}
			//Change UI Text on Screen
			if (i == 0)			//Current
			{
				tabButton.name = "tabC";
				tabButton.label.text = "C";
				tabButton.selectable.interactable = false;
				//Assign Custom Color
				//tabButton.background.color = new Color32(171, 57, 56, 255); //disabled
				//tabButton.background.color = new Color32(214, 57, 74, 255); //enabled
				if (tabButton.selectable is Toggle t0) //|| tabButton.selectable is Button
					t0.targetGraphic.color = new Color32(214, 57, 74, 255); //enabled

			}
			else if (i == 13)	//Rental
			{
				Debug.Log($"[LOG]: TabButton[{tabButton.name}].Text = R");
				tabButton.name = "tabR";
				tabButton.label.text = "R";
				//Assign Custom Color
				//tabButton.background.color = new Color32(201, 67, 182, 255);
				if (tabButton.selectable is Toggle t0) //|| tabButton.selectable is Button
				{
					t0.targetGraphic.color = new Color32(201, 67, 182, 255);
					//Toggle t1 = tabButton.GetComponent<Toggle>();
					//Debug.Log($"[LOG]: TabButton[{tabButton.name}].Toggle.Colors = " + t0.colors.normalColor);
					//t1.colors = new ColorBlock()
					//{
					//	normalColor = new Color32(201, 67, 182, 255),
					//	highlightedColor = new Color32(201, 67, 182, 255),
					//	pressedColor = new Color32(201, 67, 182, 255),
					//	disabledColor = new Color32(164, 59, 144, 255)
					//};
					//Debug.Log($"[LOG]: TabButton[{tabButton.name}].Toggle.Colors = " + t0.colors.normalColor);
				}
				//tabButton.tabTextActiveOverride = new Color32(255, 203, 249, 255);
				//tabButton.tabActiveOverride = new Color32(205, 62, 186, 255);
				selectedTab = tabButton;
			}
			else if (i == 1)	//First Player Box
			{
				tabButton.name = "tab1";
				tabButton.label.text = "GB"+i;
				tabButton.selectable.interactable = false;
			}
			else				//Player Game Boxes
			{
				tabButton.name = "tab"+i;
				tabButton.label.text = i.ToString();
				tabButton.selectable.interactable = false;
			}
			//tabButton.Select(new UnityEngine.Events.UnityAction(() =>
			//{
			//	Debug.Log($"[LOG]: TabButton[{tabButton.name}].Select(UnityAction).Invoking...");
			//	var rect1 = selectedCursor.transform.GetComponent<RectTransform>();
			//	var rect2 = tabButton.transform.GetComponent<RectTransform>();
			//	Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Rect.Height = {rect1.rect.height};");
			//	rect1.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect2.rect.height);
			//	Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Rect.Height = {rect1.rect.height};");
			//	Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Rect.Width = {rect1.rect.width};");
			//	rect1.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect2.rect.width);
			//	Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Rect.Width = {rect1.rect.width};");
			//	Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Position = ({selectedCursor.transform.position.x},{selectedCursor.transform.position.y});");
			//	selectedCursor.transform.position = tabButton.transform.position;
			//	Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Position = ({selectedCursor.transform.position.x},{selectedCursor.transform.position.y});");
			//	//LeanTween.move(selectedCursor, rect2, 1f);
			//}));
			//Add Prefab to pre-established Tab logic
			//pokemonStorageBoxesTabGroup.Subscribe(pokemonStorageBoxesTabButtons[i]);
			//pokemonStorageBoxesTabGroup.Subscribe(tabButton);
			Subscribe(tabButton);
			//Canvas.ForceUpdateCanvases();
		}
		ResetTabs();
		//Debug.Log("Tab Count: " + tabButtons.Count);
		//Canvas.ForceUpdateCanvases();
		//Debug.Log($"[LOG]: TabButton[{tabButtons[13].name}].Rect.Height = {tabButtons[13].transform.GetComponent<RectTransform>().rect.height};");
		OnTabSelected(selectedTab); //OnTabSelected(tabButtons[13]);
		//tabButtons[13].OnPointerClick(new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current));
		//UnityEngine.EventSystems.ExecuteEvents.Execute(tabButtons[13].gameObject, 
		//	new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current), 
		//	UnityEngine.EventSystems.ExecuteEvents.pointerClickHandler);
		//refreshInUpdate = true;
		//Canvas.ForceUpdateCanvases();
	}

	void Start()
	{
	}

	void OnDestroy()
	{
	}

	//void Update()
	//{
	//	if (refreshInUpdate && selectedTab != null)
	//	{
	//		Debug.LogWarning($"[WARN]: Update");
	//		Debug.Log($"[LOG]: TabGroup[{name}].MoveSelectorToCurrentItem([{selectedTab.name}])");
	//		MoveSelectorToCurrentItem(selectedTab);
	//		refreshInUpdate=false;
	//	}
	//}

	//void FixedUpdate()
	//{
	//	if (refreshInUpdate && selectedTab != null)
	//	{
	//		Debug.LogWarning($"[WARN]: FixedUpdate");
	//		Debug.Log($"[LOG]: TabGroup[{name}].MoveSelectorToCurrentItem([{selectedTab.name}])");
	//		MoveSelectorToCurrentItem(selectedTab);
	//		refreshInUpdate=false;
	//	}
	//}

	//void LateUpdate()
	//{
	//	if (refreshInUpdate && selectedTab != null)
	//	{
	//		Canvas.ForceUpdateCanvases();
	//		Debug.LogWarning($"[WARN]: LateUpdate");
	//		Debug.Log($"[LOG]: TabGroup[{name}].MoveSelectorToCurrentItem([{selectedTab.name}])");
	//		//MoveSelectorToCurrentItem(selectedTab);
	//		refreshInUpdate=false;
	//	}
	//}
	#endregion

	public override void OnTabSelected(TabButton tab)
	{
		Debug.Log($"[LOG]: TabGroup[{name}].OnTabSelect(tab[{tab.name}]);");
		if (!tab.selectable.interactable) return;
		if (selectedTab != null)
			selectedTab.Deselect();
		selectedTab = tab;
		selectedTab.Select();
		ResetTabs();
		//tab.background.color = tabActive;
		(tab as StorageBoxTabButton).label.color = new Color(1f,1f,1f,1f);
		int index = tab.transform.GetSiblingIndex();
		for (int i = 0; i < tabPages.Count; i++)
			if (i == index)
			{
				tabPages[i]?.SetActive(true);
				//SetPageContentByTabIndex(tabIndex);
			}
			else
				tabPages[i]?.SetActive(false);
	}

	public override void ResetTabs()
	{
		foreach (StorageBoxTabButton tab in tabButtons)
		{
			if (selectedTab != null && selectedTab == tab) continue;
			//tab.Deselect();
			//tab.background.color = tabIdle;
			tab.label.color = new Color(1f,1f,1f,.5f);
		}
	}

	//public void SetPokemonRental()
	//public void SetPageContentByTabIndex(int tabIndex)
	//{
	//	int i = 0;
	//	//foreach (int id in ID)
	//	//for (int id = 1; id <= 151; id++)
	//	foreach (int id in
	//		Game.PokemonData
	//		.Values
	//		.Where(x => x.ID != Pokemons.NONE &&
	//			(int)x.ID <= 1000 &&
	//			x.GenerationId <= (int)Generation.RedBlueYellow)
	//		.Select(y => (int)y.ID))
	//	{
	//		int? page = PokemonSelect.CurrentSelectedRosterPage;
	//		bool isSelected = SelectedPokemons.Contains(
	//			new KeyValuePair<KeyValuePair<bool, int?>, int>(
	//				//new KeyValuePair<bool, int?>(true, null), i));
	//				new KeyValuePair<bool, int?>(true, page), i));
	//
	//		GameObject Button = Instantiate(rosterEntryPrefab);
	//		SelectPokemonButton roster = Button.GetComponent<SelectPokemonButton>();
	//		roster.PokemonSelect = PokemonSelect; //Should be duplicated for each player controller on screen
	//											  //RentalData(id, roster);
	//		StoreButtonData.Add(id, roster);
	//		//Button.GetComponent<SelectPokemonButton>().SetID(id);
	//		roster.SetID(i, (Pokemons)id, page: page, selected: isSelected); i++;
	//		Button.SetActive(true);
	//		Button.transform.SetParent(rosterGridContent, false);
	//	}
	//	Debug.Log("Highest Species Counted: " + i);
	//	rosterGridContent.gameObject.SetActive(true);
	//}

	//public void MoveSelectorToCurrentItem(TabButton tabButton)
	//{
	//	Debug.Log($"[LOG]: TabGroup[{name}].MoveSelectorToCurrentItem([{tabButton.name}]).Invoking...");
	//	//tabButton.Select(new UnityEngine.Events.UnityAction(() =>
	//	//{
	//		Debug.Log($"[LOG]: TabButton[{tabButton.name}].Select(UnityAction).Invoking...");
	//		var rect1 = selectedCursor.transform.GetComponent<RectTransform>();
	//		var rect2 = tabButton.transform.GetComponent<RectTransform>();
	//		Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Rect.Height = {rect1.rect.height};");
	//		rect1.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect2.rect.height);
	//		Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Rect.Height = {rect1.rect.height};");
	//		Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Rect.Width = {rect1.rect.width};");
	//		rect1.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect2.rect.width);
	//		Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Rect.Width = {rect1.rect.width};");
	//		Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Position = ({selectedCursor.transform.position.x},{selectedCursor.transform.position.y});");
	//		selectedCursor.transform.position = tabButton.transform.position;
	//		Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Position = ({selectedCursor.transform.position.x},{selectedCursor.transform.position.y});");
	//		//LeanTween.move(selectedCursor, rect2, 1f);
	//	//}));
	//}
}