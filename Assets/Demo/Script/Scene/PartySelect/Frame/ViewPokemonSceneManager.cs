using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using PokemonUnity;
using PokemonUnity.Character;
using PokemonUnity.Monster;
using UnityEngine;
using UnityEngine.UI;
using PokemonUnity.Application;

namespace PokemonUnity.Stadium
{
	//[ExecuteInEditMode]
	public class ViewPokemonSceneManager : MonoBehaviour
	{
		#region Variables
		public PokemonSelect PokemonSelect;

		[SerializeField] private TrainerPartyPanel partySelectionUI;
		[SerializeField] private PokemonViewModal pokemonViewModal;
		[SerializeField] private GameObject partyEntryPrefab;
		[SerializeField] private Transform partyGridContent;

		//List
		private Dictionary<int, TrainerPokemonButton> PartyViewer;
		//Sprite
		public static Sprite[] PkmnType { get; private set; }
		public static Sprite[] IconSprites { get; private set; }
		#endregion

		#region Unity Monobehavior
		void Awake()
		{
			SelectPokemonSceneManager.SelectedPokemonViewModal = pokemonViewModal;
			Debug.Log("Is Scriptable Object Null? " + (PokemonSelect == null).ToString());
			Debug.Log("Create Dictionary for Player Party UI Mono");
			PartyViewer = new Dictionary<int, TrainerPokemonButton>();
			//
			Debug.Log("Load Assets for UI into Array");
			IconSprites = Resources.LoadAll<Sprite>("PokemonIcon");
			PkmnType = Resources.LoadAll<Sprite>("PokemonType");
		}

		void Start()
		{
			Debug.Log("Is Game Events Null? " + (GameEvents.current == null).ToString());
			GameEvents.current.onChangePartyLineup += Scene_onChangePartyLineup;

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
		/*public void PokemonStatsandMoveUI(int Pkmn_ID)
		{
			PkmnSelected = Pkmn_ID;
			Debug.Log("Pressed! " + Pkmn_ID);
			if (!StorePokemon.ContainsKey((Pokemons)Pkmn_ID))
			{
				Debug.Log("Create Pokemon!");
				StorePokemon.Add((Pokemons)PkmnSelected, new Pokemon((Pokemons)PkmnSelected, PokemonSelect.LevelFixed, false));
			}
			else
				Debug.Log("Pokemon is already created!");
			DisplayMoveSetUI();
		}
		void DisplayMoveSetUI()
		{
			pokemonViewModal.ActiveGameObject(true);
			pokemonViewModal.DisplayPkmnStats();
		}
		public void AddToParty()
		{
			if (PokemonSelect.CurrentSelectedPokemon == 0)
			{
				Debug.Log("Error. There no Pokemon!");
			}
			else
			{
				if (PokemonSelect.CurrentSelectedPartySlot >= 0 && PokemonSelect.CurrentSelectedPartySlot < Core.MAXPARTYSIZE)
				{
					StoreButtonData[PokemonSelect.CurrentSelectedPartySlot].DisableOnClick(true);
					Game.GameData.Trainer.party[PokemonSelect.CurrentSelectedPartySlot] = new Pokemon((Pokemons)PkmnSelected, PokemonSelect.LevelFixed, false);
					//PartyViewer[CurrentOnParty].DisplayPartyButton();
					PartyViewer[PokemonSelect.CurrentSelectedPartySlot].SetDisplay(); //pkmn.Name, pkmn.Species, pkmn.Level);
					PartyViewer[PokemonSelect.CurrentSelectedPartySlot].ActivePokemonDisplay(true);
					//CurrentOnParty += 1;
					//Ask player if they're done and wish to move on; but in another function...
					//if (Game.GameData.Trainer.party[5].IsNotNullOrNone())
					//{
					//	Debug.Log("Disable the UI");
					//	RentalControlUI.ActiveRentalUI(false);
					//}
				}
				pokemonViewModal.CancelUI();
			}
		}*/
		#endregion

		#region Party Roster UI
		public void SetPartyButton()
		{
			//if (ID.Count < 4)
			//{
			//	gridGroup.constraintCount = ID.Count;
			//}
			//else
			//{
			//	gridGroup.constraintCount = 3;
			//}
			for (int Id = 0; Id < Feature.MAXPARTYSIZE && Id < Core.MAXPARTYSIZE; Id++)
			{
				//if (Id == Core.MAXPARTYSIZE) break;
				GameObject Button = Instantiate(partyEntryPrefab);
				TrainerPokemonButton slot = Button.GetComponent<TrainerPokemonButton>();
				slot.PokemonSelect = PokemonSelect; //Should be duplicated for each player controller on screen
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