using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using PokemonUnity;
using PokemonUnity.Character;
using PokemonUnity.Monster;
using UnityEngine;
using UnityEngine.UI;
using PokemonEssentials.Interface.PokeBattle;

namespace PokemonUnity.Stadium
{
	/// <summary>
	/// </summary>
	//[ExecuteInEditMode]
	public class MainCameraGameManager : MonoBehaviour
	{
		#region Variables
		public float CurrentSrollPosition { get { return scrollBar.value; } set { scrollBar.value = value; } }
		public PokemonSelect PokemonSelect;
		public Scrollbar scrollBar;
		public GameObject cursorSelectedItem;
		/// <summary>
		/// Player slot for currently active trainer
		/// </summary>
		public int trainerIndex;

		public PokemonViewModal pokemonViewModal;
		public TrainerPartyPanel partyPanel;
		//[SerializeField] private PokemonViewModal pokemonViewModal;
		[SerializeField] private SelectPokemonButton pokemonButton;
		[SerializeField] private Transform pokemonListPanel;
		[SerializeField] private GameObject pageTabPrefab;
		[SerializeField] private GameObject rosterEntryPrefab;
		[SerializeField] private GameObject partyEntryPrefab;
		[SerializeField] private Transform rosterGridContent;
		[SerializeField] private Transform tabGridContent;
		[SerializeField] private ToggleGroup toggleGroup;

		//List
		private Dictionary<int, TrainerPokemonButton> PartyViewer;
		private Dictionary<int, SelectPokemonButton> StoreButtonData;
		//Sprite
		public static UnityEngine.Sprite[] PkmnType { get; private set; }
		public static UnityEngine.Sprite[] IconSprites { get; private set; }
		public static Dictionary<Pokemons, IPokemon> StorePokemon { get; private set; } //FIXME: Why is this a key-value dictionary? Why not a hashset of pokemons to prevent duplicates? What is the Key for?
		//ToDo: Move this to ScriptableObject?...
		public static HashSet<KeyValuePair<KeyValuePair<bool,int?>, int>> SelectedPokemons { get; private set; }
		public static Queue<Pokemons> ViewedRentalPokemon { get; private set; }
		//public static int PkmnSelected { get; private set; }
		//public Toggle Toggle { get { return toggleGroup.ActiveToggles().FirstOrDefault(); } }
		//public int CurrentOnParty
		//{
		//	get
		//	{
		//		//return Game.GameData.Trainer.party.
		//		//TrainerPokemonButton party = PartyViewer
		//		//	.Values
		//		//	.SingleOrDefault(x => x.IsSelected);
		//		//	//.Select(x => x.partyIndex);
		//		//return party != null ? party.partyIndex : 0;
		//		return PokemonSelect.CurrentSelectedPartySlot;
		//	}
		//}
		#endregion

