using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
[RequireComponent(typeof(Image))]
public class StorageBoxTabButton : TabButton
{
    public Text label;

    private void Awake()
    {
        //label = GetComponent<Text>();
        background = GetComponent<Image>();
    }
}