using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleRope : MonoBehaviour
{
    [SerializeField]
    private Grapple grapple;
    private LineRenderer line;
    private bool isGrappling = false;
    public bool stopGrappling = false;

    private float length, startTime;

    private float speed = 30f;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        line.positionCount = 2;
    }

    private void OnEnable()
    {
        stopGrappling = false;
        line.enabled = true;
        length = Vector2.Distance(grapple.firePoint.position, grapple.grappleTargetPos);
        startTime = Time.time;
        ResetPositons();
    }

    private void OnDisable()
    {
        line.enabled = false;
        isGrappling = false;
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
        line.SetPosition(0, grapple.firePoint.position);
        if (!isGrappling && !stopGrappling)
        {
            DrawRopeNoWaves();
        }
        if (stopGrappling)
        {
            PullRopeBack();
        }
    }

    void DrawRopeNoWaves() 
    {
        float distCovered = ((Time.time - startTime) * speed) / length;
        Vector2 position = Vector2.Lerp(grapple.firePoint.position, grapple.grappleTargetPos, distCovered);
        line.SetPosition(1, position);
        if (!isGrappling && line.GetPosition(1).Equals(grapple.grappleTargetPos))
        {
            isGrappling = true;
            grapple.StartGrapple();
        }
    }

    void PullRopeBack()
    {
        float distCovered = ((Time.time - startTime) * speed) / length;
        Vector2 position = Vector2.Lerp(line.GetPosition(1), grapple.firePoint.position, distCovered);
        line.SetPosition(1, position);
        if (line.GetPosition(1).Equals(grapple.firePoint.position))
        {
            this.enabled = false;
        }
    }

    public void StopGrappling()
    {
        stopGrappling = true;
        startTime = Time.time;
    }
}
