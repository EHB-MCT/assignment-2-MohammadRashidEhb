using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartMenuController : MonoBehaviour
{
    [Header("Panels & Buttons")]
    [SerializeField] private GameObject startMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject backButton;

    [Header("Scene Names")]
    [SerializeField] private string startSceneName = "City 2";
    [SerializeField] private string returnToCitySceneName = "City 2";

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    public void StartGame()
{
    if (SceneManager.GetActiveScene().name == "LinkUpMenu")
    {
        BuildingTracker.ResetProgress();
        PlayerPositionTracker.Reset(); // <-- Critical line
        Debug.Log("Progress and spawn position reset");
    }

    SceneTransitionController.Instance.FadeAndLoadScene(startSceneName);
}


    public void ShowOptions()
    {
        startMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(backButton);
    }

    public void HideOptions()
    {
        optionsPanel.SetActive(false);
        startMenuPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    public void ReturnToCity()
    {
        SceneTransitionController.Instance.FadeAndLoadScene(returnToCitySceneName);
    }
}