		#region Unity Monobehavior
		void Awake()
		{
			Debug.Log("Is Scriptable Object Null? " + (PokemonSelect == null).ToString());
			toggleGroup = GetComponent<ToggleGroup>();
			Debug.Log("Create Dictionary for Player Party UI Mono");
			PartyViewer = new Dictionary<int, TrainerPokemonButton>();
			Debug.Log("Create Dictionary for Temp Instantiated Pokemon Objects");
			StorePokemon = new Dictionary<Pokemons, IPokemon>();
			Debug.Log("Create Dictionary for Roster Entry UI Mono");
			StoreButtonData = new Dictionary<int, SelectPokemonButton>();
			Debug.Log("Create Dictionary for Temp Viewed Pokemons");
			ViewedRentalPokemon = new Queue<Pokemons>();
			//Debug.Log("Create Dictionary for Pokemons Selected by Player");
			//SelectedPokemons = new Dictionary<KeyValuePair<bool, int?>, int>();
			Debug.Log("Create LookUp Table for Pokemons Selected by Player");
			SelectedPokemons = new HashSet<KeyValuePair<KeyValuePair<bool, int?>, int>>();
			//
			Debug.Log("Load Assets for UI into Array");
			IconSprites = Resources.LoadAll<UnityEngine.Sprite>("PokemonIcon");
			PkmnType = Resources.LoadAll<UnityEngine.Sprite>("PokemonType");

			//GameDebug.Log("Path to DB: " + ((System.Data.SQLite.SQLiteConnection)Game.con).FileName);
			//if (Game.DatabasePath == @"Data Source=..\..\..\veekun-pokedex.sqlite")
			{
				try
				{
                    Core.Logger = LogManager.Logger;
                    //GameDebug.Log("0-" + System.IO.Path.GetFullPath("..\\veekun-pokedex.sqlite"));
                    //GameDebug.Log("1-" + System.IO.Path.GetFullPath("..\\..\\veekun-pokedex.sqlite"));
                    //GameDebug.Log("2-" + System.IO.Path.GetFullPath("..\\..\\..\\veekun-pokedex.sqlite"));
                    //GameDebug.Log("3-" + System.IO.Path.GetFullPath("..\\..\\..\\..\\veekun-pokedex.sqlite"));
                    //GameDebug.Log("Path to DB: " + ((System.Data.SQLite.SQLiteConnection)Game.con).FileName);
                    Game.DatabasePath = @"Data Source=veekun-pokedex.sqlite";
					Game.con = (System.Data.IDbConnection)new System.Data.SQLite.SQLiteConnection(Game.DatabasePath);
					//Game.con = new Mono.Data.Sqlite.SqliteConnection(Game.DatabasePath);
					Game.ResetSqlConnection(Game.DatabasePath);//@"Data\veekun-pokedex.sqlite"
					Debug.Log("Path to DB: " + ((System.Data.SQLite.SQLiteConnection)Game.con).FileName);
					//Game.ResetAndOpenSql(@"Data\veekun-pokedex.sqlite");
					//Game.ResetSqlConnection();
					Game g = new Game();
				}
				catch (InvalidOperationException) { Debug.LogError("problem connecting with database"); } //ignore...
				finally
				{
					//Game.con.Open();

					Debug.Log("Is Pokemon DB Null? " + (Kernal.PokemonData == null).ToString());
					if (Kernal.PokemonData == null)
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
						(Kernal.PokemonData.Count > 0).ToString(), Kernal.PokemonData.Count));
					if (Kernal.PokemonData.Count == 0)
						Debug.Log("Was Pokemon DB Successfully Created? " + Game.InitPokemons());
					Debug.Log(string.Format("Is Pokemon DB Greater than 0? {0} : {1}",
						(Kernal.PokemonData.Count > 0).ToString(), Kernal.PokemonData.Count));
				}
			}

			Debug.Log("Is Game Null? " + (Game.GameData == null).ToString());
			//Debug.Log("Is Player Null? " + (Game.GameData.Player == null).ToString());
			//if(Game.GameData.Player == null)
			//{
			//	Debug.Log("Create Player Object");
			//	Player p = new Player();
			//	Debug.Log("Saving Player Object to Global Singleton");
			//	Game.GameData.Player = p;
			//}

