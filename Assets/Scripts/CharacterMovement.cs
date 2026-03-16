using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody))] // Ensures that a Rigidbody component is attached to the GameObject
public class CharacterMovement : MonoBehaviour
{
    // ============================== Boosters Settings =================================
    [Header("Boosters Settings")]
    // [SerializeField] private float jumpBoostForce = 10f;    
    [SerializeField] private float jumpBoostForce = 9f;    
    [SerializeField] private float speedBoostMultiplier = 9f;    

    // ============================== Movement Settings ==============================
    [Header("Movement Settings")]
    [SerializeField] private float baseWalkSpeed = 5f;    // Base speed when walking
    [SerializeField] private float baseRunSpeed = 8f;     // Base speed when running
    [SerializeField] private float rotationSpeed = 10f;   // Speed at which the character rotates

    // ============================== Jump Settings =================================
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 5f;        // Jump force applied to the character
    [SerializeField] private float groundCheckDistance = 0.3f; // Distance to check for ground contact (Raycast)

    // ============================== Modifiable from other scripts ==================
    public float speedMultiplier = 1.0f; // Additional multiplier for character speed ( WINK WINK )

    // ============================== Private Variables ==============================
    private Rigidbody rb; // Reference to the Rigidbody component
    private Transform cameraTransform; // Reference to the camera's transform

    // Input variables
    private float moveX; // Stores horizontal movement input (A/D or Left/Right Arrow)
    private float moveZ; // Stores vertical movement input (W/S or Up/Down Arrow)
    private bool jumpRequest; // Flag to check if the player requested a jump
    private Vector3 moveDirection; // Stores the calculated movement direction
    private Text scoreText;
    
    // Boosters Variables:
    private bool isJumpBoosted = false;
    private bool didDoubleJump = false; 
    private bool doubleJumpRequested = false;

    // ============================== Animation Variables ==============================
    [Header("Anim values")]
    public float groundSpeed; // Speed value used for animations

    // ============================== Character State Properties ==============================
    /// <summary>
    /// Checks if the character is currently grounded using a Raycast.
    /// If false, the character is in the air.
    /// </summary>
    public bool IsGrounded => 
        Physics.Raycast(transform.position + Vector3.up * 0.01f, Vector3.down, groundCheckDistance);

    /// <summary>
    /// Checks if the player is currently holding the "Run" button.
    /// </summary>
    private bool IsRunning => Input.GetButton("Run");

    // ============================== Unity Built-in Methods ==============================

    /// <summary>
    /// Called when the script is first initialized.
    /// </summary>
    private void Awake()
    {
        InitializeComponents(); // Initialize Rigidbody and Camera reference
    }

    /// <summary>
    /// Called every frame, used to register player input.
    /// </summary>
    private void Update()
    {
        RegisterInput(); // Collect player input
    }

    /// <summary>
    /// Called every physics update (FixedUpdate ensures physics stability).
    /// </summary>
    private void FixedUpdate()
    {
        HandleMovement(); // Process movement and physics-based updates
    }

    // ============================== Initialization ==============================

    /// <summary>
    /// Initializes Rigidbody and camera reference.
    /// Also locks and hides the cursor for better control.
    /// </summary>
    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        rb.freezeRotation = true; // Prevent Rigidbody from rotating due to physics interactions
        rb.interpolation = RigidbodyInterpolation.Interpolate; // Smooth physics interpolation
        scoreText = GameObject.FindWithTag("Score").GetComponent<Text>();

        // Assign the main camera if available
        if (Camera.main)
            cameraTransform = Camera.main.transform;

        // Lock and hide the cursor for better gameplay control
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // ============================== Input Handling ==============================

