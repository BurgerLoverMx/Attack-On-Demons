using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerBase))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerBase player;

    // Movement
    [SerializeField]
    private float moveSpeed = 10f, jumpForce = 5f, maxVelocity = 20;

    private Animator playerAnimator;

    private Rigidbody2D rb;

    private CameraFollow cameraFollow;

    // Jump
    private bool isGrounded = false;
    public float rememberGroundedFor;
    private float lastTimeGrounded;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    // Grapple
    public LayerMask grappleAble;
    private bool grappling = false;
    public Transform grapplePoint;
    private Vector3 grappleTargetPos;

    private LineRenderer grappleLine;
    private bool startGrappling = false, stopGrappling = false, validGrappleTarget;

    [SerializeField]
    private float minDistanceToSwapMaterial = 3.0f, multiplierToSwapMaterial = 0.25f, grappleRange = 10, grappleSpeed = 40, grappleAnimationSpeed = 30, checkGrappleHitRadius = 0.0001f;

    private float initialDistance, startTime, length;

    [SerializeField]
    private PhysicsMaterial2D[] physicMaterials;

    void Start()
    {
        player = PlayerBase.Instance;
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponentInChildren<Animator>();
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        grappleLine = GetComponent<LineRenderer>();
        player.playerControls.Player.Grapple.canceled += ctx => StopGrappling();
    }

    void Update()
    {
        CheckVelocity();
        Movement();
        CheckIfGrounded();
        Jump();
        BetterJump();
        StartGrapple();

        grappleLine.SetPosition(0, grapplePoint.position);
        if (startGrappling)
        {
            DrawRope();
        }
        if (stopGrappling)
        {
            PullRopeBack();
        }
    }

    private void FixedUpdate()
    {
        if (grappling)
        {
            Grappling();
        }
    }


    private void CheckVelocity()
    {
        float xVelocity = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float yVelocity = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }

    private void DrawRope()
    {
        float distCovered = ((Time.time - startTime) * grappleAnimationSpeed) / length;
        Vector2 position = Vector2.Lerp(grapplePoint.position, grappleTargetPos, distCovered);
        grappleLine.SetPosition(1, position);

        Collider2D collider = Physics2D.OverlapCircle(grappleLine.GetPosition(1), 0.05f, grappleAble);

        if (collider != null)
        {
            startGrappling = false;
            grappling = true;
            rb.gravityScale = 0;
            initialDistance = Vector2.Distance(grappleLine.GetPosition(1), grapplePoint.position);
        }

        if (grappleLine.GetPosition(1).Equals(grappleTargetPos))
        { 
            StopGrappling();
        }
    }

    private void PullRopeBack()
    {
        float distCovered = ((Time.time - startTime) * grappleAnimationSpeed) / length;
        Vector2 position = Vector2.Lerp(grappleLine.GetPosition(1), grapplePoint.position, distCovered);
        grappleLine.SetPosition(1, position);
        if (grappleLine.GetPosition(1).Equals(grapplePoint.position))
        {
            grappleLine.enabled = false;
            stopGrappling = false;
        }
    }


    private void Grappling()
    {
        Vector3 distance = grappleLine.GetPosition(1) - grapplePoint.position;
        if (initialDistance < minDistanceToSwapMaterial ||
            distance.sqrMagnitude < initialDistance * initialDistance * multiplierToSwapMaterial)
        {
            rb.sharedMaterial = physicMaterials[1];
        }
        else
        {
            rb.sharedMaterial = physicMaterials[0];
        }
        Vector2 direction = (grappleLine.GetPosition(1) - grapplePoint.position).normalized;
        rb.AddForce(direction * grappleSpeed, ForceMode2D.Force);
    }

    private void StartGrapple()
    {
        if (player.playerControls.Player.Grapple.triggered && !stopGrappling)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(player.playerControls.Player.Aim.ReadValue<Vector2>());
            mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);
            Vector3 direction = (mousePosition - grapplePoint.position).normalized;
            direction.z = 0;

            grappleLine.enabled = true;
            grappleLine.SetPosition(0, grapplePoint.position);
            grappleLine.SetPosition(1, grapplePoint.position);
            startGrappling = true;
            stopGrappling = false;
            
            length = grappleRange;
            grappleTargetPos = grapplePoint.position + (direction * grappleRange);

            startTime = Time.time;
        }
    }

    private void StopGrappling()
    {
        grappling = false;
        startGrappling = false;
        stopGrappling = true;
        rb.gravityScale = 1f;
        rb.sharedMaterial = physicMaterials[0];
        startTime = Time.time;
    }

    private void Movement()
    {
        float moveInput = player.playerControls.Player.MoveLeftRight.ReadValue<float>();

        if (!grappling)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }

        //TBD
        /*
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
        */
    }

    private void Jump()
    {
        //can jump while grappling?
        if (player.playerControls.Player.Jump.triggered && (isGrounded || 
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
        else if (rb.velocity.y > 0 && player.playerControls.Player.Jump.phase == InputActionPhase.Waiting)
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
