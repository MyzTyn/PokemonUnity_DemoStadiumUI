using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using PokemonUnity;
using PokemonUnity.Interface;
using PokemonUnity.Character;
using PokemonUnity.Monster;
using PokemonUnity.Localization;
using PokemonEssentials.Interface;
using PokemonEssentials.Interface.Field;
using PokemonEssentials.Interface.Item;
using PokemonEssentials.Interface.Screen;
using PokemonEssentials.Interface.EventArg;
using PokemonEssentials.Interface.PokeBattle;
using PokemonEssentials.Interface.PokeBattle.Effects;
//using PokemonEssentials.Interface.PokeBattle.Rules;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PokemonUnity.Stadium
{
	/// <summary>
	/// Manager for all the Pokemon game's functions and relationship with Unity Engine
	/// </summary>
	//[ExecuteInEditMode]
	//[RequireComponent(typeof(LevelLoader))]
	public class GameManager : MonoBehaviour
	{
		#region Variables
		public static GameManager current { get; private set; }
		
		public InputManager InputManager;
		public AudioManager AudioManager;
		public FileTest FileTest;
		public IGame game; //game scope used for temp actions, without affecting original copy?
		public event Action onUpdate;
		public event Action onLevelLoaded;
		public event Action<IScene> onLoadLevel;
		/// <summary>
		/// <see cref="IGameScenesUI"/>
		/// </summary>
		public LevelLoader sceneList;
		/// <summary>
		/// <see cref="IPokeBattle_SceneIE"/>
		/// </summary>
		//[SerializeField] private BattleScene battle;
		#endregion
		
		#region Unity Monobehavior
		void Awake()
		{
			if (!current)
			{
				current = this;
			}
			// The Instance is already set! Cannot have more than one!
			else if (current != this)
			{
				Destroy(gameObject);
			}
			
			// Set the Logger System
			Core.Logger = LogManager.Logger;
			
			Debug.Log("Game Manager is Awake!");
			
			// LogManager.Logger.OnLog += (object sender, OnDebugEventArgs e) =>
			// {
			// 	if (e == null && e == System.EventArgs.Empty) 
			// 		return;
			// 	
			// 	if (e.Error == true)
			// 		global::UnityEngine.Debug.LogError(string.Format("[ERR] " + global::UnityEngine.Time.frameCount + ": " + e.Message, e.MessageParameters));
			// 	else if (e.Error == false)
			// 		global::UnityEngine.Debug.LogWarning(string.Format("[WARN] " + global::UnityEngine.Time.frameCount + ": " + e.Message, e.MessageParameters));
			// 	else
			// 		global::UnityEngine.Debug.Log(string.Format("[LOG] " + global::UnityEngine.Time.frameCount + ": " + e.Message, e.MessageParameters));
			// };
			
			// Set the logger to output the file
			//Debugger.Instance.Init("\\Logs", "GameLog"); //Path = "Logs\GameLog.txt"
			//Core.Logger?.Init("\\Logs", "GameLog"); //Path = "Logs\GameLog.txt"
			try
			{
				// Load the database
				//Game.DatabasePath = "Data Source =" + UnityEngine.Application.dataPath + "/Data/veekun-pokedex.sqlite";
				Game.DatabasePath = "Data Source=veekun-pokedex.sqlite";
				Core.Logger?.Log("ConnectionString Database Path: " + Game.DatabasePath);
				Game.con = new System.Data.SQLite.SQLiteConnection(Game.DatabasePath);
				Game.ResetSqlConnection(Game.DatabasePath);

				Core.Logger?.Log("Framework Connected to Database...");
				Core.Logger?.Log("Path to DB: " + ((System.Data.SQLite.SQLiteConnection)Game.con).FileName);

				// Create new Game
				game = new Game();
				Core.Logger?.Log("New Game Entity Successfully Instantiated!~");
			}
			catch (InvalidOperationException)
			{
				Core.Logger?.LogError("Problem executing SQL with connected database");
			}
			catch (Exception e)
			{
				Core.Logger?.LogError(e.ToString());
			}
			
			Core.Logger?.Log("Is Pokemon DB Null? " + (Kernal.PokemonData == null));
			Core.Logger?.Log($"Is Pokemon DB Greater than 0? {(Kernal.PokemonData.Count > 0)} : {Kernal.PokemonData.Count}");
			
			//Core.Logger?.Log("Is Game Null? " + (Game.GameData == null).ToString());
			Debug.Assert(Game.GameData != null, "Game is NULL!");
			
			//Core.Logger?.Log("Is Player Null? " + (Game.GameData.Player == null).ToString());
			Debug.Assert(Game.GameData.Player != null, "Player is NULL!");

			//if (Game.GameData.Player == null)
			//{
			//	Core.Logger?.Log("Create Player Object");
			//	//IGamePlayer p = new Player();
			//	Core.Logger?.Log("Saving Player Object to Global Singleton");
			//	//Game.GameData.Player = p;
			//}
			
			// ToDo: Fix this 
			if (Game.GameData.Trainer == null)
				Game.GameData.Trainer = new Trainer("Player", TrainerTypes.PLAYER);
			
			Core.Logger?.Log("Is Trainer Null? " + (Game.GameData.Trainer == null));
			
			// Prevent from destroying this object
			DontDestroyOnLoad(this);
			
			//ConfigureScenes();
		}
		void Start()
		{
			// Set the localization
			string englishLocalization = "..\\..\\..\\LocalizationStrings.xml";
			Game.LocalizationDictionary = new XmlStringRes(null);
			Game.LocalizationDictionary.Initialize(englishLocalization, (int)Languages.English);
			
			//Enable "OnStart" to trigger battle scene...
			//((object)game.Scenes?.BattleScene as GameObject)?.SetActive(true); //Scene is already active... Sort later.
		}
		
		#endregion
		
		#region Methods
		//public void OnLoadLevel(int id)
		//{
		//	if (onLoadLevel != null) onLoadLevel(id);
		//}
		public void OnLoadLevel(IScene scene)
		{
			if (onLoadLevel != null) 
				onLoadLevel(scene);
		}
		
		public void OnLoadLevel(int scene, float time = 0.5f)
		{
			sceneList.transitionTime = time;
			StartCoroutine(sceneList.LoadLevel(scene));
		}
		
		public void OnLoadScene(IScene scene)
		{
			StartCoroutine(sceneList.LoadScene(scene));
		}
		
		//
		//private void ConfigureScenes()
		//{
		//	Core.Logger?.LogDebug(message: "Run: {0}.{1}", GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
		//
		//	game.Scenes = gameObject.GetComponent<LevelLoader>() as IGameScenesUI;
		//	//ToDo: Load all the different game scenes into an array, from unity inspector, and pass them as an input parameter below
		//	//game.Scenes.initialize((IScene[])(object[])sceneList);
		//	//game.Scenes.initialize((IPokeBattle_Scene)battle.GetComponent<BattleScene>());
		//	//(game as Game).SetScenes((IPokeBattle_SceneIE)battle.GetComponent<BattleScene>());
		//}
		#endregion
	}
}