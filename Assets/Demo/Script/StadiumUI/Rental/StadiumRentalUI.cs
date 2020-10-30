using PokemonUnity;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class StadiumRentalUI : MonoBehaviour
{
    private List<PokemonRentalUI> PokemonRental;
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private GridLayoutGroup gridGroup;
    [SerializeField]
    private DemoStadiumManager demo;
    [SerializeField]
    private GameObject RentalUI;
    public void DisplayPokemonSelectUI(int level)
    {
        PokemonRental = new List<PokemonRentalUI>();
        for (int i = 1; i <= 151; i++)
        {
            PokemonRentalUI rental = new PokemonRentalUI();
            rental.IconSprite = demo.iconSprites[i];
            rental.Name = Convert.ToString((Pokemons)i);
            rental.Level = level;
            rental.ID = i;
            PokemonRental.Add(rental);
        }
        GetPokemonRental();
        ActiveRentalUI(true);
    }
    private void GetPokemonRental()
    {
        if (PokemonRental.Count < 4)
        {
            gridGroup.constraintCount = PokemonRental.Count;
        }
        else
        {
            gridGroup.constraintCount = 3;
        }
        foreach (PokemonRentalUI Rental in PokemonRental)
        {
            GameObject Button = Instantiate(buttonTemplate);
            Button.SetActive(true);
            demo.RentalData(Rental.ID, Button.GetComponent<StadiumRentalUIButton>());
            Button.GetComponent<StadiumRentalUIButton>().SetIcon(Rental.IconSprite);
            Button.GetComponent<StadiumRentalUIButton>().SetName(Rental.Name);
            Button.GetComponent<StadiumRentalUIButton>().SetLevel(Rental.Level);
            Button.GetComponent<StadiumRentalUIButton>().SetID(Rental.ID);
            Button.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }
    public void ActiveRentalUI(bool active)
    {
        RentalUI.SetActive(active);
    }
    public class PokemonRentalUI
    {
        public Sprite IconSprite;
        public string Name;
        public int Level;
        public int ID;
    }
}
