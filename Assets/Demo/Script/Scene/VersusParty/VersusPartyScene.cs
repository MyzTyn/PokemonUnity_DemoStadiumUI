using System.Collections;
using System.Linq;
using PokemonEssentials.Interface.PokeBattle;
using PokemonEssentials.Interface.Screen;
using PokemonUnity.Monster;
using UnityEngine;

namespace PokemonUnity.Stadium
{
    public class VersusPartyScene : MonoBehaviour, IScene
    {
        public int Id => (int)Scenes.VersusParty;
        
        //[SerializeField] private PokemonSelect pokemonSelect;
        [SerializeField] private VersusPartyModal versusPartyTop;
        [SerializeField] private VersusPartyModal versusPartyBottom;
        
        // ToDo: Remove those below. It is generally good practice to put in one file
        // For now, I only do this for demo purpose
        public static UnityEngine.Sprite[] IconSprites { get; private set; }

        #region Unity Methods
        private void Awake()
        {
            Core.Logger?.Log("Load Assets for UI into Array");
            IconSprites = Resources.LoadAll<UnityEngine.Sprite>("PokemonIcon");
            
            // ToDo: Remove those code.
            // This is for demo purpose only
            if (GameManager.current.sceneList.canvasGroup == null)
                GameManager.current.sceneList.canvasGroup = GetComponentInParent<CanvasGroup>();
        }
        
        private void Start()
        {
            // Load the ScriptableObject from the resources folder
            // ToDo: Below the code should be clean up.
            // Those are for demo purpose only
            PokemonSelect pokemonSelect = Resources.Load<PokemonSelect>("Data/PlayerScriptableObject");
            
            // Use the data from PokemonSelect otherwise use default
            string challenger = Game.GameData.Trainer != null ? Game.GameData.Trainer.name : "Player";
            string opponent = Game.GameData.Trainer != null ? Game.GameData.Trainer.name : "Biker";
            IPokemon[] p1 = pokemonSelect.TemporaryParty.Count != 0 ? pokemonSelect.TemporaryParty.Reverse().ToArray() : GenerateRandomParty();
            IPokemon[] p2 = pokemonSelect.TemporaryParty.Count != 0 ? pokemonSelect.TemporaryParty.ToArray() : GenerateRandomParty();
            
            // Display
            versusPartyTop.RefreshDisplay(challenger, null, p1);
            versusPartyBottom.RefreshDisplay(opponent, null, p2);
            
            // Battle Scene
            StartCoroutine(StartBattle());
            return;

            // Battle Scene Code
            IEnumerator StartBattle()
            {
                yield return new WaitForSeconds(3);
                GameManager.current.OnLoadLevel((int)Scenes.Battle, 1.5f);
            }
        }
        private void OnDestroy()
        {
            IconSprites = null;
        }
        #endregion
        
        private IPokemon[] GenerateRandomParty()
        {
            var rnd = new System.Random();
            return Enumerable.Range(0, Core.MAXPARTYSIZE)
                .Select(_ => 
                {
                    var pokemon = new Pokemon((Pokemons)rnd.Next(151) + 1, level: 50);
                    pokemon.SetNickname(pokemon.Species.ToString()); // ToDo: Remove this line when DLLs updated to handle the null
                    return (IPokemon)pokemon;
                })
                .ToArray();
        }
        
        public void Refresh() {}
        public void Display(string v) {}
        public bool DisplayConfirm(string v) => false;
    }
}