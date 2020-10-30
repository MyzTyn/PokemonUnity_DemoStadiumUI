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
    public int ID;
    public void SetIcon(Sprite mySprite)
    {
        myIcon.sprite = mySprite;
    }
    public void SetName(string name)
    {
        Name.text = name;
    }
    public void SetLevel(int Level)
    {
        L.text = "L" + Level;
    }
    public void SetID(int Id)
    {
        ID = Id;
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
    private void Awake()
    {
        button.onClick.AddListener(delegate { demo.PokemonStatsandMoveUI(ID); });
    }
    
}

