using System;
using PokemonEssentials.Interface.PokeBattle;
using PokemonEssentials.Interface.Screen;
using UnityEngine;

namespace PokemonUnity.Stadium
{
    public class RosterSelectionScene : MonoBehaviour, IScene
    {
        public int Id => (int)Scenes.RosterSelection;
        
        [SerializeField] private PokemonSelect pokemonSelect;
        [SerializeField] private SelectPokemonButton selectPokemonButton;
        [SerializeField] private PokemonViewModal pokemonViewModal;
        [SerializeField] private Transform rosterGridContent;
        
        public static UnityEngine.Sprite[] PokemonTypes { get; private set; }
        public static UnityEngine.Sprite[] IconSprites { get; private set; }
        
        public static event Action<IPokemon, int> OnChangePartyLineup;
        // ToDo: Remove below: This is for demo purpose only
        private static event Action OnLoadVersusPartyScene;
        [SerializeField] private VersusPartyScene VersusPartyScene;
        
        #region Unity Methods
        private void Awake()
        {
            Core.Logger?.Log("Load Assets for UI into Array");
            IconSprites = Resources.LoadAll<UnityEngine.Sprite>("PokemonIcon");
            PokemonTypes = Resources.LoadAll<UnityEngine.Sprite>("PokemonType");
            
            // ToDo: Remove those code.
            // This is for demo purpose only
            OnLoadVersusPartyScene += LoadVersusPartyScene;
            if (GameManager.current.sceneList.canvasGroup == null)
                GameManager.current.sceneList.canvasGroup = GetComponentInParent<CanvasGroup>();
        }
        
        private void Start()
        {
            PrepopulateRentalPokemonsUI();
        }

        private void OnDestroy()
        {
            if (rosterGridContent.childCount > 0)
            {
                //Clear child objects
                foreach (Transform child in rosterGridContent)
                    Destroy(child.gameObject);
            }
            
            IconSprites = null;
            PokemonTypes = null;
            
            // ToDo: Remove those code.
            // This is for demo purpose only
            OnLoadVersusPartyScene -= LoadVersusPartyScene;
        }
        #endregion

        #region UI Events

        public static void AddPokemonToParty(IPokemon pokemon, int index)
        {
            OnChangePartyLineup?.Invoke(pokemon, index);
        }
        public static void GoToVersusParty()
        {
            OnLoadVersusPartyScene?.Invoke();
        }
        // ToDo: Remove this. This is for demo purpose only
        private void LoadVersusPartyScene()
        {
            if (VersusPartyScene != null)
                GameManager.current.OnLoadScene(VersusPartyScene);
            else
                GameManager.current.OnLoadLevel((int)Scenes.VersusParty);
        }
        private void PrepopulateRentalPokemonsUI()
        {
            if (rosterGridContent.childCount != 0)
            {
                Core.Logger.LogWarning("Already Prepopulated!");
                return;
            }
            
            // This project only have gen 1 sprites
            Core.PokemonGeneration = (sbyte)PokemonUnity.Generation.RedBlueYellow;
            Core.Logger?.Log("Creating {0} Pokemon",Core.PokemonIndexLimit);
			
            for (int i = 0; i < Core.PokemonIndexLimit; i++)
            {
                Pokemons species = (Pokemons)(i + 1);

                SelectPokemonButton item = Instantiate(selectPokemonButton, rosterGridContent);
                item.gameObject.SetActive(true);
                item.SetID(pokemonViewModal, pokemonSelect, i, species, species.ToString(), 50);
                item.name = $"ID {i}";
            }
        }
        
        #endregion
        
        public void Refresh() {}
        public void Display(string v) {}
        public bool DisplayConfirm(string v) => false;
    }
}