			// ToDo: Fix this
			if (Game.GameData.Trainer == null)
			{
				Game.GameData.Trainer = new Trainer("Player", TrainerTypes.PLAYER);

			}
			Debug.Log("Is Trainer Null? " + (Game.GameData.Trainer == null).ToString());
		}

		void Start()
		{
			Debug.Log("Is Game Events Null? " + (GameEvents.current == null).ToString());
			GameEvents.current.onChangePartyLineup += Scene_onChangePartyLineup;

			//CurrentOnParty = 0;
			//player = new Player();
			//
			//RentalControlUI.DisplayPokemonSelectUI();
			//party.DisplayPartyUI();
			//party.GetPartyButton();
			//SetPartyButton();
			if (Game.GameData.Trainer != null)
			{
				Debug.Log("Trainer Id: " + Game.GameData.Trainer.publicID().ToString());
				//Use ID but I will leave 00000 as Example
				partyPanel.SetTrainerID(Game.GameData.Trainer.publicID(), Game.GameData.Trainer.name);
			}

			partyPanel.Subscribe(PokemonSelect.TemporaryParty);
			DisplayRentalPokemons();

			// ToDo: Fix this; What is this for?
			for (int i = 0; i < partyPanel.party.Count(); i++)
			{
				PartyViewer.Add(i, partyPanel.party[i]);
			}
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
		//public void PokemonStatsandMoveUI(int Pkmn_ID)
		//{
		//	PkmnSelected = Pkmn_ID;
		//	Debug.Log("Pressed! " + Pkmn_ID);
		//	if (!StorePokemon.ContainsKey((Pokemons)Pkmn_ID))
		//	{
		//		StorePokemon.Add((Pokemons)PkmnSelected, new Pokemon((Pokemons)PkmnSelected, PokemonSelect.LevelFixed, false));
		//		Debug.Log("Create Pokemon!");
		//		DisplayMoveSetUI();
		//	}
		//	else
		//	{
		//		Debug.Log("It already created!");
		//		DisplayMoveSetUI();
		//	}
		//}
		//void DisplayMoveSetUI()
		//{
		//	pokemonViewModal.ActiveGameobject(true);
		//	pokemonViewModal.DisplayPkmnStats();
		//}

		//public void PartyData(int id, TrainerPokemonButton partybutton)
		//{
		//	PartyViewer.Add(id, partybutton);
		//}
		//public void RentalData(int id, SelectPokemonButton button)
		//{
		//	StoreButtonData.Add(id, button);
		//}
		//public void ClearParty()
		//{
		//	if (!pokemonViewModal.IsWindowActive)
		//	{
		//		//CurrentOnParty = 0;
		//		//player = new Player();
		//		StorePokemon = new Dictionary<Pokemons, Pokemon>();
		//		for (int i = 0; i < Core.MAXPARTYSIZE; i++)
		//		{
		//			Game.GameData.Trainer.party[i] = new Pokemon();
		//			PartyViewer[i].Clear();
		//			PartyViewer[i].ActivePokemonDisplay(false);
		//		}
		//		for (int i = 1; i <= 151; i++)
		//		{
		//			StoreButtonData[i].DisableOnClick(false);
		//		}
		//		//RentalControlUI.ActiveRentalUI(true);
		//		rosterGridContent.gameObject.SetActive(true);
		//	}
		//	else
		//	{
		//		Debug.Log("Can't Clear! You need to close MoveSet UI.");
		//	}
		//}
		#endregion

		#region Party Roster UI
		private void SetPageTabs()
		{
			int i = 0;
			int? page = PokemonSelect.CurrentSelectedRosterPage;
			//foreach (int id in ID)
			//for (int id = 1; id <= 151; id++)
			foreach (Generation gen in new Generation[] {
				Generation.RedBlueYellow, Generation.GoldSilverCrystal, Generation.RubySapphireEmerald,
				Generation.DiamondPearlPlatinum, Generation.BlackWhite, Generation.XY, Generation.SunMoon })
			{
				bool isSelected = page.HasValue && page.Value == (int)gen;

				GameObject Button = Instantiate(pageTabPrefab);
				//SelectPokemonButton roster = Button.GetComponent<SelectPokemonButton>();
				//roster.PokemonSelect = PokemonSelect;
				//StoreButtonData.Add(id, roster);
				//roster.SetID(i,(Pokemons)id,page:page,selected:isSelected); i++;
				Button.SetActive(true);
				Button.transform.SetParent(tabGridContent, false);
			} Debug.Log("Highest Species Counted: " + i);
			tabGridContent.gameObject.SetActive(true);
		}

		private void Scene_onChangePartyLineup()
		{
			Game.GameData.Trainer.party.PackParty();
			foreach (TrainerPokemonButton item in PartyViewer.Values)
			{
				if (Game.GameData.Trainer.party[item.partyIndex].IsNotNullOrNone())
				{
					//Game.GameData.Trainer.party[item.partyIndex] = new Pokemon((Pokemons)PkmnSelected, LevelFixed, false);
					PartyViewer[item.partyIndex].SetDisplay(); //pkmn.Name, pkmn.Species, pkmn.Level);
					//StoreButtonData[item.partyIndex].DisableOnClick(true);
					PartyViewer[item.partyIndex].ActivePokemonDisplay(true);
				}
				else
				{
					//Game.GameData.Trainer.party[item.partyIndex] = new Pokemon((Pokemons)PkmnSelected, LevelFixed, false);
					PartyViewer[item.partyIndex].ActivePokemonDisplay(false);
					PartyViewer[item.partyIndex].SetDisplay(); //pkmn.Name, pkmn.Species, pkmn.Level);
					//StoreButtonData[item.partyIndex].DisableOnClick(true);
				}
			}
		}
		#endregion

		private void DisplayRentalPokemons()
		{
			Debug.Log($"Total StoreButtonData: {StoreButtonData.Count}");
			Debug.Assert(pokemonButton != null, "PokemonButton is null!!");

			if (StoreButtonData.Count != 0)
				return;

            Debug.Log("Creating 151 Pokemons");

            for (int i = 0; i < 151; i++)
            {
                SelectPokemonButton item = Instantiate(pokemonButton, pokemonListPanel);
                item.gameObject.SetActive(true);
                item.SetID(pokemonViewModal, PokemonSelect, i, (Pokemons)(i + 1));
                item.PokemonSelect = PokemonSelect;
                item.name = $"ID {i}";
                StoreButtonData.Add(i, item);
            }
        }
	}
}