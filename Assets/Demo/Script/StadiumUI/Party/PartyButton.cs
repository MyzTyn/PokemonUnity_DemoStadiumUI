using PokemonUnity;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PartyButton : MonoBehaviour
{
    [SerializeField]
    private Image PokemonIcon;
    [SerializeField]
    private Text Name;
    [SerializeField]
    private Text Level;
    [SerializeField]
    private GameObject PokemonDisplay;
    [SerializeField]
    private GameObject PartyUIButton;
    public void DisplayPartyButton()
    {
        PokemonIcon.sprite = DemoStadiumManager.IconSprites[DemoStadiumManager.PkmnSelected];
        Name.text = Convert.ToString((Pokemons)DemoStadiumManager.PkmnSelected);
        Level.text = "L " + DemoStadiumManager.LevelFixed;
        ActivePokemonDisplay(true);
    }
    public void ActivePokemonDisplay(bool active)
    {
        PokemonDisplay.SetActive(active);
    }
    public void ActivePartyUIButton(bool active)
    {
        PartyUIButton.SetActive(active);
    }
    public void Clear()
    {
        PokemonIcon.sprite = null;
        Name.text = null;
        Level.text = null;
    }
}