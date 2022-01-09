using System;
using System.Text;
using System.Collections.Generic;
using PokemonUnity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class TrainerPartyPanel : MonoBehaviour//, IEventSystemHandler, ISelectHandler, IDeselectHandler, ISubmitHandler//, IUpdateSelectedHandler
{
	#region 
	[SerializeField] private bool isFocused;
	public bool isSelected;
	#endregion
	//[SerializeField] private GridLayoutGroup gridGroup;
	[SerializeField] private Text TrainerID;
	/*[SerializeField] private GameObject buttonTemplate;
	[SerializeField] MainCameraGameManager demo;
	public void GetPartyButton()
	{
		//if (ID.Count < 4)
		//{
		//	gridGroup.constraintCount = ID.Count;
		//}
		//else
		//{
		//	gridGroup.constraintCount = 3;
		//}
		for (int Id = 0; Id < Game.GameData.Features.LimitPokemonPartySize && Id < Core.MAXPARTYSIZE; Id++)
		{
			//if (Id == Core.MAXPARTYSIZE) break;
			GameObject Button = Instantiate(buttonTemplate);
			demo.PartyData(Id, Button.GetComponent<TrainerPokemonButton>());
			Button.GetComponent<TrainerPokemonButton>().ActivePartyUIButton(true);
			Button.GetComponent<TrainerPokemonButton>().ActivePokemonDisplay(false);
			Button.transform.SetParent(buttonTemplate.transform.parent, false);
		}
	}*/
	public void SetTrainerID(int ID)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("Trainer");
		stringBuilder.AppendLine(string.Format("ID {0:00000}", ID));
		TrainerID.text = stringBuilder.ToString().TrimEnd();
	}
	#region 
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
