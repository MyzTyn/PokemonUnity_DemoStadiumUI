﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PokemonUnity;
using PokemonUnity.UX;
using PokemonUnity.Attack;
using PokemonUnity.Attack.Data;
using PokemonUnity.Monster;
using PokemonUnity.Monster.Data;
using PokemonUnity.Inventory;
using PokemonUnity.Combat;
using PokemonUnity.Combat.Data;
using PokemonUnity.Character;
using PokemonUnity.Overworld;
using PokemonUnity.Utility;
using PokemonEssentials.Interface;
using PokemonEssentials.Interface.Battle;
using PokemonEssentials.Interface.Item;
using PokemonEssentials.Interface.Field;
using PokemonEssentials.Interface.Screen;
using PokemonEssentials.Interface.PokeBattle;
using PokemonEssentials.Interface.PokeBattle.Effects;

namespace PokemonUnity.Stadium
{
	[Serializable]
	public partial class UnityBattle : PokemonUnity.Combat.Battle, IBattleIE, IHasDisplayMessageIE, ISerializable
	{
#pragma warning disable 0162 //Warning CS0162  Unreachable code detected
		#region Variables
		///// <summary>
		///// Scene object for this battle
		///// </summary>
		//public IScene scene { get; protected set; }
		///// <summary>
		///// Decision: 0=undecided; 1=win; 2=loss; 3=escaped; 4=caught; 5=draw
		///// </summary>
		//public BattleResults decision { get; set; }
		///// <summary>
		///// Internal battle flag
		///// </summary>
		///// <remarks>
		///// battle occurred without any frontend or UI output (most likely on backend for AI vs AI training)
		///// </remarks>
		//public bool internalbattle { get; set; }
		///// <summary>
		///// Double battle flag
		///// </summary>
		//public bool doublebattle { get; set; }
		///// <summary>
		///// True if player can't escape
		///// </summary>
		//public bool cantescape { get; set; }
		///// <summary>
		///// If game cannot progress UNLESS the player is victor of match.
		///// False if there are no consequences to player's defeat.
		///// </summary>
		///// (Ground Hogs day anyone?)
		//public bool canLose { get; protected set; }
		///// <summary>
		///// Shift/Set "battle style" option
		///// </summary>
		//public bool shiftStyle { get; set; }
		///// <summary>
		///// "Battle scene" option
		///// </summary>
		//public bool battlescene { get; set; }
		///// <summary>
		///// Debug flag
		///// </summary>
		//public bool debug { get; protected set; }
		//public int debugupdate { get; protected set; }
		///// <summary>
		///// Player trainer
		///// </summary>
		//public ITrainer[] player { get; protected set; }
		///// <summary>
		///// Opponent trainer; if null => wild encounter
		///// </summary>
		//public ITrainer[] opponent { get; protected set; }
		///// <summary>
		///// Player's Pokémon party
		///// </summary>
		//public IPokemon[] party1 { get; protected set; }
		///// <summary>
		///// Foe's Pokémon party
		///// </summary>
		//public IPokemon[] party2 { get; protected set; }
		///// <summary>
		///// Pokémon party for All Trainers in Battle.
		///// Array[4,6] = 0: Player, 1: Foe, 2: Ally, 3: Foe's Ally
		///// </summary>
		////public IPokemon[,] party { get; protected set; }
		///// <summary>
		///// Order of Pokémon in the player's party
		///// </summary>
		//public IList<int> party1order { get; protected set; }
		///// <summary>
		///// Order of Pokémon in the opponent's party
		///// </summary>
		//public IList<int> party2order { get; protected set; }
		///// <summary>
		///// True if player's party's max size is 6 instead of 3
		///// </summary>
		//public bool fullparty1 { get; set; }
		///// <summary>
		///// True if opponent's party's max size is 6 instead of 3
		///// </summary>
		//public bool fullparty2 { get; set; }
		/// <summary>
		/// Currently active Pokémon
		/// </summary>
		new public IBattlerIE[] battlers { get { return (IBattlerIE[])_battlers; } }
		///// <summary>
		///// Items held by opponents
		///// </summary>
		////public List<Items> items { get; protected set; }
		//public Items[][] items { get; set; }
		///// <summary>
		///// Effects common to each side of a battle
		///// </summary>
		///// public List<SideEffects> sides { get; protected set; }
		//public IEffectsSide[] sides { get; protected set; }
		///// <summary>
		///// Effects common to the whole of a battle
		///// </summary>
		///// public List<FieldEffects> field { get; protected set; }
		//public IEffectsField field { get; protected set; }
		///// <summary>
		///// Battle surroundings;
		///// Environment node is used for background visual,
		///// that's displayed behind the floor tile
		///// </summary>
		//public Environments environment { get; set; }
		///// <summary>
		///// Current weather, custom methods should use <see cref="pbWeather"/>  instead
		///// </summary>
		//public Weather weather { get; set; }
		////public void SetWeather (Weather weather) { this.weather = weather; }
		///// <summary>
		///// Duration of current weather, or -1 if indefinite
		///// </summary>
		//public int weatherduration { get; set; }
		///// <summary>
		///// True if during the switching phase of the round
		///// </summary>
		//public bool switching { get; protected set; }
		///// <summary>
		///// True if Future Sight is hitting
		///// </summary>
		//public bool futuresight { get; protected set; }
		///// <summary>
		///// The Struggle move
		///// </summary>
		///// <remarks>
		///// Execute whatever move/function is stored in this variable
		///// </remarks>
		///// Func<PokeBattle>
		//public IBattleMove struggle { get; protected set; }
		///// <summary>
		///// Choices made by each Pokémon this round
		///// </summary>
		//public IBattleChoice[] choices { get; protected set; }
		///// <summary>
		///// Success states
		///// </summary>
		//public ISuccessState[] successStates { get; protected set; }
		///// <summary>
		///// Last move used
		///// </summary>
		//public Moves lastMoveUsed { get; set; }
		///// <summary>
		///// Last move user
		///// </summary>
		//public int lastMoveUser { get; set; }
		///// <summary>
		///// Battle index of each trainer's Pokémon to Mega Evolve
		///// </summary>
		///// Instead of reflecting entire party, it displays for active on field?
		//public int[][] megaEvolution { get; protected set; }
		///// <summary>
		///// Whether Amulet Coin's effect applies
		///// </summary>
		//public bool amuletcoin { get; protected set; }
		///// <summary>
		///// Money gained in battle by using Pay Day
		///// </summary>
		//public int extramoney { get; set; }
		///// <summary>
		///// Whether Happy Hour's effect applies
		///// </summary>
		//public bool doublemoney { get; set; }
		///// <summary>
		///// Speech by opponent when player wins
		///// </summary>
		//public string endspeech { get; set; }
		///// <summary>
		///// Speech by opponent when player wins
		///// </summary>
		//public string endspeech2 { get; set; }
		///// <summary>
		///// Speech by opponent when opponent wins
		///// </summary>
		//public string endspeechwin { get; set; }
		///// <summary>
		///// Speech by opponent when opponent wins
		///// </summary>
		//public string endspeechwin2 { get; set; }
		///// <summary>
		///// </summary>
		//public IDictionary<string,bool> rules { get; protected set; }
		////public List<string> rules { get; protected set; }
		///// <summary>
		///// Counter to track number of turns for battle
		///// </summary>
		//public int turncount { get; set; }
		new public IBattlerIE[] priority { get { return (IBattlerIE[])_priority; } }
		//public List<int> snaggedpokemon { get; protected set; }
		///// <summary>
		///// Each time you use the option to flee, the counter goes up.
		///// </summary>
		//public int runCommand { get; protected set; }
		///// <summary>
		///// Another counter that has something to do with tracking items picked up during a battle
		///// </summary>
		//public int nextPickupUse { get { pickupUse+=1; return pickupUse; } }
		//protected int pickupUse;
		//public bool controlPlayer { get; set; }
		//public bool usepriority { get; set; }
		//public IBattlePeer peer { get; set; }
		#endregion

		#region Constructor
		/// <summary>
		/// </summary>
		public UnityBattle(IScene scene, IPokemon[] p1, IPokemon[] p2, ITrainer player, ITrainer opponent)
			//: base (scene, p1, p2, player, opponent)
			: this (scene, p1, p2, player == null ? null : new ITrainer[] { player }, opponent == null ? null : new ITrainer[] { opponent })
		{
			//(this as IBattleIE).initialize(scene, p1, p2, player, opponent);
		}
		public UnityBattle(IScene scene, IPokemon[] p1, IPokemon[] p2, ITrainer[] player, ITrainer[] opponent)
			//: base (scene, p1, p2, player, opponent)
		{
			(this as IBattleIE).initialize(scene, p1, p2, player, opponent);
		}
		IBattleIE IBattleIE.initialize(IScene scene, IPokemon[] p1, IPokemon[] p2, ITrainer player, ITrainer opponent)
		{
			return initialize(scene, p1, p2, player == null ? null : new ITrainer[] { player }, opponent == null ? null : new ITrainer[] { opponent });
		}
		IBattleIE IBattleIE.initialize(IScene scene, IPokemon[] p1, IPokemon[] p2, ITrainer[] player, ITrainer[] opponent)
		{
			return initialize(scene, p1, p2, player, opponent);
		}
		new public IBattleIE initialize(IScene scene, IPokemon[] p1, IPokemon[] p2, ITrainer[] player, ITrainer[] opponent, int maxBattlers = 4)
		{
			GameDebug.Log("Run: {0}", System.Reflection.MethodBase.GetCurrentMethod().Name);

			//if opponent (trainer battle) is not null but player array is empty, then player is null
			//if (opponent != null && player.Length == 0)
			//	this.player = null; //player[0];

			//if opponent is not null but opponent array is empty, then opponent is null
			//if (opponent != null && opponent.Length == 0)
			//	this.opponent = null; //opponent[0];

			this.player = player ?? new ITrainer[0];                   //Trainer object
			this.opponent = opponent ?? new ITrainer[0];               //Trainer object

			if (p1.Length == 0) {
				//raise new ArgumentError(Game._INTL("Party 1 has no Pokémon."))
				GameDebug.LogError("Party 1 has no Pokémon.");
				return this;
			}

			if (p2.Length == 0) {
				//raise new ArgumentError(Game._INTL("Party 2 has no Pokémon."))
				GameDebug.LogError("Party 2 has no Pokémon.");
				return this;
			}

			if (p2.Length > 2 && this.opponent.Length == 0) { //ID == TrainerTypes.WildPokemon
				//raise new ArgumentError(Game._INTL("Wild battles with more than two Pokémon are not allowed."))
				GameDebug.LogError("Wild battles with more than two Pokémon are not allowed.");
				return this;
			}

			this.scene = scene;
			decision = 0;
			internalbattle = Core.INTERNAL;
			doublebattle = false;
			cantescape = false;
			shiftStyle = true;
			battlescene = true;
			debug = Core.DEBUG;
			debugupdate = 0;

			party1 = p1;
			party2 = p2;

			party1order = new List<int>();
			//the #12 represents a double battle with 2 trainers using 6 pokemons each on 1 side
			for (int i = 0; i < Core.MAXPARTYSIZE * 2; i++)
				party1order.Add(i);

			party2order = new List<int>();
			//for i in 0...12;party2order.push(i); }
			for (int i = 0; i < Core.MAXPARTYSIZE * 2; i++)
				party2order.Add(i);

			fullparty1 = false;
			fullparty2 = false;
			_battlers = new PokemonUnity.Stadium.Battler[maxBattlers];
			//items = new List<Items>(); //null;
			items = new Items[this.opponent.Length][];
			for (int t = 0; t < this.opponent.Length; t++) //List of Trainers
				items[t] = new Items[0];

			sides = new PokemonUnity.Combat.Effects.Side[] { new PokemonUnity.Combat.Effects.Side(),	// Player's side
															 new PokemonUnity.Combat.Effects.Side() };	// Foe's side
			//sides = new Effects.Side[2];
			field = new PokemonUnity.Combat.Effects.Field();	// Whole field (gravity/rooms)
			environment = Environments.None;					// e.g. Tall grass, cave, still water
			weather = 0;
			weatherduration = 0;
			switching = false;
			futuresight = false;
			choices = new IBattleChoice[4];

			successStates = new SuccessState[battlers.Length];
			for (int i = 0; i < battlers.Length; i++)
			{
				successStates[i] = new SuccessState();
			}

			lastMoveUsed = Moves.NONE;
			lastMoveUser = -1;
			//nextPickupUse = 0;
			pickupUse = 0;

			megaEvolution = new int[][] { new int[party1.Length], new int[party2.Length] };
			//if (this.player.Length > 0) 									//ToDo: single/double or party?
			//	megaEvolution[0] = new bool?[this.player.Party.Length]; 	//[-1] * player.Length;
			//else
			//	megaEvolution[0] = new bool?[]{ null }; 					//[-1];
			//if (this.opponent.Length > 0)
			//	megaEvolution[1] = new bool?[this.opponent.Party.Length]; 	//[-1] * opponent.Length;
			//else
			//	megaEvolution[1] = new bool?[]{ null }; 					//[-1];
			for (int side = 0; side < sides.Length; side++) //2 sides (yours / theirs)
				for (int i = 0; i < megaEvolution[side].Length; i++)
					megaEvolution[side][i] = -1; //Everyone starts match in default -1 value

			amuletcoin = false;
			extramoney = 0;
			doublemoney = false;

			endspeech = ""; //opponent.ScriptBattleEnd;
			endspeech2 = "";
			endspeechwin = "";
			endspeechwin2 = "";

			rules = new Dictionary<string,bool>();

			turncount = 0;

			peer = PokemonUnity.Monster.PokeBattle_BattlePeer.create();
			//peer = new PokeBattle_BattlePeer();

			_priority = new PokemonUnity.Stadium.Battler[battlers.Length];

			//usepriority = false; //False is already default value; redundant.

			snaggedpokemon = new List<int>();

			runCommand = 0;

			if (Kernal.MoveData.Keys.Contains(Moves.STRUGGLE))
				struggle = Combat.Move.pbFromPBMove(this, new Attack.Move(Moves.STRUGGLE));
			else
				struggle = new PokeBattle_Struggle().Initialize(this, new Attack.Move(Moves.STRUGGLE));

			//struggle.PP = -1;

			for (int i = 0; i < battlers.Length; i++) {
				this._battlers[i] = new PokemonUnity.Stadium.Battler(this, (sbyte)i);
			//} for (int i = 0; i < battlers.Length; i++) {
			//	this._battlers[i].initialize(this, (sbyte)i);
			}

			foreach (var i in party1)
			{
				if (i.Species == Pokemons.NONE) continue;
				i.itemRecycle = 0;
				i.itemInitial = i.Item;
				i.belch = false;
			}

			foreach (var i in party2)
			{
				if (i.Species == Pokemons.NONE) continue;
				i.itemRecycle = 0;
				i.itemInitial = i.Item;
				i.belch = false;
			}
			return this;
		}
		protected UnityBattle(SerializationInfo info, StreamingContext context) 
			: this((IScene)info.GetValue(nameof(scene), typeof(IScene))
				  ,(IPokemon[])info.GetValue(nameof(party1), typeof(IPokemon[]))
				  ,(IPokemon[])info.GetValue(nameof(party2), typeof(IPokemon[]))
				  ,(ITrainer[])info.GetValue(nameof(player), typeof(ITrainer[]))
				  ,(ITrainer[])info.GetValue(nameof(opponent), typeof(ITrainer[])))
		{
			//this.scene = (IScene)info.GetValue(nameof(scene), typeof(IScene));
			this.decision = (BattleResults)info.GetValue(nameof(decision), typeof(BattleResults));
			this.internalbattle = (bool)info.GetValue(nameof(internalbattle), typeof(bool));
			this.doublebattle = (bool)info.GetValue(nameof(doublebattle), typeof(bool));
			this.cantescape = (bool)info.GetValue(nameof(cantescape), typeof(bool));
			this.canLose = (bool)info.GetValue(nameof(canLose), typeof(bool));
			this.shiftStyle = (bool)info.GetValue(nameof(shiftStyle), typeof(bool));
			this.battlescene = (bool)info.GetValue(nameof(battlescene), typeof(bool));
			this.debug = (bool)info.GetValue(nameof(debug), typeof(bool));
			this.debugupdate = (int)info.GetValue(nameof(debugupdate), typeof(int));
			//this.player = (ITrainer[])info.GetValue(nameof(player), typeof(ITrainer[]));
			//this.opponent = (ITrainer[])info.GetValue(nameof(opponent), typeof(ITrainer[]));
			//this.party1 = (IPokemon[])info.GetValue(nameof(party1), typeof(IPokemon[]));
			//this.party2 = (IPokemon[])info.GetValue(nameof(party2), typeof(IPokemon[]));
			this.party1order = (IList<int>)info.GetValue(nameof(party1order), typeof(IList<int>));
			this.party2order = (IList<int>)info.GetValue(nameof(party2order), typeof(IList<int>));
			this.fullparty1 = (bool)info.GetValue(nameof(fullparty1), typeof(bool));
			this.fullparty2 = (bool)info.GetValue(nameof(fullparty2), typeof(bool));
			this._battlers = (IBattlerIE[])info.GetValue(nameof(battlers), typeof(IBattlerIE[]));
			this.items = (Items[][])info.GetValue(nameof(items), typeof(Items[][]));
			this.sides = (IEffectsSide[])info.GetValue(nameof(sides), typeof(IEffectsSide[]));
			this.field = (IEffectsField)info.GetValue(nameof(field), typeof(IEffectsField));
			this.environment = (Environments)info.GetValue(nameof(environment), typeof(Environments));
			this.weather = (Weather)info.GetValue(nameof(weather), typeof(Weather));
			this.weatherduration = (int)info.GetValue(nameof(weatherduration), typeof(int));
			this.switching = (bool)info.GetValue(nameof(switching), typeof(bool));
			this.futuresight = (bool)info.GetValue(nameof(futuresight), typeof(bool));
			this.struggle = (IBattleMove)info.GetValue(nameof(struggle), typeof(IBattleMove));
			this.choices = (IBattleChoice[])info.GetValue(nameof(choices), typeof(IBattleChoice[]));
			this.successStates = (ISuccessState[])info.GetValue(nameof(successStates), typeof(ISuccessState[]));
			this.lastMoveUsed = (Moves)info.GetValue(nameof(lastMoveUsed), typeof(Moves));
			this.lastMoveUser = (int)info.GetValue(nameof(lastMoveUser), typeof(int));
			this.megaEvolution = (int[][])info.GetValue(nameof(megaEvolution), typeof(int[][]));
			this.amuletcoin = (bool)info.GetValue(nameof(amuletcoin), typeof(bool));
			this.extramoney = (int)info.GetValue(nameof(extramoney), typeof(int));
			this.doublemoney = (bool)info.GetValue(nameof(doublemoney), typeof(bool));
			this.endspeech = (string)info.GetValue(nameof(endspeech), typeof(string));
			this.endspeech2 = (string)info.GetValue(nameof(endspeech2), typeof(string));
			this.endspeechwin = (string)info.GetValue(nameof(endspeechwin), typeof(string));
			this.endspeechwin2 = (string)info.GetValue(nameof(endspeechwin2), typeof(string));
			this.rules = (IDictionary<string, bool>)info.GetValue(nameof(rules), typeof(IDictionary<string, bool>));
			this.turncount = (int)info.GetValue(nameof(turncount), typeof(int));
			this._priority = (IBattlerIE[])info.GetValue(nameof(priority), typeof(IBattlerIE[]));
			this.snaggedpokemon = (List<int>)info.GetValue(nameof(snaggedpokemon), typeof(List<int>));
			this.runCommand = (int)info.GetValue(nameof(runCommand), typeof(int));
			this.pickupUse = (int)info.GetValue(nameof(pickupUse), typeof(int));
			this.controlPlayer = (bool)info.GetValue(nameof(controlPlayer), typeof(bool));
			this.usepriority = (bool)info.GetValue(nameof(usepriority), typeof(bool));
			this.peer = (IBattlePeer)info.GetValue(nameof(peer), typeof(IBattlePeer));
		}
		#endregion

		#region Method
		//public int pbRandom(int index)
		//{
		//	return Core.Rand.Next(index);
		//}
		//
		//public void pbAbort() {
		//	//GameDebug.LogError("Battle aborted");
		//	throw new BattleAbortedException("Battle aborted");
		//}

		#region Catching and storing Pokémon.
		new public IEnumerator pbStorePokemon(IPokemon pokemon)
		{
			if(pokemon is IPokemonShadowPokemon p && !p.isShadow)
			{
				bool nicknamePokemon = false;
				yield return pbDisplayConfirm(Game._INTL("Would you like to give a nickname to {1}?", Game._INTL(pokemon.Species.ToString(TextScripts.Name))), result: value => nicknamePokemon = value);
				if (nicknamePokemon)
				{
					//string nick = @scene.pbNameEntry(Game._INTL("{1}'s nickname?", Game._INTL(pokemon.Species.ToString(TextScripts.Name))), pokemon);
					string nick = string.Empty;
					if (@scene is IPokeBattle_SceneIE s0)
					{
						yield return s0.pbNameEntry(Game._INTL("{1}'s nickname?", Game._INTL(pokemon.Species.ToString(TextScripts.Name))), pokemon, result: value => nick = value);
					}
					//if(!string.IsNullOrEmpty(nick)) pokemon.Name = nick;
					if(!string.IsNullOrEmpty(nick)) (pokemon as Monster.Pokemon).SetNickname(nick);
				}
			}
			//ToDo: Add to party before attempting to store in PC
			//Game.GameData.Player.addPokemon(pokemon)); return;
			//bool success = false;
			//int i = 0; do {
			//	//success = Game.GameData.Player.PC.addPokemon(pokemon);
			//	success = Game.GameData.Player.PC.hasSpace();
			//	if(!success) Game.GameData.Player.PC.getIndexOfFirstEmpty();
			//} while (!success); //Cycle through all boxes unless they're all full
			int oldcurbox = @peer.pbCurrentBox(); //Game.GameData.Player.PC.ActiveBox;
			int storedbox = @peer.pbStorePokemon(pbPlayer(), pokemon);
			//int? storedbox = Game.GameData.Player.PC.getIndexOfFirstEmpty();
			if (storedbox<0) yield break; //!storedbox.HasValue
			string creator = @peer.pbGetStorageCreator(); //Game.GameData.Player.IsCreator ? Game.GameData.Trainer.name : @peer.pbGetStorageCreator();
			string curboxname = @peer.pbBoxName(oldcurbox); //Game.GameData.Player.PC.BoxNames[oldcurbox];
			string boxname = @peer.pbBoxName(storedbox); //Game.GameData.Player.PC.BoxNames[storedbox.Value];
			if (storedbox != oldcurbox) {
				if (creator != null) //Game.GameData.Player.IsCreator
					yield return pbDisplayPaused(Game._INTL("Box \"{1}\" on {2}'s PC was full.", curboxname, creator));
				else
					yield return pbDisplayPaused(Game._INTL("Box \"{1}\" on someone's PC was full.", curboxname));
				yield return pbDisplayPaused(Game._INTL("{1} was transferred to box \"{2}\".", pokemon.Name, boxname));
			}
			else {
				if (creator != null) //Game.GameData.Player.IsCreator
					yield return pbDisplayPaused(Game._INTL("{1} was transferred to {2}'s PC.", pokemon.Name, creator));
				else
					yield return pbDisplayPaused(Game._INTL("{1} was transferred to someone's PC.", pokemon.Name));
				yield return pbDisplayPaused(Game._INTL("It was stored in box \"{1}\".", boxname));
			}
		}
		new public IEnumerator pbThrowPokeball(int idxPokemon, Items ball, int? rareness = null, bool showplayer = false)
		{
			string itemname = Game._INTL(ball.ToString(TextScripts.Name));
			IBattlerIE battler = null;
			if (pbIsOpposing(idxPokemon))
				battler = battlers[idxPokemon];
			else
				battler = battlers[idxPokemon].pbOppositeOpposing;
			if (battler.isFainted())
				battler = battler.pbPartner;
			yield return pbDisplayBrief(Game._INTL("{1} threw one {2}!", Game.GameData.Trainer.name, itemname));
			if (battler.isFainted())
			{
				yield return pbDisplay(Game._INTL("But there was no target..."));
				yield break;
			}
			int shakes = 0; bool critical = false;
			if (opponent.Length > 0//.ID != TrainerTypes.WildPokemon)
				&& ((Game.GameData is IItemCheck ch && !ch.pbIsSnagBall(ball)) || battler is IBattlerShadowPokemon s && !s.isShadow()))
			{
				if (@scene is IPokeBattle_SceneIE s0) yield return s0.pbThrowAndDeflect(ball, 1);
				yield return pbDisplay(Game._INTL("The Trainer blocked the Ball!\nDon't be a thief!"));
			}
			else
			{
				IPokemon pokemon = battler.pokemon;
				Pokemons species = pokemon.Species;
				if (!rareness.HasValue)
				{
					rareness = (int)Kernal.PokemonData[battler.Species].Rarity;
				}
				int a = battler.TotalHP;
				int b = battler.HP;
				//rareness = BallHandlers.ModifyCatchRate(ball, rareness, battler);
				rareness = BallHandlers.ModifyCatchRate(ball, rareness.Value, this, battler);
				int x = (int)Math.Floor(((a * 3 - b * 2) * rareness.Value) / (a * 3f));
				if (battler.Status == Status.SLEEP || battler.Status == Status.FROZEN)
					x = (int)Math.Floor(x * 2.5f);
				else if (battler.Status != Status.NONE)
					x = (int)Math.Floor(x * 1.5f);
				int c = 0;
				if (Game.GameData.Trainer != null)
					if (Game.GameData.Trainer.pokedexOwned() > 600) //Game.GameData.Player.PokedexCaught
						c = (int)Math.Floor(x * 2.5f / 6);
					else if (Game.GameData.Trainer.pokedexOwned() > 450)
						c = (int)Math.Floor(x * 2f / 6);
					else if (Game.GameData.Trainer.pokedexOwned() > 300)
						c = (int)Math.Floor(x * 1.5f / 6);
					else if (Game.GameData.Trainer.pokedexOwned() > 150)
						c = (int)Math.Floor(x * 1f / 6);
					else if (Game.GameData.Trainer.pokedexOwned() > 30)
						c = (int)Math.Floor(x * .5 / 6);
				//int shakes = 0; bool critical = false;
				if (x > 255 || BallHandlers.IsUnconditional(ball,this,battler))
					shakes = 4;
				else
				{
					if (x < 1) x = 1;
					int y = (int)Math.Floor(65536 / (Math.Pow((255.0 / x), 0.1875)));//(255.0 / x) ^ 0.1875
					if (Core.USECRITICALCAPTURE && pbRandom(256) < 0)
					{
						critical = true;
						if (pbRandom(65536) < y) shakes = 4;
					}
					else
					{
						if (pbRandom(65536)<y				) shakes+=1;
						if (pbRandom(65536)<y && shakes==1	) shakes+=1;
						if (pbRandom(65536)<y && shakes==2	) shakes+=1;
						if (pbRandom(65536)<y && shakes==3	) shakes+=1;
					}
				}
				GameDebug.Log($"[Threw Poké Ball] #{itemname}, #{shakes} shakes (4=capture)");
				if (@scene is IPokeBattle_SceneIE s0) yield return s0.pbThrow(ball, (critical) ? 1 : shakes, critical, battler.Index, showplayer);
				switch (shakes)
				{
					case 0:
						yield return pbDisplay(Game._INTL("Oh no! The Pokémon broke free!"));
						BallHandlers.OnFailCatch(ball,this,battler);
						break;
					case 1:
						yield return pbDisplay(Game._INTL("Aww... It appeared to be caught!"));
						BallHandlers.OnFailCatch(ball,this,battler);
						break;
					case 2:
						yield return pbDisplay(Game._INTL("Aargh! Almost had it!"));
						BallHandlers.OnFailCatch(ball,this,battler);
						break;
					case 3:
						yield return pbDisplay(Game._INTL("Gah! It was so close, too!"));
						BallHandlers.OnFailCatch(ball,this,battler);
						break;
					case 4:
						yield return pbDisplayBrief(Game._INTL("Gotcha! {1} was caught!", pokemon.Name));
						if (@scene is IPokeBattle_SceneIE s1) yield return s1.pbThrowSuccess();
						if (Game.GameData is IItemCheck c0 && c0.pbIsSnagBall(ball) && @opponent.Length > 0)
						{
							pbRemoveFromParty(battler.Index, battler.pokemonIndex);
							battler.pbReset();
							battler.participants = new List<int>();
						}
						else
							@decision = BattleResults.CAPTURED;
						//Script is repeated below as `SetCatchInfo`
						if (Game.GameData is IItemCheck c1 && c1.pbIsSnagBall(ball))
						{
							pokemon.ot = this.player[0].name;
							pokemon.trainerID = this.player[0].id;
							//pokemon.OT = this.player[0].Trainer;
						}
						BallHandlers.OnCatch(ball,this,pokemon);
						pokemon.ballUsed = ball; //pbGetBallType(ball);
						if (pokemon is IPokemonMegaEvolution m0) m0.makeUnmega();   //rescue null
						if (pokemon is IPokemonMegaEvolution m1) m1.makeUnprimal(); //rescue null
						pokemon.pbRecordFirstMoves();
						BallHandlers.OnCatch(ball,this,pokemon);
						//pokemon = new Monster.Pokemon(pokemon, ball, pbIsSnagBall(ball) ? Monster.Pokemon.ObtainedMethod.SNAGGED : Monster.Pokemon.ObtainedMethod.MET);
						//pokemon.SetCatchInfos(this.pbPlayer(), ball, Item.pbIsSnagBall(ball) ? Monster.Pokemon.ObtainedMethod.SNAGGED : Monster.Pokemon.ObtainedMethod.MET);
						if (Core.GAINEXPFORCAPTURE)
						{
							battler.captured = true;
							yield return pbGainEXP();
							battler.captured = false;
						}
						if (Game.GameData.Trainer.pokedex) //Trainer has a Pokedex
						{
							if (!this.player[0].hasOwned(species))
							//if (Game.GameData.Player.Pokedex[(int)pokemon.Species, 1] == 0)
							{
								this.player[0].setOwned(species);
								//Game.GameData.Player.Pokedex[(int)pokemon.Species, 1] = 1;
								yield return pbDisplayPaused(Game._INTL("{1}'s data was added to the Pokédex.", pokemon.Name));
								if (@scene is IPokeBattle_SceneIE s2) yield return s2.pbShowPokedex(pokemon.Species);
							}
						}
						if (@scene is IPokeBattle_SceneIE s3) yield return s3.pbHideCaptureBall();
						if (Game.GameData is IItemCheck c2 && c2.pbIsSnagBall(ball) && @opponent.Length > 0)
						{
							if (pokemon is IPokemonShadowPokemon sp) sp.pbUpdateShadowMoves(); //rescue null
							@snaggedpokemon.Add((byte)battler.Index); //pokemon
						}
						else
							pbStorePokemon(pokemon);
						break;
				}
			}
		}
		#endregion
		
