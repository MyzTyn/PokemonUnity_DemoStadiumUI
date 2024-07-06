using System;
using System.Collections;
using System.Collections.Generic;
using PokemonEssentials.Interface.PokeBattle;
using PokemonUnity;
using PokemonUnity.Monster;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonUnity.Stadium
{
	// ToDo: Maybe remove the data being stored in this button. Only hold the position.
	[ExecuteInEditMode]
	public class SelectPokemonButton : MonoBehaviour
	{
		[SerializeField] public IPokemon pokemon;
		[SerializeField] private Image myIcon;
		[SerializeField] private Text Name;
		[SerializeField] private Text Level;
		public bool IsDisabled;
		public bool IsSelected;
		public PokemonSelect PokemonSelect;
		public KeyValuePair<int?,int> Position;
		private PokemonViewModal PokemonViewModal;

		// ToDo: this still cuase the null reference (May check the DLL for logic check)
		//public bool IsRental { get { return pokemon?.ot == null; } } //If OriginalTrainer is Null, then it's a rental pokemon
		public bool IsRental { get { return true; } }
		//public int ID { get; private set; }

		#region Unity Monobehavior
		private void Awake()
		{
			GetComponent<Toggle>().onValueChanged.AddListener(Scene_onButtonPressed);
		}
		#endregion

		#region Methods
		/// <summary>
		/// Generate random pokemon in display matching Species
		/// </summary>
		/// <param name="pokemonViewModal"></param>
		/// <param name="pokemonSelect"></param>
		/// <param name="id"></param>
		/// <param name="pkmn"></param>
		/// <param name="isRental"></param>
		/// <param name="page"></param>
		/// <param name="selected"></param>
		/// <param name="name"></param>
		public void SetID(PokemonViewModal pokemonViewModal, PokemonSelect pokemonSelect, int id, Pokemons species, bool isRental = true, int? page = null, bool selected = false)
		{
			//ToDo: Use battle rules to determine the constraints of the pokemon
			//ToDo: PokemonSelect is null??
			Pokemon pokemon = new Pokemon(pkmn: species, level: 50);
			// ToDo: Remove this code once new DLL with the bug fixed
			pokemon.SetNickname(species.ToString());
			SetID(pokemonViewModal, pokemonSelect, id, pokemon, isRental, page, selected);
		}

		/// <summary>
		/// Load pokemon in display with given profile
		/// </summary>
		/// <param name="pokemonViewModal"></param>
		/// <param name="pokemonSelect"></param>
		/// <param name="id"></param>
		/// <param name="pkmn"></param>
		/// <param name="isRental"></param>
		/// <param name="page"></param>
		/// <param name="selected"></param>
		/// <param name="name"></param>
		public void SetID(PokemonViewModal pokemonViewModal, PokemonSelect pokemonSelect, int id, IPokemon pkmn, bool isRental = true, int? page = null, bool selected = false)
		{
			PokemonViewModal = pokemonViewModal;
			PokemonSelect = pokemonSelect;
			Position = new KeyValuePair<int?, int>(page, id);
			//IsRental = isRental;
			IsSelected = selected;
			//Species = species;
			pokemon = pkmn;
			//Just Call Refresh() instead of setting values here
			//Name.text = pokemon.Name;
			//Level.text = "L" + pokemon.Level;
			//myIcon.sprite = MainCameraGameManager.IconSprites[(int)pokemon.Species];
			Refresh();
		}

		public void DisableOnClick(bool active)
		{
			if (active)
			{
				Debug.Log($"Disabled the button for [{pokemon.Name}] in position [{Position.Key},{Position.Value}]");
				GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
			}
			else
			{
				GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
				GetComponent<Toggle>().onValueChanged.AddListener(Scene_onButtonPressed);
			}
		}

		public void Refresh()
		{
			if ((int)pokemon.Species >= MainCameraGameManager.IconSprites.Length  || pokemon.Species == Pokemons.NONE)
			{
				Core.Logger?.LogError($"Pokemon [{pokemon.Name}] Index #{(int)pokemon.Species}:{pokemon.Species} was outside the bounds of the array.");
				return;
			}

			myIcon.sprite = MainCameraGameManager.IconSprites[(int)pokemon.Species];

			if (!IsRental)
			{
				//Load data from player saved games and use as pokemons to select from
				return;
			}

			if (PokemonSelect.SelectedPokemonPositions.Contains(Position))
			{
				//Pokemon is selected and added to party already
				Debug.Log("This Pokemon is selected and added to party already");
				return;
			}

			//Create new entry display for pokemon
			Level.text = $"L{pokemon.Level}";
			Name.text = pokemon.Name;
		}

		// ToDo: Update the summary. And does this button need the param?
		// This supposed to open PokemonViewModal for stats
		/// <summary>
		/// When this button pressed, open the PokemonViewModal
		/// </summary>
		/// <param name="arg">if pokemon is queued for battle</param>
		public void Scene_onButtonPressed(bool arg)
		{
			// ToDo: Remove this and somehow PokemonViewModal or something call this button to disable (Visual change)
			if (PokemonSelect.SelectedPokemonPositions.Contains(Position))
			{
				//Pokemon is selected and added to party already
				Debug.Log("This Pokemon is selected and added to party already");
				return;
			}

			IsSelected = arg;
			//Debug.Log($"Pokemon [{pokemon.Name}] in position [{Position.Key},{Position.Value}] Pressed!"); //FIXME: Is this for when the pokemon button is pressed or selected?
			Core.Logger?.Log($"Pokemon [{pokemon.Name}] in position [{Position.Key},{Position.Value}] Pressed!"); //FIXME: Is this for when the pokemon button is pressed or selected?

			PokemonSelect.CurrentSelectedRosterPosition = Position.Value;

			PokemonSelect.IsRentalPokemon = IsRental;
			PokemonSelect.EditPokemon = true;
			PokemonViewModal.ActiveGameObject(true);
			PokemonViewModal.RefreshDisplay();//pokemon

			//GameEvents.current.OnLoadLevel(1); //Change scene...
		}
		#endregion
	}
}