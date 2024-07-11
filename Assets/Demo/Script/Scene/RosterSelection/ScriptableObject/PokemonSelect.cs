﻿using System;
using System.Collections.Generic;
using PokemonEssentials.Interface.PokeBattle;
using UnityEngine;

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
		/// ToDo: Do we need those?
		[NonSerialized] public bool EditPokemon;
		// ToDo: Do we need this?
		//[NonSerialized] public bool IsRentalPokemon;
		[NonSerialized] public byte LevelFixed = 50;
		[NonSerialized] public int? CurrentSelectedRosterPage;
		[NonSerialized] public int CurrentSelectedRosterPosition;
		[NonSerialized] public float CurrentScrollPosition;

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
					if (TempRentalPokemonObjects.TryGetValue((Pokemons)CurrentSelectedRosterPosition + 1, out IPokemon pokemon))
					{
						return pokemon;
					}
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
		public int CurrentSelectedPartySlot => TemporaryParty.Count;
		
		/// <summary>
		/// Preserve the rental pokemons that are viewed by player.
		/// </summary>
		/// <remarks>
		/// Use to reset generated pokemon in <see cref="TempRentalPokemonObjects"/>
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
		public Dictionary<Pokemons, IPokemon> TempRentalPokemonObjects { get; private set; }
		
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
		/// ToDo: DO we need those?
		public HashSet<IPokemon> SelectedPokemons { get; private set; }
		
		/// <summary>
		/// When pokemons are selected by player from UI, store in this variable
		/// </summary>
		/// <remarks>
		/// First-In, Last-Out; Player can deselect their choice by removing their last choice
		/// </remarks>
		public Stack<IPokemon> TemporaryParty => temporaryParty;
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

		#region Methods
		public void OnAfterDeserialize()
		{
			Debug.Log("Create Dictionary for Temp Instantiated Pokemon Objects");
			TempRentalPokemonObjects = new Dictionary<Pokemons, IPokemon>();

			Debug.Log("Create Dictionary for Temp Viewed Pokemons");
			ViewedRentalPokemon = new Queue<Pokemons>();

			Debug.Log("Create LookUp Table for Pokemons Selected by Player");
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
				Core.Logger.LogError("There no Pokemon!");
				return false;
			}

			// ToDo: Replace to Game.GameData.Global.Features.LimitPokemonPartySize
			if (CurrentSelectedPartySlot >= 0 && CurrentSelectedPartySlot < Core.MAXPARTYSIZE)
			{
				TemporaryParty.Push(CurrentSelectedPokemon);
				SelectedPokemonPositions.Push(new KeyValuePair<int?, int>(CurrentSelectedRosterPage, CurrentSelectedRosterPosition));
			}

			// ToDo: Ask player if they're done and wish to move on; but in another function...
			if (TemporaryParty.Count != Core.MAXPARTYSIZE) 
				return false;
			
			Debug.Log("The party is full!");
			return true;
		}

		/// <summary>
		/// Removes the last pokemon added to the player's party,
		/// and moves the cursor back to the unselected pokemon
		/// </summary>
		public void UnregisterPreviousPokemon()
		{
			if (CurrentSelectedPartySlot <= 0) 
				return;
			
			// ToDo: Implement this
			//int index = temporaryParty.Count;
			//Destroy(MainCameraGameManager.Instance.partyPanel.party[index].gameObject);
			TemporaryParty.Pop();
			KeyValuePair<int?,int> position = SelectedPokemonPositions.Pop();
			CurrentSelectedRosterPage = position.Key;
			CurrentSelectedRosterPosition = position.Value;
		}
		#endregion

		/// <summary>
		/// This should be tracked in the MainCameraManager, or SceneManager not here in scriptable object...
		/// </summary>
		/// ToDo: Do we need those?
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