		#region Info about battle.
		//public bool pbDoubleBattleAllowed()
		//{
		//	if (!@fullparty1 && @party1.Length>Core.MAXPARTYSIZE) {
		//		return false;
		//	}
		//	if (!@fullparty2 && @party2.Length>Core.MAXPARTYSIZE) {
		//		return false;
		//	}
		//	ITrainer[] _opponent=@opponent;
		//	ITrainer[] _player=@player;
		//	// Wild battle
		//	if (_opponent?.Length == 0) {
		//		if (@party2.Length==1)
		//			return false;
		//		else if (@party2.Length==2)
		//			return true;
		//		else
		//			return false;
		//	}
		//	// Trainer battle
		//	else {
		//		if (_opponent?.Length > 0) {
		//			if (_opponent.Length==1) {
		//				//_opponent=_opponent[0];
		//			}
		//			else if (_opponent.Length!=2) { //less than 2?
		//				return false;
		//			}
		//		}
		//		//_player=_player;
		//		if (_player.Length > 0) {
		//			if (_player.Length==1) {
		//				//_player=_player[0];
		//			} else if (_player.Length!=2) {
		//				return false;
		//			}
		//		}
		//		if (_opponent.Length > 0) {
		//			int sendout1=pbFindNextUnfainted(@party2,0,pbSecondPartyBegin(1));
		//			int sendout2=pbFindNextUnfainted(@party2,pbSecondPartyBegin(1));
		//			if (sendout1<0 || sendout2<0) return false;
		//		}
		//		else {
		//			int sendout1=pbFindNextUnfainted(@party2,0);
		//			int sendout2=pbFindNextUnfainted(@party2,sendout1+1);
		//			if (sendout1<0 || sendout2<0) return false;
		//		}
		//	}
		//	if (_player.Length > 0) {
		//		int sendout1=pbFindNextUnfainted(@party1,0,pbSecondPartyBegin(0));
		//		int sendout2=pbFindNextUnfainted(@party1,pbSecondPartyBegin(0));
		//		if (sendout1<0 || sendout2<0) return false;
		//	}
		//	else {
		//		int sendout1=pbFindNextUnfainted(@party1,0);
		//		int sendout2=pbFindNextUnfainted(@party1,sendout1+1);
		//		if (sendout1<0 || sendout2<0) return false;
		//	}
		//	return true;
		//}
		//
		//public Weather pbWeather { get //()
		//	{
		//		for (int i = 0; i < battlers.Length; i++) {
		//			if (@battlers[i].hasWorkingAbility(Abilities.CLOUD_NINE) ||
		//				@battlers[i].hasWorkingAbility(Abilities.AIR_LOCK)) {
		//				return Weather.NONE;
		//			}
		//		}
		//		return @weather;
		//	}
		//}
		#endregion

		#region Get Battler Info.
		//public bool pbIsOpposing(int index)
		//{
		//	return (index % 2) == 1;
		//}
		//public bool pbOwnedByPlayer(int index)
		//{
		//	if (pbIsOpposing(index)) return false;
		//	if (@player.Length > 0 && index==2) return false;
		//	return true;
		//}
		//
		//public bool pbIsDoubleBattler(int index) {
		//	return (index>=2);
		//}
		///// <summary>
		///// Only used for Wish
		///// </summary>
		///// <param name="battlerindex"></param>
		///// <param name="pokemonindex"></param>
		///// <returns></returns>
		//public string ToString(int battlerindex, int pokemonindex) {
		//	IPokemon[] party=pbParty(battlerindex);
		//	if (pbIsOpposing(battlerindex)) {
		//		if (@opponent != null) {
		//			return Game._INTL("The foe {1}", party[pokemonindex].Name);
		//			//return Game._INTL("The foe {1}", party[battlerindex,pokemonindex].Name);
		//		}
		//		else {
		//			return Game._INTL("The wild {1}", party[pokemonindex].Name);
		//			//return Game._INTL("The wild {1}", party[battlerindex,pokemonindex].Name);
		//		}
		//	}
		//	else {
		//		return Game._INTL("{1}", party[pokemonindex].Name);
		//		//return Game._INTL("{1}", party[battlerindex,pokemonindex].Name);
		//	}
		//}
		//
		///// <summary>
		///// Checks whether an item can be removed from a Pokémon.
		///// </summary>
		///// <param name="pkmn"></param>
		///// <param name="item"></param>
		///// <returns></returns>
		//public bool pbIsUnlosableItem(IBattlerIE pkmn, Items item) {
		//	if (Kernal.ItemData[item].IsLetter) return true; //pbIsMail(item)
		//	if (pkmn.effects.Transform) return false;
		//	if (pkmn.Ability == Abilities.MULTITYPE) {
		//		Items[] plates= new Items[] { Items.FIST_PLATE, Items.SKY_PLATE, Items.TOXIC_PLATE, Items.EARTH_PLATE, Items.STONE_PLATE,
		//				Items.INSECT_PLATE, Items.SPOOKY_PLATE, Items.IRON_PLATE, Items.FLAME_PLATE, Items.SPLASH_PLATE,
		//				Items.MEADOW_PLATE, Items.ZAP_PLATE, Items.MIND_PLATE, Items.ICICLE_PLATE, Items.DRACO_PLATE,
		//				Items.DREAD_PLATE, Items.PIXIE_PLATE };
		//		foreach (var i in plates) {
		//			if (item == i) return true;
		//		}
		//	}
		//	KeyValuePair<Pokemons, Items>[] combos= new KeyValuePair<Pokemons, Items>[] {
		//		new KeyValuePair<Pokemons, Items> (Pokemons.GIRATINA,		Items.GRISEOUS_ORB),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.GENESECT,		Items.BURN_DRIVE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.GENESECT,		Items.CHILL_DRIVE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.GENESECT,		Items.DOUSE_DRIVE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.GENESECT,		Items.SHOCK_DRIVE),
		//		// Mega Stones
		//		new KeyValuePair<Pokemons, Items> (Pokemons.ABOMASNOW,		Items.ABOMASITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.ABSOL,		    Items.ABSOLITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.AERODACTYL,		Items.AERODACTYLITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.AGGRON,		    Items.AGGRONITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.ALAKAZAM,		Items.ALAKAZITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.ALTARIA,		Items.ALTARIANITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.AMPHAROS,		Items.AMPHAROSITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.AUDINO,		    Items.AUDINITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.BANETTE,		Items.BANETTITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.BEEDRILL,		Items.BEEDRILLITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.BLASTOISE,		Items.BLASTOISINITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.BLAZIKEN,		Items.BLAZIKENITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.CAMERUPT,		Items.CAMERUPTITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.CHARIZARD,		Items.CHARIZARDITE_X),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.CHARIZARD,		Items.CHARIZARDITE_Y),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.DIANCIE,		Items.DIANCITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.GALLADE,		Items.GALLADITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.GARCHOMP,		Items.GARCHOMPITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.GARDEVOIR,		Items.GARDEVOIRITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.GENGAR,		    Items.GENGARITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.GLALIE,		    Items.GLALITITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.GYARADOS,		Items.GYARADOSITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.HERACROSS,		Items.HERACRONITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.HOUNDOOM,		Items.HOUNDOOMINITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.KANGASKHAN,		Items.KANGASKHANITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.LATIAS,		    Items.LATIASITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.LATIOS,		    Items.LATIOSITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.LOPUNNY,		Items.LOPUNNITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.LUCARIO,		Items.LUCARIONITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.MANECTRIC,		Items.MANECTITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.MAWILE,		    Items.MAWILITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.MEDICHAM,		Items.MEDICHAMITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.METAGROSS,	   	Items.METAGROSSITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.MEWTWO,		    Items.MEWTWONITE_X),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.MEWTWO,		    Items.MEWTWONITE_Y),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.PIDGEOT,		Items.PIDGEOTITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.PINSIR,		    Items.PINSIRITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.SABLEYE,		Items.SABLENITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.SALAMENCE,		Items.SALAMENCITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.SCEPTILE,		Items.SCEPTILITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.SCIZOR,		    Items.SCIZORITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.SHARPEDO,		Items.SHARPEDONITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.SLOWBRO,		Items.SLOWBRONITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.STEELIX,		Items.STEELIXITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.SWAMPERT,		Items.SWAMPERTITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.TYRANITAR,		Items.TYRANITARITE),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.VENUSAUR,		Items.VENUSAURITE),
		//		// Primal Reversion stones
		//		new KeyValuePair<Pokemons, Items> (Pokemons.KYOGRE,		    Items.BLUE_ORB),
		//		new KeyValuePair<Pokemons, Items> (Pokemons.GROUDON,		Items.RED_ORB)
		//	};
		//	foreach (KeyValuePair<Pokemons, Items> i in combos) {
		//		if (pkmn.Species == (Pokemons)i.Key && item == (Items)i.Value) {
		//			return true;
		//		}
		//	}
		//	return false;
		//}
		//
		//public IBattlerIE pbCheckGlobalAbility(Abilities a) {
		//	for (int i = 0; i < battlers.Length; i++) { // in order from own first, opposing first, own second, opposing second
		//		if (@battlers[i].hasWorkingAbility(a)) {
		//			return @battlers[i];
		//		}
		//	}
		//	return null;
		//}
		#endregion

		#region Player-related Info.
		//public ITrainer pbPlayer() {
		//	if (@player?.Length > 0) {
		//		return @player[0];
		//	}
		//	else {
		//		return new Trainer(battlers[0].Name, TrainerTypes.WildPokemon); //null;
		//	}
		//}
		//
		//public Items[] pbGetOwnerItems(int battlerIndex) {
		//	if (@items == null) return new Items[0];
		//	if (pbIsOpposing(battlerIndex)) {
		//		if (@opponent.Length > 0) {
		//			return (battlerIndex==1) ? @items[0] : @items[1];
		//		}
		//		else {
		//			return new Items[0]; //Wild pokemons dont get a bag, only held items...
		//		}
		//	}
		//	else {
		//		return new Items[0]; //Maybe sort option out for ally to access [their own] bag inventory too?
		//	}
		//}
		//
		//public void pbSetSeen(IPokemon pokemon) {
		//	if (Game.GameData.Trainer.pokedex &&
		//		(pokemon.IsNotNullOrNone() && @internalbattle)) { //Trainer has a Pokedex
		//		this.pbPlayer().seen[pokemon.Species]=true;
		//		if (Game.GameData is IGameUtility g) g.pbSeenForm(pokemon);
		//		//Game.GameData.Player.Pokedex[(int)pokemon.Species,0] = 1;
		//		//Game.GameData.Player.Pokedex[(int)pokemon.Species,2] = (byte)pokemon.form;
		//	}
		//}
		//
		//public string pbGetMegaRingName(int battlerIndex) {
		//	if (pbBelongsToPlayer(battlerIndex)) {
		//		foreach (Items i in Core.MEGARINGS) {
		//			//if (!hasConst(PBItems,i)) continue;
		//			if (Game.GameData.Bag.pbQuantity(i)>0) return Game._INTL(i.ToString(TextScripts.Name));
		//			//if (Game.GameData.Player.Bag.GetItemAmount(i)>0) return Game._INTL(i.ToString(TextScripts.Name));
		//		}
		//	}
		//	// Add your own Mega objects for particular trainer types here
		//	if (pbGetOwner(battlerIndex).trainertype == TrainerTypes.BUGCATCHER) {
		//		return Game._INTL("Mega Net");
		//	}
		//	return Game._INTL("Mega Ring");
		//}
		//
		//public bool pbHasMegaRing(int battlerIndex) {
		//	if (!pbBelongsToPlayer(battlerIndex)) return true;
		//	foreach (Items i in Core.MEGARINGS) {
		//		//if (!hasConst(PBItems,i)) continue;
		//		if (Game.GameData.Bag.pbQuantity(i)>0) return true;
		//		//if (Game.GameData.Player.Bag.GetItemAmount(i)>0) return true;
		//	}
		//	return false;
		//}
		#endregion
		
		#region Get party info, manipulate parties.
		//public int PokemonCount(IPokemon[] party)
		//{
		//	int count = 0;
		//	for (int i = 0; i < party.Length; i++)
		//	{
		//		if (party[i].Species == Pokemons.NONE) continue;
		//		if (party[i].HP > 0 && !party[i].isEgg) count += 1;
		//	}
		//	return count;
		//}
		//public bool AllFainted(IPokemon[] party)
		//{
		//	return PokemonCount(party) == 0;
		//}
		//public int MaxLevel(IPokemon[] party)
		//{
		//	int lv = 0;
		//	for (int i = 0; i < party.Length; i++)
		//	{
		//		if (party[i].Species == Pokemons.NONE) continue;
		//		if (lv < party[i].Level) lv = party[i].Level;
		//	}
		//	return lv;
		//}
		//public int pbPokemonCount(IPokemon[] party) {
		//	int count=0;
		//	foreach (IPokemon i in party) {
		//		if (!i.IsNotNullOrNone()) continue;
		//		if (i.HP>0 && !i.isEgg) count+=1;
		//	}
		//	return count;
		//}
		//
		//public bool pbAllFainted (IPokemon[] party) {
		//	return pbPokemonCount(party)==0;
		//}
		//
		//public int pbMaxLevel(IPokemon[] party) {
		//	int lv=0;
		//	foreach (var i in party) {
		//		if (!i.IsNotNullOrNone()) continue;
		//		if (lv<i.Level) lv=i.Level;
		//	}
		//	return lv;
		//}
		//
		//public int pbMaxLevelFromIndex(int index) {
		//	IPokemon[] party=pbParty(index);
		//	ITrainer[] owner=pbIsOpposing(index) ? @opponent : @player;
		//	int maxlevel=0;
		//	if (owner.Length > 0) {
		//		int start=0;
		//		int limit=pbSecondPartyBegin(index);
		//		if (pbIsDoubleBattler(index)) start=limit;
		//		for (int i = start; i < start+limit; i++) {
		//			if (!party[i].IsNotNullOrNone()) continue;
		//			if (maxlevel<party[i].Level) maxlevel=party[i].Level;
		//		}
		//	}
		//	else {
		//		foreach (var i in party) {
		//			if (!i.IsNotNullOrNone()) continue;
		//			if (maxlevel<i.Level) maxlevel=i.Level;
		//		}
		//	}
		//	return maxlevel;
		//}
		//
		///// <summary>
		///// Returns the trainer party of pokemon at this index?
		///// </summary>
		///// <param name="index"></param>
		///// <returns></returns>
		//public IPokemon[] pbParty(int index) {
		//	return pbIsOpposing(index) ? party2 : party1;
		//	//return battlers.Where(b => (b.Index % 2) == (index % 2)).ToArray();
		//}
		//
		//public IPokemon[] pbOpposingParty(int index) {
		//	return pbIsOpposing(index) ? party1 : party2;
		//}
		//
		//public int pbSecondPartyBegin(int battlerIndex) {
		//	if (pbIsOpposing(battlerIndex)) {
		//		//return @fullparty2 ? 6 : 3; //split in half for doubles
		//		return @fullparty2 ? (Game.GameData as Game).Features.LimitPokemonPartySize : (int)(Core.MAXPARTYSIZE * .5);
		//	}
		//	else {
		//		//return @fullparty1 ? 6 : 3; //split in half for doubles
		//		return @fullparty1 ? (Game.GameData as Game).Features.LimitPokemonPartySize : (int)(Core.MAXPARTYSIZE * .5);
		//	}
		//}
		//
		//public int pbPartyLength(int battlerIndex) {
		//	if (pbIsOpposing(battlerIndex)) {
		//		return (@opponent.Length > 0) ? pbSecondPartyBegin(battlerIndex) : (Game.GameData as Game).Features.LimitPokemonPartySize;
		//	}
		//	else {
		//		return @player.Length > 0 ? pbSecondPartyBegin(battlerIndex) : (Game.GameData as Game).Features.LimitPokemonPartySize;
		//	}
		//}
		//
		//public int pbFindNextUnfainted(IPokemon[] party,int start,int finish=-1) {
		//	if (finish<0) finish=party.Length;
		//	for (int i = start; i < finish; i++) {
		//		if (!party[i].IsNotNullOrNone()) continue;
		//		if (party[i].HP>0 && !party[i].isEgg) return i;
		//	}
		//	return -1;
		//}
		//
		//public int pbGetLastPokeInTeam(int index) {
		//	IPokemon[] party=pbParty(index);
		//	int[] partyorder=(!pbIsOpposing(index) ? @party1order : @party2order).ToArray();
		//	int plength=pbPartyLength(index);
		//	int pstart=pbGetOwnerIndex(index)*plength;
		//	int lastpoke=-1;
		//	for (int i = pstart; i < pstart+plength - 1; i++) {
		//		IPokemon p=party[partyorder[i]];
		//		if (!p.IsNotNullOrNone() || p.isEgg || p.HP<=0) continue;
		//		lastpoke=partyorder[i];
		//	}
		//	return lastpoke;
		//}
		//
		new public IBattlerIE pbFindPlayerBattler(int pkmnIndex) {
			IBattlerIE battler=null;
			for (int k = 0; k < battlers.Length; k++) {
				if (!pbIsOpposing(k) && @battlers[k].pokemonIndex==pkmnIndex) {
					battler=@battlers[k];
					break;
				}
			}
			return battler;
		}
		//
		//public bool pbIsOwner (int battlerIndex, int partyIndex) {
		//	int secondParty=pbSecondPartyBegin(battlerIndex);
		//	if (!pbIsOpposing(battlerIndex)) {
		//		if (@player ==  null || @player.Length == 0) return true;
		//		return (battlerIndex==0) ? partyIndex<secondParty : partyIndex>=secondParty;
		//	}
		//	else {
		//		if (@opponent == null || @opponent.Length == 0) return true;
		//		return (battlerIndex==1) ? partyIndex<secondParty : partyIndex>=secondParty;
		//	}
		//}
		//
		//public ITrainer pbGetOwner(int battlerIndex) {
		//	if (pbIsOpposing(battlerIndex)) {
		//		if (@opponent.Length > 0) {
		//			return (battlerIndex==1) ? @opponent[0] : @opponent[1];
		//		}
		//		else {
		//			//return null; //@opponent;
		//			return new Trainer(null,TrainerTypes.WildPokemon);
		//		}
		//	}
		//	else {
		//		if (@player.Length > 0) {
		//			return (battlerIndex==0) ? @player[0] : @player[1];
		//		}
		//		else {
		//			//return null; //@player;
		//			return new Trainer(null,TrainerTypes.WildPokemon);
		//		}
		//	}
		//}
		//
		//public ITrainer pbGetOwnerPartner(int battlerIndex) {
		//	if (pbIsOpposing(battlerIndex)) {
		//		if (@opponent.Length > 0) {
		//			return (battlerIndex==1) ? @opponent[1] : @opponent[0];
		//		}
		//		else {
		//			//return @opponent[0];
		//			return new Trainer(null,TrainerTypes.WildPokemon);
		//		}
		//	}
		//	else {
		//		if (@player.Length > 0) {
		//			return (battlerIndex==0) ? @player[1] : @player[0];
		//		}
		//		else {
		//			//return @player[0];
		//			return new Trainer(null,TrainerTypes.WildPokemon);
		//		}
		//	}
		//}
		//
		//public int pbGetOwnerIndex(int battlerIndex) {
		//	if (pbIsOpposing(battlerIndex)) {
		//		return (@opponent.Length > 0) ? ((battlerIndex==1) ? 0 : 1) : 0;
		//	}
		//	else {
		//		return (@player.Length > 0) ? ((battlerIndex==0) ? 0 : 1) : 0;
		//	}
		//}
		//
		//public bool pbBelongsToPlayer (int battlerIndex) {
		//	if (@player.Length > 0 && @player.Length>1) {
		//		return battlerIndex==0;
		//	}
		//	else {
		//		return (battlerIndex%2)==0;
		//	}
		//	//return false;
		//}
		//
		//public ITrainer pbPartyGetOwner(int battlerIndex, int partyIndex) {
		//	int secondParty=pbSecondPartyBegin(battlerIndex);
		//	if (!pbIsOpposing(battlerIndex)) {
		//		if (@player == null || @player.Length == 0) return new Trainer(null,TrainerTypes.WildPokemon);//wild pokemon instead of @player?
		//		return (partyIndex<secondParty) ? @player[0] : @player[1];
		//	}
		//	else {
		//		if (@opponent == null || @opponent.Length == 0) return new Trainer(null,TrainerTypes.WildPokemon);//wild pokemon instead of @opponent?
		//		return (partyIndex<secondParty) ? @opponent[0] : @opponent[1];
		//	}
		//}
		//
		//public void pbAddToPlayerParty(IPokemon pokemon) {
		//	IPokemon[] party=pbParty(0);
		//	for (int i = 0; i < party.Length; i++) {
		//		if (pbIsOwner(0,i) && !party[i].IsNotNullOrNone()) party[i]=pokemon;
		//	}
		//}
		//
		//public void pbRemoveFromParty(int battlerIndex, int partyIndex) {
		//	IPokemon[] party=pbParty(battlerIndex);
		//	ITrainer[] side=pbIsOpposing(battlerIndex) ? @opponent : @player;
		//	int[] order=(pbIsOpposing(battlerIndex) ? @party2order : @party1order).ToArray();
		//	int secondpartybegin=pbSecondPartyBegin(battlerIndex);
		//	party[partyIndex]=null;
		//	if (side == null || side.Length == 1) { // Wild or single opponent
		//		party.PackParty();//compact!
		//		for (int i = partyIndex; i < party.Length+1; i++) {
		//			for (int j = 0; j < @battlers.Length; j++) {
		//				if (!@battlers[j].IsNotNullOrNone()) continue;
		//				if (pbGetOwner(j)==side[0] && @battlers[j].pokemonIndex==i) {
		//					(_battlers[j] as IBattlerIE).pokemonIndex-=1; //Remove pokemon from party and adjust ref position for new party line-up
		//					break;
		//				}
		//			}
		//		}
		//		for (int i = 0; i < order.Length; i++) {
		//			order[i]=(i==partyIndex) ? order.Length-1 : order[i]-1;
		//		}
		//	}
		//	else {
		//		if (partyIndex<secondpartybegin-1) {
		//			for (int i = partyIndex; i < secondpartybegin; i++) {
		//				if (i>=secondpartybegin-1) {
		//					party[i]=null;
		//				}
		//				else {
		//					party[i]=party[i+1];
		//				}
		//			}
		//			for (int i = 0; i < order.Length; i++) {
		//				if (order[i]>=secondpartybegin) continue;
		//				order[i]=(i==partyIndex) ? secondpartybegin-1 : order[i]-1;
		//			}
		//		}
		//		else {
		//			for (int i = partyIndex; i < secondpartybegin+pbPartyLength(battlerIndex); i++) {
		//				if (i>=party.Length-1) {
		//					party[i]=null;
		//				}
		//				else {
		//					party[i]=party[i+1];
		//				}
		//			}
		//			for (int i = 0; i < order.Length; i++) {
		//				if (order[i]<secondpartybegin) continue;
		//				order[i]=(i==partyIndex) ? secondpartybegin+pbPartyLength(battlerIndex)-1 : order[i]-1;
		//			}
		//		}
		//	}
		//}
		#endregion

		///// <summary>
		///// Check whether actions can be taken.
		///// </summary>
		///// <param name="idxPokemon"></param>
		///// <returns></returns>
		//public bool pbCanShowCommands(int idxPokemon)
		//{
		//	IBattlerIE thispkmn = @battlers[idxPokemon];
		//	if (thispkmn.isFainted()) return false;
		//	if (thispkmn.effects.TwoTurnAttack > 0) return false;
		//	if (thispkmn.effects.HyperBeam > 0) return false;
		//	if (thispkmn.effects.Rollout > 0) return false;
		//	if (thispkmn.effects.Outrage > 0) return false;
		//	if (thispkmn.effects.Uproar > 0) return false;
		//	if (thispkmn.effects.Bide > 0) return false;
		//	return true;
		//}

		#region Attacking
		public IEnumerator pbCanShowFightMenu(int idxPokemon, Action<bool> result = null)
		{
			IBattlerIE thispkmn = @battlers[idxPokemon];
			if (!pbCanShowCommands(idxPokemon)) { result?.Invoke(false); yield break; }

			bool canChooseMove0 = false;
			bool canChooseMove1 = false;
			bool canChooseMove2 = false;
			bool canChooseMove3 = false;
			yield return pbCanChooseMove(idxPokemon, 0, false, result:value=>canChooseMove0=value);
			yield return pbCanChooseMove(idxPokemon, 1, false, result:value=>canChooseMove1=value);
			yield return pbCanChooseMove(idxPokemon, 2, false, result:value=>canChooseMove2=value);
			yield return pbCanChooseMove(idxPokemon, 3, false, result:value=>canChooseMove3=value);
			// No moves that can be chosen
			if (!canChooseMove0 &&
				!canChooseMove1 &&
				!canChooseMove2 &&
				!canChooseMove3)
			{ result?.Invoke(false); yield break; }

			// Encore
			if (thispkmn.effects.Encore > 0) { result?.Invoke(false); yield break; }
			result?.Invoke(true);
		}
		
		public IEnumerator pbCanChooseMove(int idxPokemon, int idxMove, bool showMessages, bool sleeptalk = false, Action<bool> result = null)
		{
			IBattlerIE thispkmn = @battlers[idxPokemon];
			IBattleMove thismove = thispkmn.moves[idxMove];

			//ToDo: Array for opposing pokemons, [i] changes based on if double battle
			IBattlerIE opp1 = thispkmn.pbOpposing1;
			IBattlerIE opp2 = null; //ToDo: thispkmn.pbOpposing2;
			if (thismove == null || thismove.id == 0) { result?.Invoke(false); yield break; }
			if (thismove.PP <= 0 && thismove.TotalPP > 0 && !sleeptalk) {
				if (showMessages) yield return pbDisplayPaused(Game._INTL("There's no PP left for this move!"));
				result?.Invoke(false); yield break;
			}
			if (thispkmn.hasWorkingItem(Items.ASSAULT_VEST)) {// && thismove.IsStatus?
				if (showMessages) yield return pbDisplayPaused(Game._INTL("The effects of the {1} prevent status moves from being used!", Game._INTL(thispkmn.Item.ToString(TextScripts.Name))));
				result?.Invoke(false); yield break;
			}
			if ((int)thispkmn.effects.ChoiceBand >= 0 &&
			   (thispkmn.hasWorkingItem(Items.CHOICE_BAND) ||
			   thispkmn.hasWorkingItem(Items.CHOICE_SPECS) ||
			   thispkmn.hasWorkingItem(Items.CHOICE_SCARF)))
			{
				bool hasmove = false;
				for (int i = 0; i < thispkmn.moves.Length; i++)
					if (thispkmn.moves[i].id==thispkmn.effects.ChoiceBand) {
						hasmove = true; break;
					}
				if (hasmove && thismove.id != thispkmn.effects.ChoiceBand) {
					if (showMessages)
						yield return pbDisplayPaused(Game._INTL("{1} allows the use of only {2}!",
							Game._INTL(thispkmn.Item.ToString(TextScripts.Name)),
							Game._INTL(thispkmn.effects.ChoiceBand.ToString(TextScripts.Name))));
					result?.Invoke(false); yield break;
				}
			}
			if (opp1.IsNotNullOrNone() && opp1.effects.Imprison)
			{
				if (thismove.id == opp1.moves[0].id ||
					thismove.id == opp1.moves[1].id ||
					thismove.id == opp1.moves[2].id ||
					thismove.id == opp1.moves[3].id)
				{
					if (showMessages) yield return pbDisplayPaused(Game._INTL("{1} can't use the sealed {2}!", thispkmn.ToString(), Game._INTL(thismove.id.ToString(TextScripts.Name))));
					GameDebug.Log($"[CanChoose][#{opp1.ToString()} has: #{Game._INTL(opp1.moves[0].id.ToString(TextScripts.Name))}, #{Game._INTL(opp1.moves[1].id.ToString(TextScripts.Name))}, #{Game._INTL(opp1.moves[2].id.ToString(TextScripts.Name))}, #{Game._INTL(opp1.moves[3].id.ToString(TextScripts.Name))}]");
					result?.Invoke(false); yield break;
				}
			}
			if (opp2.IsNotNullOrNone() && opp2.effects.Imprison)
			{
				if (thismove.id == opp2.moves[0].id ||
					 thismove.id == opp2.moves[1].id ||
					 thismove.id == opp2.moves[2].id ||
					 thismove.id == opp2.moves[3].id)
				{
					if (showMessages) yield return pbDisplayPaused(Game._INTL("{1} can't use the sealed {2}!", thispkmn.ToString(), Game._INTL(thismove.id.ToString(TextScripts.Name))));
					GameDebug.Log($"[CanChoose][#{opp2.ToString()} has: #{Game._INTL(opp2.moves[0].id.ToString(TextScripts.Name))}, #{Game._INTL(opp2.moves[1].id.ToString(TextScripts.Name))}, #{Game._INTL(opp2.moves[2].id.ToString(TextScripts.Name))}, #{Game._INTL(opp2.moves[3].id.ToString(TextScripts.Name))}]");
					result?.Invoke(false); yield break;
				}
			}
			if (thispkmn.effects.Taunt > 0 && thismove.basedamage == 0) {
				if (showMessages) yield return pbDisplayPaused(Game._INTL("{1} can't use {2} after the taunt!", thispkmn.ToString(), Game._INTL(thismove.id.ToString(TextScripts.Name))));
				result?.Invoke(false); yield break;
			}
			if (thispkmn.effects.Torment) {
				if (thismove.id==thispkmn.lastMoveUsed) {
					if (showMessages) yield return pbDisplayPaused(Game._INTL("{1} can't use the same move twice in a row due to the torment!", thispkmn.ToString()));
					result?.Invoke(false); yield break;
				}
			}
			if (thismove.id==thispkmn.effects.DisableMove && !sleeptalk) {
				if (showMessages) yield return pbDisplayPaused(Game._INTL("{1}'s {2} is disabled!", thispkmn.ToString(), Game._INTL(thismove.id.ToString(TextScripts.Name))));
				result?.Invoke(false); yield break;
			}
			if (thismove.Effect==Attack.Data.Effects.x153 && // ToDo: Belch
			   (thispkmn.Species != Pokemons.NONE || !thispkmn.pokemon.belch)) {
				if (showMessages) yield return pbDisplayPaused(Game._INTL("{1} hasn't eaten any held berry, so it can't possibly belch!", thispkmn.ToString()));
				result?.Invoke(false); yield break;
			}
			if (thispkmn.effects.Encore>0 && idxMove!=thispkmn.effects.EncoreIndex) {
				result?.Invoke(false); yield break;
			}
			result?.Invoke(true);
		}

