using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// The user's input for the Player movement (WASD)
    /// </summary>
    public InputAction MovementAction;

    /// <summary>
    /// The speed of the Player's movement
    /// </summary>
    public float moveSpeed = 5.0f;

    /// <summary>
    /// The user's input for looking around with the mouse
    /// </summary>
    public InputAction LookAction;

    /// <summary>
    /// How quickly to move the camera
    /// </summary>
    public float lookSensitivity = 175.0f;

    /// <summary>
    /// The camera looking over the Player's shoulder
    /// </summary>
    public Transform cameraTransform;

    /// <summary>
    /// The user's input for jumping
    /// </summary>
    public InputAction JumpAction;

    /// <summary>
    /// The force with which the Player jumps
    /// </summary>
    public float jumpForce = 3.0f;

    /// <summary>
    /// Check to see if the user is attempting to jump
    /// </summary>
    bool isJumping;

    /// <summary>
    /// Check to see if the Player should jump
    /// </summary>
    bool jumpBuffered;

    /// <summary>
    /// Angle of the camera (looking up/down)
    /// </summary>
    float pitch;

    /// <summary>
    /// The Player's rigidbody
    /// </summary>
    Rigidbody rb;

    /// <summary>
    /// The animator for the Player's movements
    /// </summary>
    Animator animator;

    /// <summary>
    /// The movement input by the user
    /// </summary>
    Vector2 move;

    /// <summary>
    /// The mouses movement
    /// </summary>
    Vector2 look;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Enable user inputs
        MovementAction.Enable();
        LookAction.Enable();
        JumpAction.Enable();

        // Get components from Player character for private variables
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Lock the cursor (make it invisible)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        // Player movement values
        move = MovementAction.ReadValue<Vector2>();
        look = LookAction.ReadValue<Vector2>();

        // Rotation
        transform.Rotate(Vector3.up * lookSensitivity * look.x * Time.deltaTime);
        pitch -= look.y * lookSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -60.0f, 60.0f);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 90.0f, 0.0f);

        // Jumping
        isJumping = JumpAction.ReadValue<float>() > 0.0f;
        if (isJumping)
        {
            jumpBuffered = true;
            animator.SetBool("Jumping", true);
        }
        else
        {
            animator.SetBool("Jumping", false);
        }
    }

    private void FixedUpdate()
    {
        // Player movement
        // Control player's speed (Sprint when holding shift)
        if (Keyboard.current.leftShiftKey.isPressed)
        {
            moveSpeed = 10.0f;
        }
        else
        {
            moveSpeed = 5.0f;
        }

        // Get input from Player, use it to move in direction facing
        Vector3 input = new Vector3(move.y, 0.0f, move.x * -1.0f);
        Vector3 moveDirection = transform.TransformDirection(input);
        Vector3 targetPosition = rb.position + moveDirection * moveSpeed * Time.deltaTime;
        rb.MovePosition(targetPosition);
        animator.SetFloat("Speed", move.magnitude);

        //Jump
        bool isGrounded = CheckGrounded();
        if (isGrounded && jumpBuffered)
        {
            // Trigger the jumping animation (causes PerformJump() to be called)
            animator.SetTrigger("StartJump");
            jumpBuffered = false;
        }
    }

    /// <summary>
    /// Check if the Player is on the ground or not
    /// </summary>
    /// <returns>Returns true if the Player is on the ground, false otherwise</returns>
    bool CheckGrounded()
    {
        Collider col = GetComponent<Collider>();
        Vector3 origin = col.bounds.center - new Vector3(0.0f, col.bounds.extents.y, 0.0f);
        return Physics.Raycast(origin, Vector3.down, 0.2f);
    }

    /// <summary>
    /// Cause the Player to jump upwards
    /// </summary>
    public void PerformJump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.ResetTrigger("StartJump");
    }
}
