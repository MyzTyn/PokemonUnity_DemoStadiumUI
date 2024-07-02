using System;
using System.Collections;
using System.Collections.Generic;
using PokemonUnity;
using PokemonUnity.Monster;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonUnity.Stadium
{
	[ExecuteInEditMode]
	public class PageTabButton : MonoBehaviour
	{
		public int? Id;
		[SerializeField] private bool isRental;
		public bool IsRental { get { return isRental; } set { isRental = value; } }
		public bool IsSelected { get { return toggle.isOn; } set { toggle.isOn = value; } }
		public bool IsDisabled { get { return toggle.interactable; } set { toggle.interactable = value; } }
		public string Text
		{
			get { return string.Format("{0}{1}", //C = Current PArty, GB = Game Box, R = Rental
				IsRental ? "R" : (!Id.HasValue ? "C" : (Id.Value == 0 ? "GB" : "")),
				!Id.HasValue ? (IsRental ? "0" : "") : Id.Value.ToString()); }
		}
		public KeyValuePair<bool, int?> Page
		{
			get { return new KeyValuePair<bool, int?>(IsRental, Id); }
			set { IsRental = value.Key; Id = value.Value; }
		}
		[SerializeField] private Toggle toggle;
		//[SerializeField] private int StorageBox;
		//[SerializeField] private PokemonUnity.Generation Generation;
		//[SerializeField] private Image ColorBG;
		[SerializeField] private Text Name;
		public PokemonSelect PokemonSelect; //ToDo: Remove this, and use MainCamera as Singleton?

		public void SetID(int? id, bool isRental = true, bool selected = false)
		{
			Id = id;
			IsRental = isRental;
			IsSelected = selected;
			Name.text = Text;
		}
		//public void DisableOnClick(bool active)
		//{
		//	if (active)
		//	{
		//		button.onClick.RemoveAllListeners();
		//	}
		//	else
		//	{
		//		button.onClick.RemoveAllListeners();
		//		button.onClick.AddListener(delegate { demo.PokemonStatsandMoveUI(ID); });
		//	}
		//}
		//public void PokemonStatsandMoveUI(int Pkmn_ID)
		//{
		//	PkmnSelected = Pkmn_ID;
		//	Debug.Log("Pressed! " + Pkmn_ID);
		//	if (!StorePokemon.ContainsKey((Pokemons)Pkmn_ID))
		//	{
		//		StorePokemon.Add((Pokemons)PkmnSelected, new Pokemon((Pokemons)PkmnSelected, PokemonSelect.LevelFixed, false));
		//		Debug.Log("Create Pokemon!");
		//		DisplayMoveSetUI();
		//	}
		//	else
		//	{
		//		Debug.Log("It already created!");
		//		DisplayMoveSetUI();
		//	}
		//}
		public void Scene_onButtonSelected()
		{
			Debug.Log("Change loaded roster to: " + Text);
			PokemonSelect.CurrentSelectedRosterPage = Id;
			PokemonSelect.IsRentalPokemon = IsRental;
			PokemonSelect.EditPokemon = false;
			//GameEvents.current.OnLoadLevel(1); //Change scene...
			//Refresh UI from Manager
		}
		private void Awake()
		{
			//button.onClick.AddListener(delegate { demo.PokemonStatsandMoveUI(ID); });
			//new WaitForSeconds(1);
		}
		private void Start()
		{
			Refresh();
		}
		private void OnDestory()
		{
		}
		public void Refresh()
		{
			if (IsRental)
			{
				//Change color of text
				//Change color of background
				//Change color of disabled
			}
			else
			{
				//Load data from player saved games and use as pokemons to select from
			}
		}
	}
}