		new public IEnumerator pbAutoChooseMove(int idxPokemon, bool showMessages=true) {
			IBattlerIE thispkmn=@battlers[idxPokemon];
			if (thispkmn.isFainted()) {
				//@choices[idxPokemon][0]=0;
				//@choices[idxPokemon][1]=0;
				//@choices[idxPokemon][2]=null;
				@choices[idxPokemon]=new Choice(ChoiceAction.NoAction);
				yield break;
			}
			bool canChooseMove = false;
			yield return pbCanChooseMove(idxPokemon,thispkmn.effects.EncoreIndex,false,result:value=>canChooseMove=value);
			if (thispkmn.effects.Encore>0 &&
				canChooseMove) {
				GameDebug.Log($"[Auto choosing Encore move] #{Game._INTL(thispkmn.moves[thispkmn.effects.EncoreIndex].id.ToString(TextScripts.Name))}");
				//@choices[idxPokemon][0]=1;    // "Use move"
				//@choices[idxPokemon][1]=thispkmn.effects.EncoreIndex; // Index of move
				//@choices[idxPokemon][2]=thispkmn.moves[thispkmn.effects.EncoreIndex];
				//@choices[idxPokemon][3]=-1;   // No target chosen yet
				@choices[idxPokemon]=new Choice(ChoiceAction.UseMove, thispkmn.effects.EncoreIndex, thispkmn.moves[thispkmn.effects.EncoreIndex]);
				if (@doublebattle) {
					IBattleMove thismove=thispkmn.moves[thispkmn.effects.EncoreIndex];
					Attack.Data.Targets targets=thispkmn.pbTarget(thismove);
					if (targets==Attack.Data.Targets.SELECTED_POKEMON || targets==Attack.Data.Targets.SELECTED_POKEMON_ME_FIRST) { //Targets.SingleNonUser
						int target=-1;
						yield return (@scene as IPokeBattle_SceneIE).pbChooseTarget(idxPokemon,targets,result:value=>target=value);
						if (target>=0) pbRegisterTarget(idxPokemon,target);
					}
					else if (targets==Attack.Data.Targets.USER_OR_ALLY) { //Targets.UserOrPartner
						int target=-1;
						yield return (@scene as IPokeBattle_SceneIE).pbChooseTarget(idxPokemon,targets,result:value=>target=value);
						if (target>=0 && (target&1)==(idxPokemon&1)) pbRegisterTarget(idxPokemon,target); //both integers are Even (ally) and Identical (selected)
					}
				}
			}
			else {
				if (!pbIsOpposing(idxPokemon)) {
					if (showMessages) yield return pbDisplayPaused(Game._INTL("{1} has no moves left!",thispkmn.Name));
				}
				//@choices[idxPokemon][0]=1;           // "Use move"
				//@choices[idxPokemon][1]=-1;          // Index of move to be used
				//@choices[idxPokemon][2]=@struggle;   // Use Struggle
				//@choices[idxPokemon][3]=-1;          // No target chosen yet
				@choices[idxPokemon]=new Choice(ChoiceAction.UseMove, -1, @struggle);
			}
		}

		public IEnumerator pbRegisterMove(int idxPokemon, int idxMove, bool showMessages=true, Action<bool> result = null) {
			IBattlerIE thispkmn=@battlers[idxPokemon];
			IBattleMove thismove=thispkmn.moves[idxMove];
			bool canChooseMove = false;
			yield return pbCanChooseMove(idxPokemon,idxMove,showMessages,result:value=>canChooseMove=value);
			if (!canChooseMove) { result?.Invoke(false); yield break; }
			//@choices[idxPokemon][0]=1;         // "Use move"
			//@choices[idxPokemon][1]=idxMove;   // Index of move to be used
			//@choices[idxPokemon][2]=thismove;  // PokeBattle_Move object of the move
			//@choices[idxPokemon][3]=-1;        // No target chosen yet
			@choices[idxPokemon]=new Choice(ChoiceAction.UseMove, idxMove, thismove);
			result?.Invoke(true);
		}

		//public bool pbChoseMove (int i, Moves move) {
		//	if (@battlers[i].isFainted()) return false;
		//	//if (@choices[i][0]==1 && @choices[i][1]>=0) {
		//	if (@choices[i]?.Action==ChoiceAction.UseMove && @choices[i]?.Index>=0) {
		//		int choice=@choices[i].Index; //@choices[i][1];
		//		return @battlers[i].moves[choice].id == move;
		//	}
		//	return false;
		//}
		//
		//public bool pbChoseMoveFunctionCode (int i,Attack.Data.Effects code) {
		//	if (@battlers[i].isFainted()) return false;
		//	//if (@choices[i][0]==1 && @choices[i][1]>=0) {
		//	if (@choices[i]?.Action==ChoiceAction.UseMove && @choices[i]?.Index>=0) {
		//		int choice=@choices[i].Index; //@choices[i][1];
		//		return @battlers[i].moves[choice].Effect==code;
		//	}
		//	return false;
		//}
		//
		//public bool pbRegisterTarget(int idxPokemon, int idxTarget) {
		//	//@choices[idxPokemon][3]=idxTarget;   // Set target of move
		//	@choices[idxPokemon]=new Choice(ChoiceAction.UseMove, @choices[idxPokemon].Index, @choices[idxPokemon].Move, idxTarget);   // Set target of move
		//	return true;
		//}

		new public IBattlerIE[] pbPriority(bool ignorequickclaw=false, bool log=false) {
			if (@usepriority) return priority;	// use stored priority if round isn't over yet
			_priority = new PokemonUnity.Stadium.Battler[battlers.Length]; //.Clear();
			int[] speeds=new int[battlers.Length];
			int[] priorities=new int[battlers.Length];
			bool[] quickclaw=new bool[battlers.Length]; bool[] lagging=new bool[battlers.Length];
			int minpri=0; int maxpri=0;
			List<int> temp=new List<int>();
			#region Calculate each Pokémon's speed
			for (int i = 0; i < battlers.Length; i++) {
				speeds[i]=@battlers[i].SPE;
				quickclaw[i]=false;
				lagging[i]=false;
				//if (!ignorequickclaw && @choices[i][0]==1) { // Chose to use a move
				if (!ignorequickclaw && @choices[i]?.Action==ChoiceAction.UseMove) {
					if (!quickclaw[i] && @battlers[i].hasWorkingItem(Items.CUSTAP_BERRY) &&
						!@battlers[i].pbOpposing1.hasWorkingAbility(Abilities.UNNERVE) &&
						!@battlers[i].pbOpposing2.hasWorkingAbility(Abilities.UNNERVE)) {
						if ((@battlers[i].hasWorkingAbility(Abilities.GLUTTONY) && @battlers[i].HP<=(int)Math.Floor(@battlers[i].TotalHP * .5)) ||
							@battlers[i].HP<=(int)Math.Floor(@battlers[i].TotalHP * .25)) {
							pbCommonAnimation("UseItem",@battlers[i],null);
							quickclaw[i]=true;
							pbDisplayBrief(Game._INTL("{1}'s {2} let it move first!",
								@battlers[i].ToString(),Game._INTL(@battlers[i].Item.ToString(TextScripts.Name))));
							_battlers[i].pbConsumeItem();
						}
					}
					if (!quickclaw[i] && @battlers[i].hasWorkingItem(Items.QUICK_CLAW)) {
						if (pbRandom(10)<2) {
							pbCommonAnimation("UseItem",@battlers[i],null);
							quickclaw[i]=true;
							pbDisplayBrief(Game._INTL("{1}'s {2} let it move first!",
								@battlers[i].ToString(),Game._INTL(@battlers[i].Item.ToString(TextScripts.Name))));
						}
					}
					if (!quickclaw[i] &&
						(@battlers[i].hasWorkingAbility(Abilities.STALL) ||
						@battlers[i].hasWorkingItem(Items.LAGGING_TAIL) ||
						@battlers[i].hasWorkingItem(Items.FULL_INCENSE))) {
						lagging[i]=true;
					}
				}
			}
			#endregion
			#region Calculate each Pokémon's priority bracket, and get the min/max priorities
			for (int i = 0; i < battlers.Length; i++) {
				// Assume that doing something other than using a move is priority 0
				int pri=0;
				if (@choices[i]?.Action==ChoiceAction.UseMove) { // Chose to use a move
					pri=@choices[i].Move.Priority;
					if (@battlers[i].hasWorkingAbility(Abilities.PRANKSTER) &&
						@choices[i].Move.pbIsStatus) pri+=1;
					if (@battlers[i].hasWorkingAbility(Abilities.GALE_WINGS) &&
						@choices[i].Move.Type == Types.FLYING) pri+=1;
				}
				priorities[i]=pri;
				if (i==0) {
					minpri=pri;
					maxpri=pri;
				}
				else {
					if (minpri>pri) minpri=pri;
					if (maxpri<pri) maxpri=pri;
				}
			}
			#endregion
			// Find and order all moves with the same priority
			int curpri=maxpri;
			do { //loop
				temp.Clear();
				for (int j = 0; j < battlers.Length; j++) {
					if (priorities[j]==curpri) temp.Add(j);
				}
				// Sort by speed
				if (temp.Count==1) {
					_priority[_priority.Length]=@battlers[temp[0]]; //ToDo: Redo this, maybe use Math.Min to sort..
				}
				else if (temp.Count>1) {
					int n=temp.Count - 1;
					for (int m = 0; m < temp.Count-1; m++) {
						for (int i = 1; i < temp.Count; i++) {
							// For each pair of battlers, rank the second compared to the first
							// -1 means rank higher, 0 means rank equal, 1 means rank lower
							int cmp=0;
							if (quickclaw[temp[i]]) {
								cmp=-1;
								if (quickclaw[temp[i-1]]) {
									if (speeds[temp[i]]==speeds[temp[i-1]]) {
										cmp=0;
									}
									else {
										cmp=(speeds[temp[i]]>speeds[temp[i-1]]) ? -1 : 1;
									}
								}
							}
							else if (quickclaw[temp[i-1]]) {
								cmp=1;
							}
							else if (lagging[temp[i]]) {
								cmp=1;
								if (lagging[temp[i-1]]) {
									if (speeds[temp[i]]==speeds[temp[i-1]]) {
										cmp=0;
									}
									else {
										cmp=(speeds[temp[i]]>speeds[temp[i-1]]) ? 1 : -1;
									}
								}
							}
							else if (lagging[temp[i-1]]) {
								cmp=-1;
							}
							else if (speeds[temp[i]]!=speeds[temp[i-1]]) {
								if (@field.TrickRoom>0) {
									cmp=(speeds[temp[i]]>speeds[temp[i-1]]) ? 1 : -1;
								}
								else {
									cmp=(speeds[temp[i]]>speeds[temp[i-1]]) ? -1 : 1;
								}
							}
							if (cmp<0 || // Swap the pair according to the second battler's rank
								(cmp==0 && pbRandom(2)==0)) {
								int swaptmp=temp[i];
								temp[i]=temp[i-1];
								temp[i-1]=swaptmp;
							}
						}
					}
					// Battlers in this bracket are properly sorted, so add them to _priority
					int x = 0; foreach (int i in temp) {
						//_priority[_priority.Length - 1]=@battlers[i];
						_priority[x]=@battlers[i]; x++;
					}
				}
				curpri-=1;
				if (curpri<minpri) break;
			} while (true);
			// Write the priority order to the debug log
			if (log) {
				string d="[Priority] "; bool comma=false;
				for (int i = 0; i < battlers.Length; i++) {
					if (_priority[i].IsNotNullOrNone() && !_priority[i].isFainted()) {
						if (comma) d+=", ";
						d+=$"#{_priority[i].ToString(comma)} (#{_priority[i].Index})"; comma=true;
					}
				}
				GameDebug.Log(d);
			}
			@usepriority=true;
			return priority;
		}
		#endregion

		#region Switching Pokemon
		public IEnumerator pbCanSwitchLax (int idxPokemon,int pkmnidxTo,bool showMessages,System.Action<bool> result=null) {
			if (pkmnidxTo>=0) {
				IPokemon[] party=pbParty(idxPokemon);
				if (pkmnidxTo>=party.Length) {
					result?.Invoke(false); yield break;
				}
				if (!party[pkmnidxTo].IsNotNullOrNone()) {
					result?.Invoke(false); yield break;
				}
				if (party[pkmnidxTo].isEgg) {
					if (showMessages) yield return pbDisplayPaused(Game._INTL("An Egg can't battle!"));
					result?.Invoke(false); yield break;
				}
				if (!pbIsOwner(idxPokemon,pkmnidxTo)) {
					ITrainer owner=pbPartyGetOwner(idxPokemon,pkmnidxTo);
					if (showMessages) yield return pbDisplayPaused(Game._INTL("You can't switch {1}'s Pokémon with one of yours!",owner.name));
					result?.Invoke(false); yield break;
				}
				if (party[pkmnidxTo].HP<=0) {
					if (showMessages) yield return pbDisplayPaused(Game._INTL("{1} has no energy left to battle!",party[pkmnidxTo].Name));
					result?.Invoke(false); yield break;
				}
				if (@battlers[idxPokemon].pokemonIndex==pkmnidxTo ||
					@battlers[idxPokemon].pbPartner.pokemonIndex==pkmnidxTo) {
					if (showMessages) yield return pbDisplayPaused(Game._INTL("{1} is already in battle!",party[pkmnidxTo].Name));
					result?.Invoke(false); yield break;
				}
			}
			result?.Invoke(true);
		}

		public IEnumerator pbCanSwitch (int idxPokemon, int pkmnidxTo, bool showMessages, bool ignoremeanlook=false,System.Action<bool> result=null) {
			IBattlerIE thispkmn=@battlers[idxPokemon];
			bool canSwitchLax = false;
			yield return pbCanSwitchLax(idxPokemon, pkmnidxTo, showMessages);
			// Multi-Turn Attacks/Mean Look
			if (!canSwitchLax) {
				result?.Invoke(false); yield break;
			}
			bool isOppose=pbIsOpposing(idxPokemon);
			IPokemon[] party=pbParty(idxPokemon);
			for (int i = 0; i < battlers.Length; i++) {
				if (isOppose!=pbIsOpposing(i)) continue;
				if (choices[i]?.Action==ChoiceAction.SwitchPokemon && choices[i]?.Index==pkmnidxTo) {
					if (showMessages) yield return pbDisplayPaused(Game._INTL("{1} has already been selected.",party[pkmnidxTo].Name));
					result?.Invoke(false); yield break;
				}
			}
			if (thispkmn.hasWorkingItem(Items.SHED_SHELL)) {
				result?.Invoke(true); yield break;
			}
			if (Core.USENEWBATTLEMECHANICS && thispkmn.pbHasType(Types.GHOST)) {
				result?.Invoke(true); yield break;
			}
			if (thispkmn.effects.MultiTurn>0 ||
				(!ignoremeanlook && thispkmn.effects.MeanLook>=0)) {
				if (showMessages) yield return pbDisplayPaused(Game._INTL("{1} can't be switched out!",thispkmn.ToString()));
				result?.Invoke(false); yield break;
			}
			if (@field.FairyLock>0) {
				if (showMessages) yield return pbDisplayPaused(Game._INTL("{1} can't be switched out!",thispkmn.ToString()));
				result?.Invoke(false); yield break;
			}
			if (thispkmn.effects.Ingrain) {
				if (showMessages) yield return pbDisplayPaused(Game._INTL("{1} can't be switched out!",thispkmn.ToString()));
				result?.Invoke(false); yield break;
			}
			IBattlerIE opp1=thispkmn.pbOpposing1;
			IBattlerIE opp2=thispkmn.pbOpposing2;
			IBattlerIE opp=null;
			if (thispkmn.pbHasType(Types.STEEL)) {
				if (opp1.hasWorkingAbility(Abilities.MAGNET_PULL)) opp=opp1;
				if (opp2.hasWorkingAbility(Abilities.MAGNET_PULL)) opp=opp2;
			}
			if (!thispkmn.isAirborne()) {
				if (opp1.hasWorkingAbility(Abilities.ARENA_TRAP)) opp=opp1;
				if (opp2.hasWorkingAbility(Abilities.ARENA_TRAP)) opp=opp2;
			}
			if (!thispkmn.hasWorkingAbility(Abilities.SHADOW_TAG)) {
				if (opp1.hasWorkingAbility(Abilities.SHADOW_TAG)) opp=opp1;
				if (opp2.hasWorkingAbility(Abilities.SHADOW_TAG)) opp=opp2;
			}
			if (opp.IsNotNullOrNone()) {
				string abilityname=Game._INTL(opp.Ability.ToString(TextScripts.Name));
				if (showMessages) yield return pbDisplayPaused(Game._INTL("{1}'s {2} prevents switching!",opp.ToString(),abilityname));
				result?.Invoke(false); yield break;
			}
			result?.Invoke(true);
		}

		public IEnumerator pbRegisterSwitch(int idxPokemon,int idxOther,System.Action<bool> result=null) {
			bool canSwitch = false;
			yield return pbCanSwitch(idxPokemon,idxOther,false,result:value=>canSwitch=value);
			if (!canSwitch) { result?.Invoke(false); yield break; }
			//@choices[idxPokemon][0]=2;          // "Switch Pokémon"
			//@choices[idxPokemon][1]=idxOther;   // Index of other Pokémon to switch with
			//@choices[idxPokemon][2]=null;
			@choices[idxPokemon]=new Choice(ChoiceAction.SwitchPokemon, idxOther);
			int side=pbIsOpposing(idxPokemon) ? 1 : 0;
			int owner=pbGetOwnerIndex(idxPokemon);
			if (@megaEvolution[side][owner]==idxPokemon) {
				@megaEvolution[side][owner]=-1;
			}
			result?.Invoke(true);
		}

		//public bool pbCanChooseNonActive (int index) {
		//	IPokemon[] party=pbParty(index);
		//	for (int i = 0; i < party.Length; i++) {
		//		if (pbCanSwitchLax(index,i,false)) return true;
		//	}
		//	return false;
		//}

		new public IEnumerator pbSwitch(bool favorDraws=false) {
			if (!favorDraws) {
				if (@decision>0) yield break;
			}
			else {
				if (@decision==BattleResults.DRAW) yield break;
			}
			pbJudge();
			if (@decision>0) yield break;
			int firstbattlerhp=@battlers[0].HP;
			List<int> switched=new List<int>();
			bool confirm=false;
			for (int index = 0; index < battlers.Length; index++) {
				int newenemy=-1; int newenemyname; int newpokename; int newpoke=-1;
				if (!@doublebattle && pbIsDoubleBattler(index)) continue;
				if (@battlers[index].IsNotNullOrNone() && !@battlers[index].isFainted()) continue;
				if (!pbCanChooseNonActive(index)) continue;
				if (!pbOwnedByPlayer(index)) {
					if (!pbIsOpposing(index) || (@opponent.Length > 0 && pbIsOpposing(index))) {
						yield return pbSwitchInBetween(index,false,false,result:value=>newenemy=value);
						newenemyname=newenemy;
						if (newenemy>=0 && pbParty(index)[newenemy].Ability == Abilities.ILLUSION) {
							newenemyname=pbGetLastPokeInTeam(index);
						}
						ITrainer opponent=pbGetOwner(index);
						if (!@doublebattle && firstbattlerhp>0 && @shiftStyle && this.opponent.Length > 0 &&
							@internalbattle && pbCanChooseNonActive(0) && pbIsOpposing(index) &&
							@battlers[0].effects.Outrage==0) {
							yield return pbDisplayPaused(Game._INTL("{1} is about to send in {2}.",opponent.name,pbParty(index)[newenemyname].Name));
							yield return pbDisplayConfirm(Game._INTL("Will {1} change Pokémon?",this.pbPlayer().name),result:value=>confirm=value);
							if (confirm) {
								yield return pbSwitchPlayer(0,true,true,result:value=>newpoke=value);
								if (newpoke>=0) {
									newpokename=newpoke;
									if (@party1[newpoke].Ability == Abilities.ILLUSION) {
										newpokename=pbGetLastPokeInTeam(0);
									}
									yield return pbDisplayBrief(Game._INTL("{1}, that's enough! Come back!",@battlers[0].Name));
									yield return pbRecallAndReplace(0,newpoke,newpokename);
									switched.Add(0);
								}
							}
						}
						yield return pbRecallAndReplace(index,newenemy,newenemyname,false,false);
						switched.Add(index);
					}
				}
				else if (@opponent.Length > 0) {
					yield return pbSwitchInBetween(index,true,false,result:value=>newpoke=value);
					newpokename=newpoke;
					if (@party1[newpoke].Ability == Abilities.ILLUSION) {
						newpokename=pbGetLastPokeInTeam(index);
					}
					yield return pbRecallAndReplace(index,newpoke,newpokename);
					switched.Add(index);
				}
				else {
					bool swtch=false; 
					yield return pbDisplayConfirm(Game._INTL("Use next Pokémon?"),result:value=>confirm=value);
					if (!confirm) { 
						yield return pbRun(index,true,result:value=>swtch=value<=0);
					}
					else {
						swtch=true;
					}
					if (swtch) {
						yield return pbSwitchInBetween(index,true,false,result:value=>newpoke=value);
						newpokename=newpoke;
						if (@party1[newpoke].Ability == Abilities.ILLUSION) {
							newpokename=pbGetLastPokeInTeam(index);
						}
						yield return pbRecallAndReplace(index,newpoke,newpokename);
						switched.Add(index);
					}
				}
			}
			if (switched.Count>0) {
				_priority=pbPriority();
				foreach (var i in priority) {
					if (switched.Contains(i.Index)) yield return i.pbAbilitiesOnSwitchIn(true);
				}
			}
		}

		new public IEnumerator pbSendOut(int index,IPokemon pokemon) {
			pbSetSeen(pokemon);
			if(@peer is IBattlePeerMultipleForms p) p.pbOnEnteringBattle(this,pokemon);
			if (pbIsOpposing(index)) {
				if (@scene is IPokeBattle_SceneIE s0) yield return s0.pbTrainerSendOut(index,pokemon);
			}
			else {
				if (@scene is IPokeBattle_SceneIE s0) yield return s0.pbSendOut(index,pokemon);
			}
			if (@scene is IPokeBattle_SceneIE s1) s1.pbResetMoveIndex(index);
		}

		new public IEnumerator pbReplace(int index,int newpoke,bool batonpass=false) {
			IPokemon[] party=pbParty(index);
			int oldpoke=@battlers[index].pokemonIndex;
			// Initialize the new Pokémon
			(_battlers[index] as IBattlerIE).pbInitialize(party[newpoke],(sbyte)newpoke,batonpass);
			// Reorder the party for this battle
			int[] partyorder=(!pbIsOpposing(index) ? @party1order : @party2order).ToArray();
			int bpo=-1; int bpn=-1;
			for (int i = 0; i < partyorder.Length; i++) {
				if (partyorder[i]==oldpoke) bpo=i;
				if (partyorder[i]==newpoke) bpn=i;
			}
			int p=partyorder[bpo]; partyorder[bpo]=partyorder[bpn]; partyorder[bpn]=p;
			// Send out the new Pokémon
			yield return pbSendOut(index,party[newpoke]);
			pbSetSeen(party[newpoke]);
		}

		public IEnumerator pbRecallAndReplace(int index,int newpoke,int newpokename=-1,bool batonpass=false,bool moldbreaker=false, System.Action<bool> result = null) {
			(_battlers[index] as IBattlerIE).pbResetForm();
			if (!@battlers[index].isFainted()) {
				yield return (@scene as IPokeBattle_SceneIE).pbRecall(index);
			}
			yield return pbMessagesOnReplace(index,newpoke,newpokename);
			yield return pbReplace(index,newpoke,batonpass);
			//result?.Invoke(pbOnActiveOne(@battlers[index],false,moldbreaker));
			yield return pbOnActiveOne(@battlers[index],false,moldbreaker,result);
		}

		new public IEnumerator pbMessagesOnReplace(int index,int newpoke,int newpokename=-1) {
			if (newpokename<0) newpokename=newpoke;
			IPokemon[] party=pbParty(index);
			if (pbOwnedByPlayer(index)) {
				if (!party[newpoke].IsNotNullOrNone()) {
					//p [index,newpoke,party[newpoke],pbAllFainted(party)];
					GameDebug.Log($"[{index},{newpoke},{party[newpoke]},pbMOR]");
					for (int i = 0; i < party.Length; i++) {
						GameDebug.Log($"[{i},{party[i].HP}]");
					}
					//throw new BattleAbortedException();
					GameDebug.LogError("BattleAbortedException"); pbAbort();
				}
				IBattlerIE opposing=@battlers[index].pbOppositeOpposing;
				if (opposing.isFainted() || opposing.HP==opposing.TotalHP) {
					yield return pbDisplayBrief(Game._INTL("Go! {1}!",party[newpokename].Name));
				}
				else if (opposing.HP>=(opposing.TotalHP/2)) {
					yield return pbDisplayBrief(Game._INTL("Do it! {1}!",party[newpokename].Name));
				}
				else if (opposing.HP>=(opposing.TotalHP/4)) {
					yield return pbDisplayBrief(Game._INTL("Go for it, {1}!",party[newpokename].Name));
				}
				else {
					yield return pbDisplayBrief(Game._INTL("Your opponent's weak!\nGet 'em, {1}!",party[newpokename].Name));
				}
				GameDebug.Log($"[Send out Pokémon] Player sent out #{party[newpokename].Name} in position #{index}");
			}
			else {
				if (!party[newpoke].IsNotNullOrNone()) {
					//p [index,newpoke,party[newpoke],pbAllFainted(party)]
					GameDebug.Log($"[{index},{newpoke},{party[newpoke]},pbMOR]");
					for (int i = 0; i < party.Length; i++) {
						GameDebug.Log($"[{i},{party[i].HP}]");
					}
					//throw new BattleAbortedException();
					GameDebug.LogError("BattleAbortedException"); pbAbort();
				}
				ITrainer owner=pbGetOwner(index);
				yield return pbDisplayBrief(Game._INTL("{1} sent\r\nout {2}!",owner.name,party[newpokename].Name));
				GameDebug.Log($"[Send out Pokémon] Opponent sent out #{party[newpokename].Name} in position #{index}");
			}
		}

		public IEnumerator pbSwitchInBetween(int index, bool lax, bool cancancel, System.Action<int> result = null) {
			if (!pbOwnedByPlayer(index)) {
				result?.Invoke((@scene as IPokeBattle_SceneNonInteractive).pbChooseNewEnemy(index,pbParty(index)));
				yield break;
			}
			//else {
				yield return pbSwitchPlayer(index,lax,cancancel,result);
			//}
		}

		public IEnumerator pbSwitchPlayer(int index,bool lax, bool cancancel, System.Action<int> result = null) {
			if (@debug) {
				result?.Invoke((@scene as IPokeBattle_SceneNonInteractive).pbChooseNewEnemy(index,pbParty(index)));
				yield break;
			}
			//else {
				yield return (@scene as IPokeBattle_SceneIE).pbSwitch(index,lax,cancancel,result);
			//}
		}
		#endregion

		#region Using an Item
		/// <summary>
		/// Uses an item on a Pokémon in the player's party.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="pkmnIndex"></param>
		/// <param name="userPkmn"></param>
		/// <param name="scene"></param>
		/// <returns></returns>
		//protected bool _pbUseItemOnPokemon(Items item,int pkmnIndex,IBattlerIE userPkmn,IHasDisplayMessage scene) {
		IEnumerator IBattleIE.pbUseItemOnPokemon(Items item,int pkmnIndex,IBattlerIE userPkmn,IHasDisplayMessageIE scene, System.Action<bool> result) {
			IPokemon pokemon=@party1[pkmnIndex];
			IBattlerIE battler=null;
			string name=pbGetOwner(userPkmn.Index).name;
			if (pbBelongsToPlayer(userPkmn.Index)) name=pbGetOwner(userPkmn.Index).name;
			yield return pbDisplayBrief(Game._INTL("{1} used the\r\n{2}.",name,Game._INTL(item.ToString(TextScripts.Name))));
			GameDebug.Log($"[Use item] Player used #{Game._INTL(item.ToString(TextScripts.Name))} on #{pokemon.Name}");
			bool ret=false;
			if (pokemon.isEgg) {
				yield return pbDisplay(Game._INTL("But it had no effect!"));
			}
			else {
				for (int i = 0; i < battlers.Length; i++) {
					if (!pbIsOpposing(i) && @battlers[i].pokemonIndex==pkmnIndex) {
						battler=@battlers[i];
					}
				}
				ret=ItemHandlers.triggerBattleUseOnPokemon(item,pokemon,battler,(IHasDisplayMessage)scene); //Invoke Event, returns which pokemon selected
			}
			if (!ret && pbBelongsToPlayer(userPkmn.Index)) {
				if (Game.GameData.Bag.pbCanStore(item)) {
					Game.GameData.Bag.pbStoreItem(item);
				}
				else {
					//throw new Exception(Game._INTL("Couldn't return unused item to Bag somehow."));
					GameDebug.LogError(Game._INTL("Couldn't return unused item to Bag somehow."));
				}
			}
			result?.Invoke(ret);
		}

