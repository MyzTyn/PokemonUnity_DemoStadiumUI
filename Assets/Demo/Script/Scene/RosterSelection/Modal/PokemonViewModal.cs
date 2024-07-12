using PokemonEssentials.Interface.PokeBattle;
using PokemonUnity.Monster;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PokemonUnity.Stadium
{
	public class PokemonViewModal : MonoBehaviour
	{
		#region Variables
		[SerializeField] private ViewPokemonData Data;

		// ToDo: No Button bug. Only if you click two times at sequence (No, No -> won't close the modal)
		[SerializeField] private Toggle IsPokemonSelected;
		[SerializeField] private int RentalViewCount = 5;

		private IPokemon pokemon;
		public PokemonSelect PokemonSelect;

		// ToDo: Do we need this?
		public bool IsWindowActive { get; private set; }
		#endregion

		public void Awake()
		{
			// Accept it and add to the party
			IsPokemonSelected.onValueChanged.AddListener(ToggleButtonSelected_Event);
		}

		#region Methods
		public void ActiveGameObject(bool active)
		{
			// Clear the display
			if (!active)
				ClearDisplay();

			gameObject.SetActive(active);
			IsWindowActive = active;
		}

		/// <summary>
		/// Refreshes the Pokemon view window that displays the active pokemon's stats and profile.
		/// </summary>
		/// <param name="pkmn">Pokemon to be displayed in UI</param>
		/// ToDo: pokemon should be passed as a parameter for this method, and use the object to assign values to display
		public void RefreshDisplay()
		{
			// If the rental page selected
			if (PokemonSelect.CurrentSelectedRosterPage == null)
			{
				// Get the species based on the current roster position
				Pokemons species = (Pokemons)PokemonSelect.CurrentSelectedRosterPosition + 1;

				// Check if the selected species has already been seen
				if (PokemonSelect.TempRentalPokemonObjects.ContainsKey(species))
				{
					Core.Logger.Log($"{species} is already seen!");

					//Remove the pokemon from list to prevent duplicates
					for (int i = 0; i < PokemonSelect.ViewedRentalPokemon.Count; i++)
					{
						Pokemons pkmn = PokemonSelect.ViewedRentalPokemon.Dequeue();
						if (pkmn == species)
							break;
						PokemonSelect.ViewedRentalPokemon.Enqueue(pkmn);
					}

					// Retrieve the Pokémon from the TempRentalPokemonObjects and remove it from the TempRentalPokemonObjects
					pokemon = PokemonSelect.TempRentalPokemonObjects[species];
					PokemonSelect.TempRentalPokemonObjects.Remove(species);
				}
				// If the selected species have not seen
				else
				{
					Core.Logger.Log($"{species} is not seen! Creating new Pokemon");

					// Create new Pokemon
					pokemon = new Pokemon(species, level: PokemonSelect.LevelFixed);
					// ToDo: Remove this when the DLLs updated to handle the null
					((Pokemon)pokemon).SetNickname(species.ToString());
				}

				// If the viewed rental Pokémon list is full, drop the oldest pokemon from the list
				if (PokemonSelect.ViewedRentalPokemon.Count == RentalViewCount)
					PokemonSelect.TempRentalPokemonObjects.Remove(PokemonSelect.ViewedRentalPokemon.Dequeue());

				// Add the selected or created Pokémon to the store and the viewed rental list
				PokemonSelect.TempRentalPokemonObjects[species] = pokemon;
				PokemonSelect.ViewedRentalPokemon.Enqueue(species); //Refresh to top of list
				
				// Print the current state of the viewed rental Pokémon list
				Core.Logger.Log("Current ViewedRentalPokemon: " + string.Join(", ", PokemonSelect.ViewedRentalPokemon.ToList()));
			}

			// ToDo: Handle another pages?

			// Refresh the Display
			RefreshHeaderDisplay();
			RefreshStatsDisplay();
			RefreshMoveSetDisplay();
		}
		
		public void RefreshHeaderDisplay()
		{
			Data.PkmnName.text = pokemon.Name;
			Data.Level.text = $"L {pokemon.Level}";
			Data.PkmnID.text = $"No.{(int)pokemon.Species:000}";
			Data.SpeciesName.text = pokemon.Species.ToString(TextScripts.Name);
		}

		public void RefreshStatsDisplay()
		{
			Data.PokemonSprite.sprite = RosterSelectionScene.IconSprites[(int)pokemon.Species];
			Data.Health.text		= pokemon.TotalHP.ToString();
			Data.Attack.text		= pokemon.ATK.ToString();
			Data.Defense.text		= pokemon.DEF.ToString();
			Data.Speed.text			= pokemon.SPE.ToString();
			Data.SpecialAtk.text	= pokemon.SPA.ToString();
			Data.SpecialDef.text	= pokemon.SPD.ToString();

			Data.Type1.sprite = RosterSelectionScene.PokemonTypes[(int)pokemon.Type1];

			// Not all Pokemons does have second type
			Data.Type2.sprite = pokemon.Type2 == PokemonUnity.Types.NONE ? null : RosterSelectionScene.PokemonTypes[(int)pokemon.Type2];
			Data.Type2.color = pokemon.Type2 == PokemonUnity.Types.NONE ? UnityEngine.Color.clear : UnityEngine.Color.white;
		}

		public void RefreshMoveSetDisplay()
		{
			//
			Data.MoveName1.text = ViewPokemonData.ReturnMoveName(pokemon.moves[0].id);
			Data.MoveName2.text = ViewPokemonData.ReturnMoveName(pokemon.moves[1].id);
			Data.MoveName3.text = ViewPokemonData.ReturnMoveName(pokemon.moves[2].id);
			Data.MoveName4.text = ViewPokemonData.ReturnMoveName(pokemon.moves[3].id);
			//
			Data.MoveColor1.color = ViewPokemonData.TypeToColor(pokemon.moves[0].Type);
			Data.MoveColor2.color = ViewPokemonData.TypeToColor(pokemon.moves[1].Type);
			Data.MoveColor3.color = ViewPokemonData.TypeToColor(pokemon.moves[2].Type);
			Data.MoveColor4.color = ViewPokemonData.TypeToColor(pokemon.moves[3].Type);
			//
			Data.MoveType1.text = ViewPokemonData.ReturnTypeLetter(pokemon.moves[0].Type);
			Data.MoveType2.text = ViewPokemonData.ReturnTypeLetter(pokemon.moves[1].Type);
			Data.MoveType3.text = ViewPokemonData.ReturnTypeLetter(pokemon.moves[2].Type);
			Data.MoveType4.text = ViewPokemonData.ReturnTypeLetter(pokemon.moves[3].Type);
		}

		public void ClearDisplay()
		{
			Data.PkmnName.text = null;
			Data.Level.text = null;
			Data.PkmnID.text = null;
			Data.SpeciesName.text = null;

			Data.PokemonSprite.sprite = null;
			Data.Health.text = null;
			Data.Attack.text = null;
			Data.Defense.text = null;
			Data.Speed.text = null;
			Data.SpecialAtk.text = null;
			Data.SpecialDef.text = null;
			Data.Type1.sprite = null;
			Data.Type2.sprite = null;

			Data.MoveName1.text = null;
			Data.MoveName2.text = null;
			Data.MoveName3.text = null;
			Data.MoveName4.text = null;
			Data.MoveColor1.sprite = null;
			Data.MoveColor2.sprite = null;
			Data.MoveColor3.sprite = null;
			Data.MoveColor4.sprite = null;
			Data.MoveType1.text = null;
			Data.MoveType2.text = null;
			Data.MoveType3.text = null;
			Data.MoveType4.text = null;
		}

		/// <summary>
		/// When a toggle button is pressed, close the modal
		/// </summary>
		/// <param name="arg0"></param>
		private void ToggleButtonSelected_Event(bool arg0)
		{
			Core.Logger.Log($"\"Register Pokemon?\" Toggle Button Pressed, value selected is [{(arg0 ? "YES" : "NO")}]!");
			if (!arg0)
			{
				ActiveGameObject(false);
				return;
			}

			bool result = PokemonSelect.RegisterSelectedPokemon();
			// ToDo: Fix this code and ensure PokemonSelect add to the party.
			RosterSelectionScene.AddPokemonToParty(PokemonSelect.CurrentSelectedPokemon, PokemonSelect.TemporaryParty.Count - 1);
			
			ActiveGameObject(false);
			if (result)
			{
				// ToDo: Remove this code. This is for demo purpose only
				RosterSelectionScene.GoToVersusParty();
			}
		}
		#endregion
	}
}