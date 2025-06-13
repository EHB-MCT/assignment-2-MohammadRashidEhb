using UnityEngine;

public class ClearPlayerPrefsOnStart : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs cleared at start of scene.");
    }
}
