using PokemonUnity;
using PokemonUnity.Monster;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonUnity.Stadium
{
	public class PokemonViewModal : MonoBehaviour
	{
		#region 
		[SerializeField] private GameObject MoveSetUIObject;
		[SerializeField] private ViewPokemonData Data;
		[SerializeField] private int RentalViewCount = 5;

		private Pokemon pokemon;
		public PokemonSelect PokemonSelect;
		/// <summary>
		/// When you press a pokemon button in scroll window, 
		/// pass the button into the view modal, 
		/// and assign it as selected if chosen by user
		/// </summary>
		public Toggle selectedPokemon;
		public bool IsWindowActive { get; private set; }
		#endregion

		#region 
		public void ActiveGameobject(bool active)
		{
			IsWindowActive = active;
			MoveSetUIObject.SetActive(active);
		}
		public void RefreshDisplay()
		{
			if (PokemonSelect.IsRentalPokemon)
			{
				if (MainCameraGameManager.ViewedRentalPokemon.Count == RentalViewCount)
				{
					Pokemons pkmn = MainCameraGameManager.ViewedRentalPokemon.Dequeue();
					if (MainCameraGameManager.StorePokemon.ContainsKey(pkmn)) 
						MainCameraGameManager.StorePokemon.Remove(pkmn);
				}
				if(MainCameraGameManager.ViewedRentalPokemon.Contains(PokemonSelect.Species) && 
					MainCameraGameManager.StorePokemon.ContainsKey(PokemonSelect.Species))
				{
					pokemon = MainCameraGameManager.StorePokemon[PokemonSelect.Species];
				}
				else
				{
					pokemon = new Pokemon(PokemonSelect.Species, PokemonSelect.LevelFixed, isEgg: false);
					MainCameraGameManager.StorePokemon.Add(PokemonSelect.Species, pokemon);
					MainCameraGameManager.ViewedRentalPokemon.Enqueue(PokemonSelect.Species);
				}
			}
			RefreshHeaderDisplay();
			RefreshStatsDisplay();
			RefreshMoveSetDisplay();
		}
		public void RefreshHeaderDisplay()
		{
			Data.PkmnName.text = pokemon.Name;
			Data.Level.text = "L " + pokemon.Level;
			Data.PkmnID.text = "No." + string.Format("{0:000}", (int)Kernal.PokemonFormsData[pokemon.Species][pokemon.FormId].Base);
			Data.Species_Name.text = pokemon.Species.ToString(TextScripts.Name);
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
			if (pokemon.Type2 == PokemonUnity.Types.NONE)
			{
				Data.Type1.sprite = MainCameraGameManager.PkmnType[(int)pokemon.Type1];
				Data.Type2.sprite = null;
				Data.Type2.color = UnityEngine.Color.clear;
			}
			else
			{
				Data.Type2.color = UnityEngine.Color.white;
				Data.Type1.sprite = MainCameraGameManager.PkmnType[(int)pokemon.Type1];
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
			Data.Species_Name.text = null;

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
			ActiveGameobject(false);
			IsWindowActive = false;
		}
		#endregion
	}
}