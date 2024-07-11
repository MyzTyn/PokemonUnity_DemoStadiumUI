using System;
using System.Linq;
using PokemonEssentials.Interface.Screen;
using PokemonUnity;
using PokemonUnity.Stadium;
using UnityEngine;
using UnityEngine.Serialization;

namespace Demo.Script.Scene.VersusParty
{
    public class VersusPartyScene : MonoBehaviour, IScene
    {
        public int Id => 3;
        
        [FormerlySerializedAs("PokemonSelect")] [SerializeField]
        private PokemonSelect pokemonSelect;
        
        [FormerlySerializedAs("VersusPartyTop")] [SerializeField]
        private VersusPartyModal versusPartyTop;
        
        [FormerlySerializedAs("VersusPartyBottom")] [SerializeField]
        private VersusPartyModal versusPartyBottom;
        
        private void Awake()
        {
            versusPartyTop.RefreshDisplay(Game.GameData.Trainer.name, null, pokemonSelect.TemporaryParty.Reverse().ToArray());
            versusPartyBottom.RefreshDisplay("Biker", null, pokemonSelect.TemporaryParty.ToArray());
        }

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