    /// <summary>
    /// Reads player input values and registers movement/jump requests.
    /// </summary>
    private void RegisterInput()
    {
        moveX = Input.GetAxis("Horizontal"); // Get horizontal movement input
        moveZ = Input.GetAxis("Vertical");   // Get vertical movement input

        // Register a jump request if the player presses the Jump button
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded)
            {
                jumpRequest = true;
                didDoubleJump = false;
            }
            else if (isJumpBoosted && !didDoubleJump)
            {
                doubleJumpRequested = true;
                didDoubleJump = true;
            }
        }
    }

    // ============================== Movement Handling ==============================

    /// <summary>
    /// Handles movement-related logic: calculating direction, jumping, rotating, and moving.
    /// </summary>
    private void HandleMovement()
    {
        CalculateMoveDirection(); // Compute the movement direction based on input
        HandleJump(); // Process jump input
        RotateCharacter(); // Rotate the character towards the movement direction
        MoveCharacter(); // Move the character using velocity-based movement
    }

    /// <summary>
    /// Calculates the movement direction based on player input and camera orientation.
    /// </summary>
    private void CalculateMoveDirection()
    {
        // If the camera is not assigned, move based on world space
        if (!cameraTransform)
        {
            moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        }
        else
        {
            // Get forward and right vectors from the camera perspective
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            // Ignore Y-axis movement to prevent unwanted tilting
            forward.y = 0f;
            right.y = 0f;

            // Normalize vectors to maintain consistent movement speed
            forward.Normalize();
            right.Normalize();

            // Calculate movement direction relative to the camera orientation
            moveDirection = (forward * moveZ + right * moveX).normalized;
        }
    }

    /// <summary>
    /// Handles jumping by applying an impulse force if the character is grounded.
    /// </summary>
    private void HandleJump()
    {
        // Apply jump force only if jump was requested and the character is grounded
        if (jumpRequest && IsGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply force upwards
            jumpRequest = false; // Reset jump request after applying jump
        }

        if (doubleJumpRequested)
        {
            rb.AddForce(Vector3.up * jumpBoostForce, ForceMode.Impulse); // Apply double jump force
            doubleJumpRequested = false; // Reset double jump request after applying boost
        }
    }

    /// <summary>
    /// Rotates the character towards the movement direction.
    /// </summary>
    private void RotateCharacter()
    {
        // Rotate only if the character is moving
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// Moves the character using Rigidbody's velocity instead of MovePosition.
    /// This ensures smooth movement while avoiding physics conflicts.
    /// </summary>
    private void MoveCharacter()
    {
        // Determine movement speed (walking or running)
        float speed = IsRunning ? baseRunSpeed : baseWalkSpeed;
        
        // Set ground speed value for animation purposes
        groundSpeed = (moveDirection != Vector3.zero) ? speed : 0.0f;

        // Preserve the current Y velocity to maintain gravity effects
        Vector3 newVelocity = new Vector3(
            moveDirection.x * speed * speedMultiplier, 
            rb.linearVelocity.y, // Keep the existing Y velocity for jumping & gravity
            moveDirection.z * speed * speedMultiplier
        );

        // Apply the new velocity directly
        rb.linearVelocity = newVelocity;
        Debug.Log(IsGrounded);
        Debug.DrawRay(transform.position, Vector3.down * 2f, Color.red);
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if(other.gameObject.tag == "ScoreAdd")
    //     {
    //         score += 50;
    //         scoreText.text = "Points: " + score;
    //         other.gameObject.SetActive(false);
    //         //more code according to instructions
    //     }
    //     if(other.gameObject.tag == "ScoreSubtract")
    //     {
    //         score--;
    //         scoreText.text = "Points: " + score;
    //         other.gameObject.SetActive(false);
    //         //more code according to instructions
    //     }
    //     if(other.gameObject.tag == "JumpBooster")
    //     {
    //         // Increase jump force
    //          // Reset jump force after 5 seconds
    //     }
    //     if(other.gameObject.tag == "SpeedBooster")
    //     {
    //         // Increase speed multiplier
    //         // Invoke("ResetSpeedMultiplier", 5f); 
    //         // Reset speed multiplier after 5 seconds
    //     }
    // }
    
    /***
        * This method is called by the BoostersPickupController when a booster is picked up.
        * It updates the score display and applies the appropriate effects based on the booster type.
        * The actual implementation of applying jump and speed boosts will be added according to the instructions,
        * depending if it is to add or subtract points.
        ***/
    private void OnCollisionEnter(Collision other)
    {
            // Debug.Log("Collision detected with: " + other.gameObject.name);

        // if(other.gameObject.tag == "DeathPlane")
        // {
        //     GameManager.Instance.ResetGame();
        // } this is now in levels manager script
        if(other.gameObject.tag == "ScoreAdd" || other.gameObject.tag == "ScoreSubtract" || other.gameObject.tag == "JumpBooster" || other.gameObject.tag == "SpeedBooster")
        {
            BoostersPickupController boostManager = other.gameObject.GetComponent<BoostersPickupController>();
            if (boostManager != null)
            {
                boostManager.GotHit(this);//pass script reference to use the method actions
            }
        }
        
    //Omg this is getting so tangled, comment comment commentttt (line to remove)
    }
    public void UpdateScoreText()
    {
        scoreText.text = "Points: " + GameManager.Instance.Score;
    }

    public void BoostSpeed()
    {
        speedMultiplier = speedBoostMultiplier; // Apply speed boost multiplier
        Invoke("ResetSpeedMultiplier", 5f); // Reset speed multiplier after 5 seconds
    }
    private void ResetSpeedMultiplier()
    {
        speedMultiplier = 1.0f;
    }
    public void BoostJump()
    {
        isJumpBoosted = true;
        didDoubleJump = false; 
        Invoke("ResetBoostJump", 30f); // Boost only 30 secs
    }

    public void ResetBoostJump()
    {
        isJumpBoosted = false;
        didDoubleJump = false; 
    }
}