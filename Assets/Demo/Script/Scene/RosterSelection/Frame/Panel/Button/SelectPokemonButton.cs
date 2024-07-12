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

        public KeyValuePair<int?, int> Position;

        #region Methods
        // Set Pokemon Button in display
        public void SetID(PokemonViewModal pokemonViewModal, PokemonSelect pokemonSelect, int id, Pokemons species, string name, int level, int? page = null)
        {
            // Check to ensure the Pokemon is in Generation 1 (Stadium Project only support Generation 1)
            if ((int)species >= RosterSelectionScene.IconSprites.Length || species == Pokemons.NONE)
            {
                Core.Logger?.LogError($"Pokemon [{name}] Index #{(int)species}:{species} was outside the bounds of the array.");
                return;
            }
            
            // Set the position, sprite, level and name
            Position = new KeyValuePair<int?, int>(page, id);
            myIcon.sprite = RosterSelectionScene.IconSprites[(int)species];
            Level.text = $"L{level}";
            Name.text = name;
            
            // Add Button event listener
            GetComponent<Toggle>().onValueChanged
                .AddListener((value) => Scene_onButtonPressed(value, pokemonSelect, pokemonViewModal));
        }

        // This supposed to open PokemonViewModal for stats
        /// <summary>
        /// When this button pressed, open the PokemonViewModal
        /// </summary>
        /// <param name="arg">if pokemon is queued for battle</param>
        private void Scene_onButtonPressed(bool arg, PokemonSelect PokemonSelect, PokemonViewModal PokemonViewModal)
        {
            Core.Logger?.Log($"Pokemon [{Name.text}] in position [{Position.Key},{Position.Value}] Pressed!");
            
            // Check if this Pokemon already added to the party
            if (PokemonSelect.SelectedPokemonPositions.Contains(Position))
            {
                Debug.Log("This Pokemon is selected and added to party already");
                return;
            }

            //IsSelected = arg;
            //PokemonSelect.IsRentalPokemon = IsRental;
            
            // Set the position value
            PokemonSelect.CurrentSelectedRosterPosition = Position.Value;
            PokemonSelect.EditPokemon = true; // ToDo: What is this for?
            
            // Active PokemonViewModal
            PokemonViewModal.ActiveGameObject(true);
            PokemonViewModal.RefreshDisplay();

            //GameEvents.current.OnLoadLevel(1); //Change scene...
        }
        #endregion
    }
}