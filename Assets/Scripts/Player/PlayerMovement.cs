using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerBase))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerBase player;

    // Movement
    [Header("Movement Settings")]
    [SerializeField] 
    private float moveSpeed = 10f;
    
    [SerializeField]
    private float maxVelocity = 20f;

    private Animator playerAnimator;
    private Rigidbody2D rb;
    private CameraFollow cameraFollow;

    [HideInInspector] public bool blockMovement = false;

    // Jump
    [Header("Jump Settings")]
    [SerializeField] 
    private LayerMask groundLayer;

    [SerializeField] 
    private Transform isGroundedChecker;

    [SerializeField] 
    private float jumpForce = 5f, checkGroundRadius, rememberGroundedFor, rememberJumpPressedFor = 0.1f;

    [SerializeField] 
    private float fallMultiplier = 2.5f, lowJumpMultiplier = 2f;
    
    private bool isGrounded = false;
    private float lastTimeGrounded;
    private float rememberJumpTime = 0;

    void Start()
    {
        player = PlayerBase.Instance;
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponentInChildren<Animator>();
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }

    void Update()
    {
        CheckVelocity();
        Movement();
        CheckIfGrounded();
        Jump();
        BetterJump();
    }

    private void CheckVelocity()
    {
        float xVelocity = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float yVelocity = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }

    private void Movement()
    {
        float moveInput = player.playerControls.Player.MoveLeftRight.ReadValue<float>();
        if (!blockMovement)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            //playerAnimator.SetBool("Move", rb.velocity.x != 0);
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

    private void Jump()
    {
        if (rememberJumpTime > 0)
        {
            rememberJumpTime -= Time.deltaTime;
        }

        if (player.playerControls.Player.Jump.triggered || rememberJumpTime > 0)
        {
            if (isGrounded || Time.time - rememberGroundedFor <= lastTimeGrounded)
            {
                playerAnimator.SetTrigger("Jump");
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            if (rememberJumpTime <= 0)
            {
                rememberJumpTime = rememberJumpPressedFor;
            }
        }
    }

    private void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && player.playerControls.Player.Jump.phase == InputActionPhase.Waiting)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
