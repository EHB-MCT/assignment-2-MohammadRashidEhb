using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Movement Bounds (X Axis)")]
    [SerializeField] private float leftLimit = -2f;
    [SerializeField] private float rightLimit = 2f;

    [Header("Hard Mode Only")]
    [SerializeField] private bool activateOnlyOnHard = true;

    private Vector3 direction = Vector3.right;
    private bool isHardMode = false;

    private void Start()
    {
        isHardMode = PlayerPrefs.GetString("difficulty") == "hard";

        if (activateOnlyOnHard && !isHardMode)
        {
            enabled = false;
        }
    }

    private void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Reverse direction at limits
        if (transform.position.x > rightLimit)
        {
            transform.position = new Vector3(rightLimit, transform.position.y, transform.position.z);
            direction = Vector3.left;
        }
        else if (transform.position.x < leftLimit)
        {
            transform.position = new Vector3(leftLimit, transform.position.y, transform.position.z);
            direction = Vector3.right;
        }
    }
}
