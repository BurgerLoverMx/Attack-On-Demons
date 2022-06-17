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
        if (grappling)
        {
            RotateGun(grappleTargetPos);
        }
        else
        {
            RotateGun(PlayerBase.Instance.playerControls.Player.Aim.ReadValue<Vector2>());
        }
    }

    public void RotateGun(Vector3 lookPoint)
    {
        Vector3 distanceVector = lookPoint - gameObject.transform.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;

        gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
