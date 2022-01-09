using PokemonUnity;
using PokemonUnity.Monster;
using UnityEngine;

public class MovesetUI : MonoBehaviour
{
	#region 
	[SerializeField] private GameObject MoveSetUIObject;
	[SerializeField] private MovesetData Data;
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
			if (DemoStadiumManager.ViewedRentalPokemon.Count == RentalViewCount)
			{
				Pokemons pkmn = DemoStadiumManager.ViewedRentalPokemon.Dequeue();
				if (DemoStadiumManager.StorePokemon.ContainsKey(pkmn)) 
					DemoStadiumManager.StorePokemon.Remove(pkmn);
			}
			if(DemoStadiumManager.ViewedRentalPokemon.Contains(PokemonSelect.Species) && 
				DemoStadiumManager.StorePokemon.ContainsKey(PokemonSelect.Species))
			{
				pokemon = DemoStadiumManager.StorePokemon[PokemonSelect.Species];
			}
			else
			{
				pokemon = new Pokemon(PokemonSelect.Species, PokemonSelect.LevelFixed, isEgg: false);
				DemoStadiumManager.StorePokemon.Add(PokemonSelect.Species, pokemon);
				DemoStadiumManager.ViewedRentalPokemon.Enqueue(PokemonSelect.Species);
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
		Data.PokemonSprite.sprite = DemoStadiumManager.IconSprites[(int)pokemon.Species];
		Data.Health.text = "HP   " + pokemon.TotalHP;
		Data.Attack.text = "Attack    " + pokemon.ATK;
		Data.Defense.text = "Defense " + pokemon.DEF;
		Data.Speed.text = "Speed    " + pokemon.SPE;
		Data.SpecialAtk.text = "Special  " + pokemon.SPA;
		if (pokemon.Type2 == PokemonUnity.Types.NONE)
		{
			Data.Type1.sprite = DemoStadiumManager.PkmnType[(int)pokemon.Type1];
			Data.Type2.sprite = null;
			Data.Type2.color = UnityEngine.Color.clear;
		}
		else
		{
			Data.Type2.color = UnityEngine.Color.white;
			Data.Type1.sprite = DemoStadiumManager.PkmnType[(int)pokemon.Type1];
			Data.Type2.sprite = DemoStadiumManager.PkmnType[(int)pokemon.Type2];
		}
	}
	private void Display_Move_Set_UI()
	{

		//
		Data.MoveName1.text = MovesetData.ReturnMoveName(pokemon.moves[0].MoveId);
		Data.MoveName2.text = MovesetData.ReturnMoveName(pokemon.moves[1].MoveId);
		Data.MoveName3.text = MovesetData.ReturnMoveName(pokemon.moves[2].MoveId);
		Data.MoveName4.text = MovesetData.ReturnMoveName(pokemon.moves[3].MoveId);
		//
		Data.MoveColor1.color = MovesetData.TypeToColor(pokemon.moves[0].Type);
		Data.MoveColor2.color = MovesetData.TypeToColor(pokemon.moves[1].Type);
		Data.MoveColor3.color = MovesetData.TypeToColor(pokemon.moves[2].Type);
		Data.MoveColor4.color = MovesetData.TypeToColor(pokemon.moves[3].Type);
		//
		Data.MoveType1.text = MovesetData.ReturnMoveFirstLetter(pokemon.moves[0].Type.ToString());
		Data.MoveType2.text = MovesetData.ReturnMoveFirstLetter(pokemon.moves[1].Type.ToString());
		Data.MoveType3.text = MovesetData.ReturnMoveFirstLetter(pokemon.moves[2].Type.ToString());
		Data.MoveType4.text = MovesetData.ReturnMoveFirstLetter(pokemon.moves[3].Type.ToString());
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
