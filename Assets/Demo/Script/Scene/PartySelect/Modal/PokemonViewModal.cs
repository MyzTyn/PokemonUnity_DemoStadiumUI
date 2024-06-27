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
			// ToDo: Check the toggle group code.
			yesButton.onValueChanged.AddListener(delegate {
				Debug.Log("Yes Button Pressed!");
				// Fix this bad code
				//PokemonSelect.CurrentSelectedPokemon = pokemon.Species;
				//MainCameraGameManager.Instance.AddToParty();
				PokemonSelect.RegisterSelectedPokemon(pokemon);
				CloseDisplayModal();
			});
			// Close it
			noButton.onValueChanged.AddListener(noButtonSelected_Event);
		}

		#region Methods
		public void ActiveGameobject(bool active)
		{
			IsWindowActive = active;
			gameObject.SetActive(active);
		}

		/// <summary>
		/// Refreshes the Pokemon view window that displays the active pokemon's stats and profile.
		/// </summary>
		/// <param name="pkmn">Pokemon to be displayed in UI</param>
		/// ToDo: pokemon should be passed as a parameter for this method, and use the object to assign values to display
		public void RefreshDisplay(IPokemon pkmn = null) //FIXME: Remove null overload
		{
			Debug.Assert(pkmn != null, "[PokemonViewModal] pkm is null!");
            pokemon = pkmn;
			if (PokemonSelect.IsRentalPokemon)
			{
				Pokemons species = Pokemons.NONE;
				if (MainCameraGameManager.ViewedRentalPokemon.Count == RentalViewCount)
				{
					species = MainCameraGameManager.ViewedRentalPokemon.Dequeue();
					if (MainCameraGameManager.StorePokemon.ContainsKey(species))
						MainCameraGameManager.StorePokemon.Remove(species);
				}
				species = pkmn.Species;

				if (!pkmn.IsNotNullOrNone()) 
					species = PokemonSelect.CurrentSelectedPokemon.Species; //PokemonSelect.Species;

				//This is interesting game design logic, but not sure if it should go here...
				if(MainCameraGameManager.ViewedRentalPokemon.Contains(species) &&
					MainCameraGameManager.StorePokemon.ContainsKey(species))
				{
					pokemon = MainCameraGameManager.StorePokemon[species];
				}
				else
				{
					if (pokemon.IsNotNullOrNone()) 
						pokemon = pkmn; 
					else //ToDo: if pokemon is not null then we can get rid of below
						pokemon = new Pokemon(species, PokemonSelect.LevelFixed, isEgg: false);

					MainCameraGameManager.StorePokemon.Add(species, pokemon);
					MainCameraGameManager.ViewedRentalPokemon.Enqueue(species);
				}
			}
			RefreshHeaderDisplay();
			RefreshStatsDisplay();
			RefreshMoveSetDisplay();
		}
		public void RefreshHeaderDisplay()
		{
			//Data.PkmnName.text = pokemon.Name;
			Data.PkmnName.text = pokemon.Species.ToString();
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
			//Data.SpecialDef.text	= pokemon.SPD.ToString();

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
			Data.MoveType1.text = ViewPokemonData.ReturnMoveFirstLetter(pokemon.moves[0].Type.ToString());
			Data.MoveType2.text = ViewPokemonData.ReturnMoveFirstLetter(pokemon.moves[1].Type.ToString());
			Data.MoveType3.text = ViewPokemonData.ReturnMoveFirstLetter(pokemon.moves[2].Type.ToString());
			Data.MoveType4.text = ViewPokemonData.ReturnMoveFirstLetter(pokemon.moves[3].Type.ToString());
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
			//Data.SpecialDef.text = null; //Logic can remain even if item isnt displayed
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
			ActiveGameobject(false);
			IsWindowActive = false;
		}

		/// <summary>
		/// When the no button is pressed, close the modal
		/// </summary>
		/// <param name="arg0"></param>
		private void noButtonSelected_Event(bool arg0)
		{
			Debug.Log("No Button Pressed!");
			CloseDisplayModal();
		}

		/// <summary>
		/// When a toggle button is pressed, close the modal
		/// </summary>
		/// <param name="arg0"></param>
		private void ToggleButtonSelected_Event(bool arg0)
		{
			if (arg0 == true)
				Debug.Log("\"Register Pokemon?\" Toggle Button Pressed, value selected is [YES]!");
			else
				Debug.Log("\"Register Pokemon?\" Toggle Button Pressed, value selected is [NO]!");
			CloseDisplayModal();
		}
		#endregion
	}
}