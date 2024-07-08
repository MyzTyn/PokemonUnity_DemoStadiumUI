using System.Collections.Generic;
using PokemonEssentials.Interface.PokeBattle;
using PokemonUnity.Monster;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonUnity.Stadium
{
    [ExecuteInEditMode]
	public class SelectPokemonButton : MonoBehaviour
	{
		[SerializeField] private Image myIcon;
		[SerializeField] private Text Name;
		[SerializeField] private Text Level;

        // ToDo: Do we need those?
        //public bool IsDisabled;
        //public bool IsSelected;

        public KeyValuePair<int?, int> Position;

		// ToDo: Remove those code
        //public PokemonSelect PokemonSelect;
		//private PokemonViewModal PokemonViewModal;

        // ToDo: this still cuase the null reference (May check the DLL for logic check)
        //public bool IsRental { get { return pokemon?.ot == null; } } //If OriginalTrainer is Null, then it's a rental pokemon
        //public bool IsRental { get { return true; } }

		#region Unity Monobehavior
		private void Awake()
		{
			//GetComponent<Toggle>().onValueChanged.AddListener(Scene_onButtonPressed);
		}
        #endregion

        #region Methods
        // Set Pokemon Button in display
        public void SetID(PokemonViewModal pokemonViewModal, PokemonSelect pokemonSelect, int id, Pokemons species, string name, int level, int? page = null)
        {
            Position = new KeyValuePair<int?, int>(page, id);
            
            GetComponent<Toggle>().onValueChanged.AddListener((value) => Scene_onButtonPressed(value, pokemonSelect, pokemonViewModal));

            if ((int)species >= MainCameraGameManager.IconSprites.Length || species == Pokemons.NONE)
            {
                Core.Logger?.LogError($"Pokemon [{name}] Index #{(int)species}:{species} was outside the bounds of the array.");
                return;
            }

            myIcon.sprite = MainCameraGameManager.IconSprites[(int)species];

            Level.text = $"L{level}";
            Name.text = name;
        }

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
        //public void SetID(PokemonViewModal pokemonViewModal, PokemonSelect pokemonSelect, int id, Pokemons species, int? page = null)
		//{
		//	//ToDo: Use battle rules to determine the constraints of the pokemon
		//	//Pokemon pokemon = new Pokemon(pkmn: species, level: 50);
		//	// ToDo: Remove this code once new DLL with the bug fixed
		//	//pokemon.SetNickname(species.ToString());
		//	//SetID(pokemonViewModal, pokemonSelect, id, species, page);
        //
        //    //PokemonViewModal = pokemonViewModal;
        //    //PokemonSelect = pokemonSelect;
        //    Position = new KeyValuePair<int?, int>(page, id);
        //    //IsSelected = selected;
        //    //pokemon = species;
        //
        //    GetComponent<Toggle>().onValueChanged.AddListener((value) => Scene_onButtonPressed(value, pokemonSelect, pokemonViewModal));
        //
        //    //Refresh();
        //}

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
        //public void SetID(PokemonViewModal pokemonViewModal, PokemonSelect pokemonSelect, int id, Pokemons pkmn, int? page = null)
        //{
        //	PokemonViewModal = pokemonViewModal;
        //	PokemonSelect = pokemonSelect;
        //	Position = new KeyValuePair<int?, int>(page, id);
        //	//IsSelected = selected;
        //	pokemon = pkmn;
        //
        //	Refresh();
        //}

        // ToDo: Do we need this?
        //public void DisableOnClick(bool active)
		//{
		//	if (active)
		//	{
		//		Debug.Log($"Disabled the button for [{pokemon}] in position [{Position.Key},{Position.Value}]");
		//		GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
		//	}
		//	else
		//	{
		//		GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
		//		GetComponent<Toggle>().onValueChanged.AddListener(Scene_onButtonPressed);
		//	}
		//}

		//public void Refresh()
		//{
        //    if ((int)pokemon >= MainCameraGameManager.IconSprites.Length  || pokemon == Pokemons.NONE)
        //    {
        //        Core.Logger?.LogError($"Pokemon [{pokemon}] Index #{(int)pokemon}:{pokemon} was outside the bounds of the array.");
		//		return;
        //    }
		//	
        //    myIcon.sprite = MainCameraGameManager.IconSprites[(int)pokemon];
        //
        //    //if (!IsRental)
		//	//{
        //    //    //Load data from player saved games and use as pokemons to select from
        //    //    return;
		//	//}
        //
        //    //if (PokemonSelect.SelectedPokemonPositions.Contains(Position))
        //    //{
        //    //    //Pokemon is selected and added to party already
        //    //    Debug.Log("This Pokemon is selected and added to party already");
		//	//	return;
        //    //}
        //
        //    //Create new entry display for pokemon
        //    Level.text = $"L{50}";
        //    Name.text = pokemon.ToString();
        //}

		// ToDo: Update the summary. And does this button need the param?
		// This supposed to open PokemonViewModal for stats
		/// <summary>
		/// When this button pressed, open the PokemonViewModal
		/// </summary>
		/// <param name="arg">if pokemon is queued for battle</param>
		//public void Scene_onButtonPressed(bool arg)
		//{
		//	// ToDo: Remove this and somehow PokemonViewModal or something call this button to disable (Visual change)
        //    if (PokemonSelect.SelectedPokemonPositions.Contains(Position))
        //    {
        //        //Pokemon is selected and added to party already
        //        Debug.Log("This Pokemon is selected and added to party already");
		//		// ToDo: Do we need this?
		//		DisableOnClick(true);
        //        return;
        //    }
        //
        //    //IsSelected = arg;
		//	Core.Logger?.Log($"Pokemon [{pokemon}] in position [{Position.Key},{Position.Value}] Pressed!");
        //
		//	PokemonSelect.CurrentSelectedRosterPosition = Position.Value;
        //
        //    //PokemonSelect.IsRentalPokemon = IsRental;
		//	PokemonSelect.EditPokemon = true;
		//	PokemonViewModal.ActiveGameObject(true);
		//	PokemonViewModal.RefreshDisplay();//pokemon
		//	
		//	//GameEvents.current.OnLoadLevel(1); //Change scene...
		//}

        // ToDo: Update the summary. And does this button need the param?
        // This supposed to open PokemonViewModal for stats
        /// <summary>
        /// When this button pressed, open the PokemonViewModal
        /// </summary>
        /// <param name="arg">if pokemon is queued for battle</param>
        public void Scene_onButtonPressed(bool arg, PokemonSelect PokemonSelect, PokemonViewModal PokemonViewModal)
        {
            // ToDo: Good?
            if (PokemonSelect.SelectedPokemonPositions.Contains(Position))
            {
                //Pokemon is selected and added to party already
                Debug.Log("This Pokemon is selected and added to party already");
                // ToDo: Do we need this?
                //DisableOnClick(true);
                return;
            }

            //IsSelected = arg;
            Core.Logger?.Log($"Pokemon [{Name.text}] in position [{Position.Key},{Position.Value}] Pressed!");

            PokemonSelect.CurrentSelectedRosterPosition = Position.Value;

            //PokemonSelect.IsRentalPokemon = IsRental;
            PokemonSelect.EditPokemon = true;
            PokemonViewModal.ActiveGameObject(true);
            PokemonViewModal.RefreshDisplay();//pokemon

            //GameEvents.current.OnLoadLevel(1); //Change scene...
        }
        #endregion
    }
}