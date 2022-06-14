using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    
    [SerializeField]
    private Vector3 baseOffset;
    public Vector3 offsetMouse;

    [SerializeField]
    [Range(0, 10)]
    private float smoothFactor = 6f;

    [SerializeField]
    private float minDistanceForEffect = 2f;



    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 offset = offsetMouse - target.position;
        float sqrLength = offset.sqrMagnitude;
        if (sqrLength < minDistanceForEffect * minDistanceForEffect)
        {
            offset = Vector3.zero;
        }
        else
        {
            offset.Normalize();
        }
        
        Vector3 targetPos = target.position + baseOffset + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPos;
    }

}
