﻿using PokemonUnity;
using PokemonUnity.Character;
using PokemonUnity.Monster;
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// </summary>
/// https://www.youtube.com/watch?v=CE9VOZivb3I
[ExecuteInEditMode]
public class LevelLoader : MonoBehaviour
{
	#region Variables
	public float transitionTime = .5f;
	#endregion
	#region Unity Monobehavior
	void Awake()
	{
		GameEvents.current.onLoadLevel += Scene_onLoadLevel;
	}
	void Start()
	{
	}
	void OnDestroy()
	{
		GameEvents.current.onLoadLevel -= Scene_onLoadLevel;
	}
	#endregion
	#region Methods
	public void LoadNextLevel(int id)
	{
		Scene_onLoadLevel(id);
	}

	IEnumerator LoadLevel(int level)
	{
		//play animation
		//begin fade to black...

		// wait
		yield return new WaitForSeconds(transitionTime);

		//load scene
		SceneManager.LoadScene(level);
	}

	private void Scene_onLoadLevel(int level)
	{
		//SceneManager.LoadScene(level);
		StartCoroutine(LoadLevel(level));
	}
	#endregion
}