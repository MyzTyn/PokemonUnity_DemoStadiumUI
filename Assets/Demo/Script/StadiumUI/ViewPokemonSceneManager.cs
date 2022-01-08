using PokemonUnity;
using PokemonUnity.Character;
using PokemonUnity.Monster;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class ViewPokemonSceneManager : MonoBehaviour
{
	#region Variables
	public PokemonSelect PokemonSelect;

	[SerializeField] private PartyPanelUI partySelectionUI;
	[SerializeField] private MovesetUI movesetUI;
	[SerializeField] private GameObject partyEntryPrefab;
	[SerializeField] private Transform partyGridContent;
	[SerializeField] private ToggleGroup toggleGroup;
	
	//List
	private Dictionary<int, PartyEntryButton> PartyViewer;
	//Sprite
	public static Sprite[] PkmnType { get; private set; }
	public static Sprite[] IconSprites { get; private set; }
	#endregion
	#region Unity Monobehavior
	void Awake()
	{
		Debug.Log("Is Scriptable Object Null? " + (PokemonSelect == null).ToString());
		toggleGroup = GetComponent<ToggleGroup>();
		Debug.Log("Create Dictionary for Player Party UI Mono");
		PartyViewer = new Dictionary<int, PartyEntryButton>();
		//
		Debug.Log("Load Assets for UI into Array");
		IconSprites = Resources.LoadAll<Sprite>("PokemonIcon");
		PkmnType = Resources.LoadAll<Sprite>("PokemonType");

		if (Game.DatabasePath == @"Data Source=..\..\..\\veekun-pokedex.sqlite")
		{
			try
			{
				Game.DatabasePath = @"Data Source=..\..\\veekun-pokedex.sqlite";
				Game g = new Game();
				//Game.ResetSqlConnection();
				//Debug.Log("Path to DB: " + Game.con.FileName);
			}
			catch (InvalidOperationException) { Debug.LogError("problem connecting with database"); } //ignore...
			finally
			{
				//Game.con.Open();

				Debug.Log("Is Pokemon DB Null? " + (Game.PokemonData == null).ToString());
				if (Game.PokemonData == null)
				{
					//Game.InitPokemons();
					try
					{
						Game.InitTypes();
						Game.InitNatures();
						Game.InitPokemons();
						Game.InitPokemonForms();
						Game.InitPokemonMoves();
						//Game.InitPokemonEvolutions();
						Game.InitPokemonItems();
						Game.InitMoves();
						Game.InitItems();
						Game.InitBerries();
						Game.InitTrainers();
						//Game.InitRegions();
						//Game.InitLocations();
					}
					catch (Exception) { Debug.LogError("there were some problems running sql..."); } //ignore...
				}
				Debug.Log(string.Format("Is Pokemon DB Greater than 0? {0} : {1}", 
					(Game.PokemonData.Count > 0).ToString(), Game.PokemonData.Count));
				if (Game.PokemonData.Count == 0) 
					Debug.Log("Was Pokemon DB Successfully Created? " + Game.InitPokemons());
				Debug.Log(string.Format("Is Pokemon DB Greater than 0? {0} : {1}", 
					(Game.PokemonData.Count > 0).ToString(), Game.PokemonData.Count));
			}
		}

		Debug.Log("Is Game Null? " + (Game.GameData == null).ToString());
		Debug.Log("Is Player Null? " + (Game.GameData.Player == null).ToString());
		if(Game.GameData.Player == null)
		{
			Debug.Log("Create Player Object");
			Player p = new Player();
			Debug.Log("Saving Player Object to Global Singleton");
			Game.GameData.Player = p;
		}
		Debug.Log("Is Trainer Null? " + (Game.GameData.Player.Trainer == null).ToString());
	}

	void Start()
	{
		Debug.Log("Is Game Events Null? " + (GameEvents.current == null).ToString());
		GameEvents.current.onChangePartyLineup += Scene_onChangePartyLineup;

		//party.DisplayPartyUI();
		//party.GetPartyButton();
		SetPartyButton();
		Debug.Log("Trainer Id: " + Game.GameData.Player.Trainer.TrainerID.ToString());
		//Use ID but I will leave 00000 as Example
		partySelectionUI.SetTrainerID(Game.GameData.Player.Trainer.TrainerID);
	}
	void OnDestroy()
	{
		try
		{
			GameEvents.current.onChangePartyLineup -= Scene_onChangePartyLineup;
		}
		catch (NullReferenceException) { Debug.LogError("Calling destroy before event wakes up"); } //Ignore...
	}
	#endregion
	#region Methods
	//public void SelectToggle(int id)
	//{
	//	Toggle[] toggles = toggleGroup.GetComponentsInChildren<Toggle>();
	//	toggles[id].isOn = true;
	//}
	/*public void PokemonStatsandMoveUI(int Pkmn_ID)
	{
		PkmnSelected = Pkmn_ID;
		Debug.Log("Pressed! " + Pkmn_ID);
		if (!StorePokemon.ContainsKey((Pokemons)Pkmn_ID))
		{
			Debug.Log("Create Pokemon!");
			StorePokemon.Add((Pokemons)PkmnSelected, new Pokemon((Pokemons)PkmnSelected, PokemonSelect.LevelFixed, false));
		}
		else
			Debug.Log("Pokemon is already created!");
		DisplayMoveSetUI();
	}
	void DisplayMoveSetUI()
	{
		movesetUI.ActiveGameobject(true);
		movesetUI.DisplayPkmnStats();
	}
	public void AddToParty()
	{
		if (PokemonSelect.CurrentSelectedPokemon == 0)
		{
			Debug.Log("Error. There no Pokemon!");
		}
		else
		{
			if (PokemonSelect.CurrentSelectedPartySlot >= 0 && PokemonSelect.CurrentSelectedPartySlot < Core.MAXPARTYSIZE)
			{
				StoreButtonData[PokemonSelect.CurrentSelectedPartySlot].DisableOnClick(true);
				Game.GameData.Player.Party[PokemonSelect.CurrentSelectedPartySlot] = new Pokemon((Pokemons)PkmnSelected, PokemonSelect.LevelFixed, false);
				//PartyViewer[CurrentOnParty].DisplayPartyButton();
				PartyViewer[PokemonSelect.CurrentSelectedPartySlot].SetDisplay(); //pkmn.Name, pkmn.Species, pkmn.Level);
				PartyViewer[PokemonSelect.CurrentSelectedPartySlot].ActivePokemonDisplay(true);
				//CurrentOnParty += 1;
				//Ask player if they're done and wish to move on; but in another function...
				//if (Game.GameData.Player.Party[5].IsNotNullOrNone())
				//{
				//	Debug.Log("Disable the UI");
				//	RentalControlUI.ActiveRentalUI(false);
				//}
			}
			movesetUI.CancelUI();
		}
	}*/
	#endregion
	#region Party Roster UI
	public void SetPartyButton()
	{
		//if (ID.Count < 4)
		//{
		//	gridGroup.constraintCount = ID.Count;
		//}
		//else
		//{
		//	gridGroup.constraintCount = 3;
		//}
		for (int Id = 0; Id < Game.GameData.Features.LimitPokemonPartySize && Id < Core.MAXPARTYSIZE; Id++)
		{
			//if (Id == Core.MAXPARTYSIZE) break;
			GameObject Button = Instantiate(partyEntryPrefab);
			PartyEntryButton slot = Button.GetComponent<PartyEntryButton>();
			slot.PokemonSelect = PokemonSelect; //Should be duplicated for each player controller on screen
			//PartyData(Id, slot);
			PartyViewer.Add(Id, slot);
			//Button.GetComponent<PartyEntryButton>().ActivePartyUIButton(true);
			//Button.GetComponent<PartyEntryButton>().ActivePokemonDisplay(false);
			slot.ActivePartyUIButton(true);
			slot.ActivePokemonDisplay(false);
			//Button.transform.SetParent(partyEntryPrefab.transform.parent, false);
			Button.transform.SetParent(partyGridContent, false);
		}
	}

	private void Scene_onChangePartyLineup()
	{
		Game.GameData.Player.Party.PackParty();
		foreach (PartyEntryButton item in PartyViewer.Values)
		{
			if (Game.GameData.Player.Party[item.PartySlot].IsNotNullOrNone())
			{
				//Game.GameData.Player.Party[item.PartySlot] = new Pokemon((Pokemons)PkmnSelected, LevelFixed, false);
				PartyViewer[item.PartySlot].SetDisplay(); //pkmn.Name, pkmn.Species, pkmn.Level);
				//StoreButtonData[item.PartySlot].DisableOnClick(true);
				PartyViewer[item.PartySlot].ActivePokemonDisplay(true);
			}
			else 
			{
				//Game.GameData.Player.Party[item.PartySlot] = new Pokemon((Pokemons)PkmnSelected, LevelFixed, false);
				PartyViewer[item.PartySlot].ActivePokemonDisplay(false);
				PartyViewer[item.PartySlot].SetDisplay(); //pkmn.Name, pkmn.Species, pkmn.Level);
				//StoreButtonData[item.PartySlot].DisableOnClick(true);
			}
		}
	}
	#endregion
}