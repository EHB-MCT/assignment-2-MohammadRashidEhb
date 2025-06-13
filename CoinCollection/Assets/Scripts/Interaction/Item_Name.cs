using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Item_Name : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static Item_Name instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] TMP_Text interactionText;

    public void EnableInteractionText(string text)
    {
        interactionText.text = text + "(E)";
        interactionText.gameObject.SetActive(true);
    }

    public void DisableInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }
}
