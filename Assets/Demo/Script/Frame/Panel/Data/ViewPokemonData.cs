﻿using UnityEngine;
using UnityEngine.UI;

public class ViewPokemonData : MonoBehaviour
{
	//ID
	public Text PkmnName;
	public Text Level;
	public Text PkmnID;
	public Text Species_Name;
	[Space(5)]
	//PkmnInfo
	public Image PokemonSprite;
	public Text Health;
	public Text Attack;
	public Text Defense;
	public Text Speed;
	public Text SpecialAtk;
	public Text SpecialDef;
	public Image Type1;
	public Image Type2;
	//Move Set
	[Space(5)]
	public Text MoveName1;
	public Image MoveColor1;
	public Text MoveName2;
	public Image MoveColor2;
	public Text MoveName3;
	public Image MoveColor3;
	public Text MoveName4;
	public Image MoveColor4;
	[Space(5)]
	public Text MoveType1;
	public Text MoveType2;
	public Text MoveType3;
	public Text MoveType4;

	public static Color TypeToColor(PokemonUnity.Types type)
	{
		//Convert from Hex(TypeToColorHEX) to RGB
		switch (type)
		{
			case PokemonUnity.Types.NORMAL:
				return new Color32(168, 167, 122, 255);
			case PokemonUnity.Types.FIGHTING:
				return new Color32(194, 46, 40, 255);
			case PokemonUnity.Types.FLYING:
				return new Color32(169, 143, 243, 255);
			case PokemonUnity.Types.POISON:
				return new Color32(163, 62, 161, 255);
			case PokemonUnity.Types.GROUND:
				return new Color32(226, 191, 101, 255);
			case PokemonUnity.Types.ROCK:
				return new Color32(182, 161, 54, 255);
			case PokemonUnity.Types.BUG:
				return new Color32(166, 185, 26, 255);
			case PokemonUnity.Types.GHOST:
				return new Color32(115, 87, 151, 255);
			case PokemonUnity.Types.STEEL:
				return new Color32(183, 183, 206, 255);
			case PokemonUnity.Types.FIRE:
				return new Color32(238, 129, 48, 255);
			case PokemonUnity.Types.WATER:
				return new Color32(99, 144, 240, 255);
			case PokemonUnity.Types.GRASS:
				return new Color32(122, 199, 76, 255);
			case PokemonUnity.Types.ELECTRIC:
				return new Color32(247, 208, 44, 255);
			case PokemonUnity.Types.PSYCHIC:
				return new Color32(249, 85, 135, 255);
			case PokemonUnity.Types.ICE:
				return new Color32(150, 217, 214, 255);
			case PokemonUnity.Types.DRAGON:
				return new Color32(111, 53, 252, 255);
			case PokemonUnity.Types.DARK:
				return new Color32(0, 0, 0, 255);
			case PokemonUnity.Types.FAIRY:
				return new Color32(214, 133, 173, 255);
			case PokemonUnity.Types.NONE:
			case PokemonUnity.Types.UNKNOWN:
			case PokemonUnity.Types.SHADOW:
			default:
				return Color.clear;
		}
	}
	public static string ReturnMoveName(PokemonUnity.Moves move)
	{
		if (move == PokemonUnity.Moves.NONE)
		{
			return null;
		}
		else
		{
			return move.ToString().Replace("_", " ");
		}
	}
	public static string ReturnMoveFirstLetter(string s)
	{
		if (s == "NONE")
		{
			return string.Empty;
		}
		else
		{
			return s[0].ToString().ToUpper();
		}
	}
}