		/// <summary>
		/// Uses an item on an active Pokémon.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="index"></param>
		/// <param name="userPkmn"></param>
		/// <param name="scene"></param>
		/// <returns></returns>
		public bool pbUseItemOnBattler(Items item,int index,IBattlerIE userPkmn,IHasDisplayMessageIE scene) {
			GameDebug.Log($"[Use item] Player used #{Game._INTL(item.ToString(TextScripts.Name))} on #{@battlers[index].ToString(true)}");
			bool ret=ItemHandlers.triggerBattleUseOnBattler(item,@battlers[index],(IHasDisplayMessage)scene);
			if (!ret && pbBelongsToPlayer(userPkmn.Index)) {
				if (Game.GameData.Bag.pbCanStore(item)) {
					Game.GameData.Bag.pbStoreItem(item);
				}
				else {
					//throw new Exception(Game._INTL("Couldn't return unused item to Bag somehow."));
					GameDebug.LogError(Game._INTL("Couldn't return unused item to Bag somehow."));
				}
			}
			return ret;
		}

		public IEnumerator pbRegisterItem(int idxPokemon,Items idxItem,int? idxTarget=null, System.Action<bool> result = null) {
			if (idxTarget!=null && idxTarget.Value>=0) {
				for (int i = 0; i < battlers.Length; i++) {
					if (!@battlers[i].pbIsOpposing(idxPokemon) &&
						@battlers[i].pokemonIndex==idxTarget.Value &&
						@battlers[i].effects.Embargo>0) {
						yield return pbDisplay(Game._INTL("Embargo's effect prevents the item's use on {1}!",@battlers[i].ToString(true)));
						if (pbBelongsToPlayer(@battlers[i].Index)) {
							if (Game.GameData.Bag.pbCanStore(idxItem)) {
								Game.GameData.Bag.pbStoreItem(idxItem);
							}
							else {
								//throw new Exception(Game._INTL("Couldn't return unused item to Bag somehow."));
								GameDebug.LogError(Game._INTL("Couldn't return unused item to Bag somehow."));
							}
						}
						result?.Invoke(false); yield break;
					}
				}
			}
			if (ItemHandlers.hasUseInBattle(idxItem)) {
				if (idxPokemon==0) { // Player's first Pokémon
					if (ItemHandlers.triggerBattleUseOnBattler(idxItem,@battlers[idxPokemon],this)) {
						// Using Poké Balls or Poké Doll only
						ItemHandlers.triggerUseInBattle(idxItem,@battlers[idxPokemon],this);
						if (@doublebattle) {
							@battlers[idxPokemon+2].effects.SkipTurn=true;
						}
					}
					else {
						if (Game.GameData.Bag.pbCanStore(idxItem)) {
							Game.GameData.Bag.pbStoreItem(idxItem);
						}
						else {
							//throw new Exception(Game._INTL("Couldn't return unusable item to Bag somehow."));
							GameDebug.LogError(Game._INTL("Couldn't return unusable item to Bag somehow."));
						}
						result?.Invoke(false); yield break;
					}
				}
				else {
					if (ItemHandlers.triggerBattleUseOnBattler(idxItem,@battlers[idxPokemon],this)) {
						yield return pbDisplay(Game._INTL("It's impossible to aim without being focused!"));
					}
					result?.Invoke(false); yield break;
				}
			}
			//@choices[idxPokemon][0]=3;         // "Use an item"
			//@choices[idxPokemon][1]=idxItem;   // ID of item to be used
			//@choices[idxPokemon][2]=idxTarget; // Index of Pokémon to use item on
			@choices[idxPokemon]= new Choice(ChoiceAction.UseItem,idxItem,idxTarget.Value);
			int side=pbIsOpposing(idxPokemon) ? 1 : 0;
			int owner=pbGetOwnerIndex(idxPokemon);
			if (@megaEvolution[side][owner]==idxPokemon) {
				@megaEvolution[side][owner]=-1;
			}
			result?.Invoke(true);
		}

		public IEnumerator pbEnemyUseItem(Items item, IBattlerIE battler) {
			if (!@internalbattle) yield break; //0
			Items[] items=pbGetOwnerItems(battler.Index);
			if (items == null) yield break; //Items.NONE
			ITrainer opponent=pbGetOwner(battler.Index);
			for (int i = 0; i < items.Length; i++) {
				if (items[i]==item) {
					//items.delete_at(i);
					items[i]=Items.NONE;
					break;
				}
			}
			string itemname=Game._INTL(item.ToString(TextScripts.Name));
			yield return pbDisplayBrief(Game._INTL("{1} used the\r\n{2}!",opponent.name,itemname));
			GameDebug.Log($"[Use item] Opponent used #{itemname} on #{battler.ToString(true)}");
			bool canIncreaseStatStage = false;
			if (item == Items.POTION) {
				battler.pbRecoverHP(20,true);
				yield return pbDisplay(Game._INTL("{1}'s HP was restored.",battler.ToString()));
			}
			else if (item == Items.SUPER_POTION) {
				battler.pbRecoverHP(50,true);
				yield return pbDisplay(Game._INTL("{1}'s HP was restored.",battler.ToString()));
			}
			else if (item == Items.HYPER_POTION) {
				battler.pbRecoverHP(200,true);
				yield return pbDisplay(Game._INTL("{1}'s HP was restored.",battler.ToString()));
			}
			else if (item == Items.MAX_POTION) {
				battler.pbRecoverHP(battler.TotalHP-battler.HP,true);
				yield return pbDisplay(Game._INTL("{1}'s HP was restored.",battler.ToString()));
			}
			else if (item == Items.FULL_RESTORE) {
				bool fullhp=(battler.HP==battler.TotalHP);
				battler.pbRecoverHP(battler.TotalHP-battler.HP,true);
				battler.Status=0; battler.StatusCount=0;
				battler.effects.Confusion=0;
				if (fullhp) {
					yield return pbDisplay(Game._INTL("{1} became healthy!",battler.ToString()));
				}
				else {
					yield return pbDisplay(Game._INTL("{1}'s HP was restored.",battler.ToString()));
				}
			}
			else if (item == Items.FULL_HEAL) {
				battler.Status=0; battler.StatusCount=0;
				battler.effects.Confusion=0;
				yield return pbDisplay(Game._INTL("{1} became healthy!",battler.ToString()));
			}
			else if (item == Items.X_ATTACK && battler is IBattlerEffectIE b0) {
				yield return b0.pbCanIncreaseStatStage(PokemonUnity.Combat.Stats.ATTACK,battler,result:value=>canIncreaseStatStage=value);
				if (canIncreaseStatStage) {
					yield return b0.pbIncreaseStat(PokemonUnity.Combat.Stats.ATTACK,1,battler,true);
				}
			}
			else if (item == Items.X_DEFENSE && battler is IBattlerEffectIE b1) {
				yield return b1.pbCanIncreaseStatStage(PokemonUnity.Combat.Stats.DEFENSE,battler,result:value=>canIncreaseStatStage=value);
				if (canIncreaseStatStage) {
					yield return b1.pbIncreaseStat(PokemonUnity.Combat.Stats.DEFENSE,1,battler,true);
				}
			}
			else if (item == Items.X_SPEED && battler is IBattlerEffectIE b2) {
				yield return b2.pbCanIncreaseStatStage(PokemonUnity.Combat.Stats.SPEED,battler,result:value=>canIncreaseStatStage=value);
				if (canIncreaseStatStage) {
					yield return b2.pbIncreaseStat(PokemonUnity.Combat.Stats.SPEED,1,battler,true);
				}
			}
			else if (item == Items.X_SP_ATK && battler is IBattlerEffectIE b3) {
				yield return b3.pbCanIncreaseStatStage(PokemonUnity.Combat.Stats.SPATK,battler,result:value=>canIncreaseStatStage=value);
				if (canIncreaseStatStage) {
					yield return b3.pbIncreaseStat(PokemonUnity.Combat.Stats.SPATK,1,battler,true);
				}
			}
			else if (item == Items.X_SP_DEF && battler is IBattlerEffectIE b4) {
				yield return b4.pbCanIncreaseStatStage(PokemonUnity.Combat.Stats.SPDEF,battler,result:value=>canIncreaseStatStage=value);
				if (canIncreaseStatStage) {
					yield return b4.pbIncreaseStat(PokemonUnity.Combat.Stats.SPDEF,1,battler,true);
				}
			}
			else if (item == Items.X_ACCURACY && battler is IBattlerEffectIE b5) {
				yield return b5.pbCanIncreaseStatStage(PokemonUnity.Combat.Stats.ACCURACY,battler,result:value=>canIncreaseStatStage=value);
				if (canIncreaseStatStage) {
					yield return b5.pbIncreaseStat(PokemonUnity.Combat.Stats.ACCURACY,1,battler,true);
				}
			}
		}
		#endregion

		#region Fleeing from Battle
		//public bool pbCanRun(int idxPokemon)
		//{
		//	if (@opponent.Length > 0) return false;
		//	if (@cantescape && !pbIsOpposing(idxPokemon)) return false;
		//	IBattlerIE thispkmn=@battlers[idxPokemon];
		//	if (thispkmn.pbHasType(Types.GHOST) && Core.USENEWBATTLEMECHANICS) return true;
		//	if (thispkmn.hasWorkingItem(Items.SMOKE_BALL)) return true;
		//	if (thispkmn.hasWorkingAbility(Abilities.RUN_AWAY)) return true;
		//	return pbCanSwitch(idxPokemon,-1,false);
		//}

		public IEnumerator pbRun(int idxPokemon,bool duringBattle=false, System.Action<int> result = null) {
			IBattlerIE thispkmn=@battlers[idxPokemon];
			if (pbIsOpposing(idxPokemon)) {
				if (@opponent.Length > 0) { result?.Invoke(0); yield break; }
				//@choices[i][0]=5; // run
				//@choices[i][1]=0;
				//@choices[i][2]=null;
				@choices[idxPokemon] = new Choice(ChoiceAction.Run);
				result?.Invoke(-1); yield break;
			}
			bool confirm = false;
			if (@opponent.Length > 0) {
				if (debug && Input.press((int)PokemonUnity.UX.InputKeys.DEBUG)) {
					yield return pbDisplayConfirm(Game._INTL("Treat this battle as a win?"),result:value=>confirm=value);
					if (confirm) {
						@decision=BattleResults.WON;
						result?.Invoke(1); yield break;
					}
					else {
						yield return pbDisplayConfirm(Game._INTL("Treat this battle as a loss?"),result:value=>confirm=value);
						if (confirm) {
							@decision=BattleResults.LOST;
							result?.Invoke(1); yield break;
						}
					}
				}
				else if (@internalbattle) {
					yield return pbDisplayPaused(Game._INTL("No! There's no running from a Trainer battle!"));
				}
				else
				{
					yield return pbDisplayConfirm(Game._INTL("Would you like to forfeit the match and quit now?"),result:value=>confirm=value);
					if (confirm) {
						yield return pbDisplay(Game._INTL("{1} forfeited the match!",this.pbPlayer().name));
						@decision=BattleResults.FORFEIT;
						result?.Invoke(1); yield break;
					}
				}
				result?.Invoke(0); yield break;
			}
			if (debug && Input.press((int)PokemonUnity.UX.InputKeys.DEBUG)) {
				yield return pbDisplayPaused(Game._INTL("Got away safely!"));
				@decision=BattleResults.FORFEIT;
				result?.Invoke(1); yield break;
			}
			if (@cantescape) {
				yield return pbDisplayPaused(Game._INTL("Can't escape!"));
				result?.Invoke(0); yield break;
			}
			if (thispkmn.pbHasType(Types.GHOST) && Core.USENEWBATTLEMECHANICS) {
				yield return pbDisplayPaused(Game._INTL("Got away safely!"));
				@decision=BattleResults.FORFEIT;
				result?.Invoke(1); yield break;
			}
			if (thispkmn.hasWorkingAbility(Abilities.RUN_AWAY)) {
				if (duringBattle) {
					yield return pbDisplayPaused(Game._INTL("Got away safely!"));
				}
				else {
					yield return pbDisplayPaused(Game._INTL("{1} escaped using Run Away!",thispkmn.ToString()));
				}
				@decision=BattleResults.FORFEIT;
				result?.Invoke(1); yield break;
			}
			if (thispkmn.hasWorkingItem(Items.SMOKE_BALL)) {
				if (duringBattle) {
					yield return pbDisplayPaused(Game._INTL("Got away safely!"));
				}
				else {
					yield return pbDisplayPaused(Game._INTL("{1} escaped using its {2}!",thispkmn.ToString(),Game._INTL(thispkmn.Item.ToString(TextScripts.Name))));
				}
				@decision=BattleResults.FORFEIT;
				result?.Invoke(1); yield break;
			}
			bool registerSwitch = false;
			yield return pbCanSwitch(idxPokemon,-1,false,result:value=>registerSwitch=value);
			if (!duringBattle && !registerSwitch) {
				yield return pbDisplayPaused(Game._INTL("Can't escape!"));
				result?.Invoke(0); yield break;
			}
			int rate;
			// Note: not pbSpeed, because using unmodified Speed
			int speedPlayer=@battlers[idxPokemon].pokemon.SPE;
			IBattlerIE opposing=@battlers[idxPokemon].pbOppositeOpposing;
			if (opposing.isFainted()) opposing=opposing.pbPartner;
			if (!opposing.isFainted()) {
				int speedEnemy=opposing.pokemon.SPE;
				if (speedPlayer>speedEnemy) {
					rate=256;
				}
				else {
					if (speedEnemy<=0) speedEnemy=1;
					rate=speedPlayer*128/speedEnemy;
					rate+=@runCommand*30;
					rate&=0xFF;
				}
			}
			else {
				rate=256;
			}
			int ret=1;
			if (pbRandom(256)<rate) { //pbAIRandom
				yield return pbDisplayPaused(Game._INTL("Got away safely!"));
				@decision=BattleResults.FORFEIT;
			}
			else {
				yield return pbDisplayPaused(Game._INTL("Can't escape!"));
				ret=-1;
			}
			if (!duringBattle) @runCommand+=1;
			result?.Invoke(ret);
		}
		#endregion

		#region Mega Evolve Battler
		//public bool pbCanMegaEvolve (int index) {
		//	if (Core.NO_MEGA_EVOLUTION) return false;
		//	if (!@battlers[index].hasMega) return false;
		//	if (pbIsOpposing(index) && @opponent.Length == 0) return false;
		//	if (debug && Input.press((int)PokemonUnity.UX.InputKeys.DEBUG)) return true;
		//	if (!pbHasMegaRing(index)) return false;
		//	int side=pbIsOpposing(index) ? 1 : 0;
		//	int owner=pbGetOwnerIndex(index);
		//	if (@megaEvolution[side][owner]!=-1) return false;
		//	if (@battlers[index].effects.SkyDrop) return false;
		//	return true;
		//}
		//
		//public void pbRegisterMegaEvolution(int index) {
		//	int side=pbIsOpposing(index) ? 1 : 0;
		//	int owner=pbGetOwnerIndex(index);
		//	@megaEvolution[side][owner]=(sbyte)index;
		//}

		new public IEnumerator pbMegaEvolve(int index) {
			if (!@battlers[index].IsNotNullOrNone() || !@battlers[index].pokemon.IsNotNullOrNone()) yield break;
			if (!@battlers[index].hasMega) yield break; //rescue false
			if (@battlers[index].isMega) yield break; //rescue true
			string ownername=pbGetOwner(index).name;
			if (pbBelongsToPlayer(index)) ownername=pbGetOwner(index).name;
			if (@battlers[index].pokemon is IPokemonMegaEvolution m && m.megaMessage() == 1) { //switch (@battlers[index].pokemon.megaMessage) rescue 0
				//case 1: // Rayquaza
					yield return pbDisplay(Game._INTL("{1}'s fervent wish has reached {2}!",ownername,@battlers[index].ToString()));
				//  break;
			} else { //default:
				yield return pbDisplay(Game._INTL("{1}'s {2} is reacting to {3}'s {4}!",
					@battlers[index].ToString(),Game._INTL(@battlers[index].Item.ToString(TextScripts.Name)),
					ownername,pbGetMegaRingName(index)));
				//  break;
			}
			yield return pbCommonAnimation("MegaEvolution",@battlers[index],null);
			if (@battlers[index].pokemon is IPokemonMegaEvolution p) p.makeMega();
			(_battlers[index] as IBattlerIE).form=@battlers[index].pokemon is IPokemonMultipleForms f ? f.form : 0;
			(_battlers[index] as IBattlerIE).pbUpdate(true);
			if (@scene is IPokeBattle_Scene s0) s0.pbChangePokemon(@battlers[index],@battlers[index].pokemon);
			yield return pbCommonAnimation("MegaEvolution2",@battlers[index],null);
			string meganame=@battlers[index].pokemon.Name; //megaName rescue null
			if (string.IsNullOrEmpty(meganame)) {
				meganame=Game._INTL("Mega {1}",Game._INTL(@battlers[index].pokemon.Species.ToString(TextScripts.Name)));
			}
			yield return pbDisplay(Game._INTL("{1} has Mega Evolved into {2}!",@battlers[index].ToString(),meganame));
			GameDebug.Log($"[Mega Evolution] #{@battlers[index].ToString()} Mega Evolved");
			int side=pbIsOpposing(index) ? 1 : 0;
			int owner=pbGetOwnerIndex(index);
			@megaEvolution[side][owner]=-2;
		}
		#endregion

		#region Primal Revert Battler
		new public IEnumerator pbPrimalReversion(int index) {
			if (!@battlers[index].IsNotNullOrNone() || !@battlers[index].pokemon.IsNotNullOrNone()) yield break;
			if (!@battlers[index].hasPrimal) yield break; //rescue false
			if (@battlers[index].pokemon.Species != Pokemons.KYOGRE ||
				@battlers[index].pokemon.Species != Pokemons.GROUDON) yield break;
			if (@battlers[index].isPrimal) yield break; //rescue true
			if (@battlers[index].pokemon.Species == Pokemons.KYOGRE) {
				yield return pbCommonAnimation("PrimalKyogre",@battlers[index],null);
			}
			else if (@battlers[index].pokemon.Species == Pokemons.GROUDON) {
				yield return pbCommonAnimation("PrimalGroudon",@battlers[index],null);
			}
			if (@battlers[index].pokemon is IPokemonMegaEvolution p) p.makePrimal();
			(_battlers[index] as IBattlerIE).form=@battlers[index].pokemon is IPokemonMultipleForms f ? f.form : 0;
			(_battlers[index] as IBattlerIE).pbUpdate(true);
			if (@scene is IPokeBattle_Scene s0) s0.pbChangePokemon(@battlers[index],@battlers[index].pokemon);
			if (@battlers[index].pokemon.Species == Pokemons.KYOGRE) {
				yield return pbCommonAnimation("PrimalKyogre2",@battlers[index],null);
			}
			else if (@battlers[index].pokemon.Species == Pokemons.GROUDON) {
				yield return pbCommonAnimation("PrimalGroudon2",@battlers[index],null);
			}
			yield return pbDisplay(Game._INTL("{1}'s Primal Reversion!\nIt reverted to its primal form!",@battlers[index].ToString()));
			GameDebug.Log($"[Primal Reversion] #{@battlers[index].ToString()} Primal Reverted");
		}
		#endregion

		#region Call Battler
		new public IEnumerator pbCall(int index) {
			ITrainer owner=pbGetOwner(index);
			yield return pbDisplay(Game._INTL("{1} called {2}!",owner.name,@battlers[index].Name));
			yield return pbDisplay(Game._INTL("{1}!",@battlers[index].Name));
			GameDebug.Log($"[Call to Pokémon] #{owner.name} called to #{@battlers[index].ToString(true)}");
			if (@battlers[index] is IBattlerShadowPokemon b && b.isShadow()) {
				if (b.inHyperMode() && @battlers[index].pokemon is IPokemonShadowPokemon p) {
					p.hypermode=false;
					p.adjustHeart(-300);
					//ToDo: There should be a method for this in pokemon class?...
					//(b as PokemonUnity.UX.Battler).isHyperMode=false;
					//(p as PokemonUnity.Monster.Pokemon).ChangeHappiness(HappinessMethods.CALL);
					yield return pbDisplay(Game._INTL("{1} came to its senses from the Trainer's call!",@battlers[index].ToString()));
				}
				else {
					yield return pbDisplay(Game._INTL("But nothing happened!"));
				}
			}
			else if (@battlers[index].Status!=Status.SLEEP && @battlers[index] is IBattlerEffectIE b0) {
				bool canIncreaseStatStage = false; 
				yield return b0.pbCanIncreaseStatStage(PokemonUnity.Combat.Stats.ACCURACY,@battlers[index],result:value=>canIncreaseStatStage=value);
				if (canIncreaseStatStage)
					yield return b0.pbIncreaseStat(PokemonUnity.Combat.Stats.ACCURACY,1,@battlers[index],true);
			}
			else {
				yield return pbDisplay(Game._INTL("But nothing happened!"));
			}
		}
		#endregion

		#region Gaining Experience
		new public IEnumerator pbGainEXP() {
			if (!@internalbattle) yield break;
			bool successbegin=true;
			for (int i = 0; i < battlers.Length; i++) { // Not ordered by priority
				if (!@doublebattle && pbIsDoubleBattler(i)) {
					_battlers[i].participants.Clear();//=[];
					continue;
				}
				if (pbIsOpposing(i) && @battlers[i].participants.Count>0 &&
					(@battlers[i].isFainted() || @battlers[i].captured)) {
					bool haveexpall=Game.GameData.Bag.pbQuantity(Items.EXP_ALL)>0; //hasConst(Items.EXP_ALL) &&
					// First count the number of participants
					int partic=0;
					int expshare=0;
					foreach (var j in @battlers[i].participants) {
						if (!@party1[j].IsNotNullOrNone() || !pbIsOwner(0,j)) continue;
						if (@party1[j].HP>0 && !@party1[j].isEgg) partic+=1;
					}
					if (!haveexpall) {
						for (int j = 0; j < @party1.Length; j++) {
							if (!@party1[j].IsNotNullOrNone() || !pbIsOwner(0,j)) continue;
							if (@party1[j].HP>0 && !@party1[j].isEgg &&
								(@party1[j].Item == Items.EXP_SHARE ||
								@party1[j].itemInitial == Items.EXP_SHARE)) expshare+=1;
						}
					}
					// Now calculate EXP for the participants
					if (partic>0 || expshare>0 || haveexpall) {
						if (@opponent == null && successbegin && pbAllFainted(@party2)) {
							yield return (@scene as IPokeBattle_SceneIE).pbWildBattleSuccess();
							successbegin=false;
						}
						for (int j = 0; j < @party1.Length; j++) {
							if (!@party1[j].IsNotNullOrNone() || !pbIsOwner(0,j)) continue;
							if (@party1[j].HP<=0 || @party1[j].isEgg) continue;
							bool haveexpshare=@party1[j].Item == Items.EXP_SHARE ||
												@party1[j].itemInitial == Items.EXP_SHARE;
							if (!haveexpshare && !@battlers[i].participants.Contains((byte)j)) continue;
								pbGainExpOne(j,@battlers[i],partic,expshare,haveexpall);
						}
						if (haveexpall) {
							bool showmessage=true;
							for (int j = 0; j < @party1.Length; j++) {
								if (!@party1[j].IsNotNullOrNone() || !pbIsOwner(0,j)) continue;
								if (@party1[j].HP<=0 || @party1[j].isEgg) continue;
								if (@party1[j].Item == Items.EXP_SHARE ||
									@party1[j].itemInitial == Items.EXP_SHARE) continue;
								if (@battlers[i].participants.Contains((byte)j)) continue;
								if (showmessage) yield return pbDisplayPaused(Game._INTL("The rest of your team gained Exp. Points thanks to the {1}!",
									Game._INTL(Items.EXP_ALL.ToString(TextScripts.Name))));
								showmessage=false;
								yield return pbGainExpOne(j,@battlers[i],partic,expshare,haveexpall,false);
							}
						}
					}
					// Now clear the participants array
					_battlers[i].participants.Clear();//=[];
				}
			}
		}

		public IEnumerator pbGainExpOne(int index,IBattlerIE defeated,int partic,int expshare,bool haveexpall,bool showmessages=true) {
			IPokemon thispoke=@party1[index];
			// Original species, not current species
			int level=defeated.Level;
			float baseexp=Kernal.PokemonData[defeated.Species].BaseExpYield;
			int[] evyield=Kernal.PokemonData[defeated.Species].EVYield;
			// Gain effort value points, using RS effort values
			int totalev=0;
			for (int k = 0; k < Core.MAXPARTYSIZE; k++) {
				totalev+=thispoke.EV[k];
			}
			for (int k = 0; k < Core.MAXPARTYSIZE; k++) {
				int evgain=evyield[k];
				if (thispoke.Item == Items.MACHO_BRACE ||
					thispoke.itemInitial == Items.MACHO_BRACE) evgain*=2;
				switch ((PokemonUnity.Monster.Stats)k) {
					case PokemonUnity.Monster.Stats.HP:
						if (thispoke.Item == Items.POWER_WEIGHT ||
							thispoke.itemInitial == Items.POWER_WEIGHT) evgain+=4;
						break;
					case PokemonUnity.Monster.Stats.ATTACK:
						if (thispoke.Item == Items.POWER_BRACER ||
							thispoke.itemInitial == Items.POWER_BRACER) evgain+=4;
						break;
					case PokemonUnity.Monster.Stats.DEFENSE:
						if (thispoke.Item == Items.POWER_BELT ||
							thispoke.itemInitial == Items.POWER_BELT) evgain+=4;
						break;
					case PokemonUnity.Monster.Stats.SPATK:
						if (thispoke.Item == Items.POWER_LENS ||
							thispoke.itemInitial == Items.POWER_LENS) evgain+=4;
						break;
					case PokemonUnity.Monster.Stats.SPDEF:
						if (thispoke.Item == Items.POWER_BAND ||
							thispoke.itemInitial == Items.POWER_BAND) evgain+=4;
						break;
					case PokemonUnity.Monster.Stats.SPEED:
						if (thispoke.Item == Items.POWER_ANKLET ||
							thispoke.itemInitial == Items.POWER_ANKLET) evgain+=4;
						break;
				}
				//if (thispoke.PokerusStage>=1) evgain*=2;	// Infected or cured
				if (thispoke.PokerusStage == true) evgain*=2;	// Infected only
				if (evgain>0) {
					// Can't exceed overall limit
					if (totalev+evgain>Monster.Pokemon.EVLIMIT) evgain-=totalev+evgain-Monster.Pokemon.EVLIMIT;
					// Can't exceed stat limit
					if (thispoke.EV[k]+evgain>Monster.Pokemon.EVSTATLIMIT) evgain-=thispoke.EV[k]+evgain-Monster.Pokemon.EVSTATLIMIT;
					// Add EV gain
					//thispoke.EV[k]+=evgain;
					thispoke.EV[k]=(byte)(thispoke.EV[k]+evgain);
					if (thispoke.EV[k]>Monster.Pokemon.EVSTATLIMIT) {
						GameDebug.LogWarning($"Single-stat EV limit #{Monster.Pokemon.EVSTATLIMIT} exceeded.\r\nStat: #{k}  EV gain: #{evgain}  EVs: #{thispoke.EV.ToString()}");
						thispoke.EV[k]=Monster.Pokemon.EVSTATLIMIT;
					}
					totalev+=evgain;
					if (totalev>Monster.Pokemon.EVLIMIT) {
						GameDebug.LogWarning($"EV limit #{Monster.Pokemon.EVLIMIT} exceeded.\r\nTotal EVs: #{totalev} EV gain: #{evgain}  EVs: #{thispoke.EV.ToString()}");
					}
				}
			}
			(thispoke as Monster.Pokemon).GainEffort(defeated.Species);
			// Gain experience
			bool ispartic=false;
			//if (defeated.participants.Contains(index)) ispartic=true;
			ispartic=defeated.participants.Contains((byte)index);
			bool haveexpshare=thispoke.Item == Items.EXP_SHARE ||
							thispoke.itemInitial == Items.EXP_SHARE;
			int exp=0;
			if (expshare>0) {
				if (partic==0) { // No participants, all Exp goes to Exp Share holders
					exp=(int)Math.Floor(level*baseexp);
					//exp=(int)Math.Floor(exp/(Core.NOSPLITEXP ? 1 : expshare))*haveexpshare;
					exp=haveexpshare ? (int)Math.Floor(exp/(Core.NOSPLITEXP ? 1f : expshare)) : 0;
				}
				else {
					if (Core.NOSPLITEXP) {
						//exp=(int)Math.Floor(level*baseexp)*ispartic;
						//if (!ispartic) exp=(int)Math.Floor(level*baseexp*.5)*haveexpshare;
						exp=ispartic?(int)Math.Floor(level*baseexp):0;
						if (!ispartic) exp=haveexpshare ? (int)Math.Floor(level*baseexp*.5) : 0;
					}
					else {
						exp=(int)Math.Floor(level*baseexp*.5);
						//exp=(int)Math.Floor(exp/partic)*ispartic + (int)Math.Floor(exp/expshare)*haveexpshare;
						exp=haveexpshare ? (int)Math.Floor(exp/(float)partic)*(ispartic?1:0) + (int)Math.Floor(exp/(float)expshare) : 0;
					}
				}
			}
			else if (ispartic) {
				exp=(int)Math.Floor(level*baseexp/(Core.NOSPLITEXP ? 1 : partic));
			}
			else if (haveexpall) {
				exp=(int)Math.Floor(level*baseexp/2);
			}
			if (exp<=0) yield break;
			if (@opponent.Length>0) exp=(int)Math.Floor(exp*3*.5);
			if (Core.USESCALEDEXPFORMULA) {
				exp=(int)Math.Floor(exp/5f);
				double leveladjust=(2*level+10.0)/(level+thispoke.Level+10.0);
				leveladjust=Math.Pow(leveladjust,5);
				leveladjust=Math.Sqrt(leveladjust);
				exp=(int)Math.Floor(exp*leveladjust);
				if (ispartic || haveexpshare) exp+=1;
			}
			else {
				exp=(int)Math.Floor(exp/7f);
			}
			bool isOutsider = thispoke.isForeign(pbPlayer());
			//				|| (thispoke.language!=0 && thispoke.language!=this.pbPlayer().language);
			if (isOutsider) {
				//if (thispoke.language!=0 && thispoke.language!=this.pbPlayer().language) {
				//  exp=(int)Math.Floor(exp*1.7);
				//}
				//else {
					exp=(int)Math.Floor(exp*3/2f);
				//}
			}
			if (thispoke.Item == Items.LUCKY_EGG ||
				thispoke.itemInitial == Items.LUCKY_EGG) exp=(int)Math.Floor(exp*3/2f);
			Monster.LevelingRate growthrate=thispoke.GrowthRate;
			//int newexp=new Experience(thispoke.exp,exp,growthrate).AddExperience(exp).Current;
			Monster.Data.Experience gainedexp=new Monster.Data.Experience(growthrate,thispoke.Exp);
			gainedexp.AddExperience(exp);
			int newexp=gainedexp.Total;
			exp=newexp-thispoke.Exp;
			if (exp>0) {
				if (showmessages) {
					if (isOutsider) {
						yield return pbDisplayPaused(Game._INTL("{1} gained a boosted {2} Exp. Points!",thispoke.Name,exp.ToString()));
					}
					else {
						yield return pbDisplayPaused(Game._INTL("{1} gained {2} Exp. Points!",thispoke.Name,exp.ToString()));
					}
				}
				int newlevel=Monster.Data.Experience.GetLevelFromExperience(growthrate,newexp);
				//int tempexp=0;
				int curlevel=thispoke.Level;
				if (newlevel<curlevel) {
					string debuginfo=$"#{thispoke.Name}: #{thispoke.Level}/#{newlevel} | #{thispoke.Exp}/#{newexp} | gain: #{exp}";
					//throw new RuntimeError(Game._INTL("The new level ({1}) is less than the Pokémon's\r\ncurrent level ({2}), which shouldn't happen.\r\n[Debug: {3}]",
					GameDebug.LogError(Game._INTL("The new level {1) is less than the Pokémon's\r\ncurrent level (2), which shouldn't happen.\r\n[Debug: {3}]",
					newlevel.ToString(),curlevel.ToString(),debuginfo));
					yield break;
				}
				if (thispoke is IPokemonShadowPokemon p && p.isShadow) {
					//thispoke.exp+=exp;
					//thispoke.Experience.AddExperience(exp);
					p.savedexp+=exp;
				}
				else {
					int tempexp1=thispoke.Exp;
					int tempexp2=0;
					// Find battler
					IBattlerIE battler=pbFindPlayerBattler(index);
					do { //loop
						// EXP Bar animation
						int startexp=Monster.Data.Experience.GetStartExperience(growthrate,curlevel); //0
						int endexp=Monster.Data.Experience.GetStartExperience(growthrate,curlevel+1); //100
						tempexp2=(endexp<newexp) ? endexp : newexp; //final < 100?
						thispoke.Exp = tempexp2;
						//thispoke.Experience.AddExperience(tempexp2 - thispoke.exp);
						yield return (@scene as IPokeBattle_SceneIE).pbEXPBar(battler,thispoke,startexp,endexp,tempexp1,tempexp2);
						tempexp1=tempexp2;
						curlevel+=1;
						if (curlevel>newlevel) {
							thispoke.calcStats();
							if (battler.IsNotNullOrNone()) battler.pbUpdate(false);
							@scene.pbRefresh();
							break;
						}
						int oldtotalhp=thispoke.TotalHP;
						int oldattack=thispoke.ATK;
						int olddefense=thispoke.DEF;
						int oldspeed=thispoke.SPE;
						int oldspatk=thispoke.SPA;
						int oldspdef=thispoke.SPD;
						if (battler.IsNotNullOrNone() && @internalbattle) { //&& battler.pokemon.IsNotNullOrNone()
							(battler.pokemon as Monster.Pokemon).ChangeHappiness(HappinessMethods.LEVELUP);//"level up"
						}
						thispoke.calcStats();
						if (battler.IsNotNullOrNone()) battler.pbUpdate(false);
						@scene.pbRefresh();
						yield return pbDisplayPaused(Game._INTL("{1} grew to Level {2}!",thispoke.Name,curlevel.ToString()));
						//ToDo: Can Evolve during battle?
						yield return (@scene as IPokeBattle_SceneIE).pbLevelUp(battler,thispoke,oldtotalhp,oldattack,
										olddefense,oldspeed,oldspatk,oldspdef);
						// Finding all moves learned at this level
						Moves[] movelist=thispoke.getMoveList(Monster.LearnMethod.levelup);
						foreach (Moves k in movelist) {
							//if (k[0]==thispoke.Level)     // Learned a new move
								//pbLearnMove(index,k[1]);
								pbLearnMove(index,k);
						}
					} while (true);
				}
			}
		}
		#endregion

