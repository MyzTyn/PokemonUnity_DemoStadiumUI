using PokemonUnity;
using System;
using UnityEngine;
using UnityEngine.UI;

public class StadiumRentalUIButton : MonoBehaviour
{
    [SerializeField]
    private Image myIcon;
    [SerializeField]
    private Text Name;
    [SerializeField]
    private Text L;
    [SerializeField]
    private Button button;
    [SerializeField]
    private DemoStadiumManager demo;
    public int ID { get; private set; }
    public void SetID(int id)
    {
        ID = id;
    }
    public void DisableOnClick(bool active)
    {
        if (active)
        {
            button.onClick.RemoveAllListeners();
        }
        else
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(delegate { demo.PokemonStatsandMoveUI(ID); });
        }
    }
    private void Start()
    {
        button.onClick.AddListener(delegate { demo.PokemonStatsandMoveUI(ID); });
        new WaitForSeconds(1);
        myIcon.sprite = DemoStadiumManager.IconSprites[ID];
        L.text = "L" + DemoStadiumManager.LevelFixed;
        Name.text = Convert.ToString((Pokemons)ID);
    }
}

