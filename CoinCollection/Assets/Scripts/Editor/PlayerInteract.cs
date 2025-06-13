using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInteract : MonoBehaviour
{
    private void Update() {
        // Check for interaction input (keyboard or controller)
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Interact")) {
            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray) 
            {
                if (collider.TryGetComponent(out NPCInteractable npcInteractable)) {
                    npcInteractable.Interact();
                }
            }
        }
    }
}
