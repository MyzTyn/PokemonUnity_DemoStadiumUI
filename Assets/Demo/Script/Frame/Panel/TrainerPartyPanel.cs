using System;
using System.Text;
using System.Collections.Generic;
using PokemonUnity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ToggleGroup))]
public class TrainerPartyPanel : MonoBehaviour//, IEventSystemHandler, ISelectHandler, IDeselectHandler, ISubmitHandler//, IUpdateSelectedHandler
{
	#region Variables
	//public ToggleGroup toggleGroup;
	public TrainerPokemonButton[] party;
	public TrainerPokemonButton pokemonButtonPrefab;
	public GameObject partyContentFrame;
	public Text trainerName;
	public Text trainerId;
	public int currentSlot;
	#endregion

	#region 
	private void Awake()
	{
		//Clear child objects
		//var children = partyContentFrame.GetComponentsInChildren<Transform>(true);
		//foreach (Transform child in children) Destroy(child.gameObject);
		foreach (Transform child in partyContentFrame.transform) Destroy(child.gameObject);

		//toggleGroup = GetComponent<ToggleGroup>();
		party = new TrainerPokemonButton[6];
		//foreach(TrainerPokemonButton pokemon in party)
		for(int i = 0; i < party.Length; i++)
		{
			//Instantiate new Prefab to Scene
			TrainerPokemonButton pokemon = Instantiate<TrainerPokemonButton>(pokemonButtonPrefab, partyContentFrame.transform);
			pokemon.toggle.group = GetComponent<ToggleGroup>(); //toggleGroup;
			pokemon.toggle.interactable = false;
			pokemon.name = "Slot" + i;
			party[i] = pokemon;
		}
		SetTrainerID(0);
		RefreshPartyDisplay();
	}
	#endregion

	#region 
	public void RefreshPartyDisplay()
	{
		for (int Id = 0; Id < ((Game)Game.GameData).Features.LimitPokemonPartySize && Id < Core.MAXPARTYSIZE; Id++)
		{
			currentSlot = Id;
			party[Id].toggle.interactable = false;
			//party[Id].IsSelected = true;
			party[Id].toggle.Select();
			if (((Game)Game.GameData).Features.LimitPokemonPartySize == Id && Game.GameData.Trainer.party[Id].IsNotNullOrNone())
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
	public void SetTrainerID(int ID, string name = null)
	{
		//StringBuilder stringBuilder = new StringBuilder();
		//stringBuilder.AppendLine("Trainer");
		//stringBuilder.AppendLine(string.Format("ID {0:00000}", ID));
		//trainerId.text = stringBuilder.ToString().TrimEnd();
		trainerName.text = string.IsNullOrWhiteSpace(name) ? "Trainer" : name.ToString().TrimEnd();
		trainerId.text = string.Format("ID {0:00000}", ID).ToString().TrimEnd();
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
