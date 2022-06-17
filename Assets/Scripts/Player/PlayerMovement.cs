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
    public Transform grapplePoint;


    private Vector3 grappleTargetPos;
    void Start()
    {
        player = PlayerBase.Instance;
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponentInChildren<Animator>();
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        player.playerControls.Player.Grapple.canceled += ctx => StopGrappling();
    }

    void Update()
    {
        Movement();
        CheckIfGrounded();
        Jump();
        BetterJump();
        StartGrapple(); 
    }

    private void FixedUpdate()
    {
        if (grappling)
        {
            Grappling();
        }
    }


    private void Grappling()
    {
        if(grappleTargetPos != null)
        {
            Debug.Log(grappleTargetPos);
            Debug.Log("target pos: " + grappleTargetPos + ", grapple point: " + grapplePoint.position);
            Vector2 direction = (grappleTargetPos - grapplePoint.position).normalized;

            Vector3 distance = grappleTargetPos - grapplePoint.position;
            float sqrLength = distance.sqrMagnitude;
            sqrLength /= grappleDistanceUntilMaxSpeed;
            if(sqrLength < 1)
            {
                sqrLength = 1;
            }

            float force = 1 / sqrLength;

            float grappleForce = Mathf.Lerp(minGrappleSpeed, maxGrappleSpeed, force);
            rb.AddForce(direction * grappleForce, ForceMode2D.Force);
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
                Debug.DrawLine(grapplePoint.position, hit.point, Color.green);
            }
            else
            {
                Debug.DrawRay(grapplePoint.position, Vector2.right, Color.red);
            }
        }
    }

    private void StopGrappling()
    {
        Debug.Log("grapple stop");
        grappling = false;
    }

    private void Movement()
    {
        float moveInput = player.playerControls.Player.MoveLeftRight.ReadValue<float>();

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

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
