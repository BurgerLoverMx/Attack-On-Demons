using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerBase))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerBase player;

    [SerializeField]
    private float moveSpeed = 10f, jumpForce = 5f, grappleRange = 50, minGrappleSpeed = 5, maxGrappleSpeed = 50, grappleDistanceUntilMaxSpeed = 10;

    private Animator playerAnimator;

    private Rigidbody2D rb;

    private CameraFollow cameraFollow;

    // Jump
    private bool isGrounded = false, grappling = false;
    public float rememberGroundedFor;
    private float lastTimeGrounded;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public LayerMask grappleAble;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    // Grapple
    public Transform grapplePoint;
    private Vector3 grappleTargetPos;
    private LineRenderer grappleLine;

    [SerializeField]
    private float minDistanceToSwapMaterial = 3.0f, multiplierToSwapMaterial = 0.25f, maxVelocity = 20;

    private float initialDistance;

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
        Movement();
        CheckIfGrounded();
        Jump();
        BetterJump();
        StartGrapple();
        CheckVelocity();
    }


    private void CheckVelocity()
    {
        Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);
    }

    private void FixedUpdate()
    {
        if (grappling)
        {
            grappleLine.enabled = true;
            Grappling();
        }
        else
        {
            grappleLine.enabled = false;
        }
    }


    private void Grappling()
    {
        if(grappleTargetPos != null)
        {
            Vector3 distance = grappleTargetPos - grapplePoint.position;
            if (initialDistance < minDistanceToSwapMaterial ||
                distance.sqrMagnitude < initialDistance * initialDistance * multiplierToSwapMaterial)
            {
                rb.sharedMaterial = physicMaterials[1];
            }
            else
            {
                rb.sharedMaterial = physicMaterials[0];
            }

            Vector2 direction = (grappleTargetPos - grapplePoint.position).normalized;
            Debug.DrawLine(grapplePoint.position, grappleTargetPos, Color.green, 1);
            float sqrLength = distance.sqrMagnitude;
            sqrLength /= grappleDistanceUntilMaxSpeed;
            if(sqrLength < 1)
            {
                sqrLength = 1;
            }

            float force = 1 / sqrLength;

            float grappleForce = Mathf.Lerp(minGrappleSpeed, maxGrappleSpeed, force);
            rb.AddForce(direction * grappleForce, ForceMode2D.Force);

            //Render line
            grappleLine.SetPosition(0, grapplePoint.position);
            grappleLine.SetPosition(1, grappleTargetPos);
        }
        else
        {
            Debug.Log("grapple stop");
            grappling = false;
        }
    }

    private void StartGrapple()
    {
        if (player.playerControls.Player.Grapple.triggered)
        {
            Debug.Log("grapple start");
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(player.playerControls.Player.Aim.ReadValue<Vector2>());
            mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);
            Debug.Log("mouse pos: " + mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(grapplePoint.position, (mousePosition - grapplePoint.position).normalized, grappleRange, grappleAble);
            if (hit.collider != null)
            {
                grappling = true;
                grappleTargetPos = hit.point;
                Debug.DrawLine(grapplePoint.position, hit.point, Color.green, 1);
                rb.gravityScale = 0;
                initialDistance = Vector2.Distance(grappleTargetPos, grapplePoint.position);
                Debug.Log(initialDistance);
            }
            else
            {
                Debug.DrawRay(grapplePoint.position, Vector2.right, Color.red, 1);
            }
        }
    }

    private void StopGrappling()
    {
        Debug.Log("grapple stop");
        grappling = false;
        rb.gravityScale = 1f;
        rb.sharedMaterial = physicMaterials[0];
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