		#region Learning a move.
		new public IEnumerator pbLearnMove(int pkmnIndex,Moves move) {
			IPokemon pokemon=@party1[pkmnIndex];
			if (!pokemon.IsNotNullOrNone()) yield break;
			string pkmnname=pokemon.Name;
			IBattlerIE battler=pbFindPlayerBattler(pkmnIndex);
			string movename=Game._INTL(move.ToString(TextScripts.Name));
			for (int i = 0; i < pokemon.moves.Length; i++) {
				if (pokemon.moves[i].id==move) yield break;
				if (pokemon.moves[i].id==0) {
					pokemon.moves[i]=new PokemonUnity.Attack.Move(move); //ToDo: Use LearnMove Method in Pokemon Class?
					if (battler.IsNotNullOrNone())
						battler.moves[i]=Combat.Move.pbFromPBMove(this,pokemon.moves[i]);
					yield return pbDisplayPaused(Game._INTL("{1} learned {2}!",pkmnname,movename));
					GameDebug.Log($"[Learn move] #{pkmnname} learned #{movename}");
					yield break;
				}
			}
			bool confirm = false;
			do { //loop
				yield return pbDisplayPaused(Game._INTL("{1} is trying to learn {2}.",pkmnname,movename));
				yield return pbDisplayPaused(Game._INTL("But {1} can't learn more than four moves.",pkmnname));
				yield return pbDisplayConfirm(Game._INTL("Delete a move to make room for {1}?",movename),result:value=>confirm=value);
				if (confirm) {
					yield return pbDisplayPaused(Game._INTL("Which move should be forgotten?"));
					int forgetmove=(@scene as IPokeBattle_DebugSceneNoGraphics).pbForgetMove(pokemon,move);
					if (forgetmove>=0) {
						string oldmovename=Game._INTL(pokemon.moves[forgetmove].id.ToString(TextScripts.Name));
						pokemon.moves[forgetmove]=new PokemonUnity.Attack.Move(move); // Replaces current/total PP
						if (battler.IsNotNullOrNone())
							battler.moves[forgetmove]=Combat.Move.pbFromPBMove(this,pokemon.moves[forgetmove]); //ToDo: Use ForgetMove Method in Pokemon Class?
							//battler.pokemon.DeleteMoveAtIndex(forgetmove);
						yield return pbDisplayPaused(Game._INTL("1,  2, and... ... ...")); //ToDo: 2sec delay between text
						yield return pbDisplayPaused(Game._INTL("Poof!"));
						yield return pbDisplayPaused(Game._INTL("{1} forgot {2}.",pkmnname,oldmovename));
						yield return pbDisplayPaused(Game._INTL("And..."));
						yield return pbDisplayPaused(Game._INTL("{1} learned {2}!",pkmnname,movename));
						GameDebug.Log($"[Learn move] #{pkmnname} forgot #{oldmovename} and learned #{movename}");
						yield break;
					}
					else {
						yield return pbDisplayConfirm(Game._INTL("Should {1} stop learning {2}?",pkmnname,movename),result:value=>confirm=value);
						if (confirm) {
							yield return pbDisplayPaused(Game._INTL("{1} did not learn {2}.",pkmnname,movename));
							yield break;
						}
					}
				}
				else {
					yield return pbDisplayConfirm(Game._INTL("Should {1} stop learning {2}?",pkmnname,movename),result:value=>confirm=value);
					if (confirm) {
						yield return pbDisplayPaused(Game._INTL("{1} did not learn {2}.",pkmnname,movename));
						yield break;
					}
				}
			} while(true);
		}
		#endregion

		#region Abilities.
		new public IEnumerator pbOnActiveAll() {
			for (int i = 0; i < battlers.Length; i++) { // Currently unfainted participants will earn EXP even if they faint afterwards
				if (pbIsOpposing(i)) (_battlers[i] as IBattlerIE).pbUpdateParticipants();
				if (!pbIsOpposing(i) &&
					(@battlers[i].Item == Items.AMULET_COIN ||
					@battlers[i].Item == Items.LUCK_INCENSE)) @amuletcoin=true;
			}
			for (int i = 0; i < battlers.Length; i++) {
				if (!@battlers[i].isFainted()) {
					if (@battlers[i] is IBattlerShadowPokemon b && b.isShadow() && pbIsOpposing(i)) {
						yield return pbCommonAnimation("Shadow",@battlers[i],null);
						yield return pbDisplay(Game._INTL("Oh!\nA Shadow Pokémon!"));
					}
				}
			}
			// Weather-inducing abilities, Trace, Imposter, etc.
			@usepriority=false;
			IBattlerIE[] priority_=pbPriority();
			foreach (var i in priority_) {
				yield return i?.pbAbilitiesOnSwitchIn(true);
			}
			// Check forms are correct
			for (int i = 0; i < battlers.Length; i++) {
				if (@battlers[i].isFainted()) continue;
				yield return (_battlers[i] as IBattlerIE).pbCheckForm();
			}
		}

		public IEnumerator pbOnActiveOne(IBattlerIE pkmn,bool onlyabilities=false,bool moldbreaker=false,System.Action<bool> result=null) {
			if (pkmn.isFainted()) { result?.Invoke(false); yield break; }
			if (!onlyabilities) {
				for (int i = 0; i < battlers.Length; i++) { // Currently unfainted participants will earn EXP even if they faint afterwards
					if (pbIsOpposing(i)) (_battlers[i] as IBattlerIE).pbUpdateParticipants();
					if (!pbIsOpposing(i) &&
						(@battlers[i].Item == Items.AMULET_COIN ||
						@battlers[i].Item == Items.LUCK_INCENSE)) @amuletcoin=true;
				}
				if (pkmn is IPokemonShadowPokemon p && p.isShadow && pbIsOpposing(pkmn.Index)) {
					yield return pbCommonAnimation("Shadow",pkmn,null);
					yield return pbDisplay(Game._INTL("Oh!\nA Shadow Pokémon!"));
				}
				// Healing Wish
				if (pkmn.effects.HealingWish) {
					GameDebug.Log($"[Lingering effect triggered] #{pkmn.ToString()}'s Healing Wish");
					yield return pbCommonAnimation("HealingWish",pkmn,null);
					yield return pbDisplayPaused(Game._INTL("The healing wish came true for {1}!",pkmn.ToString(true)));
					yield return pkmn.pbRecoverHP(pkmn.TotalHP,true);
					if (pkmn is IBattlerEffectIE b0) yield return b0.pbCureStatus(false);
					pkmn.effects.HealingWish=false;
				}
				// Lunar Dance
				if (pkmn.effects.LunarDance) {
					GameDebug.Log($"[Lingering effect triggered] #{pkmn.ToString()}'s Lunar Dance");
					yield return pbCommonAnimation("LunarDance",pkmn,null);
					yield return pbDisplayPaused(Game._INTL("{1} became cloaked in mystical moonlight!",pkmn.ToString()));
					yield return pkmn.pbRecoverHP(pkmn.TotalHP,true);
					if (pkmn is IBattlerEffectIE b1) yield return b1.pbCureStatus(false);
					for (int i = 0; i < pkmn.moves.Length; i++) {
						pkmn.moves[i].PP=(byte)pkmn.moves[i].TotalPP;
					}
					pkmn.effects.LunarDance=false;
				}
				// Spikes
				if (pkmn.pbOwnSide.Spikes>0 && !pkmn.isAirborne(moldbreaker)) {
					if (!pkmn.hasWorkingAbility(Abilities.MAGIC_GUARD)) {
						GameDebug.Log($"[Entry hazard] #{pkmn.ToString()} triggered Spikes");
						float spikesdiv=new int[] { 8, 6, 4 }[pkmn.pbOwnSide.Spikes-1];
						yield return (@scene as IPokeBattle_SceneIE).pbDamageAnimation(pkmn,0);
						yield return pkmn.pbReduceHP((int)Math.Floor(pkmn.TotalHP/spikesdiv));
						yield return pbDisplayPaused(Game._INTL("{1} is hurt by the spikes!",pkmn.ToString()));
					}
				}
				if (pkmn.isFainted()) yield return pkmn.pbFaint();
				// Stealth Rock
				if (pkmn.pbOwnSide.StealthRock && !pkmn.isFainted()) {
					if (!pkmn.hasWorkingAbility(Abilities.MAGIC_GUARD)) {
						/*Types atype=Types.ROCK; //|| 0;
						//ToDo: Deal with Type Advantage...
						float eff=atype.GetCombinedEffectiveness(pkmn.Type1,pkmn.Type2,pkmn.effects.Type3);
						if (eff>0) {
							GameDebug.Log($"[Entry hazard] #{pkmn.ToString()} triggered Stealth Rock");
							yield return (@scene as IPokeBattle_SceneIE).pbDamageAnimation(pkmn,0);
							yield return pkmn.pbReduceHP((int)Math.Floor((pkmn.TotalHP*eff)/64f));
							yield return pbDisplayPaused(Game._INTL("Pointed stones dug into {1}!",pkmn.ToString()));
						}*/
					}
				}
				if (pkmn.isFainted()) yield return pkmn.pbFaint();
				// Toxic Spikes
				if (pkmn.pbOwnSide.ToxicSpikes>0 && !pkmn.isFainted()) {
					if (!pkmn.isAirborne(moldbreaker)) {
						if (pkmn.pbHasType(Types.POISON)) {
							GameDebug.Log($"[Entry hazard] #{pkmn.ToString()} absorbed Toxic Spikes");
							pkmn.pbOwnSide.ToxicSpikes=0;
							yield return pbDisplayPaused(Game._INTL("{1} absorbed the poison spikes!",pkmn.ToString()));
						}
						else if (pkmn is IBattlerEffectIE b2 && b2.pbCanPoisonSpikes(moldbreaker)) {
							GameDebug.Log($"[Entry hazard] #{pkmn.ToString()} triggered Toxic Spikes");
							if (pkmn.pbOwnSide.ToxicSpikes==2) {
								yield return b2.pbPoison(null,Game._INTL("{1} was badly poisoned by the poison spikes!",pkmn.ToString()),true);
							}
							else {
								yield return b2.pbPoison(null,Game._INTL("{1} was poisoned by the poison spikes!",pkmn.ToString()));
							}
						}
					}
				}
				// Sticky Web
				if (pkmn.pbOwnSide.StickyWeb && !pkmn.isFainted() &&
					!pkmn.isAirborne(moldbreaker) && pkmn is IBattlerEffectIE b) {
					bool canReduceStatStage = false;
					yield return b.pbCanReduceStatStage(PokemonUnity.Combat.Stats.SPEED,null,false,null,moldbreaker,result:value=>canReduceStatStage=value);
					if (canReduceStatStage) {
						GameDebug.Log($"[Entry hazard] #{pkmn.ToString()} triggered Sticky Web");
						yield return pbDisplayPaused(Game._INTL("{1} was caught in a sticky web!",pkmn.ToString()));
						yield return b.pbReduceStat(PokemonUnity.Combat.Stats.SPEED,1,null,false,null,true,moldbreaker);
					}
				}
			}
			yield return pkmn.pbAbilityCureCheck();
			if (pkmn.isFainted()) {
				yield return pbGainEXP();
				pbJudge(); //      pbSwitch
				result?.Invoke(false); yield break;
			}
			yield return pkmn.pbAbilitiesOnSwitchIn(true);
			if (!onlyabilities) {
				yield return pkmn.pbCheckForm();
				yield return pkmn.pbBerryCureCheck();
			}
			result?.Invoke(true);
		}

		new public IEnumerator pbPrimordialWeather() {
			// End Primordial Sea, Desolate Land, Delta Stream
			bool hasabil=false;
			switch (@weather) {
				case Weather.HEAVYRAIN:
					for (int i = 0; i < battlers.Length; i++) {
						if (@battlers[i].Ability == Abilities.PRIMORDIAL_SEA &&
							!@battlers[i].isFainted()) {
							hasabil=true; break;
						}
						if (!hasabil) {
							@weather=Weather.NONE;
							yield return pbDisplayBrief("The heavy rain has lifted!");
						}
					}
					break;
				case Weather.HARSHSUN:
					for (int i = 0; i < battlers.Length; i++) {
						if (@battlers[i].Ability == Abilities.DESOLATE_LAND &&
							!@battlers[i].isFainted()) {
							hasabil=true; break;
						}
						if (!hasabil) {
							@weather=Weather.NONE;
							yield return pbDisplayBrief("The harsh sunlight faded!");
						}
					}
					break;
				case Weather.STRONGWINDS:
					for (int i = 0; i < battlers.Length; i++) {
						if (@battlers[i].Ability == Abilities.DELTA_STREAM &&
							!@battlers[i].isFainted()) {
							hasabil=true; break;
						}
						if (!hasabil) {
							@weather=Weather.NONE;
							yield return pbDisplayBrief("The mysterious air current has dissipated!");
						}
					}
					break;
			}
		}
		#endregion

		#region Judging
		//protected void _pbJudgeCheckpoint(IBattlerIE attacker,IBattleMove move=null) {
		//void IBattleIE.pbJudgeCheckpoint(IBattlerIE attacker,IBattleMove move=null) {
		//}

		//public BattleResults pbDecisionOnTime() {
		//	int count1=0;
		//	int count2=0;
		//	int hptotal1=0;
		//	int hptotal2=0;
		//	foreach (var i in @party1) {
		//		if (!i.IsNotNullOrNone()) continue;
		//		if (i.HP>0 && !i.isEgg) {
		//			count1+=1;
		//			hptotal1+=i.HP;
		//		}
		//	}
		//	foreach (var i in @party2) {
		//		if (!i.IsNotNullOrNone()) continue;
		//		if (i.HP>0 && !i.isEgg) {
		//			count2+=1;
		//			hptotal2+=i.HP;
		//		}
		//	}
		//	if (count1>count2    ) return BattleResults.WON;	// win
		//	if (count1<count2    ) return BattleResults.LOST;	// loss
		//	if (hptotal1>hptotal2) return BattleResults.WON;	// win
		//	if (hptotal1<hptotal2) return BattleResults.LOST;	// loss
		//	return BattleResults.DRAW;                          // draw;
		//}

		//public BattleResults pbDecisionOnTime2() {
		//	int count1=0;
		//	int count2=0;
		//	int hptotal1=0;
		//	int hptotal2=0;
		//	foreach (var i in @party1) {
		//		if (!i.IsNotNullOrNone()) continue;
		//		if (i.HP>0 && !i.isEgg) {
		//			count1+=1;
		//			hptotal1+=(i.HP*100/i.TotalHP); //the difference between the first and second function is this line...
		//		}
		//	}
		//	if (count1>0) hptotal1/=count1;
		//	foreach (var i in @party2) {
		//		if (!i.IsNotNullOrNone()) continue;
		//		if (i.HP>0 && !i.isEgg) {
		//			count2+=1;
		//			hptotal2+=(i.HP*100/i.TotalHP);
		//		}
		//	}
		//	if (count2>0) hptotal2/=count2; //and this line...
		//	if (count1>count2    ) return BattleResults.WON;	// win
		//	if (count1<count2    ) return BattleResults.LOST;	// loss
		//	if (hptotal1>hptotal2) return BattleResults.WON;	// win
		//	if (hptotal1<hptotal2) return BattleResults.LOST;	// loss
		//	return BattleResults.DRAW;                          // draw;
		//}

		//protected BattleResults _pbDecisionOnDraw() {
		//BattleResults IBattle.pbDecisionOnDraw() {
		//	return BattleResults.DRAW; // draw;
		//}

		//public void pbJudge() {
		//	GameDebug.Log($"[Counts: #{pbPokemonCount(@party1)}/#{pbPokemonCount(@party2)}]");
		//	if (pbAllFainted(@party1) && pbAllFainted(@party2)) {
		//		@decision=pbDecisionOnDraw(); // Draw
		//		return;
		//	}
		//	if (pbAllFainted(@party1)) {
		//		@decision=BattleResults.LOST; // Loss
		//		return;
		//	}
		//	if (pbAllFainted(@party2)) {
		//		@decision=BattleResults.WON; // Win
		//		return;
		//	}
		//}
		#endregion

		#region Messages and animations.
		/// <summary>
		/// Displays a message on screen, and wait for player input
		/// </summary>
		/// <param name="text"></param>
		new public IEnumerator pbDisplay(string msg) {
			yield return (@scene as IPokeBattle_SceneIE).pbDisplayMessage(msg);
		}

		new public IEnumerator pbDisplayPaused(string msg) {
			yield return (@scene as IPokeBattle_SceneIE).pbDisplayPausedMessage(msg);
		}

		/// <summary>
		/// Displays a message on screen,
		/// but will continue without player input after short delay
		/// </summary>
		/// <param name="text"></param>
		new public IEnumerator pbDisplayBrief(string msg) {
			yield return (@scene as IPokeBattle_SceneIE).pbDisplayMessage(msg,true);
		}

		public IEnumerator pbDisplayConfirm(string msg,System.Action<bool> result) {
			yield return (@scene as IPokeBattle_SceneIE).pbDisplayConfirmMessage(msg,result);
		}

		new public IEnumerator pbShowCommands(string msg,string[] commands,bool cancancel=true) {
			yield return (@scene as IPokeBattle_SceneIE).pbShowCommands(msg,commands,cancancel);
		}

		new public IEnumerator pbShowCommands(string msg,string[] commands,int cancancel) {
			yield return (@scene as IPokeBattle_SceneIE).pbShowCommands(msg,commands,cancancel);
		}

		public IEnumerator pbAnimation(Moves move,IBattlerIE attacker,IBattlerIE opponent,int hitnum=0) {
			if (@battlescene) {
				if (@scene is IPokeBattle_SceneIE s0) yield return s0.pbAnimation(move,attacker,opponent,hitnum);
			}
		}

		public IEnumerator pbCommonAnimation(string name,IBattlerIE attacker,IBattlerIE opponent,int hitnum=0) {
			if (@battlescene) {
				if (@scene is IPokeBattle_SceneIE s0) yield return s0.pbCommonAnimation(name,attacker,opponent,hitnum);
			}
		}
		#endregion

		#region Battle Core.
		public override BattleResults pbStartBattle(bool canlose=false) {
		//public IEnumerator pbStartBattle(bool canlose=false, System.Action<BattleResults> result=null) {
			GameDebug.Log($"");
			GameDebug.Log($"******************************************");
			@decision = BattleResults.InProgress;
			try {
				//yield return pbStartBattleCore(canlose);
				GameEvents.current.StartCoroutine(pbStartBattleCore(canlose));
			} catch (BattleAbortedException e) { //rescue BattleAbortedException;
				GameDebug.LogError(e.Message);
				GameDebug.LogError(e.StackTrace);

				@decision = BattleResults.ABORTED;
				if (@scene is IPokeBattle_SceneIE s0)
					//yield return s0.pbEndBattle(@decision);
					GameEvents.current.StartCoroutine(s0.pbEndBattle(@decision));
			}
			return @decision; //result?.Invoke(@decision);
		}

