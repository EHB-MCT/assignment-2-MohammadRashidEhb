using StarterAssets;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    Interactable_Items currentInteractable;

    [SerializeField] private StarterAssetsInputs starterAssetsInputs;

    void Start()
    {
        if (starterAssetsInputs == null)
            starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        CheckInteraction();

        // Use the existing 'interact' property with immediate reset
        if (starterAssetsInputs != null && currentInteractable != null && starterAssetsInputs.interact)
        {
            currentInteractable.Interact();
            starterAssetsInputs.interact = false; // Reset immediately to prevent buffering
        }
    }

    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit, playerReach))
        {
            if (hit.collider.tag == "Interactable")
            {
                Interactable_Items newInteractable = hit.collider.GetComponent<Interactable_Items>();
                if (currentInteractable && newInteractable != currentInteractable)
                {
                    currentInteractable.DisableOutline();
                }
                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else
                {
                    DisableCurrentInteractable();
                }
            }
        }
        else
        {
            DisableCurrentInteractable();
        }
    }

    void SetNewCurrentInteractable(Interactable_Items newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
        Item_Name.instance.EnableInteractionText(currentInteractable.message);
    }

    void DisableCurrentInteractable()
    {
        Item_Name.instance.DisableInteractionText();
        if (currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }
}
