using PokemonUnity;
using PokemonUnity.Character;
using PokemonUnity.Inventory;
using PokemonUnity.Monster;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class DemoPokemonParty : MonoBehaviour
{
    #region Unity Engine UI
    public Text Party1;
    public Text Party2;
    public Text Party3;
    public Text Party4;
    public Text Party5;
    public Text Party6;
    public Text PartyINFO;
    public Dropdown PokemonsEnum;
    #endregion
    #region Variable
    private int Current;
    private string[] PokemonDropList;
    private bool PermitClearPokemon;
    private bool PermitRandomStatusEffect;
    private bool PermitRandomItems;
    //StringBuilder
    StringBuilder stringBuilder = new StringBuilder();
    //Return Pokemon's Name
    string Party1Name { get { return !player.Party[0].IsNotNullOrNone() ? null: player.Party[0].Name; } }
    string Party2Name { get { return !player.Party[1].IsNotNullOrNone() ? null: player.Party[1].Name; } }
    string Party3Name { get { return !player.Party[2].IsNotNullOrNone() ? null: player.Party[2].Name; } }
    string Party4Name { get { return !player.Party[3].IsNotNullOrNone() ? null: player.Party[3].Name; } }
    string Party5Name { get { return !player.Party[4].IsNotNullOrNone() ? null: player.Party[4].Name; } }
    string Party6Name { get { return !player.Party[5].IsNotNullOrNone() ? null: player.Party[5].Name; } }
    //Use the Combat Trainer not Player because Player is obsolete.
    Player player;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //new Player()
        player = new Player();
        //Call the InitDemoParty()
        InitDemoParty();
        //Call the InitDropList()
        InitDropList();
        //Call the DisplayInfo(0) and auto selected First Party
        DisplayInfo(0);
    }

    private void InitDemoParty()
    {
        //Create the pokemon
        player.Party[0] = new Pokemon(Pokemons.ABOMASNOW, Convert.ToByte(UnityEngine.Random.Range(1, 100)), isEgg: false);
        //Call InitPartyUI()
        InitPartyUI();
        //Maunal Update the text
        PartyINFO.text = "";
    }
    #region For Unity UI
    public void DisplayInfo(int number)
    {
        if (!player.Party[number].IsNotNullOrNone()) //if party pokemon is empty (false)
        {
            Current = number;
            PermitClearPokemon = false;
            PermitRandomItems = false;
            PermitRandomStatusEffect = false;
            PartyINFO.text = "";
            Debug.Log("No Pokemon In Party");
            InitPartyUI();
        }
        else //If party pokemon is not empty and it is will display infomaction information
        {
            // Update the current Party Number
            Current = number;
            // Bool
            PermitClearPokemon = true;
            PermitRandomItems = true;
            PermitRandomStatusEffect = true;
            // StringBuilder
            stringBuilder.AppendLine("Name : " + player.Party[number].Name);
            stringBuilder.AppendLine("Level : " + player.Party[number].Level);
            stringBuilder.AppendLine(string.Format("Health : {0}/{1}", player.Party[number].HP, player.Party[number].TotalHP));
            stringBuilder.AppendLine("Status : " + player.Party[number].Status);
            stringBuilder.AppendLine("Item : " + player.Party[number].Item);
            //Display the Info
            PartyINFO.text = stringBuilder.ToString();
            //Clear stringBuilder
            stringBuilder.Length = 0;
            //Call the InitPartyUI()
            InitPartyUI();
        }
    }
    #region For Unity UI's Button. 
    public void ClearPokemon()
    {
        if (PermitClearPokemon) //If PermitClearPokemon true 
        {
            //Clear the pokemon to remove from the party
            player.Party[Current] = new Pokemon(Pokemons.NONE);
            //Call the function to display information
            DisplayInfo(Current);
        }
        else //If PermitClearPokemon false 
        {
            Debug.Log("It is already cleared!");
        }
    }
    public void InitDropList()
    {
        //Convert from Enum to string[]
        PokemonDropList = Enum.GetNames(typeof(Pokemons));
        //From String[] to List
        List<string> name = new List<string>(PokemonDropList);
        //From List to DropDown
        PokemonsEnum.AddOptions(name);
    }
    public void CreatePokemon()
    {
        //Convert from Int to Pokemons Enum
        Pokemons FromIntToEnum = (Pokemons)PokemonsEnum.value;
        //Update the Pokemon and random the level
        player.Party[Current] = new Pokemon(FromIntToEnum, Convert.ToByte(UnityEngine.Random.Range(1, 100)), isEgg: false);
        //Call the function to display information
        DisplayInfo(Current);
    }
    public void RandomStatusEffect()
    {
        if (PermitRandomStatusEffect) //If PermitRandomStatus true 
        {
            //Random Status using UnityEngine.Random.Range
            player.Party[Current].Status = (Status)UnityEngine.Random.Range(0, 6);
            //Call the function to display information
            DisplayInfo(Current);
        }
        else //If PermitRandomStatusEffect false 
        {
            Debug.Log("There no Pokemon to add Status Effect!");
        }
    }
    public void RandomAddItems()
    {
        if (PermitRandomItems) //If PermitRandomItems true 
        {
            //Random Items using UnityEngine.Random.Range
            player.Party[Current].setItem((Items)UnityEngine.Random.Range(0, 956));
            //Call the function to display information
            DisplayInfo(Current);
        }
        else //If PermitRandomItems false 
        {
            Debug.Log("There no Pokemon to add item!");
        }
    }
    #endregion
    public void InitPartyUI()
    {
        //Update The Party Name
        Party1.text = " Party 1: " + Party1Name;
        Party2.text = " Party 2: " + Party2Name;
        Party3.text = " Party 3: " + Party3Name;
        Party4.text = " Party 4: " + Party4Name;
        Party5.text = " Party 5: " + Party5Name;
        Party6.text = " Party 6: " + Party6Name;
    }
    #endregion
}
