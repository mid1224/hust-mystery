using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private InputSystem_Actions input;
    private SoundManager soundManager;
    private Flashlight flashlight;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] bool isMoving;
    [SerializeField] Vector2 moveDirection;

    [Header("Animation")]
    [SerializeField] Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = new InputSystem_Actions();

        soundManager = FindFirstObjectByType<SoundManager>();
        flashlight = FindFirstObjectByType<Flashlight>();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Update()
    {
        HandleMovement();
        HandleFlashlight();

        AnimateMovement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    #region Movement
    private void HandleMovement()
    {
        moveDirection = input.Player.Move.ReadValue<Vector2>();

        if (moveDirection.sqrMagnitude > 1f)
        {
            moveDirection = moveDirection.normalized;
        }

        isMoving = moveDirection.sqrMagnitude > 0.01f;

        if (isMoving)
        {
            soundManager.PlayFootsteps();
        }
    }

    private void Move()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
        //Debug.Log($"Move Direction: {moveDirection}, Velocity: {rb.linearVelocity}");
    }

    #endregion

    #region Flashlight
    private void HandleFlashlight()
    {
        if (input.Player.ToggleFlashlight.WasPressedThisFrame())
        {
            flashlight.ToggleFlashlight();
        }

        if (isMoving)
        {
            flashlight.SetFlashlightDirection(moveDirection);
        }
    }
    #endregion

    #region Animation
    private void AnimateMovement()
    {
        if (isMoving == true)
        {
            animator.SetFloat("MoveX", moveDirection.x);
            animator.SetFloat("MoveY", moveDirection.y);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }
    #endregion
}
