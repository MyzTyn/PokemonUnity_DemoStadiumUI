using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class StadiumRentalUI : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private GridLayoutGroup gridGroup;
    [SerializeField]
    private DemoStadiumManager demo;
    [SerializeField]
    private GameObject RentalUI;
    private List<int> ID;
    public void DisplayPokemonSelectUI()
    {
        ID = new List<int>();
        for (int i = 1; i <= 151; i++)
        {
            ID.Add(i);
        }
        GetPokemonRental();
        ActiveRentalUI(true);
    }
    private void GetPokemonRental()
    {
        if (ID.Count < 4)
        {
            gridGroup.constraintCount = ID.Count;
        }
        else
        {
            gridGroup.constraintCount = 3;
        }
        foreach (int id in ID)
        {
            GameObject Button = Instantiate(buttonTemplate);
            Button.SetActive(true);
            demo.RentalData(id, Button.GetComponent<StadiumRentalUIButton>());
            Button.GetComponent<StadiumRentalUIButton>().SetID(id);
            Button.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }
    public void ActiveRentalUI(bool active)
    {
        RentalUI.SetActive(active);
    }

}
