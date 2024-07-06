using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using PokemonUnity;
using PokemonUnity.Character;
using PokemonUnity.Monster;
using PokemonEssentials.Interface.PokeBattle;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PokemonUnity.Stadium
{
	/// <summary>
	/// Global and persistent variables for pokemon select screen of Stadium Scene and UI
	/// </summary>
	/// <remarks>
	/// If multiplayer or split screen, can use this to store individual player selection data
	/// </remarks>
	/// https://unity.com/how-to/architect-game-code-scriptable-objects
	[CreateAssetMenu(fileName ="PlayerSelectionState", menuName = "ScriptableObjects/PlayerSelection")]
	public class PokemonSelect : ScriptableObject, ISerializationCallbackReceiver //ToDo: Rename to contain ScriptableObject, and remove the word "Pokemon"?
	{
		#region Variables
		/// <summary>
		/// State or Mode for viewing stats and attributes of a selected pokemon
		/// </summary>
		[NonSerialized] public bool EditPokemon;
		[NonSerialized] public bool IsRentalPokemon;
		[NonSerialized] public byte LevelFixed = 50;
		//[Obsolete("What is this used for? Use `PokemonPosition` as a pointer to collection to reference pokemon")]
		//[NonSerialized] public Pokemons Species; //ToDo: Phase this out with a Manager class in scene
		//May not need since values below does same thing...
		//[Obsolete("Use `SelectedPokemonPositions` instead")]
		//[NonSerialized] public KeyValuePair<int?,int> PokemonPosition;
		[NonSerialized] public int? CurrentSelectedRosterPage;
		[NonSerialized] public int CurrentSelectedRosterPosition;
		[NonSerialized] public float CurrentScrollPosition;
		//[NonSerialized] public Scrollbar CurrentScrollPosition;

		/// <summary>
		/// Which pokemon slot is currently active for choice of player's decision
		/// </summary>
		/// FIXME: use current selected pokemon "slot" or "cursor" to reference the returning pokemon species?
		public IPokemon CurrentSelectedPokemon
		{
			get
			{
				// Use the RosterPage and RosterPosition to get the selected pokemon
				if (CurrentSelectedRosterPage == null) //Maybe use all negative numbers for multiple rental pages? Can be used for different generations
				{
					//Search for the pokemon in the rental list
					//if(MainCameraGameManager.Instance)
					return MainCameraGameManager.Instance.StoreButtonData[CurrentSelectedRosterPosition].pokemon; //FIXME: Use roster collection that populates the rental list
					//return null;
				}
				else if (CurrentSelectedRosterPage == 0)
				{
					//Search for the pokemon in the party list
					return Game.GameData.Trainer.party[CurrentSelectedPartySlot];
				}
				else if (CurrentSelectedRosterPage > 0)
				{
					//Search for the pokemon in the trainer's PC storage box
					return Game.GameData.PokemonStorage.boxes[CurrentSelectedRosterPage.Value][CurrentSelectedPartySlot];
				}
				return null; //Pokemons.NONE;
			}
		}
		/// <summary>
		/// Which pokemon slot is currently active for choice of player's decision
		/// </summary>
		public int CurrentSelectedPartySlot { get { return TemporaryParty.Count; } }
		/// <summary>
		/// Preserve the rental pokemons that are viewed by player.
		/// </summary>
		/// <remarks>
		/// Use to reset generated pokemon in <see cref="StorePokemon"/>
		/// after X amount of pokemons are viewed
		/// </remarks>
		/// ToDo: What if instead of a list of pokemons, it was a list of positions? what are the chances the same id could appear in roster?
		public Queue<Pokemons> ViewedRentalPokemon { get; private set; }
		/// <summary>
		/// Use to store temporary instantiated rental pokemon objects
		/// </summary>
		/// <remarks>
		/// Each Pokemon Id is only instantiated once and stored here
		/// </remarks>
		/// ToDo: Rename to TempRentalPokemonObjects?
		public Dictionary<Pokemons, IPokemon> StorePokemon { get; private set; }
		/// <summary>
		/// When pokemons are selected by player from UI, store in this variable
		/// </summary>
		/// <remarks>
		/// When player selects a pokemon, store the pokemon and its position in the party.
		/// </remarks>
		/// <example>
		/// Should be: IDictionary<SlotId, IsSelected>, PositionId> or something...
		/// </example>
		/// Why is the key-value pair using a bool as key, and not value? What is the purpose of the bool?
		//public HashSet<KeyValuePair<KeyValuePair<bool, int?>, int>> SelectedPokemons { get; private set; }
		public HashSet<IPokemon> SelectedPokemons { get; private set; }
		/// <summary>
		/// When pokemons are selected by player from UI, store in this variable
		/// </summary>
		/// <remarks>
		/// First-In, Last-Out; Player can deselect their choice by removing their last choice
		/// </remarks>
		public Stack<IPokemon> TemporaryParty { get { return temporaryParty; } }
		private Stack<IPokemon> temporaryParty;
		/// <summary>
		/// Placeholder to store the source position of the selected pokemon in the party
		/// </summary>
		/// <remarks>
		/// [int?] PageTab, [int] Position
		///<para>Page: null => Rental, 0 => Party, 1+ => Box</para>
		/// </remarks>
		public Stack<KeyValuePair<int?, int>> SelectedPokemonPositions { get; private set; }
		#endregion

		#region Unity Monobehavior
		#endregion

		#region Methods
		public void OnAfterDeserialize()
		{
			//RuntimeValue = InitialValue;
			//PokemonGens = new List<Generation>();
			Debug.Log("Create Dictionary for Temp Instantiated Pokemon Objects");
			StorePokemon = new Dictionary<Pokemons, IPokemon>();
			Debug.Log("Create Dictionary for Temp Viewed Pokemons");
			ViewedRentalPokemon = new Queue<Pokemons>();
			//Debug.Log("Create Dictionary for Pokemons Selected by Player");
			//SelectedPokemons = new Dictionary<KeyValuePair<bool, int?>, int>();
			Debug.Log("Create LookUp Table for Pokemons Selected by Player");
			//SelectedPokemons = new HashSet<KeyValuePair<KeyValuePair<bool, int?>, int>>();
			SelectedPokemons = new HashSet<IPokemon>();
			SelectedPokemonPositions = new Stack<KeyValuePair<int?, int>>();
			temporaryParty = new Stack<IPokemon>();
		}

		public void OnBeforeSerialize() { }

		/// <summary>
		/// Adds the selected pokemon to the player's party
		/// </summary>
		/// <returns>
		/// Returns true if the party is full, and the player is ready to move on;
		/// display "Start Battle" button
		/// </returns>
		/// True if the pokemon was successfully added to the party
		public bool RegisterSelectedPokemon()
		{
			if (!CurrentSelectedPokemon.IsNotNullOrNone())
			{
				Core.Logger.Log("Error. There no Pokemon!");
			}
			else
			{
				// ToDo: Fix this mess code
				if (CurrentSelectedPartySlot >= 0 && CurrentSelectedPartySlot < Core.MAXPARTYSIZE) //Game.GameData.Global.Features.LimitPokemonPartySize
				{
					//if (CurrentSelectedRosterPage != null)
					//{
					//	//Search for the pokemon in the rental list
					//	SelectedPokemonPositions.Push(CurrentSelectedPokemon); //FIXME: Use roster collection that populates the rental list
					//}
					//else if (CurrentSelectedRosterPage == 0)
					//{
					//	//Search for the pokemon in the party list
					//	TemporaryParty.Push(Game.GameData.Trainer.party[CurrentSelectedPartySlot]);
					//}
					//else if (CurrentSelectedRosterPage > 0)
					//{
					//	//Search for the pokemon in the trainer's PC storage box
					//	TemporaryParty.Push(Game.GameData.PokemonStorage.boxes[CurrentSelectedRosterPage.Value][CurrentSelectedPartySlot]);
					//}
					int index = temporaryParty.Count;
					TemporaryParty.Push(CurrentSelectedPokemon);

					//SelectedPokemonPositions.Push(new KeyValuePair<int?, int>(CurrentSelectedRosterPage,CurrentSelectedPartySlot));
					SelectedPokemonPositions.Push(new KeyValuePair<int?, int>(CurrentSelectedRosterPage,CurrentSelectedRosterPosition));

					// ToDo: Signal the Party UI
					//PartyViewer[CurrentOnParty].DisplayPartyButton();
					//PartyViewer[CurrentSelectedPartySlot].SetDisplay(); //pkmn.Name, pkmn.Species, pkmn.Level);
					//PartyViewer[CurrentSelectedPartySlot].ActivePokemonDisplay(true);
					//Instantiate new Prefab to Scene
					TrainerPokemonButton pokemon = Instantiate(MainCameraGameManager.Instance.partyPanel.pokemonButtonPrefab, MainCameraGameManager.Instance.partyPanel.partyContentFrame.transform);

					// Set the width/height (Fix this code => There's nothing to fix as Unity components are set to automatically adjust size, and position)
					//var layoutElement = pokemon.GetComponent<LayoutElement>();
					//layoutElement.preferredWidth = 125;
					//layoutElement.preferredHeight = 41;

					pokemon.partyIndex = index;
					pokemon.toggle.group = MainCameraGameManager.Instance.partyPanel.GetComponent<ToggleGroup>(); //toggleGroup;
					pokemon.toggle.interactable = false;
					pokemon.name = "Slot" + index;
					pokemon.SetDisplay(CurrentSelectedPokemon);
					//pokemon.PokemonSelect = PokemonSelect;
					MainCameraGameManager.Instance.partyPanel.party[index] = pokemon;

					// ToDo: Fix this code
					//StoreButtonData[PokemonPosition.Value].DisableOnClick(true);
				}

				//Ask player if they're done and wish to move on; but in another function...
				//if (Game.GameData.Trainer.party[5].IsNotNullOrNone())
				if (TemporaryParty.Count == Core.MAXPARTYSIZE)
				{
					Debug.Log("Disable the UI");
					// RentalControlUI.ActiveRentalUI(false);
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Removes the last pokemon added to the player's party,
		/// and moves the cursor back to the unselected pokemon
		/// </summary>
		public void UnregisterPreviousPokemon()
		{
			if (CurrentSelectedPartySlot > 0)
			{
				//CurrentSelectedPartySlot--;
				//StoreButtonData[PokemonSelect.CurrentSelectedPartySlot].DisableOnClick(false);
				//Game.GameData.Trainer.party[PokemonSelect.CurrentSelectedPartySlot] = new Pokemon(Pokemons.NONE, 0, false);
				//PartyViewer[CurrentOnParty].DisplayPartyButton();
				//PartyViewer[CurrentSelectedPartySlot].SetDisplay(); //pkmn.Name, pkmn.Species, pkmn.Level);
				//PartyViewer[CurrentSelectedPartySlot].ActivePokemonDisplay(false);
				//StoreButtonData[PokemonSelect.CurrentSelectedPartySlot].DisableOnClick(false);

				int index = temporaryParty.Count;
				Destroy(MainCameraGameManager.Instance.partyPanel.party[index].gameObject);
				TemporaryParty.Pop();
				KeyValuePair<int?,int> position = SelectedPokemonPositions.Pop();
				CurrentSelectedRosterPage = position.Key;
				CurrentSelectedRosterPosition = position.Value;
			}
		}
		#endregion

		/// <summary>
		/// This should be tracked in the MainCameraManager, or SceneManager not here in scriptable object...
		/// </summary>
		public enum SelectionState
		{
			/// <summary>
			/// Going through list of pokemons to choose from
			/// </summary>
			SELECT,
			/// <summary>
			/// Viewing selected Pokemon profile and stats (Shows Moves, Stats, and other Pokédex related info)
			/// </summary>
			VIEW,
			/// <summary>
			/// Editing Pokemon Summary...
			/// </summary>
			EDIT
		}
	}
}