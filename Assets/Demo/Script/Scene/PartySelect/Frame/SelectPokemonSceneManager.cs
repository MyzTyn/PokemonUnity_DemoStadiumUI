using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using PokemonUnity;
using PokemonUnity.Character;
using PokemonUnity.Monster;
using PokemonEssentials.Interface;
using PokemonEssentials.Interface.PokeBattle;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonUnity.Stadium
{
	//[ExecuteInEditMode]
	public class SelectPokemonSceneManager : MonoBehaviour
	{
		#region Variables
		public static PokemonViewModal SelectedPokemonViewModal;
		public PokemonSelect SelectionState;

		[SerializeField] private TrainerPartyPanel partySelectionUI;
		[SerializeField] private GameObject rosterEntryPrefab;
		[SerializeField] private GameObject partyEntryPrefab;
		[SerializeField] private Transform partyGridContent;
		[SerializeField] private Transform rosterGridContent;
		[SerializeField] private ToggleGroup toggleGroup;

		//List
		private Dictionary<int, TrainerPokemonButton> PartyViewer;
		private Dictionary<int, SelectPokemonButton> StoreButtonData;
		/// <summary>
		/// When pokemons are selected by player from UI, store in this variable
		/// </summary>
		/// <remarks>
		/// First-In, Last-Out; Player can deselect their choice by removing their last choice
		/// </remarks>
		private Stack<IPokemon> TemporaryParty;
		//Sprite
		public static Sprite[] PkmnType { get; private set; }
		public static Sprite[] IconSprites { get; private set; }

		//public Toggle Toggle { get { return toggleGroup.ActiveToggles().FirstOrDefault(); } }
		//public int CurrentOnParty
		//{
		//	get
		//	{
		//		//return Game.GameData.Trainer.party.
		//		//TrainerPokemonButton party = PartyViewer
		//		//	.Values
		//		//	.SingleOrDefault(x => x.IsSelected);
		//		//	//.Select(x => x.partyIndex);
		//		//return party != null ? party.partyIndex : 0;
		//		return PokemonSelect.CurrentSelectedPartySlot;
		//	}
		//}
		#endregion

		#region Unity Monobehavior
		void Awake()
		{
			Debug.Log("Is Scriptable Object Null? " + (SelectionState == null).ToString());
			toggleGroup = GetComponent<ToggleGroup>();
			Debug.Log("Create Dictionary for Player Party UI Mono");
			PartyViewer = new Dictionary<int, TrainerPokemonButton>();
			Debug.Log("Create Dictionary for Roster Entry UI Mono");
			StoreButtonData = new Dictionary<int, SelectPokemonButton>();
			//Debug.Log("Create Dictionary for Temp Instantiated Pokemon Objects");
			//StorePokemon = new Dictionary<Pokemons, Pokemon>();
			//Debug.Log("Create Dictionary for Temp Viewed Pokemons");
			//ViewedRentalPokemon = new Queue<Pokemons>();
			////Debug.Log("Create Dictionary for Pokemons Selected by Player");
			////SelectedPokemons = new Dictionary<KeyValuePair<bool, int?>, int>();
			//Debug.Log("Create LookUp Table for Pokemons Selected by Player");
			//SelectedPokemons = new HashSet<KeyValuePair<KeyValuePair<bool, int?>, int>>();
			//
			Debug.Log("Load Assets for UI into Array");
			IconSprites = Resources.LoadAll<Sprite>("PokemonIcon");
			PkmnType = Resources.LoadAll<Sprite>("PokemonType");
		}

		void Start()
		{
			Debug.Log("Is Game Events Null? " + (GameEvents.current == null).ToString());
			GameEvents.current.onChangePartyLineup += Scene_onChangePartyLineup;

			//RentalControlUI.DisplayPokemonSelectUI();
			SetPokemonRental();
			//party.DisplayPartyUI();
			//party.GetPartyButton();
			SetPartyButton();
			Debug.Log("Trainer Id: " + Game.GameData.Trainer.publicID().ToString());
			//Use ID but I will leave 00000 as Example
			partySelectionUI.SetTrainerID(Game.GameData.Trainer.publicID());
		}
		void OnDestroy()
		{
			try
			{
				GameEvents.current.onChangePartyLineup -= Scene_onChangePartyLineup;
			}
			catch (NullReferenceException) { Debug.LogError("Calling destroy before event wakes up"); } //Ignore...
		}
		#endregion

		#region Methods
		//public void SelectToggle(int id)
		//{
		//	Toggle[] toggles = toggleGroup.GetComponentsInChildren<Toggle>();
		//	toggles[id].isOn = true;
		//}
		#endregion

		#region Party Roster UI
		public void SetPartyButton()
		{
			for (int Id = 0; Id < ((Game)Game.GameData).Features.LimitPokemonPartySize && Id < Core.MAXPARTYSIZE; Id++)
			{
				//if (Id == Core.MAXPARTYSIZE) break;
				GameObject Button = Instantiate(partyEntryPrefab);
				TrainerPokemonButton slot = Button.GetComponent<TrainerPokemonButton>();
				slot.PokemonSelect = SelectionState; //Should be duplicated for each player controller on screen
				//PartyData(Id, slot);
				PartyViewer.Add(Id, slot);
				//Button.GetComponent<TrainerPokemonButton>().ActivePartyUIButton(true);
				//Button.GetComponent<TrainerPokemonButton>().ActivePokemonDisplay(false);
				slot.ActivePartyUIButton(true);
				slot.ActivePokemonDisplay(false);
				//Button.transform.SetParent(partyEntryPrefab.transform.parent, false);
				Button.transform.SetParent(partyGridContent, false);
			}
		}
		private void SetPokemonRental()
		{
			int i = 0;
			//foreach (int id in ID)
			//for (int id = 1; id <= 151; id++)
			foreach (int id in
				Kernal.PokemonData
				.Values
				.Where(x => x.ID != Pokemons.NONE &&
					(int)x.ID <= 1000 &&
					//x.GenerationId <= (int)Generation.RedBlueYellow)
					SelectionState.PokemonGens.Contains((Generation)x.GenerationId))
				.Select(y => (int)y.ID))
			{
				int? page = SelectionState.CurrentSelectedRosterPage;
				bool isSelected = SelectionState.SelectedPokemons.Contains(
					new KeyValuePair<KeyValuePair<bool, int?>, int>(
						//new KeyValuePair<bool, int?>(true, null), i));
						new KeyValuePair<bool, int?>(true, page), i));

				GameObject Button = Instantiate(rosterEntryPrefab);
				SelectPokemonButton roster = Button.GetComponent<SelectPokemonButton>();
				roster.PokemonSelect = SelectionState; //Should be duplicated for each player controller on screen
				//RentalData(id, roster);
				StoreButtonData.Add(id, roster);
				//Button.GetComponent<SelectPokemonButton>().SetID(id);
				roster.SetID(SelectedPokemonViewModal,SelectionState,i,(Pokemons)id,page:page,selected:isSelected); i++;
				Button.SetActive(true);
				Button.transform.SetParent(rosterGridContent, false);
			} Debug.Log("Highest Species Counted: " + i);
			rosterGridContent.gameObject.SetActive(true);
		}

		private void Scene_onChangePartyLineup()
		{
			Game.GameData.Trainer.party.PackParty();
			foreach (TrainerPokemonButton item in PartyViewer.Values)
			{
				if (Game.GameData.Trainer.party[item.partyIndex].IsNotNullOrNone())
				{
					//Game.GameData.Trainer.party[item.partyIndex] = new Pokemon((Pokemons)PkmnSelected, LevelFixed, false);
					PartyViewer[item.partyIndex].SetDisplay(); //pkmn.Name, pkmn.Species, pkmn.Level);
					//StoreButtonData[item.partyIndex].DisableOnClick(true);
					PartyViewer[item.partyIndex].ActivePokemonDisplay(true);
				}
				else
				{
					//Game.GameData.Trainer.party[item.partyIndex] = new Pokemon((Pokemons)PkmnSelected, LevelFixed, false);
					PartyViewer[item.partyIndex].ActivePokemonDisplay(false);
					PartyViewer[item.partyIndex].SetDisplay(); //pkmn.Name, pkmn.Species, pkmn.Level);
					//StoreButtonData[item.partyIndex].DisableOnClick(true);
				}
			}
		}
		#endregion
	}
}