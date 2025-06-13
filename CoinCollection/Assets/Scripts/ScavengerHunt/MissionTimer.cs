using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MissionTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float totalTime = 120f;
    private float timeRemaining;
    private bool timerRunning = false;

    [Header("UI")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject failScreen;
    [SerializeField] private GameObject tryAgainButton; // Assign your Try Again button here in Inspector

    [Header("Player")]
    [SerializeField] private GameObject playerObject; // Assign your player GameObject here in Inspector

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        timeRemaining = totalTime;
        timerRunning = true;
        UpdateTimerUI();

        if (failScreen != null)
            failScreen.SetActive(false);
    }

    private void Update()
    {
        if (!timerRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerRunning = false;
            TimerEnded();
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"Time Left: {minutes:00}:{seconds:00}";
    }

    private void TimerEnded()
{
    Debug.Log("Time is up! Mission failed.");
    timerText.text = "Time's Up!";

    // Enable fail screen
    if (failScreen != null)
        failScreen.SetActive(true);

    // Unlock and show cursor
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;

    // Find and disable player movement (automatically find PlayerCapsule)
    GameObject playerCapsule = GameObject.Find("PlayerCapsule");
    if (playerCapsule != null)
    {
        // Disable movement script (replace "YourMovementScriptName" with actual script name)
        var moveScript = playerCapsule.GetComponent<MonoBehaviour>(); // Generic approach
        if (moveScript != null)
            moveScript.enabled = false;
    }

    // Set Try Again button as selected
    if (tryAgainButton != null)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(tryAgainButton);
    }
}


    public void RetryMission()
    {
        Time.timeScale = 1f; // Resume game time in case it's paused
        SceneManager.LoadScene("ScavengerHunt");
    }

    public void StopTimer() => timerRunning = false;
    public void StartTimer() => timerRunning = true;
}
