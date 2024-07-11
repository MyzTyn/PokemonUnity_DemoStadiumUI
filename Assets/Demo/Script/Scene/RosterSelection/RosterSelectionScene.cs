using System;
using PokemonEssentials.Interface.PokeBattle;
using PokemonEssentials.Interface.Screen;
using PokemonUnity;
using PokemonUnity.Stadium;
using UnityEngine;
using UnityEngine.Serialization;
using Sprite = UnityEngine.Sprite;

namespace Demo.Script.Scene.PartySelect
{
    public class RosterSelectionScene : MonoBehaviour, IScene
    {
        public int Id => 2;

        [FormerlySerializedAs("PokemonSelect")] [SerializeField]
        private PokemonSelect pokemonSelect;
        
        [FormerlySerializedAs("SelectPokemonButton")] [SerializeField]
        private SelectPokemonButton selectPokemonButton;

        [FormerlySerializedAs("PokemonViewModal")] [SerializeField]
        private PokemonViewModal pokemonViewModal;
        
        [FormerlySerializedAs("RosterGridContent")] [SerializeField]
        private Transform rosterGridContent;
        
        public static Sprite[] PokemonTypes { get; private set; }
        public static Sprite[] IconSprites { get; private set; }
        
        // UI Events
        public static event Action<IPokemon, int> OnChangePartyLineup;
        
        #region Unity Methods
        private void Awake()
        {
            Core.Logger?.Log("Load Assets for UI into Array");
            IconSprites = Resources.LoadAll<Sprite>("PokemonIcon");
            PokemonTypes = Resources.LoadAll<Sprite>("PokemonType");
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
        }
        #endregion

        #region UI Events

        public static void AddPokemonToParty(IPokemon pokemon, int index)
        {
            OnChangePartyLineup?.Invoke(pokemon, index);
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
            Core.Logger.Log("Creating {0} Pokemon",Core.PokemonIndexLimit);
			
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
        
        public void Refresh()
        {
            throw new System.NotImplementedException();
        }

        public void Display(string v)
        {
            throw new System.NotImplementedException();
        }

        public bool DisplayConfirm(string v)
        {
            throw new System.NotImplementedException();
        }
    }
}