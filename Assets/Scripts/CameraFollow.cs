using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform bird;
    public float smoothSpeed = 0.125f; 
    public Vector3 offset; 

    private void LateUpdate()
    {
        if (bird == null) return;


        Vector3 desiredPosition = bird.position + offset;


        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
