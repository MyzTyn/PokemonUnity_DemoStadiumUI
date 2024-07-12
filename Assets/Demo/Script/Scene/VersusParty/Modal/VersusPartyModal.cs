using PokemonEssentials.Interface.PokeBattle;
using PokemonUnity.Application;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonUnity.Stadium
{
	public class VersusPartyModal : MonoBehaviour
	{
		[Header("Trainer Card")] 
		[SerializeField] private TextMeshProUGUI TrainerName;
		[SerializeField] private Image TrainerImage;

		[Space(10)]
		
		[SerializeField] private GameObject PartyGrid;
		[SerializeField] private Image[] pokemons;

		#region Unity Methods
		
		private void Awake()
		{
			// Load Images of the pokemons
			pokemons = PartyGrid.GetComponentsInChildren<Image>().Skip(1).ToArray();
		}
		
		// Clear the display once this cannot be seen
		private void OnBecameInvisible()
		{
			ClearDisplay();
		}
		
		#endregion
		
		// Set the display with data
		public void RefreshDisplay(string trainerName, UnityEngine.Sprite trainerSprite, IPokemon[] party)
		{
			TrainerName.text = trainerName;
			TrainerImage.sprite = trainerSprite;
			
			for (int i = 0; i < Feature.MAXPARTYSIZE; i++)
			{
				pokemons[i].sprite = VersusPartyScene.IconSprites[(int)party[i].Species];
			}
		}
		
		// Clear the display
		private void ClearDisplay()
		{
			TrainerName.text = null;
			TrainerImage.sprite = null;
			
			foreach (Image image in pokemons)
			{
				image.sprite = null;
			}
		}
	}
}