using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
        
    public float FollowSpeed = 0.125f;

    public Vector3 Offset = new Vector3(0f, 0f, -10f);

    void LateUpdate()
    {
        if (_target == null) return;
        Vector3 desiredPosition = _target.position + Offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, FollowSpeed);

        transform.position = smoothedPosition;
    }
}
