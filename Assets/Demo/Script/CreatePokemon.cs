using PokemonUnity;
using PokemonUnity.Monster;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class CreatePokemon : MonoBehaviour
{
    //Unity's UI of DropDown
    public Dropdown dropdown;
    //Unity's UI of Button
    public Button button;
    //Unity's UI of Text
    public Text Name;
    //Unity's UI of Text
    public Text Stats;
    //Unity's UI of Text
    public Text Move;
    //Unity's UI of InputField
    public InputField LevelInput;
    //Pokemons Enum into string[]
    public string[] PokemonDropList;
    [HideInInspector]
    //Pokemon
    public Pokemon pokemon = null;
    void Start()
    {
        //Call the DropList function
        InitDropList();
    }
    public void InitDropList()
    {
        //Convert from Enum to string[]
        PokemonDropList = Enum.GetNames(typeof(Pokemons));
        //From String[] to List
        List<string> name = new List<string>(PokemonDropList);
        //From List to DropDown
        dropdown.AddOptions(name);
    }
    public void PokemonCreated()
    {
        Debug.Log("Enums ID: " +dropdown.value);
        try
        {
            //Local Byte for Level
            byte LevelLocal = Core.EGGINITIALLEVEL;
            //Check for level
            if (Convert.ToByte(LevelInput.text.ToString()) > Core.MAXIMUMLEVEL)
            {
                Debug.Log("The Level is too over. The Max Level is " + Core.MAXIMUMLEVEL + ". Using Default level.");
            }
            //Check for level
            else if (Convert.ToInt32(LevelInput.text.ToString()) <= 0)
            {
                Debug.Log("The Level is too low. The Min Level is 1. Using Default Level.");
            }
            //If the check is passed then it is will using the InputValue
            else
            {
                LevelLocal = Convert.ToByte(LevelInput.text.ToString());
            }
            //Convert from Int to Enum
            Pokemons FromIntToEnum = (Pokemons)dropdown.value;
            //Create the Pokemon
            pokemon = new Pokemon(FromIntToEnum, LevelLocal ,isEgg: false);
            //Debug name
            Debug.Log("Debug Name: "+pokemon.Name);
            //Call the PokemonUI() function
            PokemonUI();
        }
        //Some Pokemons(Enum) is not for creating Pokemon();
        catch (KeyNotFoundException)
        {
            Debug.Log("Not in the Pokemon Database");
        }
    }
    public void PokemonUI()
    {
        //Use StringBuilder for Stats
        StringBuilder Stat = new StringBuilder();
        //Display Name in Unity's text
        Name.text = "Name : " + string.Format("{0}{1}", pokemon.Name[0], pokemon.Name.Substring(1).ToLowerInvariant());
        //StringBuilder Value
        Stat.AppendLine("Level : " + pokemon.Level);
        Stat.AppendLine("Health : " + pokemon.HP + "/" + pokemon.TotalHP);
        Stat.AppendLine("Attack : " + pokemon.ATK);
        Stat.AppendLine("Defense : " + pokemon.DEF);
        Stat.AppendLine("Speical Attack : " + pokemon.SPA);
        Stat.AppendLine("Speical Defense : " + pokemon.SPD);
        Stat.AppendLine("Speed : " + pokemon.SPE);
        //Display Stats in Unity's text
        Stats.text = Stat.ToString();
        //Reset the StringBuilder
        Stat.Length = 0;
        //StringBuilder Value
        Stat.AppendLine("Type: " + pokemon.Type1 + "/" + pokemon.Type2);
        Stat.AppendLine("Move 1: " + pokemon.moves[0].MoveId.ToString());
        Stat.AppendLine("Move 2: " + pokemon.moves[1].MoveId.ToString());
        Stat.AppendLine("Move 3: " + pokemon.moves[2].MoveId.ToString());
        Stat.AppendLine("Move 4: " + pokemon.moves[3].MoveId.ToString());
        //Display Value to Unity's text
        Move.text = Stat.ToString();
        //Clear the stringbuilder
        Stat.Length = 0;
    }
}
