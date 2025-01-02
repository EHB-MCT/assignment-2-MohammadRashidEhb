using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public CanvasGroup OptionPanel;

    public void PlayGame()
    {
        // Load the next scene in the build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Option()
    {
        // Show the options panel
        OptionPanel.alpha = 1;
        OptionPanel.blocksRaycasts = true;
        OptionPanel.interactable = true;
    }

    public void Back()
    {
        // Hide the options panel
        OptionPanel.alpha = 0;
        OptionPanel.blocksRaycasts = false;
        OptionPanel.interactable = false;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        // Show debug message when running in the editor
        Debug.Log("Quit Game called. Application.Quit() will work in a built application.");
#else
        // Quit the application
        Application.Quit();
#endif
    }
}
