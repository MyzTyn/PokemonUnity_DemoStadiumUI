using PokemonUnity;
using PokemonUnity.Attack;
using PokemonUnity.Attack.Data;
using PokemonUnity.Monster;
using System;
using System.Net.NetworkInformation;
using UnityEngine;

public class MovesetUI : MonoBehaviour
{
    [SerializeField]
    private GameObject MoveSetUIObject;
    [SerializeField]
    private MovesetData Data;
    [SerializeField]
    private DemoStadiumManager demo;
    private Pokemon pokemon;
    public bool IsWindowActive { get; private set; } = false;
    public void ActiveGameobject(bool active)
    {
        IsWindowActive = true;
        MoveSetUIObject.SetActive(active);
    }
    public void DisplayPkmnStats(Pokemon pokemon)
    {
        this.pokemon = pokemon;
        Display_ID_UI();
        Display_PkmnInfo_UI();
        Display_Move_Set_UI();
    }
    private void Display_ID_UI()
    {
        Data.PkmnName.text = pokemon.Name;
        Data.Level.text = "L "+pokemon.Level;
        Data.PkmnID.text = "No." + string.Format("{0:000}", (int)pokemon.Species);
        Data.Species_Name.text = pokemon.Species.ToString();
    }
    private void Display_PkmnInfo_UI()
    {
        Data.PokemonSprite.sprite = demo.iconSprites[(int)pokemon.Species];
        Data.Health.text = "HP   " + pokemon.TotalHP;
        Data.Attack.text = "Attack    " + pokemon.ATK;
        Data.Defense.text = "Defense " + pokemon.DEF;
        Data.Speed.text = "Speed    " + pokemon.SPE;
        Data.Special.text = "Special  " + pokemon.SPA;
        if (pokemon.Type2 == PokemonUnity.Types.NONE)
        {
            Data.Type1.sprite = demo.pkmnType[(int)pokemon.Type1];
            Data.Type2.sprite = null;
            Data.Type2.color = UnityEngine.Color.clear;
        }
        else
        {
            Data.Type2.color = UnityEngine.Color.white;
            Data.Type1.sprite = demo.pkmnType[(int)pokemon.Type1];
            Data.Type2.sprite = demo.pkmnType[(int)pokemon.Type2];
        }
    }
    private void Display_Move_Set_UI()
    {
        
        //
        Data.Move_1.text = MovesetData.ReturnMoveName(pokemon.moves[0].MoveId);
        Data.Move_2.text = MovesetData.ReturnMoveName(pokemon.moves[1].MoveId);
        Data.Move_3.text = MovesetData.ReturnMoveName(pokemon.moves[2].MoveId);
        Data.Move_4.text = MovesetData.ReturnMoveName(pokemon.moves[3].MoveId);
        //
        Data.Move_1Type.color = MovesetData.TypeToColor(pokemon.moves[0].Type);
        Data.Move_2Type.color = MovesetData.TypeToColor(pokemon.moves[1].Type);
        Data.Move_3Type.color = MovesetData.TypeToColor(pokemon.moves[2].Type);
        Data.Move_4Type.color = MovesetData.TypeToColor(pokemon.moves[3].Type);
        //
        Data.TypeMove1.text = MovesetData.ReturnMoveFirstLetter(pokemon.moves[0].Type.ToString());
        Data.TypeMove2.text = MovesetData.ReturnMoveFirstLetter(pokemon.moves[1].Type.ToString());
        Data.TypeMove3.text = MovesetData.ReturnMoveFirstLetter(pokemon.moves[2].Type.ToString());
        Data.TypeMove4.text = MovesetData.ReturnMoveFirstLetter(pokemon.moves[3].Type.ToString());
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
        Data.Special.text = null;
        Data.Type1.sprite = null;
        Data.Type2.sprite = null;

        Data.Move_1.text = null;
        Data.Move_2.text = null;
        Data.Move_3.text = null;
        Data.Move_4.text = null;
        Data.Move_1Type.sprite = null;
        Data.Move_2Type.sprite = null;
        Data.Move_3Type.sprite = null;
        Data.Move_4Type.sprite = null;
        Data.TypeMove1.text = null;
        Data.TypeMove2.text = null;
        Data.TypeMove3.text = null;
        Data.TypeMove4.text = null;
    }
    public void CancelUI()
    {
        ClearDataDisplay();
        ActiveGameobject(false);
        IsWindowActive = false;
    }
    
}
