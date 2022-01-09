using PokemonUnity;
using PokemonUnity.Monster;
using UnityEngine;

public class SelectedPokemonViewModal : MonoBehaviour
{
	#region 
	[SerializeField] private GameObject MoveSetUIObject;
	[SerializeField] private ViewPokemonData Data;
	[SerializeField] private int RentalViewCount = 5;

	private Pokemon pokemon;
	public PokemonSelect PokemonSelect;
	public bool IsWindowActive { get; private set; }
	#endregion

	#region 
	public void ActiveGameobject(bool active)
	{
		IsWindowActive = active;
		MoveSetUIObject.SetActive(active);
	}
	public void DisplayPkmnStats()
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
		Display_ID_UI();
		Display_PkmnInfo_UI();
		Display_Move_Set_UI();
	}
	private void Display_ID_UI()
	{
		Data.PkmnName.text = pokemon.Name;
		Data.Level.text = "L " + pokemon.Level;
		Data.PkmnID.text = "No." + string.Format("{0:000}", (int)Game.PokemonFormsData[pokemon.Species][pokemon.FormId].Base);
		Data.Species_Name.text = pokemon.Species.ToString(TextScripts.Name);
	}
	private void Display_PkmnInfo_UI()
	{
		Data.PokemonSprite.sprite = MainCameraGameManager.IconSprites[(int)pokemon.Species];
		Data.Health.text = "HP   " + pokemon.TotalHP;
		Data.Attack.text = "Attack    " + pokemon.ATK;
		Data.Defense.text = "Defense " + pokemon.DEF;
		Data.Speed.text = "Speed    " + pokemon.SPE;
		Data.SpecialAtk.text = "Special  " + pokemon.SPA;
		Data.SpecialDef.text = "Special  " + pokemon.SPD;
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
	private void Display_Move_Set_UI()
	{

		//
		Data.MoveName1.text = ViewPokemonData.ReturnMoveName(pokemon.moves[0].MoveId);
		Data.MoveName2.text = ViewPokemonData.ReturnMoveName(pokemon.moves[1].MoveId);
		Data.MoveName3.text = ViewPokemonData.ReturnMoveName(pokemon.moves[2].MoveId);
		Data.MoveName4.text = ViewPokemonData.ReturnMoveName(pokemon.moves[3].MoveId);
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
	private void ClearDataDisplay()
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
	public void CancelUI()
	{
		ClearDataDisplay();
		ActiveGameobject(false);
		IsWindowActive = false;
	}
	#endregion
}
