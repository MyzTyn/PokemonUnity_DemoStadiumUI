using System;
using PokemonEssentials.Interface.Screen;
using UnityEngine;

namespace PokemonUnity.Stadium
{
    // ToDo: Fix the IntroScene.cs and use it!
    public class StadiumIntroScene : MonoBehaviour, IScene
    {
        public int Id => (int)Scenes.Intro;

        [SerializeField] private RosterSelectionScene RosterSelectionScene;

        private void Awake()
        {
            // ToDo: Remove those code.
            // This is for demo purpose only
            if (GameManager.current.sceneList.canvasGroup == null)
                GameManager.current.sceneList.canvasGroup = GetComponentInParent<CanvasGroup>();
        }

        private void Start()
        {
            GameManager.current.OnLoadScene(RosterSelectionScene);
        }

        public void Refresh() {}

        public void Display(string v) {}

        public bool DisplayConfirm(string v) => false;
    }
}