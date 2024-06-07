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
	[ExecuteInEditMode]
	public class SelectPokemonButton : MonoBehaviour
	{
		//[SerializeField] private MainCameraGameManager demo;
		//[SerializeField] private Button button;
		[SerializeField] private IPokemon pokemon;
		[SerializeField] private Image myIcon;
		[SerializeField] private Text Name;
		[SerializeField] private Text Level;
		public bool IsDisabled;
		public bool IsSelected;
		//public Pokemon Pokemon;
		public PokemonSelect PokemonSelect;
		public KeyValuePair<int?,int> Position;
		private PokemonViewModal PokemonViewModal;
		public bool IsRental { get { return pokemon.ot == null; } }
		//public int ID { get; private set; }

		#region Unity Monobehavior
		private void Awake()
		{
			GetComponent<Toggle>().onValueChanged.AddListener(Scene_onButtonSelected);
		}
		private void Start()
		{
			//Just call Refresh() when values are set, it's possible values could change but still display wrong values
			//Refresh();
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
			IPokemon pokemon = new Pokemon(pkmn: species, level: PokemonSelect.LevelFixed);
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
				GetComponent<Toggle>().onValueChanged.AddListener(Scene_onButtonSelected);
			}
		}

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

		public void Refresh()
		{
			if (MainCameraGameManager.IconSprites.Length > (int)pokemon.Species)
			{
				myIcon.sprite = MainCameraGameManager.IconSprites[(int)pokemon.Species];
			}
			else
			{
				Debug.LogError($"Pokemon [{pokemon.Name}] Index #{(int)pokemon.Species}:{pokemon.Species} was outside the bounds of the array.");
			}

			if (IsRental)
			{
				if(MainCameraGameManager.SelectedPokemons.Contains(new KeyValuePair<KeyValuePair<bool, int?>, int>(new KeyValuePair<bool, int?>(IsRental, Position.Key), Position.Value)))
				{
					//Pokemon is selected and added to party already
					Debug.Log("This Pokemon is selected and added to party already");
				}
				else
				{
					//Create new entry display for pokemon
					//Level.text = "L" + PokemonSelect.LevelFixed;
					Level.text = "L" + pokemon.Level;
					Name.text = pokemon.Name;
				}
			}
			else
			{
				//Load data from player saved games and use as pokemons to select from
			}
		}

		/// <summary>
		/// When pokemon has been registered to player's active pokemon party
		/// </summary>
		/// <param name="arg">if pokemon is queued for battle</param>
		public void Scene_onButtonSelected(bool arg)
		{
			IsSelected = arg;
			Debug.Log($"Pokemon [{pokemon.Name}] in position [{Position.Key},{Position.Value}] Pressed!"); //FIXME: Is this for when the pokemon button is pressed or selected?
			//PokemonSelect.Species = pokemon.Species;
			PokemonSelect.PokemonPosition = Position;
			//if (arg) // If selected
			//	PokemonSelect.SelectedPokemonPositions.Push(Position);
			PokemonSelect.IsRentalPokemon = IsRental;
			PokemonSelect.EditPokemon = true;
			PokemonViewModal.ActiveGameobject(true);
			PokemonViewModal.RefreshDisplay(PokemonSelect.CurrentSelectedPokemon);

			//GameEvents.current.OnLoadLevel(1); //Change scene...
		}
		#endregion
	}
}