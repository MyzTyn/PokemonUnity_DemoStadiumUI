using PokemonUnity;
using PokemonUnity.Character;
using PokemonUnity.Monster;
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// If multiplayer or split screen, can use this to store individual player selection data
/// </summary>
/// <remarks>
/// Global and persistent variables for pokemon select screen of Stadium Scene and UI 
/// </remarks>
/// https://unity.com/how-to/architect-game-code-scriptable-objects
[CreateAssetMenu(fileName ="PlayerSelectionState", menuName = "ScriptableObjects/PlayerSelection")]
public class PokemonSelect : ScriptableObject
{
	#region Variables
	//public float InitialValue;
	//[NonSerialized] public float RuntimeValue;
	/// <summary>
	/// State or Mode for viewing stats and attributes of a selected pokemon
	/// </summary>
	[NonSerialized] public bool EditPokemon;
	[NonSerialized] public bool IsRentalPokemon;
	[NonSerialized] public byte LevelFixed = 50;
	[NonSerialized] public Pokemons Species;
	//May not need since values below does same thing...
	[NonSerialized] public KeyValuePair<int?,int> PokemonPosition;
	/// <summary>
	/// Which pokemon slot is currently active for choice of player's decision
	/// </summary>
	[NonSerialized] public int CurrentSelectedPartySlot;
	[NonSerialized] public int? CurrentSelectedRosterPage;
	[NonSerialized] public int CurrentSelectedRosterPosition;
	[NonSerialized] public Pokemons CurrentSelectedPokemon;
	[NonSerialized] public float CurrentSrollPosition;
	//[NonSerialized] public Scrollbar CurrentSrollPosition;

	public List<Generation> PokemonGens { get; private set; }
	public Queue<Pokemons> ViewedRentalPokemon { get; private set; }
	public Dictionary<Pokemons, Pokemon> StorePokemon { get; private set; }
	public HashSet<KeyValuePair<KeyValuePair<bool, int?>, int>> SelectedPokemons { get; private set; }
	#endregion

	#region Unity Monobehavior
	public void OnAfterDeserialize()
	{
		//RuntimeValue = InitialValue;
		PokemonGens = new List<Generation>();
		Debug.Log("Create Dictionary for Temp Instantiated Pokemon Objects");
		StorePokemon = new Dictionary<Pokemons, Pokemon>();
		Debug.Log("Create Dictionary for Temp Viewed Pokemons");
		ViewedRentalPokemon = new Queue<Pokemons>();
		//Debug.Log("Create Dictionary for Pokemons Selected by Player");
		//SelectedPokemons = new Dictionary<KeyValuePair<bool, int?>, int>();
		Debug.Log("Create LookUp Table for Pokemons Selected by Player");
		SelectedPokemons = new HashSet<KeyValuePair<KeyValuePair<bool, int?>, int>>();
	}

	public void OnBeforeSerialize() { }
	#endregion
	
	#region Methods
	#endregion

	public enum SelectionState
	{
		/// <summary>
		/// Going through list of pokemons to choose from
		/// </summary>
		SELECT,
		/// <summary>
		/// Viewing selected Pokemon profile and stats (Shows Moves, Stats, and other Pokedex related info)
		/// </summary>
		VIEW,
		/// <summary>
		/// Editing Pokemon Summary...
		/// </summary>
		EDIT
	}
}