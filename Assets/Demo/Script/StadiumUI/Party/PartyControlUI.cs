using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PartyControlUI : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private GridLayoutGroup gridGroup;
    [SerializeField]
    private Text TrainerID;
    [SerializeField]
    DemoStadiumManager demo;
    List<int> ID;
    public void DisplayPartyUI()
    {
        ID = new List<int>();

        for (int i = 0; i <= 5; i++)
        {
            ID.Add(i);

        }
        GetPartyButton();

    }
    private void GetPartyButton()
    {
        if (ID.Count < 4)
        {
            gridGroup.constraintCount = ID.Count;
        }
        else
        {
            gridGroup.constraintCount = 3;
        }
        foreach (int Id in ID)
        {
            GameObject Button = Instantiate(buttonTemplate);
            demo.PartyData(Id, Button.GetComponent<PartyButton>());
            Button.GetComponent<PartyButton>().ActivePartyUIButton(true);
            Button.GetComponent<PartyButton>().ActivePokemonDisplay(false);
            Button.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }
    public void SetTrainerID(int ID)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Trainer");
        stringBuilder.AppendLine(string.Format("ID ", ID));
        TrainerID.text = stringBuilder.ToString();
    }
}
