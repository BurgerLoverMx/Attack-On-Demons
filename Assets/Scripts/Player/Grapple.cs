using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    private PlayerBase player;

    private Rigidbody2D rb;
    
    [Header("Grapple Settings")]
    [SerializeField] 
    private LayerMask grappleAble;

    [SerializeField] 
    private Transform grapplePoint;

    [SerializeField] 
    private float grappleRange = 10, grappleSpeed = 40, grappleAnimationSpeed = 30;

    [SerializeField] 
    private float minDistanceToSwapMaterial = 3.0f, multiplierToSwapMaterial = 0.25f, checkGrappleHitRadius = 0.0001f;

    private bool grappling = false;
    private Vector3 grappleTargetPos, grappleTargetDirection;
    private GameObject grappleTargetObject;
    private Vector3 grappleTargetObjectLastPosition = Vector3.zero;
    private LineRenderer grappleLine;
    private bool startGrappling = false, stopGrappling = false;
    private float initialDistance, startTime, length;

    [SerializeField] private PhysicsMaterial2D[] physicMaterials;

    void Start()
    {
        player = PlayerBase.Instance;
        rb = GetComponent<Rigidbody2D>();
        grappleLine = GetComponent<LineRenderer>();
        player.playerControls.Player.Grapple.canceled += ctx => StopGrappling();
    }

    void Update()
    {
        StartGrapple();

        if (grappleLine.enabled)
        {
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
    }

    private void FixedUpdate()
    {
        if (grappling)
        {
            Grappling();
        }
    }

    private void DrawRope()
    {
        float distCovered = ((Time.time - startTime) * grappleAnimationSpeed) / length;
        Vector2 position = Vector2.Lerp(grapplePoint.position, grappleTargetPos, distCovered);
        grappleLine.SetPosition(1, position);

        Vector3 lineCastOrigin = grappleLine.GetPosition(1) - (grappleTargetDirection * checkGrappleHitRadius); 
        RaycastHit2D hit = Physics2D.Linecast(lineCastOrigin, grappleLine.GetPosition(1), grappleAble);

        if (hit.collider != null)
        {
            grappleTargetObject = hit.collider.gameObject;
            grappleLine.SetPosition(1, hit.point);
            startGrappling = false;
            player.playerMovement.blockMovement = true;
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
        if(grappleTargetObject != null)
        {
            if (grappleTargetObject.tag == "Enemy")
            {
                if (grappleTargetObject.GetComponent<Enemy>().enabled == false)
                {
                    grappleTargetObject = null;
                    return;
                }
                if (grappleTargetObjectLastPosition != Vector3.zero)
                {
                    Vector2 position = grappleLine.GetPosition(1) + (grappleTargetObject.transform.position - grappleTargetObjectLastPosition);
                    grappleLine.SetPosition(1, position);
                }
            }
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

            grappleTargetObjectLastPosition = grappleTargetObject.transform.position;
        }

        else
        {
            StopGrappling();
        }
    }

    private void StartGrapple()
    {
        if (player.playerControls.Player.Grapple.triggered && !stopGrappling)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(player.playerControls.Player.Aim.ReadValue<Vector2>());
            Debug.Log(mousePosition);
            mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);
            Vector3 direction = (mousePosition - grapplePoint.position).normalized;
            direction.z = 0;
            grappleTargetDirection = direction;

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
        if (startGrappling || grappling)
        {
            player.playerMovement.blockMovement = false;
            grappling = false;
            startGrappling = false;
            stopGrappling = true;
            rb.gravityScale = 1f;
            rb.sharedMaterial = physicMaterials[0];
            grappleTargetObjectLastPosition = Vector3.zero;
            startTime = Time.time;
        }
    }
}
