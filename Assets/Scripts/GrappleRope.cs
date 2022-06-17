using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleRope : MonoBehaviour
{
    [SerializeField]
    private Grapple grapple;

    private LineRenderer line;

    private bool isGrappling = false;
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }

    private void OnEnable()
    {
        line.enabled = true;
        ResetPositons();
    }

    private void OnDisable()
    {
        line.enabled = false;
    }

    private void ResetPositons()
    {
        for(int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, grapple.firePoint.position);
        }
    }
    void Update()
    {
        DrawRopeNoWaves();
    }

    void DrawRopeNoWaves() 
    {
        line.positionCount = 2;
        line.SetPosition(0, grapple.firePoint.position);
        line.SetPosition(1, grapple.grappleTargetPos);
        if (!isGrappling)
        {
            isGrappling = true;
            grapple.StartGrapple();
        }
    }
}