		new public IEnumerator pbStartBattleCore(bool canlose) {
			if (!@fullparty1 && @party1.Length>Core.MAXPARTYSIZE) {
				//throw new Exception(new ArgumentError(Game._INTL("Party 1 has more than {1} Pokémon.",Core.MAXPARTYSIZE)));
				GameDebug.LogError(Game._INTL("Party 1 has more than {1} Pokémon.",Core.MAXPARTYSIZE));
				@party1=new IPokemon[Core.MAXPARTYSIZE]; //Fixed error.
				for(int i = 0; i < Core.MAXPARTYSIZE; i++)
					@party1[i] = @party1[i];
			}
			if (!@fullparty2 && @party2.Length>Core.MAXPARTYSIZE) {
				//throw new Exception(new ArgumentError(Game._INTL("Party 2 has more than {1} Pokémon.",Core.MAXPARTYSIZE)));
				GameDebug.LogError(Game._INTL("Party 2 has more than {1} Pokémon.",Core.MAXPARTYSIZE));
				@party2=new IPokemon[Core.MAXPARTYSIZE]; //Fixed error.
				for(int i = 0; i < Core.MAXPARTYSIZE; i++)
					@party2[i] = @party2[i];
			}
			#region Initialize wild Pokémon;
			if (@opponent == null || @opponent.Length == 0) {
				initialize_wild_battle:
				if (@party2.Length==1) {
					if (@doublebattle) {
						//throw new Exception(Game._INTL("Only two wild Pokémon are allowed in double battles"));
						GameDebug.LogError(Game._INTL("Only two wild Pokémon are allowed in double battles"));
						doublebattle = false;
							GameDebug.LogWarning("Changed battle to single.");
					}
					IPokemon wildpoke=@party2[0];
					(_battlers[1] as IBattlerIE).pbInitialize(wildpoke,0,false);
					if (@peer is IBattlePeerMultipleForms p) p.pbOnEnteringBattle(this,wildpoke);
					pbSetSeen(wildpoke);
					yield return (@scene as IPokeBattle_SceneIE).pbStartBattle(this);
					yield return pbDisplayPaused(Game._INTL("Wild {1} appeared!",Game._INTL(wildpoke.Species.ToString(TextScripts.Name)))); //Wild pokemons dont get nicknames
				}
				else if (@party2.Length>1) { //length==2
					if (!@doublebattle) {
						//throw new Exception(Game._INTL("Only one wild Pokémon is allowed in single battles"));
						GameDebug.LogError(Game._INTL("Only one wild Pokémon is allowed in single battles"));
						if (party1.GetBattleCount() > 1) //either set double to true, or remove wild pokemon
						{
							doublebattle = true;
							GameDebug.LogWarning("Changed battle to double.");
						}
						else
						{
							@party2 = new IPokemon[] { @party2.First((x) => x.IsNotNullOrNone()) };
							GameDebug.LogWarning("Removed additional wild pokemon from opposing side.");
							goto initialize_wild_battle;
						}
					}
					(_battlers[1] as IBattlerIE).pbInitialize(@party2[0],0,false);
					(_battlers[3] as IBattlerIE).pbInitialize(@party2[1],0,false);
					if (@peer is IBattlePeerMultipleForms p0) p0.pbOnEnteringBattle(this,@party2[0]);
					if (@peer is IBattlePeerMultipleForms p1) p1.pbOnEnteringBattle(this,@party2[1]);
					pbSetSeen(@party2[0]);
					pbSetSeen(@party2[1]);
					yield return (@scene as IPokeBattle_SceneIE).pbStartBattle(this);
					yield return pbDisplayPaused(Game._INTL("Wild {1} and\r\n{2} appeared!",
						Game._INTL(@party2[0].Species.ToString(TextScripts.Name)),Game._INTL(@party2[1].Species.ToString(TextScripts.Name)))); //Wild pokemons dont get nicknames
				}
				else {
					//throw new Exception(Game._INTL("Only one or two wild Pokémon are allowed"));
					GameDebug.LogError(Game._INTL("Only one or two wild Pokémon are allowed"));
				}
			}
			#endregion
			#region Initialize opponents in double battles;
			else if (@doublebattle) {
				if (@opponent.Length > 0) {
					if (@opponent.Length==1) {
						//@opponent=@opponent[0]; //No changes
					}
					else if (@opponent.Length!=2) {
						//throw new Exception(Game._INTL("Opponents with zero or more than two people are not allowed"));
						GameDebug.LogError(Game._INTL("Opponents with zero or more than two people are not allowed"));
						@opponent= new ITrainer[] { @opponent[0], @opponent[1] }; //Resolved Error
					}
				}
				if (@player.Length > 0) {
					if (@player.Length==1) {
						//@player=@player[0]; //No changes
					}
					else if (@player.Length!=2) {
						//throw new Exception(Game._INTL("Player trainers with zero or more than two people are not allowed"));
						GameDebug.LogError(Game._INTL("Player trainers with zero or more than two people are not allowed"));
						@player= new ITrainer[] { @player[0], @player[1] }; //Resolved Error
					}
				}
				yield return (@scene as IPokeBattle_SceneIE).pbStartBattle(this);
				if (@opponent.Length > 0) {
					yield return pbDisplayPaused(Game._INTL("{1} and {2} want to battle!",@opponent[0].name,@opponent[1].name));
					int sendout1=pbFindNextUnfainted(@party2,0,pbSecondPartyBegin(1));
					if (sendout1<0) GameDebug.LogError(Game._INTL("Opponent 1 has no unfainted Pokémon")); //throw new Exception(Game._INTL("Opponent 1 has no unfainted Pokémon"));
					int sendout2=pbFindNextUnfainted(@party2,pbSecondPartyBegin(1));
					if (sendout2<0) GameDebug.LogError(Game._INTL("Opponent 2 has no unfainted Pokémon")); //throw new Exception(Game._INTL("Opponent 2 has no unfainted Pokémon"));
					(_battlers[1] as IBattlerIE).pbInitialize(@party2[sendout1],(sbyte)sendout1,false);
					yield return pbDisplayBrief(Game._INTL("{1} sent\r\nout {2}!",@opponent[0].name,@battlers[1].Name));
					yield return pbSendOut(1,@party2[sendout1]);
					(_battlers[3] as IBattlerIE).pbInitialize(@party2[sendout2],(sbyte)sendout2,false);
					yield return pbDisplayBrief(Game._INTL("{1} sent\r\nout {2}!",@opponent[1].name,@battlers[3].Name));
					yield return pbSendOut(3,@party2[sendout2]);
				}
				else {
					yield return pbDisplayPaused(Game._INTL("{1}\r\nwould like to battle!",@opponent[0].name));
					int sendout1=pbFindNextUnfainted(@party2,0);
					int sendout2=pbFindNextUnfainted(@party2,sendout1+1);
					if (sendout1<0 || sendout2<0) {
						//throw new Exception(Game._INTL("Opponent doesn't have two unfainted Pokémon"));
						GameDebug.LogError(Game._INTL("Opponent doesn't have two unfainted Pokémon"));
					}
					(_battlers[1] as IBattlerIE).pbInitialize(@party2[sendout1],(sbyte)sendout1,false);
					(_battlers[3] as IBattlerIE).pbInitialize(@party2[sendout2],(sbyte)sendout2,false);
					yield return pbDisplayBrief(Game._INTL("{1} sent\r\nout {2} and {3}!",
						@opponent[0].name,@battlers[1].Name,@battlers[3].Name));
					yield return pbSendOut(1,@party2[sendout1]);
					yield return pbSendOut(3,@party2[sendout2]);
				}
			}
			#endregion
			#region Initialize opponent in single battles
			else {
				int sendout=pbFindNextUnfainted(@party2,0);
				if (sendout<0) GameDebug.LogError(Game._INTL("Trainer has no unfainted Pokémon")); //throw new Exception(Game._INTL("Trainer has no unfainted Pokémon"));
				if (@opponent.Length > 0) {
					if (@opponent.Length!=1) GameDebug.LogError(Game._INTL("Opponent trainer must be only one person in single battles")); //throw new Exception(Game._INTL("Opponent trainer must be only one person in single battles"));
					@opponent=new ITrainer[] { @opponent[0] };
				}
				if (@player.Length > 0) {
					if (@player.Length!=1) GameDebug.LogError(Game._INTL("Player trainer must be only one person in single battles")); //throw new Exception(Game._INTL("Player trainer must be only one person in single battles"));
					@player=new ITrainer[] { @player[0] };
				}
				IPokemon trainerpoke=@party2[sendout];
				yield return (@scene as IPokeBattle_SceneIE).pbStartBattle(this);
				yield return pbDisplayPaused(Game._INTL("{1}\r\nwould like to battle!",@opponent[0].name));
				(_battlers[1] as IBattlerIE).pbInitialize(trainerpoke,(sbyte)sendout,false);
				yield return pbDisplayBrief(Game._INTL("{1} sent\r\nout {2}!",@opponent[0].name,@battlers[1].Name));
				yield return pbSendOut(1,trainerpoke);
			}
			#endregion
			#region Initialize players in double battles
			if (@doublebattle) {
				int sendout1 = 0; int sendout2 = 0;
				if (@player.Length > 0) {
					sendout1=pbFindNextUnfainted(@party1,0,pbSecondPartyBegin(0));
					if (sendout1<0) GameDebug.LogError(Game._INTL("Player 1 has no unfainted Pokémon")); //throw new Exception(Game._INTL("Player 1 has no unfainted Pokémon"));
					sendout2=pbFindNextUnfainted(@party1,pbSecondPartyBegin(0));
					if (sendout2<0) GameDebug.LogError(Game._INTL("Player 2 has no unfainted Pokémon")); //throw new Exception(Game._INTL("Player 2 has no unfainted Pokémon"));
					(_battlers[0] as IBattlerIE).pbInitialize(@party1[sendout1],(sbyte)sendout1,false);
					(_battlers[2] as IBattlerIE).pbInitialize(@party1[sendout2],(sbyte)sendout2,false);
					yield return pbDisplayBrief(Game._INTL("{1} sent\r\nout {2}! Go! {3}!",
						@player[1].name,@battlers[2].Name,@battlers[0].Name));
					pbSetSeen(@party1[sendout1]);
					pbSetSeen(@party1[sendout2]);
				}
				else {
					sendout1=pbFindNextUnfainted(@party1,0);
					sendout2=pbFindNextUnfainted(@party1,sendout1+1);
					if (sendout1<0 || sendout2<0) {
						//throw new Exception(Game._INTL("Player doesn't have two unfainted Pokémon"));
						GameDebug.LogError(Game._INTL("Player doesn't have two unfainted Pokémon"));
					}
					(_battlers[0] as IBattlerIE).pbInitialize(@party1[sendout1],(sbyte)sendout1,false);
					(_battlers[2] as IBattlerIE).pbInitialize(@party1[sendout2],(sbyte)sendout2,false);
					yield return pbDisplayBrief(Game._INTL("Go! {1} and {2}!",@battlers[0].Name,@battlers[2].Name));
				}
				yield return pbSendOut(0,@party1[sendout1]);
				yield return pbSendOut(2,@party1[sendout2]);
			}
			#endregion
			#region Initialize player in single battles
			else {
				int sendout=pbFindNextUnfainted(@party1,0);
				if (sendout<0) {
					//throw new Exception(Game._INTL("Player has no unfainted Pokémon"));
					GameDebug.LogError(Game._INTL("Player has no unfainted Pokémon"));
				}
				(_battlers[0] as IBattlerIE).pbInitialize(@party1[sendout],(sbyte)sendout,false);
				yield return pbDisplayBrief(Game._INTL("Go! {1}!",@battlers[0].Name));
				yield return pbSendOut(0,@party1[sendout]);
			}
			#endregion
			#region Initialize battle
			if (@weather==Weather.SUNNYDAY) {
				yield return pbCommonAnimation("Sunny",null,null);
				yield return pbDisplay(Game._INTL("The sunlight is strong."));
			}
			else if (@weather==Weather.RAINDANCE) {
				yield return pbCommonAnimation("Rain",null,null);
				yield return pbDisplay(Game._INTL("It is raining."));
			}
			else if (@weather==Weather.SANDSTORM) {
				yield return pbCommonAnimation("Sandstorm",null,null);
				yield return pbDisplay(Game._INTL("A sandstorm is raging."));
			}
			else if (@weather==Weather.HAIL) {
				yield return pbCommonAnimation("Hail",null,null);
				yield return pbDisplay(Game._INTL("Hail is falling."));
			}
			else if (@weather==Weather.HEAVYRAIN) {
				yield return pbCommonAnimation("HeavyRain",null,null);
				yield return pbDisplay(Game._INTL("It is raining heavily."));
			}
			else if (@weather==Weather.HARSHSUN) {
				yield return pbCommonAnimation("HarshSun",null,null);
				yield return pbDisplay(Game._INTL("The sunlight is extremely harsh."));
			}
			else if (@weather==Weather.STRONGWINDS) {
				yield return pbCommonAnimation("StrongWinds",null,null);
				yield return pbDisplay(Game._INTL("The wind is strong."));
			}
			yield return pbOnActiveAll();   // Abilities
			@turncount=0;
			#endregion
			#region Battle-Sequence Loop
			do {   // Now begin the battle loop
				GameDebug.Log($"");
				GameDebug.Log($"***Round #{@turncount+1}***");
				if (@debug && @turncount>=100) {
					@decision=pbDecisionOnTime();
					GameDebug.Log($"");
					GameDebug.Log($"***Undecided after 100 rounds, aborting***");
					pbAbort();
					break;
				}
				//try
				//{
					//PBDebug.logonerr{
						yield return pbCommandPhase();
					//}
					if (@decision>0) break;
					//PBDebug.logonerr{
						yield return pbAttackPhase();
					//}
					if (@decision>0) break;
					//PBDebug.logonerr{
						yield return pbEndOfRoundPhase();
					//}
				//}
				//catch (BattleAbortedException ex)
				//{
				//	GameDebug.Log(ex.ToString());
				//	pbAbort();
				//	break;
				//}
				//catch (Exception ex)
				//{
				//	GameDebug.Log(ex.ToString());
				//}
				if (@decision>0) break;
				@turncount+=1;
			} while (this.decision == BattleResults.InProgress); //while (true);
			#endregion
			yield return pbEndOfBattle(canlose);
		}
		#endregion

		#region Command phase.
		public IEnumerator pbCommandMenu(int i, System.Action<MenuCommands> result) {
			yield return (@scene as IPokeBattle_SceneIE).pbCommandMenu(i, result);//result:value=>selected=value
		}

		public IEnumerator pbItemMenu(int i, System.Action<KeyValuePair<Items, int?>> result) {
			//return (@scene as IPokeBattle_SceneNonInteractive).pbItemMenu(i);
			//Returns from UI the Selected item, and the target for the item's usage
			yield return (@scene as IPokeBattle_SceneIE).pbItemMenu(i, result:value=>result?.Invoke(new KeyValuePair<Items,int?>(value, null)));
		}

		//public bool pbAutoFightMenu(int i) {
		//	return false;
		//}

		new public IEnumerator pbCommandPhase() {
			if (@scene is IPokeBattle_DebugSceneNoGraphics s0) s0.pbBeginCommandPhase();
			if (@scene is IPokeBattle_Scene s1) s1.pbResetCommandIndices();
			for (int i = 0; i < battlers.Length; i++) {   // Reset choices if commands can be shown
				(_battlers[i] as IBattlerIE).effects.SkipTurn=false;
				if (@battlers[i].IsNotNullOrNone() && (pbCanShowCommands(i) || @battlers[i].isFainted())) {
					//@choices[i][0]=0;
					//@choices[i][1]=0;
					//@choices[i][2]=null;
					//@choices[i][3]=-1;
					@choices[i]=new Choice(ChoiceAction.NoAction);
				}
				else {
					if (@doublebattle && !pbIsDoubleBattler(i)) {
						GameDebug.Log($"[Reusing commands] #{@battlers[i].ToString(true)}");
					}
				}
			}
			// Reset choices to perform Mega Evolution if it wasn't done somehow
			for (int i = 0; i < sides.Length; i++) {
				for (int j = 0; j < @megaEvolution[i].Length; j++) {
					if (@megaEvolution[i][j]>=0) @megaEvolution[i][j]=-1;
				}
			}
			for (int i = 0; i < battlers.Length; i++) {
				if (@decision==0) break;
				if (@choices[i]?.Action!=0) continue; //@choices[i][0]!=0
				if (!pbOwnedByPlayer(i) || @controlPlayer) {
					if (!@battlers[i].isFainted() && pbCanShowCommands(i)) {
						(@scene as IPokeBattle_SceneNonInteractive).pbChooseEnemyCommand(i);
					}
				}
				else {
					bool commandDone=false;
					//bool commandEnd=false;
					if (pbCanShowCommands(i)) {
						do { //loop
							MenuCommands cmd=MenuCommands.CANCEL;
							yield return pbCommandMenu(i,result:value=>cmd=value);
							if (cmd==MenuCommands.FIGHT) { // Fight
								bool canShowFightMenu = false;
								yield return pbCanShowFightMenu(i,result:value=>canShowFightMenu=value);
								if (canShowFightMenu) {
									if (pbAutoFightMenu(i)) commandDone=true;
									do {
										int index = -1;
										yield return (@scene as IPokeBattle_SceneIE).pbFightMenu(i,result:value=>index=value);
										if (index<0) {
											int side=(pbIsOpposing(i)) ? 1 : 0;
											int owner=pbGetOwnerIndex(i);
											if (@megaEvolution[side][owner]==i) {
												@megaEvolution[side][owner]=-1;
											}
											break;
										}
										bool registerMove = false;
										yield return pbRegisterMove(i,index,result:value=>registerMove=value);
										if (!registerMove) continue;
										if (@doublebattle) {
											IBattleMove thismove=@battlers[i].moves[index];
											//Attack.Target target=@battlers[i].pbTarget(thismove);
											Attack.Data.Targets targets=(_battlers[i] as IBattlerIE).pbTarget(thismove);
											//if (target==Attack.Target.SingleNonUser) {            // single non-user
											if (targets==Attack.Data.Targets.SELECTED_POKEMON ||	// single non-user
												targets==Attack.Data.Targets.SELECTED_POKEMON_ME_FIRST) {
												int target=-1;
												yield return (@scene as IPokeBattle_SceneIE).pbChooseTarget(i,targets,result:value=>target=value);
												if (target<0) continue;
												pbRegisterTarget(i,target);
											}
											//else if (target==Attack.Target.UserOrPartner) {       // Acupressure
											else if (targets==Attack.Data.Targets.USER_OR_ALLY) {   // Acupressure
												int target=-1;
												yield return (@scene as IPokeBattle_SceneIE).pbChooseTarget(i,targets,result:value=>target=value);
												if (target<0 || (target%2)==1) continue; //no choice or enemy
												pbRegisterTarget(i,target);
											}
											//ToDo: Else... random selected pokemon (not target select, but still register target)
										}
										commandDone=true;
									} while (!commandDone);
								}
								else {
									yield return pbAutoChooseMove(i);
									commandDone=true;
								}
							}
							else if (cmd!=MenuCommands.FIGHT && @battlers[i].effects.SkyDrop) {
								yield return pbDisplay(Game._INTL("Sky Drop won't let {1} go!",@battlers[i].ToString(true)));
							}
							else if (cmd==MenuCommands.BAG) { // Bag
								if (!@internalbattle) {
									if (pbOwnedByPlayer(i)) {
										yield return pbDisplay(Game._INTL("Items can't be used here."));
									}
								}
								else {
									KeyValuePair<Items,int?> item=new KeyValuePair<Items, int?>(); 
									yield return pbItemMenu(i,result:value=>item=value);
									if (item.Key>0) {
										bool registerItem = false;
										yield return pbRegisterItem(i,(Items)item.Key,item.Value,result:value=>registerItem=value);
										if (registerItem) {
											commandDone=true;
										}
									}
								}
							}
							else if (cmd==MenuCommands.POKEMON) { // Pokémon
								int pkmn=-1;
								yield return pbSwitchPlayer(i,false,true,result:value=>pkmn=value);
								if (pkmn>=0) {
									bool registerSwitch = false;
									yield return pbRegisterSwitch(i,pkmn,result:value=>registerSwitch=value);
									if (registerSwitch) commandDone=true;
								}
							}
							else if (cmd==MenuCommands.RUN) {   // Run
								int run=-1;
								yield return pbRun(i,result:value=>run=value);
								if (run>0) {
									commandDone=true;
									yield break;
								}
								else if (run<0) {
									commandDone=true;
									int side=(pbIsOpposing(i)) ? 1 : 0;
									int owner=pbGetOwnerIndex(i);
									if (@megaEvolution[side][owner]==i) {
										@megaEvolution[side][owner]=-1;
									}
								}
							}
							else if (cmd==MenuCommands.CALL) {   // Call
								IBattlerIE thispkmn=@battlers[i];
								//@choices[i][0]=4;   // "Call Pokémon"
								//@choices[i][1]=0;
								//@choices[i][2]=null;
								@choices[i]=new Choice(ChoiceAction.CallPokemon);
								int side=pbIsOpposing(i) ? 1 : 0;
								int owner=pbGetOwnerIndex(i);
								if (@megaEvolution[side][owner]==i) {
									@megaEvolution[side][owner]=-1;
								}
								commandDone=true;
							}
							else if (cmd==MenuCommands.CANCEL) {   // Go back to first battler's choice
								if (@megaEvolution[0][0]>=0) @megaEvolution[0][0]=-1;
								if (@megaEvolution[1][0]>=0) @megaEvolution[1][0]=-1;
								// Restore the item the player's first Pokémon was due to use
								//if (@choices[0][0]==3 && Game.GameData.Bag && Game.GameData.Bag.pbCanStore(@choices[0][1])) {
								if (@choices[0].Action==ChoiceAction.UseItem && Game.GameData.Bag != null && Game.GameData.Bag.pbCanStore((Items)@choices[0].Index)) {
									Game.GameData.Bag.pbStoreItem((Items)@choices[0].Index); //@choices[0][1]
								}
								yield return pbCommandPhase();
								yield break;
							}
							if (commandDone) break;
						} while (true);
					}
				}
			}
		}
		#endregion

		#region Attack phase.
		new public IEnumerator pbAttackPhase() {
			if (@scene is IPokeBattle_SceneIE s0) yield return s0.pbBeginAttackPhase();
			for (int i = 0; i < battlers.Length; i++) {
				@successStates[i].Clear();
				if (@choices[i]?.Action!= ChoiceAction.UseMove && @choices[i]?.Action!=ChoiceAction.SwitchPokemon) {
					(_battlers[i] as IBattlerIE).effects.DestinyBond=false;
					(_battlers[i] as IBattlerIE).effects.Grudge=false;
				}
				if (!@battlers[i].isFainted()) (_battlers[i] as IBattlerIE).turncount+=1;
				if (!pbChoseMove(i,Moves.RAGE)) (_battlers[i] as IBattlerIE).effects.Rage=false;
			}
			// Calculate priority at this time
			@usepriority=false;
			_priority=pbPriority(false,true);
			// Mega Evolution
			List<int> megaevolved=new List<int>();
			foreach (var i in priority) {
				if (@choices[i.Index]?.Action==ChoiceAction.UseMove && !i.effects.SkipTurn) {
					int side=(pbIsOpposing(i.Index)) ? 1 : 0;
					int owner=pbGetOwnerIndex(i.Index);
					if (@megaEvolution[side][owner]==i.Index) { //@megaEvolution[side][owner]
						//pbMegaEvolve(i.Index);
						megaevolved.Add(i.Index);
					}
				}
			}
			if (megaevolved.Count>0) {
				foreach (var i in priority) {
					if (megaevolved.Contains(i.Index)) yield return i.pbAbilitiesOnSwitchIn(true);
				}
			}
			// Call at Pokémon
			foreach (var i in priority) {
				if (@choices[i.Index]?.Action==ChoiceAction.CallPokemon && !i.effects.SkipTurn) {
					yield return pbCall(i.Index);
				}
			}
			// Switch out Pokémon
			@switching=true;
			List<int> switched=new List<int>();
			foreach (var i in priority) {
				if (@choices[i.Index]?.Action==ChoiceAction.SwitchPokemon && !i.effects.SkipTurn) {
					int index=@choices[i.Index].Index; // party position of Pokémon to switch to
					int newpokename=index;
					if (pbParty(i.Index)[index].Ability == Abilities.ILLUSION) {
						newpokename=pbGetLastPokeInTeam(i.Index);
					}
					this.lastMoveUser=i.Index;
					if (!pbOwnedByPlayer(i.Index)) {
						ITrainer owner=pbGetOwner(i.Index);
						yield return pbDisplayBrief(Game._INTL("{1} withdrew {2}!",owner.name,i.Name));
						GameDebug.Log($"[Withdrew Pokémon] Opponent withdrew #{i.ToString(true)}");
					}
					else {
						yield return pbDisplayBrief(Game._INTL("{1}, that's enough!\r\nCome back!",i.Name));
						GameDebug.Log($"[Withdrew Pokémon] Player withdrew #{i.ToString(true)}");
					}
					foreach (var j in priority) {
						if (!i.pbIsOpposing(j.Index)) continue;
						// if Pursuit and this target ("i") was chosen
						if (pbChoseMoveFunctionCode(j.Index,Attack.Data.Effects.x081) && // Pursuit
							!j.hasMovedThisRound()) {
							if (j.Status!=Status.SLEEP && j.Status!=Status.FROZEN &&
								!j.effects.SkyDrop &&
								(!j.hasWorkingAbility(Abilities.TRUANT) || !j.effects.Truant)) {
								//@choices[j.Index].Target=i.Index; // Make sure to target the switching Pokémon
								@choices[j.Index]=new Choice(@choices[j.Index].Action, @choices[j.Index].Index, @choices[j.Index].Move, target: i.Index); // Make sure to target the switching Pokémon
								yield return j.pbUseMove(@choices[j.Index]); // This calls pbGainEXP as appropriate
								j.effects.Pursuit=true;
								@switching=false;
								if (@decision>0) yield break;
							}
						}
						if (i.isFainted()) break;
					}
					bool recallAndReplace = false;
					yield return pbRecallAndReplace(i.Index,index,newpokename,false,false,value=>recallAndReplace=value);
					if (!recallAndReplace) {
						// If a forced switch somehow occurs here in single battles
						// the attack phase now ends
						if (!@doublebattle) {
							@switching=false;
							yield break;
						}
					}
					else {
						switched.Add(i.Index);
					}
				}
			}
			if (switched.Count>0) {
				foreach (var i in priority) {
					if (switched.Contains(i.Index)) i.pbAbilitiesOnSwitchIn(true);
				}
			}
			@switching=false;
			// Use items
			foreach (var i in priority) {
				if (@choices[i.Index]?.Action==ChoiceAction.UseItem && !i.effects.SkipTurn) {
					if (pbIsOpposing(i.Index)) {
						// Opponent use item
						yield return pbEnemyUseItem((Items)@choices[i.Index].Index,i);
					}
					else {
						// Player use item
						Items item=(Items)@choices[i.Index].Index;
						if (item>0) {
							//usetype=ItemData[item][Core.ITEMBATTLEUSE]; ToDo
							int usetype=0;//Kernal.ItemData[item].Flags.[Core.ITEMBATTLEUSE]; //AIs can use items?
							if (usetype==1 || usetype==3) {
								if (@choices[i.Index].Target>=0) { //@choices[i.Index][2]
									pbUseItemOnPokemon(item,@choices[i.Index].Target,i,@scene);
								}
							}
							else if (usetype==2 || usetype==4) {
								if (!ItemHandlers.hasUseInBattle(item)) { // Poké Ball/Poké Doll used already
									pbUseItemOnBattler(item,@choices[i.Index].Target,i,@scene);
								}
							}
						}
					}
				}
			}
			// Use attacks
			foreach (var i in priority) {
				if (i.effects.SkipTurn) continue;
				if (pbChoseMoveFunctionCode(i.Index,Attack.Data.Effects.x0AB)) { // Focus Punch
					yield return pbCommonAnimation("FocusPunch",i,null);
					yield return pbDisplay(Game._INTL("{1} is tightening its focus!",i.ToString()));
				}
			}
			int n = 0; do { //10.times
				// Forced to go next
				bool advance=false;
				foreach (var i in priority) {
					if (!i.effects.MoveNext) continue;
					if (i.hasMovedThisRound() || i.effects.SkipTurn) continue;
					yield return i.pbProcessTurn(@choices[i.Index], value => advance = value);
					if (advance) break;
				}
				if (@decision>0) yield break;
				if (advance) continue;
				// Regular priority order
				foreach (var i in priority) {
					if (i.effects.Quash) continue;
					if (i.hasMovedThisRound() || i.effects.SkipTurn) continue;
					yield return i.pbProcessTurn(@choices[i.Index], value => advance = value);
					if (advance) break;
				}
				if (@decision>0) yield break;
				if (advance) continue;
				// Quashed
				foreach (var i in priority) {
					if (!i.effects.Quash) continue;
					if (i.hasMovedThisRound() || i.effects.SkipTurn) continue;
					yield return i.pbProcessTurn(@choices[i.Index], value => advance = value);
					if (advance) break;
				}
				if (@decision>0) yield break;
				if (advance) continue;
				// Check for all done
				foreach (var i in priority) {
					if (@choices[i.Index]?.Action==ChoiceAction.UseMove && !i.hasMovedThisRound() &&
						!i.effects.SkipTurn) advance=true;
					if (advance) break;
				}
				if (advance) continue;
				n++;//break;
			} while (n < 10);
			if (Game.GameData is IGameField f) f.pbWait(20);
		}
		#endregion

