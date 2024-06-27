using System;
using System.Linq;
using System.Collections.Generic;
using PokemonUnity;
using PokemonUnity.Monster;
using PokemonEssentials.Interface;
using PokemonEssentials.Interface.PokeBattle;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PokemonUnity.Stadium
{
	//[ExecuteInEditMode]
	[RequireComponent(typeof(Toggle))]
	public class TrainerPokemonButton : MonoBehaviour//, IEventSystemHandler, ISelectHandler, IDeselectHandler, ISubmitHandler//, IUpdateSelectedHandler
	{
		#region Variables
		public Image PokemonIcon;
		public Text Name;
		public Text Level;
		[SerializeField] private GameObject PokemonDisplay;
		//[SerializeField] private GameObject PartyUIButton;

		public Toggle toggle;
		public bool IsSelected;
		public bool IsRental;
		public int partyIndex;
		public PokemonSelect PokemonSelect;
		/// <summary>
		/// Where was this pokemon original from in menu selection? (box, rental, party...)
		/// </summary>
		/// <remarks>
		/// Key is Page/Tab | Value is Index
		/// </remarks>
		public KeyValuePair<int,int> Position;
		#endregion

		#region Methods
		//public void DisplayPartyButton()
		//{
		//	PokemonIcon.sprite = MainCameraGameManager.IconSprites[MainCameraGameManager.PkmnSelected];
		//	Name.text = Convert.ToString((Pokemons)MainCameraGameManager.PkmnSelected);
		//	Level.text = "L " + MainCameraGameManager.LevelFixed;
		//	ActivePokemonDisplay(true);
		//}
		//public void SetDisplay(string name, Pokemons pkmn, int level)
		//{
		//	PokemonIcon.sprite = MainCameraGameManager.IconSprites[(int)pkmn];
		//	//Name.text = pkmn.ToString(TextScripts.Name);
		//	Name.text = name;
		//	Level.text = "L " + level;
		//	ActivePokemonDisplay(true);
		//}
		/// <summary>
		/// Shows pokemon text and sprite display for this pokemon
		/// </summary>
		public void SetDisplay(IPokemon pkmn = null)
		{
			//IPokemon pkmn = Game.GameData.Trainer.party[partyIndex];
			//IPokemon pkmn = PokemonSelect.TemporaryParty[partyIndex];
			
			if (!pkmn.IsNotNullOrNone())
			{
				ActivePokemonDisplay(false);
				Clear();
				return;
			}

            PokemonIcon.sprite = MainCameraGameManager.IconSprites[(int)pkmn.Species];
            //Name.text = pkmn.ToString(TextScripts.Name);
            //Name.text = pkmn.Name;
            Name.text = pkmn.Species.ToString();
            Level.text = "L " + pkmn.Level;
            ActivePokemonDisplay(true);
        }
		public void ActivePokemonDisplay(bool active)
		{
			PokemonDisplay.SetActive(active);
		}
		public void ActivePartyUIButton(bool active)
		{
			//PartyUIButton.SetActive(active);
			gameObject.SetActive(active);
		}
		public void Clear()
		{
			PokemonIcon.sprite = null;
			Name.text = null;
			Level.text = null;
		}

		/// <summary>
		/// If player selects a pokemon, the party changes the bool of all pokemon buttons to active selection
		/// </summary>
		/// <param name="obj"></param>
		private void Scene_onSelectPartyEntry(int obj)
		{
			//PokemonSelect.CurrentSelectedPartySlot = obj;
			if(partyIndex == obj)
			{
				IsSelected = true;
			}
			else
			{
				IsSelected = false;
			}
		}
		#endregion

		#region Unity Monobehavior
		void Awake()
		{
			toggle = GetComponent<Toggle>();
			//GameEvents.current.onSelectPartyEntry += Scene_onSelectPartyEntry;
		}
		void OnDestroy()
		{
			try
			{
				//GameEvents.current.onSelectPartyEntry -= Scene_onSelectPartyEntry;
			}
			catch (NullReferenceException) { } //Ignore...
		}
		//public void OnSelect(BaseEventData eventData)
		//{
		//	//base.OnSelect(eventData);
		//	UnityEngine.Debug.Log("Selected");
		//	isFocused = true;
		//}
		//
		//public void OnDeselect(BaseEventData eventData)
		//{
		//	//base.OnDeselect(eventData);
		//	UnityEngine.Debug.Log("De-Selected");
		//	isFocused = false;
		//}
		//
		//public void OnSubmit(BaseEventData eventData)
		//{
		//	throw new NotImplementedException();
		//}
		#endregion
	}
}