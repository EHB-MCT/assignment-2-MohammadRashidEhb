using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    public static int itemCount = 0;
    [SerializeField] GameObject collectedItems;

    private TMPro.TMP_Text _text;

    // Use Awake instead of Start to ensure it runs before Update
    void Awake()
    {
        // Add null check for collectedItems
        if (collectedItems != null)
        {
            _text = collectedItems.GetComponent<TMPro.TMP_Text>();
            
            // Additional null check for the component
            if (_text == null)
            {
                Debug.LogError("TMP_Text component not found on " + collectedItems.name);
            }
        }
        else
        {
            Debug.LogError("CollectedItems GameObject is not assigned in the inspector!");
        }
    }

    // Reset static variable when the object is enabled (scene load)
    void OnEnable()
    {
        itemCount = 0;
    }

    void Update()
    {
        // Add null check before using _text
        if (_text != null)
        {
            _text.text = "ITEMS: " + itemCount;
        }
    }
}
