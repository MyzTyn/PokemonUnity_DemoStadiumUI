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
	public class SelectPokemonButton : MonoBehaviour
	{
		[SerializeField] private Pokemons Species;
		[SerializeField] private Image myIcon;
		[SerializeField] private Text Name;
		[SerializeField] private Text L;
		//[SerializeField] private Button button;
		//[SerializeField] private MainCameraGameManager demo;
		public bool IsRental;
		public bool IsDisabled;
		public bool IsSelected;
		//public Pokemon Pokemon;
		public PokemonSelect PokemonSelect;
		public KeyValuePair<int?,int> Position;
		//public int ID { get; private set; }
		//public void SetID(int id)
		//{
		//	ID = id;
		//}
		public void SetID(int id, Pokemons species, bool isRental = true, int? page = null, bool selected = false, string name = null)
		{
			Position = new KeyValuePair<int?, int>(page, id);
			IsRental = isRental;
			IsSelected = selected;
			Species = species;
			Name.text = name;
		}
		public void DisableOnClick(bool active)
		{
			if (active)
			{
				Debug.Log($"Disabled the {Species} button");
                GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
			}
			else
			{
                GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
                GetComponent<Toggle>().onValueChanged.AddListener(delegate { Scene_onButtonSelected(); });
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
		public void Scene_onButtonSelected()
		{
			Debug.Log("Pressed! " + Species);
			PokemonSelect.Species = Species;
			PokemonSelect.PokemonPosition = Position;
			PokemonSelect.IsRentalPokemon = IsRental;
			PokemonSelect.EditPokemon = true;
			// ToDo: Avoid this hack
			MainCameraGameManager.Instance.pokemonViewModal.ActiveGameobject(true);
			MainCameraGameManager.Instance.pokemonViewModal.RefreshDisplay();

			//GameEvents.current.OnLoadLevel(1); //Change scene...
		}
        private void Awake()
		{
			GetComponent<Toggle>().onValueChanged.AddListener(delegate { Scene_onButtonSelected(); });
			//new WaitForSeconds(1);
		}
        private void Start()
		{
			Refresh();
		}

		public void Refresh()
		{
            if (MainCameraGameManager.IconSprites.Length > (int)Species)
            {
                myIcon.sprite = MainCameraGameManager.IconSprites[(int)Species];
            }
            else
            {
                Debug.LogError($"Index #{(int)Species}:{Species} was outside the bounds of the array.");
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
					L.text = "L" + PokemonSelect.LevelFixed;
					Name.text = Species.ToString(TextScripts.Name);
				}
			}
			else
			{
				//Load data from player saved games and use as pokemons to select from
			}
		}
	}
}