using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public Transform firePoint;
    public Vector2 grappleTargetPos;

    [SerializeField]
    private GrappleRope grappleRope;

    [SerializeField]
    private LayerMask grappleAble;

    [SerializeField]
    private SpringJoint2D springJoint;

    [SerializeField]
    private float launchSpeed = 1f;

    private bool grappling = false;

    void Start()
    {
        grappleRope.enabled = false;
        springJoint.enabled = false;
    }

    void Update()
    {

    }

    public void SetGrapplePoint(Vector3 mousePosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, (mousePosition - firePoint.position).normalized, 50, grappleAble);
        if (hit.collider != null)
        {
            grappleTargetPos = hit.point;
            grappleRope.enabled = true;
        }
        else
        {
            Debug.DrawRay(firePoint.position, Vector2.right, Color.red, 1);
        }
    }

    public void StopGrappling()
    {
        grappling = false;
        grappleRope.enabled = false;
        springJoint.enabled = false;
    }

    public void StartGrapple()
    {
        springJoint.connectedAnchor = grappleTargetPos;
        springJoint.distance = 1;
        springJoint.frequency = launchSpeed;
        springJoint.enabled = true;
        grappling = true;
    }
}
