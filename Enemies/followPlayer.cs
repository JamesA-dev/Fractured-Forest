using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public float smoothTime = 0.5f;
    Vector3 velocity;
    public Transform target;
    public Vector3 targetPosition;
    public float speed = 10f;

    public void Update()
    {
        targetPosition = target.transform.position;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, speed);

        if (target != null)
        {
            transform.LookAt(target);
        }
    }
}
