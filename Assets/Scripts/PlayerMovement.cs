using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f, jumpForce = 5f;

    private Animator playerAnimator;

    private PlayerControls playerControls;

    private Rigidbody2D rb;

    // Jump
    private bool isGrounded = false;
    public float rememberGroundedFor;
    private float lastTimeGrounded;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Update()
    {
        Movement();
        Jump();
        BetterJump();
        CheckIfGrounded();
    }

    private void Movement()
    {
        float moveInput = playerControls.Player.MoveLeftRight.ReadValue<float>();

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (moveInput  != 0)
            playerAnimator.SetBool("Move", true);
        else
            playerAnimator.SetBool("Move", false);
    }

    private void Jump()
    {
        if (playerControls.Player.Jump.triggered && (isGrounded || 
            Time.time - rememberGroundedFor <= lastTimeGrounded))
        {
            playerAnimator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && playerControls.Player.Jump.phase == InputActionPhase.Waiting)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);

        if (collider != null)
        {
            isGrounded = true;
        }
        else
        {
            if (isGrounded)
                lastTimeGrounded = Time.time;
            isGrounded = false;
        }
    }
}
