using PokemonEssentials.Interface.PokeBattle;
using PokemonUnity;
using PokemonUnity.Monster;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonUnity.Stadium
{
	public class PokemonViewModal : MonoBehaviour
	{
		#region Variables
		[SerializeField] private ViewPokemonData Data;
		// ToDo: Fix this; Rename to: IsYesButtonPressed or IsPokemonSelected/Registered
		[SerializeField] private Toggle ToggleButtonPrompt; //Do we need two for a yes/no (true/false) bool?
		[SerializeField] private Toggle yesButton; //Do we need two for a yes/no (true/false) bool?
		[SerializeField] private Toggle noButton;
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
			// Bug: At start of the scene this button will pressed if no button pressed after that it is normal
			// Possible Fix: Start with object disabled, then enable it after the scene is loaded
			// ToDo: Check the toggle group code; remove "Yes/No" buttons, and use the toggle group to select pokemon
			ToggleButtonPrompt.onValueChanged.AddListener(ToggleButtonSelected_Event);
			yesButton.onValueChanged.AddListener(ToggleButtonSelected_Event);
			noButton.onValueChanged.AddListener(ToggleButtonSelected_Event);
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
		public void RefreshDisplay() //FIXME: Remove null overload
		{
			if(pokemon != null) pokemon = PokemonSelect.CurrentSelectedPokemon;
				//Core.Logger.LogError("[PokemonViewModal] pkm is null!");
			if (PokemonSelect.CurrentSelectedRosterPage == null || PokemonSelect.IsRentalPokemon) //Maybe dont need "IsRental"?...
			{
				Pokemons species = species = PokemonSelect.CurrentSelectedPokemon.Species;
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
					if (!pokemon.IsNotNullOrNone())
					//	pokemon = pkmn;
					//else //ToDo: if pokemon is not null then we can get rid of below
						pokemon = new Pokemon(species, PokemonSelect.LevelFixed, isEgg: false);

					PokemonSelect.StorePokemon.Add(species, pokemon);
					PokemonSelect.ViewedRentalPokemon.Enqueue(species); //Refresh to top of list
				}
			}
			RefreshHeaderDisplay();
			RefreshStatsDisplay();
			RefreshMoveSetDisplay();
		}
		public void RefreshHeaderDisplay()
		{
			Data.PkmnName.text = pokemon.Name;
			//Data.PkmnName.text = pokemon.Species.ToString();
			Data.Level.text = "L " + pokemon.Level;
			//Data.PkmnID.text = "No." + string.Format("{0:000}", (int)Kernal.PokemonFormsData[pokemon.Species][(pokemon as Pokemon).FormId].Base); //Why are we using Form to lookup data?
			//Data.PkmnID.text = "No." + string.Format("{0:000}", (int)Kernal.PokemonData[pokemon.Species].ID); //Refactored above; Why are we performing a lookup in dictionary for pokemon Id?
			Data.PkmnID.text = "No." + string.Format("{0:000}", (int)pokemon.Species); //All 3 lines perform the same action and refer to the same value
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
			// Use Null handing? No, just dont display (leave unity inspector toggle to determine if used)
			Data.SpecialDef.text	= pokemon.SPD.ToString();

			Data.Type1.sprite = MainCameraGameManager.PkmnType[(int)pokemon.Type1];
			if (pokemon.Type2 == PokemonUnity.Types.NONE)
			{
				Data.Type2.sprite = null;
				Data.Type2.color = UnityEngine.Color.clear;
			}
			else
			{
				Data.Type2.color = UnityEngine.Color.white;
				Data.Type2.sprite = MainCameraGameManager.PkmnType[(int)pokemon.Type2];
			}
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
			Data.SpecialDef.text = null; //Logic can remain even if item isnt displayed
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
			if (arg0 == true)
			{
				Core.Logger.Log("\"Register Pokemon?\" Toggle Button Pressed, value selected is [YES]!");
				//PokemonSelect.CurrentSelectedPokemon = pokemon.Species;
				//MainCameraGameManager.Instance.AddToParty();
				bool result = PokemonSelect.RegisterSelectedPokemon();
				CloseDisplayModal();
				if (result)
					MainCameraGameManager.Instance.ShowVersusPartyUI();
			}
			else
				Core.Logger.Log("\"Register Pokemon?\" Toggle Button Pressed, value selected is [NO]!");
			CloseDisplayModal();
		}
		#endregion
	}
}