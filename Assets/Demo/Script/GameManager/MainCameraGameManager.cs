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
using UnityEngine.SceneManagement;
using System.Collections;

namespace PokemonUnity.Stadium
{
	/// <summary>
	/// </summary>
	//[ExecuteInEditMode]
	public class MainCameraGameManager : MonoBehaviour
	{
		#region Variables
		public float CurrentScrollPosition { get { return scrollBar.value; } set { scrollBar.value = value; } }
		public PokemonSelect PokemonSelect;
		public Scrollbar scrollBar;
		public GameObject cursorSelectedItem;
		/// <summary>
		/// Player slot for currently active trainer
		/// </summary>
		public int trainerIndex;

		public PokemonViewModal pokemonViewModal;
		public TrainerPartyPanel partyPanel;

		[SerializeField] private SelectPokemonButton pokemonButton;
		[SerializeField] private Transform pokemonListPanel;
		[SerializeField] private GameObject pageTabPrefab;
		[SerializeField] private GameObject rosterEntryPrefab;
		[SerializeField] private GameObject partyEntryPrefab;
		[SerializeField] private Transform rosterGridContent;
		[SerializeField] private Transform tabGridContent;
		[SerializeField] private ToggleGroup toggleGroup;

		// Versus Party Script Reference (ToDo: Clean the code)
		[SerializeField] private VersusPartyModal VersusPartyTop;
		[SerializeField] private VersusPartyModal VersusPartyBottom;
		[SerializeField] private GameObject VersusPanel;
		[SerializeField] private GameObject RosterPanel;
		[SerializeField] private GameObject TabPanel;

		//List
		//public Dictionary<int, SelectPokemonButton> StoreButtonData;

		//Sprite
		public static UnityEngine.Sprite[] PkmnType { get; private set; }
		public static UnityEngine.Sprite[] IconSprites { get; private set; }
		#endregion

		#region Unity Monobehavior
		void Awake()
		{
			// Set the Logger System
            Core.Logger = LogManager.Logger;

            Debug.Log("Is Scriptable Object Null? " + (PokemonSelect == null).ToString());
			toggleGroup = GetComponent<ToggleGroup>();

			//Debug.Log("Create Dictionary for Roster Entry UI Mono");
			//StoreButtonData = new Dictionary<int, SelectPokemonButton>();

			Debug.Log("Load Assets for UI into Array");
			IconSprites = Resources.LoadAll<UnityEngine.Sprite>("PokemonIcon");
			PkmnType = Resources.LoadAll<UnityEngine.Sprite>("PokemonType");

			{
				try
				{
					Game.DatabasePath = @"Data Source=veekun-pokedex.sqlite";
					Game.con = new System.Data.SQLite.SQLiteConnection(Game.DatabasePath);
                    //@"Data\veekun-pokedex.sqlite"
                    Game.ResetSqlConnection(Game.DatabasePath);
					Debug.Log("Path to DB: " + ((System.Data.SQLite.SQLiteConnection)Game.con).FileName);

					Game g = new Game();
				}
				catch (InvalidOperationException) { Debug.LogError("problem connecting with database"); } //ignore...
				finally
				{
					Debug.Log("Is Pokemon DB Null? " + (Kernal.PokemonData == null).ToString());
					if (Kernal.PokemonData == null)
					{
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
						catch (Exception) { 
							Debug.LogError("There were some problems running sql..."); 
						}
					}
					Debug.Log($"Is Pokemon DB Greater than 0? {Kernal.PokemonData.Count > 0} : {Kernal.PokemonData.Count}");
					
					// ToDo: Is this need?
					if (Kernal.PokemonData.Count == 0)
					{
                        Debug.Log("Was Pokemon DB Successfully Created? " + Game.InitPokemons());
                        Debug.Log($"Is Pokemon DB Greater than 0? {Kernal.PokemonData.Count > 0} : {Kernal.PokemonData.Count}");
                    }
				}
			}

			Debug.Log("Is Game Null? " + Game.GameData == null);

			// ToDo: Fix this
			if (Game.GameData.Trainer == null)
                Game.GameData.Trainer = new Trainer("Player", TrainerTypes.PLAYER);

            Debug.Log("Is Trainer Null? " + (Game.GameData.Trainer == null).ToString());
		}

		void Start()
		{
			Debug.Log("Is Game Events Null? " + GameEvents.current == null);

			if (Game.GameData.Trainer != null)
			{
				Debug.Log("Trainer Id: " + Game.GameData.Trainer.publicID().ToString());
				partyPanel.SetTrainerID(Game.GameData.Trainer.publicID(), Game.GameData.Trainer.name);
			}

			// ToDo: Remove this
			DisplayRentalPokemons();
		}
		#endregion

		#region Versus Party UI
		// ToDo: Remove this
		public void ShowVersusPartyUI()
		{
			Debug.Log("ShowVersusPartyUI Called");
			partyPanel.gameObject.SetActive(false);
			RosterPanel.gameObject.SetActive(false);
			TabPanel.gameObject.SetActive(false);

			VersusPanel.gameObject.SetActive(true);

			VersusPartyTop.RefreshDisplay(Game.GameData.Trainer.name, null, PokemonSelect.TemporaryParty.Reverse().ToArray());
			VersusPartyBottom.RefreshDisplay("Biker", null, PokemonSelect.TemporaryParty.ToArray());

			StartCoroutine(LoadBattleScene());
		}

		// Battle Scene ToDo: Fix the scene manager and name. This is rough code
		public IEnumerator LoadBattleScene()
		{
			yield return new WaitForSeconds(3);
			SceneManager.LoadScene(1);
		}
		#endregion

		private void DisplayRentalPokemons()
		{
			//Debug.Log($"Total StoreButtonData: {StoreButtonData.Count}");
			//Debug.Assert(pokemonButton != null, "PokemonButton is null!!");
			//
			//if (StoreButtonData.Count != 0)
			//	return;

			Core.Logger.Log("Creating {0} Pokemons",Core.PokemonIndexLimit);

			for (int i = 0; i < Core.PokemonIndexLimit; i++)
			{
				Pokemons species = (Pokemons)(i + 1);

                SelectPokemonButton item = Instantiate(pokemonButton, pokemonListPanel);
				item.gameObject.SetActive(true);
				//item.SetID(pokemonViewModal, PokemonSelect, i, (Pokemons)(i + 1));
				item.SetID(pokemonViewModal, PokemonSelect, i, species, species.ToString(), 50);
				item.name = $"ID {i}";
				//StoreButtonData.Add(i, item);
			}
		}
	}
}