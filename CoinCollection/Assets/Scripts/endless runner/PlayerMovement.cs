using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public Animator playerAnimator;
    public float playerSpeed = 10f;
    public float horizontalSpeed = 10f;
    public float rightLimit = 5.5f;
    public float leftLimit = -5.5f;

    [Header("Jump Settings")]
    public float jumpForce = 7f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private Animator anim;
    private bool isGrounded;

    // Input System
    private RunnerInputActions inputActions;
    private float horizontalInput;
    private bool jumpPressed;

    void Awake()
    {
        inputActions = new RunnerInputActions();

        // Listen for jump action
        inputActions.Player.Jump.performed += ctx => jumpPressed = true;
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Start()
    {
        string difficulty = PlayerPrefs.GetString("difficulty", "medium");

        switch (difficulty)
        {
            case "easy":
                playerSpeed = 8f;
                break;
            case "medium":
                playerSpeed = 12f;
                break;
            case "hard":
                playerSpeed = 18f;
                break;
        }

        rb = GetComponent<Rigidbody>();
        anim = playerAnimator;

        if (anim == null)
        {
            Debug.LogError("Animator not found!");
        }
    }

    void Update()
    {
        // Move forward constantly
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);

        // Get horizontal input from Input System
        horizontalInput = inputActions.Player.Move.ReadValue<Vector2>().x;

        // Left and right movement (clamped)
        float newX = Mathf.Clamp(transform.position.x + horizontalInput * Time.deltaTime * horizontalSpeed, leftLimit, rightLimit);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Jump
        if (jumpPressed && isGrounded)
        {
            jumpPressed = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("Jump");
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
