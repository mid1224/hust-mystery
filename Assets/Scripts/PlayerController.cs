using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private InputSystem_Actions input;
    private SoundManager soundManager;
    private Flashlight flashlight;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] bool isMoving;
    [SerializeField] Vector2 moveDirection;
    private Vector2 lastFacingDirection = Vector2.down;

    public bool disableMovement;

    [Header("Jump Settings")]
    [SerializeField] float jumpDuration = 0.5f;
    [SerializeField] float jumpHeight = 1.5f;
    private bool isJumping;

    [Header("Animation")]
    [SerializeField] Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
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
        if (isJumping) return;

        HandleMovement();
        HandleFlashlight();
        AnimateMovement();
    }

    private void FixedUpdate()
    {
        if (isJumping) return;

        Move();
    }

    #region Movement
    private void HandleMovement()
    {
        if (disableMovement == true)
        {
            moveDirection = Vector2.zero;
            isMoving = false;
            return;
        }

        moveDirection = input.Player.Move.ReadValue<Vector2>();

        if (moveDirection.sqrMagnitude > 1f)
        {
            moveDirection = moveDirection.normalized;
        }

        isMoving = moveDirection.sqrMagnitude > 0.01f;

        if (isMoving)
        {
            lastFacingDirection = moveDirection.normalized;
            soundManager.PlayFootsteps();
        }
    }

    private void Move()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    #endregion

    #region Jump Mechanic

    // --> CHANGED: Renamed method and hardcoded Vector2.left <--
    public void JumpLeft(float jumpDistance)
    {
        if (!isJumping)
        {
            // Calculate the landing spot: Current Position + (Left * Distance)
            Vector2 targetPosition = (Vector2)transform.position + (Vector2.left * jumpDistance);
            StartCoroutine(JumpRoutine(targetPosition));
        }
    }

    private IEnumerator JumpRoutine(Vector2 targetPosition)
    {
        isJumping = true;
        DisableMovement();
        rb.linearVelocity = Vector2.zero;

        if (playerCollider != null) playerCollider.enabled = false;

        animator.SetBool("IsJumping", true);

        Vector2 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < jumpDuration)
        {
            float t = elapsedTime / jumpDuration;

            Vector2 currentBasePos = Vector2.Lerp(startPosition, targetPosition, t);
            float heightOffset = 4f * jumpHeight * t * (1f - t);

            transform.position = new Vector3(currentBasePos.x, currentBasePos.y + heightOffset, transform.position.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);

        animator.SetBool("IsJumping", false);
        if (playerCollider != null) playerCollider.enabled = true;

        isJumping = false;
        EnableMovement();
    }
    #endregion

    #region Flashlight
    private void HandleFlashlight()
    {
        if (input.Player.ToggleFlashlight.WasPressedThisFrame())
        {
            flashlight.ToggleFlashlight();
        }

        flashlight.AimWithMouse();
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

    public void DisableMovement()
    {
        disableMovement = true;
    }
    public void EnableMovement()
    {
        disableMovement = false;
    }
}