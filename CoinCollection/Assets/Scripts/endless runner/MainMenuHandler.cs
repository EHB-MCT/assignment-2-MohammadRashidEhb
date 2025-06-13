using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuHandler : MonoBehaviour
{
    public GameObject firstSelectedButton;

    private void OnEnable()
    {
        if (firstSelectedButton != null)
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

    public void StartEasy() => SceneTransitionController.Instance.FadeAndLoadScene("Office", "easy");
    public void StartMedium() => SceneTransitionController.Instance.FadeAndLoadScene("Office", "medium");
    public void StartHard() => SceneTransitionController.Instance.FadeAndLoadScene("Office", "hard");
    public void ReturnToCity() => SceneTransitionController.Instance.FadeAndLoadScene("City 2");
}
