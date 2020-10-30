using PokemonUnity;
using PokemonUnity.Character;
using PokemonUnity.Monster;
using System.Collections.Generic;
using UnityEngine;

public class DemoStadiumManager : MonoBehaviour
{


    static public byte LevelFixed = 50;
    private int CurrentOnParty;
    static public int PkmnSelected { get; private set; } = 0;

    private Player player;
    //
    [SerializeField]
    private StadiumRentalUI RentalControlUI;
    [SerializeField]
    private PartyControlUI party;
    [SerializeField]
    private MovesetUI movesetUI;
    //
    //List
    private Dictionary<int, PartyButton> PartyViewer;
    static public Dictionary<int, Pokemon> StorePokemon { get; private set; }
    private Dictionary<int, StadiumRentalUIButton> StoreButtonData;
    //Sprite
    static public Sprite[] IconSprites { get; private set; }
    static public Sprite[] PkmnType { get; private set; }
    void Start()
    {
        PartyViewer = new Dictionary<int, PartyButton>();
        StorePokemon = new Dictionary<int, Pokemon>();
        StoreButtonData = new Dictionary<int, StadiumRentalUIButton>();
        //
        IconSprites = Resources.LoadAll<Sprite>("PokemonIcon");
        PkmnType = Resources.LoadAll<Sprite>("PokemonType");
        //
        CurrentOnParty = 0;
        player = new Player();
        //
        RentalControlUI.DisplayPokemonSelectUI();
        party.DisplayPartyUI();
        //Use ID but I will leave 00000 as Example
        party.SetTrainerID(00000);
    }
    public void PokemonStatsandMoveUI(int Pkmn_ID)
    {
        PkmnSelected = Pkmn_ID;
        Debug.Log("Pressed! " + Pkmn_ID);
        if (!StorePokemon.ContainsKey(Pkmn_ID))
        {
            StorePokemon.Add(PkmnSelected, new Pokemon((Pokemons)PkmnSelected, LevelFixed, false));
            Debug.Log("Create Pokemon!");
            DisplayMoveSetUI();
        }
        else
        {
            Debug.Log("It already created!");
            DisplayMoveSetUI();
        }
    }
    void DisplayMoveSetUI()
    {
        movesetUI.ActiveGameobject(true);
        movesetUI.DisplayPkmnStats();
    }
    public void AddToParty()
    {
        if (PkmnSelected == 0)
        {
            Debug.Log("Error. There no Pokemon!");
        }
        else
        {
            if (CurrentOnParty <= 5)
            {
                StoreButtonData[PkmnSelected].DisableOnClick(true);
                player.Party[CurrentOnParty] = new Pokemon((Pokemons)PkmnSelected, LevelFixed, false);
                PartyViewer[CurrentOnParty].DisplayPartyButton();
                PartyViewer[CurrentOnParty].ActivePokemonDisplay(true);
                CurrentOnParty += 1;
                if (player.Party[5].IsNotNullOrNone())
                {
                    Debug.Log("Disable the UI");
                    RentalControlUI.ActiveRentalUI(false);
                }
            }
            movesetUI.CancelUI();
        }
    }
    public void PartyData(int id, PartyButton partybutton)
    {
        PartyViewer.Add(id, partybutton);
    }
    public void RentalData(int id, StadiumRentalUIButton button)
    {
        StoreButtonData.Add(id, button);
    }
    public void ClearParty()
    {
        if (!movesetUI.IsWindowActive)
        {
            CurrentOnParty = 0;
            player = new Player();
            StorePokemon = new Dictionary<int, Pokemon>();
            for (int i = 0; i <= 5; i++)
            {
                PartyViewer[i].Clear();
                PartyViewer[i].ActivePokemonDisplay(false);
            }
            for (int i = 1; i <= 151; i++)
            {

                StoreButtonData[i].DisableOnClick(false);
            }
            RentalControlUI.ActiveRentalUI(true);
        }
        else
        {
            Debug.Log("Can't Clear! You need to close MoveSet UI.");
        }
    }
}
