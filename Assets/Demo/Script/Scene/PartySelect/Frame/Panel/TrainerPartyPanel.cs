using System;
using System.Text;
using System.Collections.Generic;
using PokemonUnity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using PokemonUnity.Application;
using PokemonEssentials.Interface.PokeBattle;
using System.Linq;

namespace PokemonUnity.Stadium
{
	[RequireComponent(typeof(ToggleGroup))]
	public class TrainerPartyPanel : MonoBehaviour, IObserver<StackChangedEventArgs<IPokemon>>  //, IEventSystemHandler, ISelectHandler, IDeselectHandler, ISubmitHandler//, IUpdateSelectedHandler
	{
		#region Variables
		//public ToggleGroup toggleGroup;
		public TrainerPokemonButton[] party;
		public TrainerPokemonButton pokemonButtonPrefab;
		public GameObject partyContentFrame;
		public Text trainerName;
		public Text trainerId;
		public int currentSlot;
		public PokemonSelect PokemonSelect;

		private IDisposable unsubscriber;
		#endregion

		#region Unity
		private void Awake()
		{
			//Clear child objects
			foreach (Transform child in partyContentFrame.transform) 
				Destroy(child.gameObject);

			//toggleGroup = GetComponent<ToggleGroup>();
			party = new TrainerPokemonButton[6];
			//foreach(TrainerPokemonButton pokemon in party)
			for(int i = 0; i < party.Length; i++)
			{
				//Instantiate new Prefab to Scene
				TrainerPokemonButton pokemon = Instantiate(pokemonButtonPrefab, partyContentFrame.transform);
				
				// Set the width/height (ToDo: Fix this code)
				var layoutElement = pokemon.GetComponent<LayoutElement>();
				layoutElement.preferredWidth = 125;
				layoutElement.preferredHeight = 41;
				pokemon.partyIndex = i;

                pokemon.toggle.group = GetComponent<ToggleGroup>(); //toggleGroup;
				pokemon.toggle.interactable = false;
				pokemon.name = "Slot" + i;
				//pokemon.PokemonSelect = PokemonSelect;
				party[i] = pokemon;
			}
			SetTrainerID(0);
			RefreshPartyDisplay();
		}
		#endregion

		#region Methods
		public void RefreshPartyDisplay()
		{
			for (int Id = 0; Id < Feature.MAXPARTYSIZE && Id < Core.MAXPARTYSIZE; Id++)
			{
				currentSlot = Id;
				party[Id].toggle.interactable = false;
				//party[Id].IsSelected = true;
				party[Id].toggle.Select();
				
				if (Feature.MAXPARTYSIZE == Id && Game.GameData.Trainer.party[Id].IsNotNullOrNone())
				{
					//party[Id].IsSelected = true;
					party[Id].toggle.isOn = false;
					break;
				}
				//GameObject Button = Instantiate(buttonTemplate);
				//demo.PartyData(Id, Button.GetComponent<TrainerPokemonButton>());
				//party[Id].ActivePartyUIButton(true);
				//party[Id].ActivePokemonDisplay(false);
			}
		}

        public void RefreshPartyDisplay(Stack<IPokemon> pokemons)
        {
            for (int Id = 0; Id < Feature.MAXPARTYSIZE && Id < Core.MAXPARTYSIZE && Id < pokemons.Count; Id++)
            {
                currentSlot = Id;
                party[Id].toggle.interactable = false;
                //party[Id].IsSelected = true;
                party[Id].toggle.Select();

                if (Feature.MAXPARTYSIZE == Id && Game.GameData.Trainer.party[Id].IsNotNullOrNone())
                {
                    //party[Id].IsSelected = true;
                    party[Id].toggle.isOn = false;
                    break;
                }
                //GameObject Button = Instantiate(buttonTemplate);
                //demo.PartyData(Id, Button.GetComponent<TrainerPokemonButton>());party
                //party[Id].ActivePartyUIButton(true);
                //party[Id].ActivePokemonDisplay(false);

				party[Id].SetDisplay(pokemons.ElementAt(Id));
            }
        }

        public void SetTrainerID(int ID, string name = null)
		{
			trainerName.text = string.IsNullOrWhiteSpace(name) ? "Trainer" : name.ToString().TrimEnd();
			trainerId.text = string.Format("ID {0:00000}", ID).ToString().TrimEnd();
		}

        public void OnCompleted()
        {
			Core.Logger?.Log("TrainerPartyPanel Observer Completed");
        }

        public void OnError(Exception error) => Core.Logger?.LogError(error.ToString());

        public void OnNext(StackChangedEventArgs<IPokemon> value)
        {
			switch (value.Operation)
			{
				case StackChangedEventArgs<IPokemon>.OperationType.Update:
                case StackChangedEventArgs<IPokemon>.OperationType.Push:
                case StackChangedEventArgs<IPokemon>.OperationType.Pop:
                    {
						Debug.Log($"TrainerPartyPanel {value.Operation} Triggered!");
						RefreshPartyDisplay(value.Stack);
						break;
					}
			}
        }

		public void Subscribe(IObservable<StackChangedEventArgs<IPokemon>> provider)
		{
            unsubscriber = provider?.Subscribe(this);
        }

		public void UnSubscribe()
		{
			unsubscriber?.Dispose(); 
		}

        //public void OnSelect(BaseEventData eventData)
        //{
        //	//base.OnSelect(eventData);
        //	UnityEngine.Debug.Log("Selected");
        //	isFocused = true;
        //}
        //
        //public void OnDeselect(BaseEventData eventData)
        //{
        //	//base.OnDeselect(eventData);
        //	UnityEngine.Debug.Log("De-Selected");
        //	isFocused = false;
        //}
        //
        //public void OnSubmit(BaseEventData eventData)
        //{
        //	throw new NotImplementedException();
        //}
        #endregion
    }
}