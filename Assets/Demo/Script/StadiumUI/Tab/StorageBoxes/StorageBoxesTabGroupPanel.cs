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
	public GameObject selectedCursor;
	bool refreshInUpdate;

	#region Unity Monobehavior
	void Awake()
	{
		refreshInUpdate = true;
		//Clear child objects
		//var children = GetComponentsInChildren<Transform>(true);
		//foreach (var child in children) Destroy(child.gameObject);
		selectedCursor = GetComponentsInChildren<Transform>(false)[1].gameObject;
		selectedCursor.name = "SelectCursor";

		//pokemonStorageBoxesTabGroup = GetComponent<StorageBoxesTabGroupPanel>() as TabGroup;
		//pokemonStorageBoxesTabButtons = new TabButton[13];
		//for (int i = 0; i < pokemonStorageBoxesTabButtons.Length; i++)
		for (int i = 0; i < 14; i++)
		{
			//Instantiate new Prefab to Scene
			var tabButton = Instantiate<StorageBoxTabButton>(pokemonStorageBoxesTabButtonPrefab, transform);
			tabButton.tabGroup = this;
			//Change UI Text on Screen
			if (i == 0)			//Current
			{
				tabButton.name = "tabC";
				tabButton.label.text = "C";
				//Assign Custom Color
				//tabButton.tabIdleOverride = 
				tabButton.disabled = true;

			}
			else if (i == 13)	//Rental
			{
				Debug.Log($"[LOG]: TabButton[{tabButton.name}].Text = R");
				tabButton.name = "tabR";
				tabButton.label.text = "R";
				//Assign Custom Color
				//tabButton.tabTextActiveOverride = new Color(255, 203, 249, 255);
				//tabButton.tabActiveOverride = new Color(205, 62, 186, 255);
				//tabButton.tabIdleOverride = 
				selectedTab = tabButtons[13];
			}
			else if (i == 1)	//First Player Box
			{
				tabButton.name = "tab1";
				tabButton.label.text = "GB"+i;
				tabButton.disabled = true;
			}
			else				//Player Game Boxes
			{
				tabButton.name = "tab"+i;
				tabButton.label.text = i.ToString();
				tabButton.disabled = true;
			}
			tabButton.Select(new UnityEngine.Events.UnityAction(() =>
			{
				Debug.Log($"[LOG]: TabButton[{tabButton.name}].Select(UnityAction).Invoking...");
				var rect1 = selectedCursor.transform.GetComponent<RectTransform>();
				var rect2 = tabButton.transform.GetComponent<RectTransform>();
				Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Rect.Height = {rect1.rect.height};");
				rect1.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect2.rect.height);
				Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Rect.Height = {rect1.rect.height};");
				Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Rect.Width = {rect1.rect.width};");
				rect1.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect2.rect.width);
				Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Rect.Width = {rect1.rect.width};");
				Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Position = ({selectedCursor.transform.position.x},{selectedCursor.transform.position.y});");
				selectedCursor.transform.position = tabButton.transform.position;
				Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Position = ({selectedCursor.transform.position.x},{selectedCursor.transform.position.y});");
				//LeanTween.move(selectedCursor, rect2, 1f);
			}));
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
		//OnTabSelected(tabButtons[13]);
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

	void LateUpdate()
	{
		if (refreshInUpdate && selectedTab != null)
		{
			Canvas.ForceUpdateCanvases();
			Debug.LogWarning($"[WARN]: LateUpdate");
			Debug.Log($"[LOG]: TabGroup[{name}].MoveSelectorToCurrentItem([{selectedTab.name}])");
			MoveSelectorToCurrentItem(selectedTab);
			refreshInUpdate=false;
		}
	}
	#endregion

	public void MoveSelectorToCurrentItem(TabButton tabButton)
	{
		Debug.Log($"[LOG]: TabGroup[{name}].MoveSelectorToCurrentItem([{tabButton.name}]).Invoking...");
		//tabButton.Select(new UnityEngine.Events.UnityAction(() =>
		//{
			Debug.Log($"[LOG]: TabButton[{tabButton.name}].Select(UnityAction).Invoking...");
			var rect1 = selectedCursor.transform.GetComponent<RectTransform>();
			var rect2 = tabButton.transform.GetComponent<RectTransform>();
			Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Rect.Height = {rect1.rect.height};");
			rect1.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect2.rect.height);
			Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Rect.Height = {rect1.rect.height};");
			Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Rect.Width = {rect1.rect.width};");
			rect1.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect2.rect.width);
			Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Rect.Width = {rect1.rect.width};");
			Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Position = ({selectedCursor.transform.position.x},{selectedCursor.transform.position.y});");
			selectedCursor.transform.position = tabButton.transform.position;
			Debug.Log($"[LOG]: TabButton[{tabButton.name}].SelectedCursor.Position = ({selectedCursor.transform.position.x},{selectedCursor.transform.position.y});");
			//LeanTween.move(selectedCursor, rect2, 1f);
		//}));
	}
}