		#region End of round.
		//protected void _pbEndOfRoundPhase() {
		new public IEnumerator pbEndOfRoundPhase() {
			GameDebug.Log($"[End of round]");
			for (int i = 0; i < battlers.Length; i++) {
				(_battlers[i] as IBattlerIE).effects.Electrify=false;
				(_battlers[i] as IBattlerIE).effects.Endure=false;
				(_battlers[i] as IBattlerIE).effects.FirstPledge=0;
				if (@battlers[i].effects.HyperBeam>0) (_battlers[i] as IBattlerIE).effects.HyperBeam-=1;
				(_battlers[i] as IBattlerIE).effects.KingsShield=false;
				(_battlers[i] as IBattlerIE).effects.LifeOrb=false;
				(_battlers[i] as IBattlerIE).effects.MoveNext=false;
				(_battlers[i] as IBattlerIE).effects.Powder=false;
				(_battlers[i] as IBattlerIE).effects.Protect=false;
				(_battlers[i] as IBattlerIE).effects.ProtectNegation=false;
				(_battlers[i] as IBattlerIE).effects.Quash=false;
				(_battlers[i] as IBattlerIE).effects.Roost=false;
				(_battlers[i] as IBattlerIE).effects.SpikyShield=false;
			}
			@usepriority=false;  // recalculate priority
			_priority=pbPriority(true); // Ignoring Quick Claw here
			// Weather
			switch (@weather) {
				case Weather.SUNNYDAY:
					if (@weatherduration>0) @weatherduration=@weatherduration-1;
					if (@weatherduration==0) {
						yield return pbDisplay(Game._INTL("The sunlight faded."));
						@weather=0;
						GameDebug.Log($"[End of effect] Sunlight weather ended");
					}
					else {
						yield return pbCommonAnimation("Sunny",null,null);
						yield return pbDisplay(Game._INTL("The sunlight is strong."));
						if (pbWeather==Weather.SUNNYDAY) {
							foreach (var i in priority) {
								if (i.hasWorkingAbility(Abilities.SOLAR_POWER)) {
									GameDebug.Log($"[Ability triggered] #{i.ToString()}'s Solar Power");
									yield return (@scene as IPokeBattle_SceneIE).pbDamageAnimation(i,0);
									yield return i.pbReduceHP((int)Math.Floor(i.TotalHP/8f));
									yield return pbDisplay(Game._INTL("{1} was hurt by the sunlight!",i.ToString()));
									if (i.isFainted()) {
										bool faint = false;
										yield return i.pbFaint(result: value => faint = value);
										if (!faint) yield break;
									}
								}
							}
						}
					}
					break;
				case Weather.RAINDANCE:
					if (@weatherduration>0) @weatherduration=@weatherduration-1;
					if (@weatherduration==0) {
						yield return pbDisplay(Game._INTL("The rain stopped."));
						@weather=0;
						GameDebug.Log($"[End of effect] Rain weather ended");
					}
					else {
						yield return pbCommonAnimation("Rain",null,null);
						yield return pbDisplay(Game._INTL("Rain continues to fall."));
					}
					break;
				case Weather.SANDSTORM:
					if (@weatherduration>0) @weatherduration=@weatherduration-1;
					if (@weatherduration==0) {
						yield return pbDisplay(Game._INTL("The sandstorm subsided."));
						@weather=0;
						GameDebug.Log($"[End of effect] Sandstorm weather ended");
					}
					else {
						yield return pbCommonAnimation("Sandstorm",null,null);
						yield return pbDisplay(Game._INTL("The sandstorm rages."));
						if (pbWeather==Weather.SANDSTORM) {
							GameDebug.Log($"[Lingering effect triggered] Sandstorm weather damage");
							foreach (var i in priority) {
								if (i.isFainted()) continue;
								if (!i.pbHasType(Types.GROUND) && !i.pbHasType(Types.ROCK) && !i.pbHasType(Types.STEEL) &&
									!i.hasWorkingAbility(Abilities.SAND_VEIL) &&
									!i.hasWorkingAbility(Abilities.SAND_RUSH) &&
									!i.hasWorkingAbility(Abilities.SAND_FORCE) &&
									!i.hasWorkingAbility(Abilities.MAGIC_GUARD) &&
									!i.hasWorkingAbility(Abilities.OVERCOAT) &&
									!i.hasWorkingItem(Items.SAFETY_GOGGLES) &&
									!new Attack.Data.Effects[] {
										Attack.Data.Effects.x101, // Dig
										Attack.Data.Effects.x100  // Dive
									}.Contains(Kernal.MoveData[i.effects.TwoTurnAttack].Effect)) {
									yield return (@scene as IPokeBattle_SceneIE).pbDamageAnimation(i,0);
									yield return i.pbReduceHP((int)Math.Floor(i.TotalHP/16f));
									yield return pbDisplay(Game._INTL("{1} is buffeted by the sandstorm!",i.ToString()));
									if (i.isFainted()) {
										bool faint = false;
										yield return i.pbFaint(result: value => faint = value);
										if (!faint) yield break;
									}
								}
							}
						}
					}
					break;
				case Weather.HAIL:
					if (@weatherduration>0) @weatherduration=@weatherduration-1;
					if (@weatherduration==0) {
						yield return pbDisplay(Game._INTL("The hail stopped."));
						@weather=0;
						GameDebug.Log($"[End of effect] Hail weather ended");
					}
					else {
						yield return pbCommonAnimation("Hail",null,null);
						yield return pbDisplay(Game._INTL("Hail continues to fall."));
						if (pbWeather==Weather.HAIL) {
							GameDebug.Log($"[Lingering effect triggered] Hail weather damage");
							foreach (var i in priority) {
								if (i.isFainted()) continue;
								if (!i.pbHasType(Types.ICE) &&
									!i.hasWorkingAbility(Abilities.ICE_BODY) &&
									!i.hasWorkingAbility(Abilities.SNOW_CLOAK) &&
									!i.hasWorkingAbility(Abilities.MAGIC_GUARD) &&
									!i.hasWorkingAbility(Abilities.OVERCOAT) &&
									!i.hasWorkingItem(Items.SAFETY_GOGGLES) &&
									!new int[] { 0xCA,0xCB }.Contains((int)Kernal.MoveData[i.effects.TwoTurnAttack].Effect)) { // Dig, Dive
									yield return (@scene as IPokeBattle_SceneIE).pbDamageAnimation(i,0);
									yield return i.pbReduceHP((int)Math.Floor(i.TotalHP/16f));
									yield return pbDisplay(Game._INTL("{1} is buffeted by the hail!",i.ToString()));
									if (i.isFainted()) {
										bool faint = false;
										yield return i.pbFaint(result: value => faint = value);
										if (!faint) yield break;
									}
								}
							}
						}
					}
					break;
				case Weather.HEAVYRAIN:
					bool hasabil=false;
					for (int i = 0; i < battlers.Length; i++) {
						if (@battlers[i].Ability == Abilities.PRIMORDIAL_SEA && !@battlers[i].isFainted()) {
							hasabil=true; break;
						}
					}
					if (!hasabil) @weatherduration=0;
					if (@weatherduration==0) {
						yield return pbDisplay(Game._INTL("The heavy rain stopped."));
						@weather=0;
						GameDebug.Log($"[End of effect] Primordial Sea's rain weather ended");
					}
					else {
						yield return pbCommonAnimation("HeavyRain",null,null);
						yield return pbDisplay(Game._INTL("It is raining heavily."));
					}
					break;
				case Weather.HARSHSUN:
					hasabil=false;
					for (int i = 0; i < battlers.Length; i++) {
						if (@battlers[i].Ability == Abilities.DESOLATE_LAND && !@battlers[i].isFainted()) {
							hasabil=true; break;
						}
					}
					if (!hasabil) @weatherduration=0;
					if (@weatherduration==0) {
						yield return pbDisplay(Game._INTL("The harsh sunlight faded."));
						@weather=0;
						GameDebug.Log($"[End of effect] Desolate Land's sunlight weather ended");
					}
					else {
						yield return pbCommonAnimation("HarshSun",null,null);
						yield return pbDisplay(Game._INTL("The sunlight is extremely harsh."));
						if (pbWeather==Weather.HARSHSUN) {
							foreach (var i in priority) {
								if (i.hasWorkingAbility(Abilities.SOLAR_POWER)) {
									GameDebug.Log($"[Ability triggered] #{i.ToString()}'s Solar Power");
									yield return (@scene as IPokeBattle_SceneIE).pbDamageAnimation(i,0);
									yield return i.pbReduceHP((int)Math.Floor(i.TotalHP/8f));
									yield return pbDisplay(Game._INTL("{1} was hurt by the sunlight!",i.ToString()));
									if (i.isFainted()) {
										bool faint = false;
										yield return i.pbFaint(result: value => faint = value);
										if (!faint) yield break;
									}
								}
							}
						}
					}
					break;
				case Weather.STRONGWINDS:
					hasabil=false;
					for (int i = 0; i < battlers.Length; i++) {
						if (@battlers[i].Ability == Abilities.DELTA_STREAM && !@battlers[i].isFainted()) {
							hasabil=true; break;
						}
					}
					if (!hasabil) @weatherduration=0;
					if (@weatherduration==0) {
						yield return pbDisplay(Game._INTL("The air current subsided."));
						@weather=Weather.NONE;
						GameDebug.Log($"[End of effect] Delta Stream's wind weather ended");
					}
					else {
						yield return pbCommonAnimation("StrongWinds",null,null);
						yield return pbDisplay(Game._INTL("The wind is strong."));
					}
					break;
			}
			// Shadow Sky weather
			if (@weather == Weather.SHADOWSKY) {
				if (@weatherduration>0) @weatherduration=@weatherduration-1;
				if (@weatherduration==0) {
					yield return pbDisplay(Game._INTL("The shadow sky faded."));
					@weather=Weather.NONE;
					GameDebug.Log($"[End of effect] Shadow Sky weather ended");
				}
				else {
					yield return pbCommonAnimation("ShadowSky",null,null);
					yield return pbDisplay(Game._INTL("The shadow sky continues."));
					if (pbWeather == Weather.SHADOWSKY) {
						GameDebug.Log($"[Lingering effect triggered] Shadow Sky weather damage");
						foreach (var i in priority) {
							if (i.isFainted()) continue;
							if (i is IBattlerShadowPokemon s && !s.isShadow()) {
								yield return (@scene as IPokeBattle_SceneIE).pbDamageAnimation(i,0);
								yield return i.pbReduceHP((int)Math.Floor(i.TotalHP/16f));
								yield return pbDisplay(Game._INTL("{1} was hurt by the shadow sky!",i.ToString()));
								if (i.isFainted()) {
									bool faint = false;
									yield return i.pbFaint(result: value => faint = value);
									if (!faint) yield break;
								}
							}
						}
					}
				}
			}
			// Future Sight/Doom Desire
			foreach (IBattlerIE i in battlers) {   // not priority
				if (i.isFainted()) continue;
				if (i.effects.FutureSight>0) {
					i.effects.FutureSight-=1;
					if (i.effects.FutureSight==0) {
						Moves move=i.effects.FutureSightMove;
						GameDebug.Log($"[Lingering effect triggered] #{Game._INTL(move.ToString(TextScripts.Name))} struck #{i.ToString(true)}");
						yield return pbDisplay(Game._INTL("{1} took the {2} attack!",i.ToString(),Game._INTL(move.ToString(TextScripts.Name))));
						IBattlerIE moveuser=null;
						foreach (var j in battlers) {
						if (j.pbIsOpposing(i.effects.FutureSightUserPos)) continue;
							if (j.pokemonIndex==i.effects.FutureSightUser && !j.isFainted()) {
								moveuser=j; break;
							}
						}
						if (!moveuser.IsNotNullOrNone()) {
							IPokemon[] party=pbParty(i.effects.FutureSightUserPos);
							if (party[i.effects.FutureSightUser].HP>0) {
								moveuser=new PokemonUnity.Stadium.Battler(this,(sbyte)i.effects.FutureSightUserPos);
								moveuser.pbInitPokemon(party[i.effects.FutureSightUser],
													(sbyte)i.effects.FutureSightUser);
							}
						}
						if (!moveuser.IsNotNullOrNone()) {
							yield return pbDisplay(Game._INTL("But it failed!"));
						}
						else {
							@futuresight=true;
							yield return moveuser.pbUseMoveSimple(move,-1,i.Index);
							@futuresight=false;
						}
						i.effects.FutureSight=0;
						i.effects.FutureSightMove=0;
						i.effects.FutureSightUser=-1;
						i.effects.FutureSightUserPos=-1;
						if (i.isFainted()) {
							bool faint = false;
							yield return i.pbFaint(result: value => faint = value);
							if (!faint) yield break;
							continue;
						}
					}
				}
			}
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				// Rain Dish
				if (i.hasWorkingAbility(Abilities.RAIN_DISH) &&
					(pbWeather==Weather.RAINDANCE ||
					pbWeather==Weather.HEAVYRAIN)) {
					GameDebug.Log($"[Ability triggered] #{i.ToString()}'s Rain Dish");
					int hpgain=0;
					yield return i.pbRecoverHP((int)Math.Floor(i.TotalHP/16f),true,result:value=>hpgain=value);
					if (hpgain>0) yield return pbDisplay(Game._INTL("{1}'s {2} restored its HP a little!",i.ToString(),Game._INTL(i.Ability.ToString(TextScripts.Name))));
				}
				// Dry Skin
				if (i.hasWorkingAbility(Abilities.DRY_SKIN)) {
					if (pbWeather==Weather.RAINDANCE ||
						pbWeather==Weather.HEAVYRAIN) {
						GameDebug.Log($"[Ability triggered] #{i.ToString()}'s Dry Skin (in rain)");
						int hpgain=0;
						yield return i.pbRecoverHP((int)Math.Floor(i.TotalHP/8f),true,result:value=>hpgain=value);
						if (hpgain>0) yield return pbDisplay(Game._INTL("{1}'s {2} was healed by the rain!",i.ToString(),Game._INTL(i.Ability.ToString(TextScripts.Name))));
					}
					else if (pbWeather==Weather.SUNNYDAY ||
							pbWeather==Weather.HARSHSUN) {
						GameDebug.Log($"[Ability triggered] #{i.ToString()}'s Dry Skin (in sun)");
						yield return(@scene as IPokeBattle_SceneIE).pbDamageAnimation(i,0);
						int hploss=0;
						yield return i.pbReduceHP((int)Math.Floor(i.TotalHP/8f),result:value=>hploss=value);
						if (hploss>0) yield return pbDisplay(Game._INTL("{1}'s {2} was hurt by the sunlight!",i.ToString(),Game._INTL(i.Ability.ToString(TextScripts.Name))));
					}
				}
				// Ice Body
				if (i.hasWorkingAbility(Abilities.ICE_BODY) && pbWeather==Weather.HAIL) {
					GameDebug.Log($"[Ability triggered] #{i.ToString()}'s Ice Body");
					int hpgain=0;
					yield return i.pbRecoverHP((int)Math.Floor(i.TotalHP/16f),true,result:value=>hpgain=value);
					if (hpgain>0) yield return pbDisplay(Game._INTL("{1}'s {2} restored its HP a little!",i.ToString(),Game._INTL(i.Ability.ToString(TextScripts.Name))));
				}
				if (i.isFainted()) {
					bool faint = false;
					yield return i.pbFaint(result: value => faint = value);
					if (!faint) yield break;
				}
			}
			// Wish
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				if (i.effects.Wish>0) {
					i.effects.Wish-=1;
					if (i.effects.Wish==0) {
						GameDebug.Log($"[Lingering effect triggered] #{i.ToString()}'s Wish");
						int hpgain=0;
						yield return i.pbRecoverHP(i.effects.WishAmount,true,result:value=>hpgain=value);
						if (hpgain>0) {
							string wishmaker=ToString(i.Index,i.effects.WishMaker);
							yield return pbDisplay(Game._INTL("{1}'s wish came true!",wishmaker));
						}
					}
				}
			}
			// Fire Pledge + Grass Pledge combination damage
			for (int i = 0; i < sides.Length; i++) {
				if (sides[i].SeaOfFire>0 &&
					pbWeather!=Weather.RAINDANCE &&
					pbWeather!=Weather.HEAVYRAIN) {
					if (i==0) yield return pbCommonAnimation("SeaOfFire",null,null);     //@battle.
					if (i==1) yield return pbCommonAnimation("SeaOfFireOpp",null,null);  //@battle.
					foreach (var j in priority) {
						if ((j.Index&1)!=i) continue;
						if (j.pbHasType(Types.FIRE) || j.hasWorkingAbility(Abilities.MAGIC_GUARD)) continue;
						yield return (@scene as IPokeBattle_SceneIE).pbDamageAnimation(j,0);
						int hploss=0;
						yield return j.pbReduceHP((int)Math.Floor(j.TotalHP/8f),result:value=>hploss=value);
						if (hploss>0) yield return pbDisplay(Game._INTL("{1} is hurt by the sea of fire!",j.ToString()));
						if (j.isFainted()) {
							bool faint = false;
							yield return j.pbFaint(result: value => faint = value);
							if (!faint) yield break;
						}
					}
				}
			}
			foreach (IBattlerIE i in priority) {
				if (i.isFainted()) continue;
				// Shed Skin, Hydration
				if ((i.hasWorkingAbility(Abilities.SHED_SKIN) && pbRandom(10)<3) ||
					(i.hasWorkingAbility(Abilities.HYDRATION) && (pbWeather==Weather.RAINDANCE ||
					pbWeather==Weather.HEAVYRAIN))) {
					if (i.Status>0) {
						GameDebug.Log($"[Ability triggered] #{i.ToString()}'s #{Game._INTL(i.Ability.ToString(TextScripts.Name))}");
						Status s=i.Status;
						if (i is IBattlerEffectIE b) yield return b.pbCureStatus(false);
						switch (s) {
							case Status.SLEEP:
								yield return pbDisplay(Game._INTL("{1}'s {2} cured its sleep problem!",i.ToString(),Game._INTL(i.Ability.ToString(TextScripts.Name))));
								break;
							case Status.POISON:
								yield return pbDisplay(Game._INTL("{1}'s {2} cured its poison problem!",i.ToString(),Game._INTL(i.Ability.ToString(TextScripts.Name))));
								break;
							case Status.BURN:
								yield return pbDisplay(Game._INTL("{1}'s {2} healed its burn!",i.ToString(),Game._INTL(i.Ability.ToString(TextScripts.Name))));
								break;
							case Status.PARALYSIS:
								yield return pbDisplay(Game._INTL("{1}'s {2} cured its paralysis!",i.ToString(),Game._INTL(i.Ability.ToString(TextScripts.Name))));
								break;
							case Status.FROZEN:
								yield return pbDisplay(Game._INTL("{1}'s {2} thawed it out!",i.ToString(),Game._INTL(i.Ability.ToString(TextScripts.Name))));
								break;
						}
					}
				}
				// Healer
				if (i.hasWorkingAbility(Abilities.HEALER) && pbRandom(10)<3) {
					IBattlerIE partner=i.pbPartner;
					if (partner.IsNotNullOrNone() && partner.Status>0) {
						GameDebug.Log($"[Ability triggered] #{i.ToString()}'s #{Game._INTL(i.Ability.ToString(TextScripts.Name))}");
						Status s=partner.Status;
						if (partner is IBattlerEffectIE b) yield return b.pbCureStatus(false);
						switch (s) {
							case Status.SLEEP:
								yield return pbDisplay(Game._INTL("{1}'s {2} cured its partner's sleep problem!",i.ToString(),Game._INTL(i.Ability.ToString(TextScripts.Name))));
								break;
							case Status.POISON:
								yield return pbDisplay(Game._INTL("{1}'s {2} cured its partner's poison problem!",i.ToString(),Game._INTL(i.Ability.ToString(TextScripts.Name))));
								break;
							case Status.BURN:
								yield return pbDisplay(Game._INTL("{1}'s {2} healed its partner's burn!",i.ToString(),Game._INTL(i.Ability.ToString(TextScripts.Name))));
								break;
							case Status.PARALYSIS:
								yield return pbDisplay(Game._INTL("{1}'s {2} cured its partner's paralysis!",i.ToString(),Game._INTL(i.Ability.ToString(TextScripts.Name))));
								break;
							case Status.FROZEN:
								yield return pbDisplay(Game._INTL("{1}'s {2} thawed its partner out!",i.ToString(),Game._INTL(i.Ability.ToString(TextScripts.Name))));
								break;
						}
					}
				}
			}
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				// Grassy Terrain (healing)
				if (@field.GrassyTerrain>0 && !i.isAirborne()) {
					int hpgain=0;
					yield return i.pbRecoverHP((int)Math.Floor(i.TotalHP/16f),true,result:value=>hpgain=value);
					if (hpgain>0) yield return pbDisplay(Game._INTL("{1}'s HP was restored.",i.ToString()));
				}
				// Held berries/Leftovers/Black Sludge
				yield return i.pbBerryCureCheck(true);
				if (i.isFainted()) {
					bool faint = false;
					yield return i.pbFaint(result: value => faint = value);
					if (!faint) yield break;
				}
			}
			// Aqua Ring
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				if (i.effects.AquaRing) {
					GameDebug.Log($"[Lingering effect triggered] #{i.ToString()}'s Aqua Ring");
					int hpgain=(int)Math.Floor(i.TotalHP/16f);
					if (i.hasWorkingItem(Items.BIG_ROOT)) hpgain=(int)Math.Floor(hpgain*1.3);
					hpgain=0;
					yield return i.pbRecoverHP(hpgain,true,result:value=>hpgain=value);
					if (hpgain>0) yield return pbDisplay(Game._INTL("Aqua Ring restored {1}'s HP!",i.ToString()));
				}
			}
			// Ingrain
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				if (i.effects.Ingrain) {
					GameDebug.Log($"[Lingering effect triggered] #{i.ToString()}'s Ingrain");
					int hpgain=(int)Math.Floor(i.TotalHP/16f);
					if (i.hasWorkingItem(Items.BIG_ROOT)) hpgain=(int)Math.Floor(hpgain*1.3);
					hpgain=0;
					yield return i.pbRecoverHP(hpgain,true,result:value=>hpgain=value);
					if (hpgain>0) yield return pbDisplay(Game._INTL("{1} absorbed nutrients with its roots!",i.ToString()));
				}
			}
			// Leech Seed
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				if (i.effects.LeechSeed>=0 && !i.hasWorkingAbility(Abilities.MAGIC_GUARD)) {
					IBattlerIE recipient=@battlers[i.effects.LeechSeed];
					if (recipient.IsNotNullOrNone() && !recipient.isFainted()) {
						GameDebug.Log($"[Lingering effect triggered] #{i.ToString()}'s Leech Seed");
						yield return pbCommonAnimation("LeechSeed",recipient,i);
						int hploss=0;
						yield return i.pbReduceHP((int)Math.Floor(i.TotalHP/8f),true,result:value=>hploss=value);
						if (i.hasWorkingAbility(Abilities.LIQUID_OOZE)) {
							yield return recipient.pbReduceHP(hploss,true);
							yield return pbDisplay(Game._INTL("{1} sucked up the liquid ooze!",recipient.ToString()));
						}
						else {
							if (recipient.effects.HealBlock==0) {
								if (recipient.hasWorkingItem(Items.BIG_ROOT)) hploss=(int)Math.Floor(hploss*1.3);
								yield return recipient.pbRecoverHP(hploss,true);
							}
							yield return pbDisplay(Game._INTL("{1}'s health was sapped by Leech Seed!",i.ToString()));
						}
						if (i.isFainted()) {
							bool faint = false;
							yield return i.pbFaint(result: value => faint = value);
							if (!faint) yield break;
						}
						if (recipient.isFainted()) {
							bool faint = false;
							yield return recipient.pbFaint(result: value => faint = value);
							if (!faint) yield break;
						}
					}
				}
			}
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				// Poison/Bad poison
				if (i.Status==Status.POISON) {
					if (i.StatusCount>0) {
						i.effects.Toxic+=1;
						i.effects.Toxic=(int)Math.Min(15,i.effects.Toxic);
					}
					if (i.hasWorkingAbility(Abilities.POISON_HEAL)) {
						yield return pbCommonAnimation("Poison",i,null);
						if (i.effects.HealBlock==0 && i.HP<i.TotalHP) {
							GameDebug.Log($"[Ability triggered] #{i.ToString()}'s Poison Heal");
							yield return i.pbRecoverHP((int)Math.Floor(i.TotalHP/8f),true);
							yield return pbDisplay(Game._INTL("{1} is healed by poison!",i.ToString()));
						}
					}
					else {
						if (!i.hasWorkingAbility(Abilities.MAGIC_GUARD)) {
							GameDebug.Log($"[Status damage] #{i.ToString()} took damage from poison/toxic");
							if (i.StatusCount==0) {
								yield return i.pbReduceHP((int)Math.Floor(i.TotalHP/8f));
							}
							else {
								yield return i.pbReduceHP((int)Math.Floor((i.TotalHP*i.effects.Toxic)/16f));
							}
							if (i is IBattlerEffectIE b) yield return b.pbContinueStatus();
						}
					}
				}
				// Burn
				if (i.Status==Status.BURN) {
					if (!i.hasWorkingAbility(Abilities.MAGIC_GUARD)) {
						GameDebug.Log($"[Status damage] #{i.ToString()} took damage from burn");
						if (i.hasWorkingAbility(Abilities.HEATPROOF)) {
							GameDebug.Log($"[Ability triggered] #{i.ToString()}'s Heatproof");
							yield return i.pbReduceHP((int)Math.Floor(i.TotalHP/16f));
						}
						else {
							yield return i.pbReduceHP((int)Math.Floor(i.TotalHP/8f));
						}
					}
					if (i is IBattlerEffectIE b) yield return b.pbContinueStatus();
				}
				// Nightmare
				if (i.effects.Nightmare) {
					if (i.Status==Status.SLEEP) {
						if (!i.hasWorkingAbility(Abilities.MAGIC_GUARD)) {
							GameDebug.Log($"[Lingering effect triggered] #{i.ToString()}'s nightmare");
							yield return i.pbReduceHP((int)Math.Floor(i.TotalHP/4f),true);
							yield return pbDisplay(Game._INTL("{1} is locked in a nightmare!",i.ToString()));
						}
					}
					else {
						i.effects.Nightmare=false;
					}
				}
				if (i.isFainted()) {
					bool faint = false;
					yield return i.pbFaint(result:value=>faint=value);
					if (!faint) yield break;
					continue;
				}
			}
			// Curse
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				if (i.effects.Curse && !i.hasWorkingAbility(Abilities.MAGIC_GUARD)) {
					GameDebug.Log($"[Lingering effect triggered] #{i.ToString()}'s curse");
					yield return i.pbReduceHP((int)Math.Floor(i.TotalHP/4f),true);
					yield return pbDisplay(Game._INTL("{1} is afflicted by the curse!",i.ToString()));
				}
				if (i.isFainted()) {
					bool faint = false;
					yield return i.pbFaint(result: value => faint = value);
					if (!faint) yield break;
					continue;
				}
			}
			// Multi-turn attacks (Bind/Clamp/Fire Spin/Magma Storm/Sand Tomb/Whirlpool/Wrap)
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				if (i.effects.MultiTurn>0) {
					i.effects.MultiTurn-=1;
					string movename=Game._INTL(i.effects.MultiTurnAttack.ToString(TextScripts.Name));
					if (i.effects.MultiTurn==0) {
						GameDebug.Log($"[End of effect] Trapping move #{movename} affecting #{i.ToString()} ended");
						yield return pbDisplay(Game._INTL("{1} was freed from {2}!",i.ToString(),movename));
					}
					else {
						if (i.effects.MultiTurnAttack == Moves.BIND) {
							yield return pbCommonAnimation("Bind",i,null);
						}
						else if (i.effects.MultiTurnAttack == Moves.CLAMP) {
							yield return pbCommonAnimation("Clamp",i,null);
						}
						else if (i.effects.MultiTurnAttack == Moves.FIRE_SPIN) {
							yield return pbCommonAnimation("FireSpin",i,null);
						}
						else if (i.effects.MultiTurnAttack == Moves.MAGMA_STORM) {
							yield return pbCommonAnimation("MagmaStorm",i,null);
						}
						else if (i.effects.MultiTurnAttack == Moves.SAND_TOMB) {
							yield return pbCommonAnimation("SandTomb",i,null);
						}
						else if (i.effects.MultiTurnAttack == Moves.WRAP) {
							yield return pbCommonAnimation("Wrap",i,null);
						}
						else if (i.effects.MultiTurnAttack == Moves.INFESTATION) {
							yield return pbCommonAnimation("Infestation",i,null);
						}
						else {
							yield return pbCommonAnimation("Wrap",i,null);
						}
						if (!i.hasWorkingAbility(Abilities.MAGIC_GUARD)) {
							GameDebug.Log($"[Lingering effect triggered] #{i.ToString()} took damage from trapping move #{movename}");
							yield return (@scene as IPokeBattle_SceneIE).pbDamageAnimation(i,0);
							int amt=Core.USENEWBATTLEMECHANICS ? (int)Math.Floor(i.TotalHP/8f) : (int)Math.Floor(i.TotalHP/16f);
							if (@battlers[i.effects.MultiTurnUser].hasWorkingItem(Items.BINDING_BAND)) {
								amt=Core.USENEWBATTLEMECHANICS ? (int)Math.Floor(i.TotalHP/6f) : (int)Math.Floor(i.TotalHP/8f);
							}
							yield return i.pbReduceHP(amt);
							yield return pbDisplay(Game._INTL("{1} is hurt by {2}!",i.ToString(),movename));
						}
					}
				}
				if (i.isFainted()) {
					bool faint = false;
					yield return i.pbFaint(result: value => faint = value);
					if (!faint) yield break;
				}
			}
			// Taunt
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				if (i.effects.Taunt>0) {
					i.effects.Taunt-=1;
					if (i.effects.Taunt==0) {
						yield return pbDisplay(Game._INTL("{1}'s taunt wore off!",i.ToString()));
						GameDebug.Log($"[End of effect] #{i.ToString()} is no longer taunted");
					}
				}
			}
			// Encore
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				if (i.effects.Encore>0) {
					if (i.moves[i.effects.EncoreIndex].id!=i.effects.EncoreMove) {
						i.effects.Encore=0;
						i.effects.EncoreIndex=0;
						i.effects.EncoreMove=0;
						GameDebug.Log($"[End of effect] #{i.ToString()} is no longer encored (encored move was lost)");
					}
					else {
						i.effects.Encore-=1;
						if (i.effects.Encore==0 || i.moves[i.effects.EncoreIndex].PP==0) {
							i.effects.Encore=0;
							yield return pbDisplay(Game._INTL("{1}'s encore ended!",i.ToString()));
							GameDebug.Log($"[End of effect] #{i.ToString()} is no longer encored");
						}
					}
				}
			}
			// Disable/Cursed Body
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				if (i.effects.Disable>0) {
					i.effects.Disable-=1;
					if (i.effects.Disable==0) {
						i.effects.DisableMove=0;
						yield return pbDisplay(Game._INTL("{1} is no longer disabled!",i.ToString()));
						GameDebug.Log($"[End of effect] #{i.ToString()} is no longer disabled");
					}
				}
			}
			// Magnet Rise
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				if (i.effects.MagnetRise>0) {
					i.effects.MagnetRise-=1;
					if (i.effects.MagnetRise==0) {
						yield return pbDisplay(Game._INTL("{1} stopped levitating.",i.ToString()));
						GameDebug.Log($"[End of effect] #{i.ToString()} is no longer levitating by Magnet Rise");
					}
				}
			}
			// Telekinesis
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				if (i.effects.Telekinesis>0) {
					i.effects.Telekinesis-=1;
					if (i.effects.Telekinesis==0) {
						yield return pbDisplay(Game._INTL("{1} stopped levitating.",i.ToString()));
						GameDebug.Log($"[End of effect] #{i.ToString()} is no longer levitating by Telekinesis");
					}
				}
			}
			// Heal Block
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				if (i.effects.HealBlock>0) {
					i.effects.HealBlock-=1;
					if (i.effects.HealBlock==0) {
						yield return pbDisplay(Game._INTL("{1}'s Heal Block wore off!",i.ToString()));
						GameDebug.Log($"[End of effect] #{i.ToString()} is no longer Heal Blocked");
					}
				}
			}
			// Embargo
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				if (i.effects.Embargo>0) {
					i.effects.Embargo-=1;
					if (i.effects.Embargo==0) {
						yield return pbDisplay(Game._INTL("{1} can use items again!",i.ToString(true)));
						GameDebug.Log($"[End of effect] #{i.ToString()} is no longer affected by an embargo");
					}
				}
			}
			// Yawn
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				if (i.effects.Yawn>0) {
					i.effects.Yawn-=1;
					if (i.effects.Yawn==0 && i is IBattlerClause b0 && b0.pbCanSleepYawn()) {
						GameDebug.Log($"[Lingering effect triggered] #{i.ToString()}'s Yawn");
						if (i is IBattlerEffectIE b1) yield return b1.pbSleep();
					}
				}
			}
			// Perish Song
			List<int> perishSongUsers=new List<int>();
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				if (i.effects.PerishSong>0) {
					i.effects.PerishSong-=1;
					yield return pbDisplay(Game._INTL("{1}'s perish count fell to {2}!",i.ToString(),i.effects.PerishSong.ToString()));
					GameDebug.Log($"[Lingering effect triggered] #{i.ToString()}'s Perish Song count dropped to #{i.effects.PerishSong}");
					if (i.effects.PerishSong==0) {
						perishSongUsers.Add(i.effects.PerishSongUser);
						yield return i.pbReduceHP(i.HP,true);
					}
				}
				if (i.isFainted()) {
					bool faint = false;
					yield return i.pbFaint(result: value => faint = value);
					if (!faint) yield break;
				}
			}
			if (perishSongUsers.Count>0) {
				// If all remaining Pokemon fainted by a Perish Song triggered by a single side
				if ((perishSongUsers.Count(item => pbIsOpposing(item))==perishSongUsers.Count) ||
					(perishSongUsers.Count(item => !pbIsOpposing(item))==perishSongUsers.Count)) {
					pbJudgeCheckpoint(@battlers[(int)perishSongUsers[0]]);
				}
			}
			if (@decision>0) {
				yield return pbGainEXP();
				yield break;
			}
			// Reflect
			for (int i = 0; i < sides.Length; i++) {
				if (sides[i].Reflect>0) {
					sides[i].Reflect-=1;
					if (sides[i].Reflect==0) {
						if (i==0) yield return pbDisplay(Game._INTL("Your team's Reflect faded!"));
						if (i==1) yield return pbDisplay(Game._INTL("The opposing team's Reflect faded!"));
						if (i==0) GameDebug.Log($"[End of effect] Reflect ended on the player's side");
						if (i==1) GameDebug.Log($"[End of effect] Reflect ended on the opponent's side");
					}
				}
			}
			// Light Screen
			for (int i = 0; i < sides.Length; i++) {
				if (sides[i].LightScreen>0) {
					sides[i].LightScreen-=1;
					if (sides[i].LightScreen==0) {
						if (i==0) yield return pbDisplay(Game._INTL("Your team's Light Screen faded!"));
						if (i==1) yield return pbDisplay(Game._INTL("The opposing team's Light Screen faded!"));
						if (i==0) GameDebug.Log($"[End of effect] Light Screen ended on the player's side");
						if (i==1) GameDebug.Log($"[End of effect] Light Screen ended on the opponent's side");
					}
				}
			}
			// Safeguard
			for (int i = 0; i < sides.Length; i++) {
				if (sides[i].Safeguard>0) {
					sides[i].Safeguard-=1;
					if (sides[i].Safeguard==0) {
						if (i==0) yield return pbDisplay(Game._INTL("Your team is no longer protected by Safeguard!"));
						if (i==1) yield return pbDisplay(Game._INTL("The opposing team is no longer protected by Safeguard!"));
						if (i==0) GameDebug.Log($"[End of effect] Safeguard ended on the player's side");
						if (i==1) GameDebug.Log($"[End of effect] Safeguard ended on the opponent's side");
					}
				}
			}
			// Mist
			for (int i = 0; i < sides.Length; i++) {
				if (sides[i].Mist>0) {
					sides[i].Mist-=1;
					if (sides[i].Mist==0) {
						if (i==0) yield return pbDisplay(Game._INTL("Your team's Mist faded!"));
						if (i==1) yield return pbDisplay(Game._INTL("The opposing team's Mist faded!"));
						if (i==0) GameDebug.Log($"[End of effect] Mist ended on the player's side");
						if (i==1) GameDebug.Log($"[End of effect] Mist ended on the opponent's side");
					}
				}
			}
			// Tailwind
			for (int i = 0; i < sides.Length; i++) {
				if (sides[i].Tailwind>0) {
					sides[i].Tailwind-=1;
					if (sides[i].Tailwind==0) {
						if (i==0) yield return pbDisplay(Game._INTL("Your team's Tailwind petered out!"));
						if (i==1) yield return pbDisplay(Game._INTL("The opposing team's Tailwind petered out!"));
						if (i==0) GameDebug.Log($"[End of effect] Tailwind ended on the player's side");
						if (i==1) GameDebug.Log($"[End of effect] Tailwind ended on the opponent's side");
					}
				}
			}
			// Lucky Chant
			for (int i = 0; i < sides.Length; i++) {
				if (sides[i].LuckyChant>0) {
					sides[i].LuckyChant-=1;
					if (sides[i].LuckyChant==0) {
						if (i==0) yield return pbDisplay(Game._INTL("Your team's Lucky Chant faded!"));
						if (i==1) yield return pbDisplay(Game._INTL("The opposing team's Lucky Chant faded!"));
						if (i==0) GameDebug.Log($"[End of effect] Lucky Chant ended on the player's side");
						if (i==1) GameDebug.Log($"[End of effect] Lucky Chant ended on the opponent's side");
					}
				}
			}
			// End of Pledge move combinations
			for (int i = 0; i < sides.Length; i++) {
				if (sides[i].Swamp>0) {
					sides[i].Swamp-=1;
					if (sides[i].Swamp==0) {
						if (i==0) yield return pbDisplay(Game._INTL("The swamp around your team disappeared!"));
						if (i==1) yield return pbDisplay(Game._INTL("The swamp around the opposing team disappeared!"));
						if (i==0) GameDebug.Log($"[End of effect] Grass Pledge's swamp ended on the player's side");
						if (i==1) GameDebug.Log($"[End of effect] Grass Pledge's swamp ended on the opponent's side");
					}
				}
				if (sides[i].SeaOfFire>0) {
					sides[i].SeaOfFire-=1;
					if (sides[i].SeaOfFire==0) {
						if (i==0) yield return pbDisplay(Game._INTL("The sea of fire around your team disappeared!"));
						if (i==1) yield return pbDisplay(Game._INTL("The sea of fire around the opposing team disappeared!"));
						if (i==0) GameDebug.Log($"[End of effect] Fire Pledge's sea of fire ended on the player's side");
						if (i==1) GameDebug.Log($"[End of effect] Fire Pledge's sea of fire ended on the opponent's side");
					}
				}
				if (sides[i].Rainbow>0) {
					sides[i].Rainbow-=1;
					if (sides[i].Rainbow==0) {
						if (i==0) yield return pbDisplay(Game._INTL("The rainbow around your team disappeared!"));
						if (i==1) yield return pbDisplay(Game._INTL("The rainbow around the opposing team disappeared!"));
						if (i==0) GameDebug.Log($"[End of effect] Water Pledge's rainbow ended on the player's side");
						if (i==1) GameDebug.Log($"[End of effect] Water Pledge's rainbow ended on the opponent's side");
					}
				}
			}
			// Gravity
			if (@field.Gravity>0) {
				@field.Gravity-=1;
				if (@field.Gravity==0) {
					yield return pbDisplay(Game._INTL("Gravity returned to normal."));
					GameDebug.Log($"[End of effect] Strong gravity ended");
				}
			}
			// Trick Room
			if (@field.TrickRoom>0) {
				@field.TrickRoom-=1;
				if (@field.TrickRoom==0) {
					yield return pbDisplay(Game._INTL("The twisted dimensions returned to normal."));
					GameDebug.Log($"[End of effect] Trick Room ended");
				}
			}
			// Wonder Room
			if (@field.WonderRoom>0) {
				@field.WonderRoom-=1;
				if (@field.WonderRoom==0) {
					yield return pbDisplay(Game._INTL("Wonder Room wore off, and the Defense and Sp. public void stats returned to normal!"));
					GameDebug.Log($"[End of effect] Wonder Room ended");
				}
			}
			// Magic Room
			if (@field.MagicRoom>0) {
				@field.MagicRoom-=1;
				if (@field.MagicRoom==0) {
					yield return pbDisplay(Game._INTL("The area returned to normal."));
					GameDebug.Log($"[End of effect] Magic Room ended");
				}
			}
			// Mud Sport
			if (@field.MudSportField>0) {
				@field.MudSportField-=1;
				if (@field.MudSportField==0) {
					yield return pbDisplay(Game._INTL("The effects of Mud Sport have faded."));
					GameDebug.Log($"[End of effect] Mud Sport ended");
				}
			}
			// Water Sport
			if (@field.WaterSportField>0) {
				@field.WaterSportField-=1;
				if (@field.WaterSportField==0) {
					yield return pbDisplay(Game._INTL("The effects of Water Sport have faded."));
					GameDebug.Log($"[End of effect] Water Sport ended");
				}
			}
			// Electric Terrain
			if (@field.ElectricTerrain>0) {
				@field.ElectricTerrain-=1;
				if (@field.ElectricTerrain==0) {
					yield return pbDisplay(Game._INTL("The electric current disappeared from the battlefield."));
					GameDebug.Log($"[End of effect] Electric Terrain ended");
				}
			}
			// Grassy Terrain (counting down)
			if (@field.GrassyTerrain>0) {
				@field.GrassyTerrain-=1;
				if (@field.GrassyTerrain==0) {
					yield return pbDisplay(Game._INTL("The grass disappeared from the battlefield."));
					GameDebug.Log($"[End of effect] Grassy Terrain ended");
				}
			}
			// Misty Terrain
			if (@field.MistyTerrain>0) {
				@field.MistyTerrain-=1;
				if (@field.MistyTerrain==0) {
					yield return pbDisplay(Game._INTL("The mist disappeared from the battlefield."));
					GameDebug.Log($"[End of effect] Misty Terrain ended");
				}
			}
			// Uproar
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				if (i.effects.Uproar>0) {
					foreach (var j in priority) {
						if (!j.isFainted() && j.Status==Status.SLEEP && !j.hasWorkingAbility(Abilities.SOUNDPROOF)) {
							GameDebug.Log($"[Lingering effect triggered] Uproar woke up #{j.ToString(true)}");
							if (j is IBattlerEffectIE b) yield return b.pbCureStatus(false);
							yield return pbDisplay(Game._INTL("{1} woke up in the uproar!",j.ToString()));
						}
					}
					i.effects.Uproar-=1;
					if (i.effects.Uproar==0) {
						yield return pbDisplay(Game._INTL("{1} calmed down.",i.ToString()));
						GameDebug.Log($"[End of effect] #{i.ToString()} is no longer uproaring");
					}
					else {
						yield return pbDisplay(Game._INTL("{1} is making an uproar!",i.ToString()));
					}
				}
			}
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				// Speed Boost
				// A Pokémon's turncount is 0 if it became active after the beginning of a round
				if (i.turncount>0 && i.hasWorkingAbility(Abilities.SPEED_BOOST) && i is IBattlerEffectIE b) {
					bool increaseStatWithCause = false;
					yield return b.pbIncreaseStatWithCause(PokemonUnity.Combat.Stats.SPEED,1,i,Game._INTL(i.Ability.ToString(TextScripts.Name)),result:value=>increaseStatWithCause=value);
					if (increaseStatWithCause) {
						GameDebug.Log($"[Ability triggered] #{i.ToString()}'s #{Game._INTL(i.Ability.ToString(TextScripts.Name))}");
					}
				}
				// Bad Dreams
				if (i.Status==Status.SLEEP && !i.hasWorkingAbility(Abilities.MAGIC_GUARD)) {
					if (i.pbOpposing1.hasWorkingAbility(Abilities.BAD_DREAMS) ||
						i.pbOpposing2.hasWorkingAbility(Abilities.BAD_DREAMS)) {
						GameDebug.Log($"[Ability triggered] #{i.ToString()}'s opponent's Bad Dreams");
						int hploss=0;
						yield return i.pbReduceHP((int)Math.Floor(i.TotalHP/8f),true,result:value=>hploss=value);
						if (hploss>0) yield return pbDisplay(Game._INTL("{1} is having a bad dream!",i.ToString()));
					}
				}
				if (i.isFainted()) {
					bool faint = false;
					yield return i.pbFaint(result: value => faint = value);
					if (!faint) yield break;
					continue;
				}
				// Pickup
				if (i.hasWorkingAbility(Abilities.PICKUP) && i.Item<=0) {
					Items item=0; int index=-1; int use=0;
					for (int j = 0; j < battlers.Length; j++) {
						if (j==i.Index) continue;
						if (@battlers[j].effects.PickupUse>use) {
							item=@battlers[j].effects.PickupItem;
							index=j;
							use=@battlers[j].effects.PickupUse;
						}
					}
					if (item>0) {
						i.Item=item;
						(_battlers[index] as IBattlerIE).effects.PickupItem=0;
						(_battlers[index] as IBattlerIE).effects.PickupUse=0;
						if (@battlers[index].pokemon.itemRecycle==item) (_battlers[index] as IBattlerIE).pokemon.itemRecycle=0;
						if (@opponent.Length == 0 && // In a wild battle
							i.pokemon.itemInitial==0 &&
							@battlers[index].pokemon.itemInitial==item) {
							i.pokemon.itemInitial=item;
							(_battlers[index] as IBattlerIE).pokemon.itemInitial=0;
						}
						yield return pbDisplay(Game._INTL("{1} found one {2}!",i.ToString(),Game._INTL(item.ToString(TextScripts.Name))));
						yield return i.pbBerryCureCheck(true);
					}
				}
				// Harvest
				if (i.hasWorkingAbility(Abilities.HARVEST) && i.Item<=0 && i.pokemon.itemRecycle>0) {
					if (Kernal.ItemData[i.pokemon.itemRecycle].IsBerry && //pbIsBerry(i.itemRecycle)
						(pbWeather==Weather.SUNNYDAY ||
						pbWeather==Weather.HARSHSUN || pbRandom(10)<5)) {
						i.Item=i.pokemon.itemRecycle;
						i.pokemon.itemRecycle=0;
						if (i.pokemon.itemInitial==0) i.pokemon.itemInitial=i.Item;
						yield return pbDisplay(Game._INTL("{1} harvested one {2}!",i.ToString(),Game._INTL(i.Item.ToString(TextScripts.Name))));
						yield return i.pbBerryCureCheck(true);
					}
				}
				// Moody
				if (i.hasWorkingAbility(Abilities.MOODY)) {
					List<PokemonUnity.Combat.Stats> randomup=new List<PokemonUnity.Combat.Stats>(); List<PokemonUnity.Combat.Stats> randomdown=new List<PokemonUnity.Combat.Stats>();
					foreach (var j in new PokemonUnity.Combat.Stats[] { PokemonUnity.Combat.Stats.ATTACK,PokemonUnity.Combat.Stats.DEFENSE,PokemonUnity.Combat.Stats.SPEED,PokemonUnity.Combat.Stats.SPATK,
								PokemonUnity.Combat.Stats.SPDEF,PokemonUnity.Combat.Stats.ACCURACY,PokemonUnity.Combat.Stats.EVASION }) {
						if (i is IBattlerEffectIE b0) {
							bool canIncreaseStatStage = false;
							yield return b0.pbCanIncreaseStatStage(j,i,result:value=>canIncreaseStatStage=value);
							if (canIncreaseStatStage)
								randomup.Add(j);
						}
						if (i is IBattlerEffectIE b1) {
							bool canReduceStatStage = false;
							yield return b1.pbCanReduceStatStage(j,i,result:value=>canReduceStatStage=value);
							if (canReduceStatStage)
								randomdown.Add(j);
						}
					}
					if (randomup.Count>0) {
						GameDebug.Log($"[Ability triggered] #{i.ToString()}'s Moody (raise stat)");
						int r=pbRandom(randomup.Count);
						if (i is IBattlerEffectIE b2) yield return b2.pbIncreaseStatWithCause(randomup[r],2,i,Game._INTL(i.Ability.ToString(TextScripts.Name)));
						for (int j = 0; j < randomdown.Count; j++) {
							if (randomdown[j]==randomup[r]) {
								//randomdown[j]=null; randomdown.compact!;
								randomdown.RemoveAt(j);
								break;
							}
						}
					}
					if (randomdown.Count>0) {
						GameDebug.Log($"[Ability triggered] #{i.ToString()}'s Moody (lower stat)");
						int r=pbRandom(randomdown.Count);
						if (i is IBattlerEffectIE b3) yield return b3.pbReduceStatWithCause(randomdown[r],1,i,Game._INTL(i.Ability.ToString(TextScripts.Name)));
					}
				}
			}
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				// Toxic Orb
				if (i.hasWorkingItem(Items.TOXIC_ORB) && i.Status==0 && i is IBattlerEffectIE b0) {
					bool canPoison = false; 
					yield return b0.pbCanPoison(null,false,result:value=>canPoison=value);
					if (canPoison) {
						GameDebug.Log($"[Item triggered] #{i.ToString()}'s Toxic Orb");
						yield return b0.pbPoison(null,Game._INTL("{1} was badly poisoned by its {2}!",i.ToString(),
							Game._INTL(i.Item.ToString(TextScripts.Name))),true);
					}
				}
				// Flame Orb
				if (i.hasWorkingItem(Items.FLAME_ORB) && i.Status==0 && i is IBattlerEffectIE b1) {
					bool canBurn = false; 
					yield return b1.pbCanBurn(null,false,result:value=>canBurn=value);
					if (canBurn) {
						GameDebug.Log($"[Item triggered] #{i.ToString()}'s Flame Orb");
						yield return b1.pbBurn(null,Game._INTL("{1} was burned by its {2}!",i.ToString(),Game._INTL(i.Item.ToString(TextScripts.Name))));
					}
				}
				// Sticky Barb
				if (i.hasWorkingItem(Items.STICKY_BARB) && !i.hasWorkingAbility(Abilities.MAGIC_GUARD)) {
					GameDebug.Log($"[Item triggered] #{i.ToString()}'s Sticky Barb");
					yield return (@scene as IPokeBattle_SceneIE).pbDamageAnimation(i,0);
					yield return i.pbReduceHP((int)Math.Floor(i.TotalHP/8f));
					yield return pbDisplay(Game._INTL("{1} is hurt by its {2}!",i.ToString(),Game._INTL(i.Item.ToString(TextScripts.Name))));
				}
				if (i.isFainted()) {
					bool faint = false;
					yield return i.pbFaint(result:value=>faint=value);
					if (!faint) yield break;
				}
			}
			// Form checks
			for (int i = 0; i < battlers.Length; i++) {
				if (@battlers[i].isFainted()) continue;
				yield return (_battlers[i] as IBattlerIE).pbCheckForm();
			}
			yield return pbGainEXP();
			yield return pbSwitch();
			if (@decision>0) yield break;
			foreach (var i in priority) {
				if (i.isFainted()) continue;
				yield return i.pbAbilitiesOnSwitchIn(false);
			}
			// Healing Wish/Lunar Dance - should go here
			// Spikes/Toxic Spikes/Stealth Rock - should go here (in order of their 1st use)
			for (int i = 0; i < battlers.Length; i++) {
				if (@battlers[i].turncount>0 && @battlers[i].hasWorkingAbility(Abilities.TRUANT)) {
					(_battlers[i] as IBattlerIE).effects.Truant=!@battlers[i].effects.Truant;
				}
				if (@battlers[i].effects.LockOn>0) {   // Also Mind Reader
					(_battlers[i] as IBattlerIE).effects.LockOn-=1;
					if (@battlers[i].effects.LockOn==0) (_battlers[i] as IBattlerIE).effects.LockOnPos=-1;
				}
				(_battlers[i] as IBattlerIE).effects.Flinch=false;
				(_battlers[i] as IBattlerIE).effects.FollowMe=0;
				(_battlers[i] as IBattlerIE).effects.HelpingHand=false;
				(_battlers[i] as IBattlerIE).effects.MagicCoat=false;
				(_battlers[i] as IBattlerIE).effects.Snatch=false;
				if (@battlers[i].effects.Charge>0) (_battlers[i] as IBattlerIE).effects.Charge-=1;
				(_battlers[i] as IBattlerIE).lastHPLost=0;
				(_battlers[i] as IBattlerIE).tookDamage=false;
				(_battlers[i] as IBattlerIE).lastAttacker.Clear();
				(_battlers[i] as IBattlerIE).effects.Counter=-1;
				(_battlers[i] as IBattlerIE).effects.CounterTarget=-1;
				(_battlers[i] as IBattlerIE).effects.MirrorCoat=-1;
				(_battlers[i] as IBattlerIE).effects.MirrorCoatTarget=-1;
			}
			for (int i = 0; i < sides.Length; i++) {
				if (!@sides[i].EchoedVoiceUsed) {
					@sides[i].EchoedVoiceCounter=0;
				}
				@sides[i].EchoedVoiceUsed=false;
				@sides[i].QuickGuard=false;
				@sides[i].WideGuard=false;
				@sides[i].CraftyShield=false;
				@sides[i].Round=0;
			}
			@field.FusionBolt=false;
			@field.FusionFlare=false;
			@field.IonDeluge=false;
			if (@field.FairyLock>0) @field.FairyLock-=1;
			// invalidate stored priority
			@usepriority=false;
			(this as IBattleClause).pbEndOfRoundPhase();
		}
		#endregion

		#region End of battle.
		public IEnumerator pbEndOfBattle(bool canlose=false, System.Action<BattleResults> result=null) {
			//switch (@decision) {
				//#### WIN ####//
				if (@decision == BattleResults.WON) { //case BattleResults.WON:
					GameDebug.Log($"");
					GameDebug.Log($"***Player won***");
					if (@opponent.Length > 0) {
						yield return (@scene as IPokeBattle_SceneIE).pbTrainerBattleSuccess();
						if (@opponent.Length > 0) {
							yield return pbDisplayPaused(Game._INTL("{1} defeated {2} and {3}!",this.pbPlayer().name,@opponent[0].name,@opponent[1].name));
						}
						else {
							yield return pbDisplayPaused(Game._INTL("{1} defeated\r\n{2}!",this.pbPlayer().name,@opponent[0].name));
						}
						yield return (@scene as IPokeBattle_SceneIE).pbShowOpponent(0);
						yield return pbDisplayPaused(@endspeech.Replace("/\\[Pp][Nn]/",this.pbPlayer().name));
						if (@opponent.Length > 0) {
							yield return (@scene as IPokeBattle_SceneIE).pbHideOpponent();
							yield return (@scene as IPokeBattle_SceneIE).pbShowOpponent(1);
							yield return pbDisplayPaused(@endspeech2.Replace("/\\[Pp][Nn]/",this.pbPlayer().name));
						}
						// Calculate money gained for winning
						if (@internalbattle) {
							int tmoney=0;
							if (@opponent.Length > 1) {   // Double battles
								int maxlevel1=0; int maxlevel2=0; int limit=pbSecondPartyBegin(1);
								for (int i = 0; i < limit; i++) {
									if (@party2[i].IsNotNullOrNone()) {
										if (maxlevel1<@party2[i].Level) maxlevel1=@party2[i].Level;
									}
									if (@party2[i+limit].IsNotNullOrNone()) {
										if (maxlevel1<@party2[i+limit].Level) maxlevel2=@party2[i+limit].Level;
									}
								}
								tmoney+=maxlevel1*@opponent[0].Money;
								tmoney+=maxlevel2*@opponent[1].Money;
							}
							else {
								int maxlevel=0;
								foreach (var i in @party2) {
									if (!i.IsNotNullOrNone()) continue;
									if (maxlevel<i.Level) maxlevel=i.Level;
								}
								tmoney+=maxlevel*@opponent[0].Money;
							}
							// If Amulet Coin/Luck Incense's effect applies, double money earned
							if (@amuletcoin) tmoney*=2;
							// If Happy Hour's effect applies, double money earned
							if (@doublemoney) tmoney*=2;
							int oldmoney=this.pbPlayer().Money;
							this.pbPlayer().Money+=tmoney;
							int moneygained=this.pbPlayer().Money-oldmoney;
							if (moneygained>0) {
								yield return pbDisplayPaused(Game._INTL("{1} got ${2}\r\nfor winning!",this.pbPlayer().name,tmoney.ToString()));
							}
						}
					}
					if (@internalbattle && @extramoney>0) {
						if (@amuletcoin) @extramoney*=2;
						if (@doublemoney) @extramoney*=2;
						int oldmoney=this.pbPlayer().Money;
						this.pbPlayer().Money+=@extramoney;
						int moneygained=this.pbPlayer().Money-oldmoney;
						if (moneygained>0) {
							yield return pbDisplayPaused(Game._INTL("{1} picked up ${2}!",this.pbPlayer().name,@extramoney.ToString()));
						}
					}
					foreach (var p in @snaggedpokemon) {
						IPokemon pkmn = this.party2[p];
						yield return pbStorePokemon(pkmn);
						//if (this.pbPlayer().shadowcaught == null) this.pbPlayer().shadowcaught=new Dictionary<Pokemons,bool>();
						if (this.pbPlayer().shadowcaught == null) this.pbPlayer().shadowcaught=new List<Pokemons>();
						//this.pbPlayer().shadowcaught[pkmn.Species]=true;
						if (!this.pbPlayer().shadowcaught.Contains(pkmn.Species)) this.pbPlayer().shadowcaught.Add(pkmn.Species);
					}
					@snaggedpokemon.Clear();
					//break;
				}
				//#### LOSE, DRAW ####//
				else if (@decision == BattleResults.LOST || @decision == BattleResults.DRAW) { //case BattleResults.LOST: case BattleResults.DRAW:
					GameDebug.Log($"");
					if (@decision==BattleResults.LOST) GameDebug.Log($"***Player lost***");
					if (@decision==BattleResults.DRAW) GameDebug.Log($"***Player drew with opponent***");
					if (@internalbattle) {
						yield return pbDisplayPaused(Game._INTL("{1} is out of usable Pokémon!",this.pbPlayer().name));
						int moneylost=pbMaxLevelFromIndex(0);   // Player's Pokémon only, not partner's
						int[] multiplier=new int[] { 8, 16, 24, 36, 48, 60, 80, 100, 120 };
						moneylost*=multiplier[(int)Math.Min(multiplier.Length-1,this.pbPlayer().badges.Length)];
						if (moneylost>this.pbPlayer().Money) moneylost=this.pbPlayer().Money;
						if (Core.NO_MONEY_LOSS) moneylost=0;
						int oldmoney=this.pbPlayer().Money;
						this.pbPlayer().Money-=moneylost;
						int lostmoney=oldmoney-this.pbPlayer().Money;
						if (@opponent.Length > 0) {
							if (@opponent.Length > 0) {
								yield return pbDisplayPaused(Game._INTL("{1} lost against {2} and {3}!",this.pbPlayer().name,@opponent[0].name,@opponent[1].name));
							}
							else {
								yield return pbDisplayPaused(Game._INTL("{1} lost against\r\n{2}!",this.pbPlayer().name,@opponent[0].name));
							}
							if (moneylost>0) {
								yield return pbDisplayPaused(Game._INTL("{1} paid ${2}\r\nas the prize money...",this.pbPlayer().name,lostmoney.ToString()));
								if (!canlose) yield return pbDisplayPaused(Game._INTL("..."));
							}
						}
						else {
							if (moneylost>0) {
								yield return pbDisplayPaused(Game._INTL("{1} panicked and lost\r\n${2}...",this.pbPlayer().name,lostmoney.ToString()));
								if (!canlose) yield return pbDisplayPaused(Game._INTL("..."));
							}
						}
						if (!canlose) yield return pbDisplayPaused(Game._INTL("{1} blacked out!",this.pbPlayer().name));
					}
					else if (@decision==BattleResults.LOST) {
						yield return (@scene as IPokeBattle_SceneIE).pbShowOpponent(0);
						yield return pbDisplayPaused(@endspeechwin.Replace("/\\[Pp][Nn]/",this.pbPlayer().name));
						if (@opponent.Length > 0) {
							yield return (@scene as IPokeBattle_SceneIE).pbHideOpponent();
							yield return (@scene as IPokeBattle_SceneIE).pbShowOpponent(1);
							yield return pbDisplayPaused(@endspeechwin2.Replace("/\\[Pp][Nn]/",this.pbPlayer().name));
						}
					}
					//break;
			}
			// Pass on Pokérus within the party
			List<int> infected=new List<int>();
			for (int i = 0; i < Game.GameData.Trainer.party.Length; i++) {
				if (Game.GameData.Trainer.party[i].PokerusStage.HasValue && Game.GameData.Trainer.party[i].PokerusStage.Value) { //Game.GameData.Trainer.party[i].PokerusStage==1
					infected.Add(i);
				}
			}
			if (infected.Count>=1) {
				foreach (var i in infected) {
					int strain=(Game.GameData.Trainer.party[i] as Monster.Pokemon).PokerusStrain;//.pokerus/16;
					if (i>0 && !Game.GameData.Trainer.party[i-1].PokerusStage.HasValue) { //Game.GameData.Trainer.party[i-1].PokerusStage==0
						if (pbRandom(3)==0) Game.GameData.Trainer.party[i-1].GivePokerus(strain);
					}
					if (i<Game.GameData.Trainer.party.Length-1 && !Game.GameData.Trainer.party[i+1].PokerusStage.HasValue) { //Game.GameData.Trainer.party[i-1].PokerusStage==0
						if (pbRandom(3)==0) Game.GameData.Trainer.party[i+1].GivePokerus(strain);
					}
				}
			}
			yield return (@scene as IPokeBattle_SceneIE).pbEndBattle(@decision);
			foreach (IBattlerIE i in @battlers) {
				i.pbResetForm();
				if (i.hasWorkingAbility(Abilities.NATURAL_CURE)) {
					i.Status=0;
				}
			}
			foreach (IPokemon i in Game.GameData.Trainer.party) {
				i.setItem(i.itemInitial);
				i.itemInitial=i.itemRecycle=0;
				i.belch=false;
			}
			result?.Invoke(@decision);
		}
		#endregion

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("scene", (object)this.scene, typeof(IScene));
			info.AddValue("decision", (object)this.decision, typeof(BattleResults));
			info.AddValue("internalbattle", (object)this.internalbattle, typeof(bool));
			info.AddValue("doublebattle", (object)this.doublebattle, typeof(bool));
			info.AddValue("cantescape", (object)this.cantescape, typeof(bool));
			info.AddValue("canLose", (object)this.canLose, typeof(bool));
			info.AddValue("shiftStyle", (object)this.shiftStyle, typeof(bool));
			info.AddValue("battlescene", (object)this.battlescene, typeof(bool));
			info.AddValue("debug", (object)this.debug, typeof(bool));
			info.AddValue("debugupdate", (object)this.debugupdate, typeof(int));
			info.AddValue("player", (object)this.player, typeof(ITrainer[]));
			info.AddValue("opponent", (object)this.opponent, typeof(ITrainer[]));
			info.AddValue("party1", (object)this.party1, typeof(IPokemon[]));
			info.AddValue("party2", (object)this.party2, typeof(IPokemon[]));
			info.AddValue("party1order", (object)this.party1order, typeof(IList<int>));
			info.AddValue("party2order", (object)this.party2order, typeof(IList<int>));
			info.AddValue("fullparty1", (object)this.fullparty1, typeof(bool));
			info.AddValue("fullparty2", (object)this.fullparty2, typeof(bool));
			info.AddValue("battlers", (object)this.battlers, typeof(IBattlerIE[]));
			info.AddValue("items", (object)this.items, typeof(Items[][]));
			info.AddValue("sides", (object)this.sides, typeof(IEffectsSide[]));
			info.AddValue("field", (object)this.field, typeof(IEffectsField));
			info.AddValue("environment", (object)this.environment, typeof(Environments));
			info.AddValue("weather", (object)this.weather, typeof(Weather));
			info.AddValue("weatherduration", (object)this.weatherduration, typeof(int));
			info.AddValue("switching", (object)this.switching, typeof(bool));
			info.AddValue("futuresight", (object)this.futuresight, typeof(bool));
			info.AddValue("struggle", (object)this.struggle, typeof(IBattleMove));
			info.AddValue("choices", (object)this.choices, typeof(IBattleChoice[]));
			info.AddValue("successStates", (object)this.successStates, typeof(ISuccessState[]));
			info.AddValue("lastMoveUsed", (object)this.lastMoveUsed, typeof(Moves));
			info.AddValue("lastMoveUser", (object)this.lastMoveUser, typeof(int));
			info.AddValue("megaEvolution", (object)this.megaEvolution, typeof(int[][]));
			info.AddValue("amuletcoin", (object)this.amuletcoin, typeof(bool));
			info.AddValue("extramoney", (object)this.extramoney, typeof(int));
			info.AddValue("doublemoney", (object)this.doublemoney, typeof(bool));
			info.AddValue("endspeech", (object)this.endspeech, typeof(string));
			info.AddValue("endspeech2", (object)this.endspeech2, typeof(string));
			info.AddValue("endspeechwin", (object)this.endspeechwin, typeof(string));
			info.AddValue("endspeechwin2", (object)this.endspeechwin2, typeof(string));
			info.AddValue("rules", (object)this.rules, typeof(IDictionary<string, bool>));
			info.AddValue("turncount", (object)this.turncount, typeof(int));
			info.AddValue("priority", (object)this.priority, typeof(IBattlerIE[]));
			info.AddValue("snaggedpokemon", (object)this.snaggedpokemon, typeof(List<int>));
			info.AddValue("runCommand", (object)this.runCommand, typeof(int));
			info.AddValue("pickupUse", (object)this.pickupUse, typeof(int));
			info.AddValue("controlPlayer", (object)this.controlPlayer, typeof(bool));
			info.AddValue("usepriority", (object)this.usepriority, typeof(bool));
			info.AddValue("peer", (object)this.peer, typeof(IBattlePeer));
		}
		#endregion
#pragma warning restore 0162
	}
}