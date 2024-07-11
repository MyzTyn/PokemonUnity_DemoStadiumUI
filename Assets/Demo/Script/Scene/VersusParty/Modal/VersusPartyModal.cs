using PokemonEssentials.Interface.PokeBattle;
using PokemonUnity.Application;
using PokemonUnity.Stadium;
using System.Linq;
using Demo.Script.Scene.PartySelect;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VersusPartyModal : MonoBehaviour
{
	[Header("Trainer Card")]
	[SerializeField] private TextMeshProUGUI TrainerName;
	[SerializeField] private Image TrainerImage;
	
	[Space(10)]
	
	[Header("Party Grid")]
	[SerializeField] private GameObject PartyGrid;
	[SerializeField] private Image[] pokemons;

	public void Awake()
	{
		// Load Images of the pokemons
		pokemons = PartyGrid.GetComponentsInChildren<Image>().Skip(1).ToArray();
	}

	public void ActiveWindow(bool active)
	{
		if (!active)
			ClearDisplay();

		gameObject.SetActive(active);
	}

	public void RefreshDisplay(string trainerName, UnityEngine.Sprite trainerSprite, IPokemon[] party)
	{
		TrainerName.text = trainerName;
		TrainerImage.sprite = trainerSprite;

		for (int i = 0; i < Feature.MAXPARTYSIZE; i++)
		{
			pokemons[i].sprite = RosterSelectionScene.IconSprites[(int)party[i].Species];
		}
	}

	public void ClearDisplay()
	{
		TrainerName.text = null;
		TrainerImage.sprite = null;

		foreach (Image image in pokemons)
		{
			image.sprite = null;
		}
	}
}
