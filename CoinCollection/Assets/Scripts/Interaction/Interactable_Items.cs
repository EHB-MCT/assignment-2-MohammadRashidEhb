using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class Interactable_Items : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Outline outline;
    public string message;

    public UnityEvent onInteraction;


    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Interact()
    {
        onInteraction.Invoke();

    // Notify inventory
    InventoryManager.Instance?.ItemCollected();

    // Optionally disable object
    gameObject.SetActive(false);
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }
    public void EnableOutline()
    {
        outline.enabled = true;
    }

}
