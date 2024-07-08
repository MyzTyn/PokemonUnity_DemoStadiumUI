using PokemonEssentials.Interface.PokeBattle;
using PokemonUnity;
using PokemonUnity.Monster;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonUnity.Stadium
{
	public class PokemonViewModal : MonoBehaviour
	{
        #region Variables
        // ToDo: Remove this code. For now, I am using it to show the VersusParty UI (Then Battle). But it shouldn't be in MainCameraGame
        [SerializeField] private MainCameraGameManager mainCameraGameManager;

		[SerializeField] private TrainerPartyPanel partyPanel;

		[SerializeField] private ViewPokemonData Data;
		// ToDo: No Button bug. Only if you click two times at sequence (No, No -> won't close the modal)
		[SerializeField] private Toggle IsPokemonSelected;
		[SerializeField] private int RentalViewCount = 5;

		private IPokemon pokemon;
		public PokemonSelect PokemonSelect;
		/// <summary>
		/// When you press a pokemon button in scroll window,
		/// pass the button into the view modal,
		/// and assign it as selected if chosen by user
		/// </summary>
		public Toggle selectedPokemon;
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
			IsWindowActive = active;
			gameObject.SetActive(active);
		}

		/// <summary>
		/// Refreshes the Pokemon view window that displays the active pokemon's stats and profile.
		/// </summary>
		/// <param name="pkmn">Pokemon to be displayed in UI</param>
		/// ToDo: pokemon should be passed as a parameter for this method, and use the object to assign values to display
		public void RefreshDisplay()
		{
			// ToDo: Is this need?
			if(pokemon != null)
				pokemon = PokemonSelect.CurrentSelectedPokemon;

			if (PokemonSelect.CurrentSelectedRosterPage == null)
			{
				Pokemons species = PokemonSelect.CurrentSelectedPokemon.Species;
				if (PokemonSelect.StorePokemon.ContainsKey(species)) //if selected pokemon is the same as already seen
				{
					//Remove the pokemon from list to prevent duplicates
					Pokemons pkmn = Pokemons.NONE;
					for (int i = 0; i < PokemonSelect.ViewedRentalPokemon.Count; i++)
					{
						pkmn = PokemonSelect.ViewedRentalPokemon.Dequeue();
						if (pkmn == species)
							break;
						PokemonSelect.ViewedRentalPokemon.Enqueue(pkmn);
					}
					pokemon = PokemonSelect.StorePokemon[species];
					PokemonSelect.StorePokemon.Remove(species);
				}
				if (PokemonSelect.ViewedRentalPokemon.Count == RentalViewCount) //If the list is full
				{
					//Remove the oldest pokemon from the list
					PokemonSelect.ViewedRentalPokemon.Dequeue(); //Drop the last from the list
				}

				//if (!pkmn.IsNotNullOrNone())
				//	species = PokemonSelect.CurrentSelectedPokemon.Species; //PokemonSelect.Species;

				//This is interesting game design logic, but not sure if it should go here...
				//if(PokemonSelect.ViewedRentalPokemon.Contains(species) &&
				//	PokemonSelect.StorePokemon.ContainsKey(species))
				//{
				//	pokemon = PokemonSelect.StorePokemon[species];
				//}
				//else
				{
					// ToDo: Clean this up
					//if (!pokemon.IsNotNullOrNone())
					//	pokemon = pkmn;
					//else //ToDo: if pokemon is not null then we can get rid of below
						//pokemon = new Pokemon(species, PokemonSelect.LevelFixed, isEgg: false);
					if (pokemon == null)
						pokemon = PokemonSelect.CurrentSelectedPokemon;
					//if (!pokemon.IsNotNullOrNone()) //if pokemon IS null or none... then create a new pokemon using the species
					//	pokemon = new Pokemon(species, PokemonSelect.LevelFixed, isEgg: false);

					PokemonSelect.StorePokemon.Add(species, pokemon);
					PokemonSelect.ViewedRentalPokemon.Enqueue(species); //Refresh to top of list
					//if (pokemon == null)
					//	pokemon = PokemonSelect.CurrentSelectedPokemon;
				}
			}
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
			Data.PokemonSprite.sprite = MainCameraGameManager.IconSprites[(int)pokemon.Species];
			Data.Health.text		= pokemon.TotalHP.ToString();
			Data.Attack.text		= pokemon.ATK.ToString();
			Data.Defense.text		= pokemon.DEF.ToString();
			Data.Speed.text			= pokemon.SPE.ToString();
			Data.SpecialAtk.text	= pokemon.SPA.ToString();
			Data.SpecialDef.text	= pokemon.SPD.ToString();

			Data.Type1.sprite = MainCameraGameManager.PkmnType[(int)pokemon.Type1];

			// Not all Pokemons does have second type
			Data.Type2.sprite = pokemon.Type2 == PokemonUnity.Types.NONE ? null : MainCameraGameManager.PkmnType[(int)pokemon.Type2];
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

		public void CloseDisplayModal()
		{
			ClearDisplay();
			ActiveGameObject(false);
			IsWindowActive = false;
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
				CloseDisplayModal();
				return;
			}

            bool result = PokemonSelect.RegisterSelectedPokemon();
			// ToDo: Fix this code and ensure PokemonSelect add to the party.
			partyPanel.AddPokemonToParty(PokemonSelect.CurrentSelectedPokemon, PokemonSelect.TemporaryParty.Count - 1);

            CloseDisplayModal();
            if (result)
				// ToDo: Remove this!
                mainCameraGameManager.ShowVersusPartyUI();
        }
		#endregion
	}
}