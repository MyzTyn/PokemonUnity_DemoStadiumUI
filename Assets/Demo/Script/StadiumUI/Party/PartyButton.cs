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
    public void SetIcon(Sprite icon)
    {
        PokemonIcon.sprite = icon;
    }
    public void SetName(string name)
    {
        Name.text = name;
    }
    public void ActivePokemonDisplay(bool active)
    {
        PokemonDisplay.SetActive(active);
    }
    public void ActivePartyUIButton(bool active)
    {
        PartyUIButton.SetActive(active);
    }
    public void SetBackgroundColor(Color color)
    {
        PartyUIButton.GetComponent<Image>().color = color;
    }
    public void SetLevel(int level)
    {
        Level.text = "L " + level;
    }
    public void Clear()
    {
        PokemonIcon.sprite = null;
        Name.text = null;
        Level.text = null;

    }
}