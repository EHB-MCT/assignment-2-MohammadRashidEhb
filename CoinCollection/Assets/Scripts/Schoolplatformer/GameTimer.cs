using UnityEngine;
using TMPro; // Make sure to use TextMeshPro

public class GameTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private TextMeshProUGUI timerText; // Use TextMeshProUGUI
    [SerializeField] private bool countUp = true; // true = count up, false = count down
    [SerializeField] private float countdownTime = 60f; // Only used if countUp = false

    [Header("Timer State")]
    public bool isRunning = false;
    public bool hasFinished = false;

    private float startTime;
    private float currentTime;

    public static GameTimer Instance; // Singleton for easy access

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (countUp)
        {
            currentTime = 0f;
        }
        else
        {
            currentTime = countdownTime;
        }
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (!isRunning || hasFinished) return;

        if (countUp)
        {
            currentTime = Time.time - startTime;
        }
        else
        {
            currentTime = countdownTime - (Time.time - startTime);
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                FinishTimer();
            }
        }

        UpdateTimerDisplay();
    }

    public void StartTimer()
    {
        if (!isRunning && !hasFinished)
        {
            Debug.Log("[GameTimer] Timer started!");
            isRunning = true;
            startTime = Time.time;

            if (countUp)
            {
                currentTime = 0f;
            }
        }
    }

    public void StopTimer()
    {
        if (isRunning)
        {
            Debug.Log($"[GameTimer] Timer stopped! Final time: {GetFormattedTime()}");
            isRunning = false;
        }
    }

    public void FinishTimer()
    {
        Debug.Log($"[GameTimer] Timer finished! Final time: {GetFormattedTime()}");
        isRunning = false;
        hasFinished = true;

        if (timerText != null)
        {
            timerText.color = Color.yellow; // Change color when finished
        }
    }

    public void ResetTimer()
    {
        Debug.Log("[GameTimer] Timer reset!");
        isRunning = false;
        hasFinished = false;

        if (countUp)
        {
            currentTime = 0f;
        }
        else
        {
            currentTime = countdownTime;
        }

        if (timerText != null)
        {
            timerText.color = Color.white; // Reset color
        }

        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            // Only the timer digits are monospaced; the label is normal
            timerText.text = $"TIME: <mspace=0.6em>{GetFormattedTime()}</mspace>";
        }
    }

    // In GameTimer.cs
public string GetFormattedTime()
{
    int minutes = Mathf.FloorToInt(currentTime / 60f);
    int seconds = Mathf.FloorToInt(currentTime % 60f);
    int milliseconds = Mathf.FloorToInt(currentTime % 1f * 100f);
    return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
}

    public float GetCurrentTime()
    {
        return currentTime;
    }
}
