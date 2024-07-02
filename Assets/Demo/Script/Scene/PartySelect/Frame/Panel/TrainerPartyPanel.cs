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
	/// <summary>
	/// This is a container to hold objects, it doesnt really need any logic
	/// </summary>
	[RequireComponent(typeof(ToggleGroup))]
	public class TrainerPartyPanel : MonoBehaviour //, IEventSystemHandler, ISelectHandler, IDeselectHandler, ISubmitHandler//, IUpdateSelectedHandler
	{
		#region Variables
		//public ToggleGroup toggleGroup;
		public TrainerPokemonButton[] party;
		public TrainerPokemonButton pokemonButtonPrefab;
		/// <summary>
		/// Nested <see cref="UnityEngine.UI.LayoutGroup"/> to hold the party pokemon
		/// </summary>
		public GameObject partyContentFrame; //ToDo: What is this?
		public Text trainerName;
		public Text trainerId;
		//public int currentSlot;
		public PokemonSelect PokemonSelect;
		#endregion

		#region Unity
		private void Awake()
		{
			//Clear child objects
			foreach (Transform child in partyContentFrame.transform)
				Destroy(child.gameObject);

			//toggleGroup = GetComponent<ToggleGroup>();
			party = new TrainerPokemonButton[Core.MAXPARTYSIZE]; //Should be 6, or Game.GameData.Global.Features.LimitPokemonPartySize
			//foreach(TrainerPokemonButton pokemon in party)
			//for(int i = 0; i < party.Length; i++)
			//{
			//	//Instantiate new Prefab to Scene
			//	TrainerPokemonButton pokemon = Instantiate(pokemonButtonPrefab, partyContentFrame.transform);
			//
			//	// Set the width/height (Fix this code => There's nothing to fix as Unity components are set to automatically adjust size, and position)
			//	//var layoutElement = pokemon.GetComponent<LayoutElement>();
			//	//layoutElement.preferredWidth = 125;
			//	//layoutElement.preferredHeight = 41;
			//
			//	pokemon.partyIndex = i;
			//	pokemon.toggle.group = GetComponent<ToggleGroup>(); //toggleGroup;
			//	pokemon.toggle.interactable = false;
			//	pokemon.name = "Slot" + i;
			//	//pokemon.PokemonSelect = PokemonSelect;
			//	party[i] = pokemon;
			//}
			SetTrainerID(0);
			//RefreshPartyDisplay();
		}
		#endregion

		#region Methods
		//public void RefreshPartyDisplay()
		//{
		//	for (int Id = 0; Id < Core.MAXPARTYSIZE && Id < PokemonSelect.SelectedPokemons.Count; Id++) //Id < Game.GameData.Global.Features.LimitPokemonPartySize
		//	{
		//		currentSlot = Id;
		//		party[Id].toggle.interactable = false;
		//		//party[Id].IsSelected = true;
		//		party[Id].toggle.Select();
		//
		//		if (Core.MAXPARTYSIZE == Id && Game.GameData.Trainer.party[Id].IsNotNullOrNone()) //Id == Game.GameData.Global.Features.LimitPokemonPartySize
		//		{
		//			//party[Id].IsSelected = true;
		//			party[Id].toggle.isOn = false;
		//			break;
		//		}
		//		//GameObject Button = Instantiate(buttonTemplate);
		//		//demo.PartyData(Id, Button.GetComponent<TrainerPokemonButton>());
		//		//party[Id].ActivePartyUIButton(true);
		//		//party[Id].ActivePokemonDisplay(false);
		//
		//		party[Id].SetDisplay();//PokemonSelect.SelectedPokemons.ElementAt(Id)
		//	}
		//}

		public void SetTrainerID(int ID, string name = null)
		{
			trainerName.text = string.IsNullOrWhiteSpace(name) ? "Trainer" : name.ToString().TrimEnd();
			trainerId.text = string.Format("ID {0:00000}", ID).ToString().TrimEnd();
		}
		#endregion
	}
}