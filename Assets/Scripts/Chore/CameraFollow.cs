using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float Smoothing;

    Vector3 offSetVal;

    private void Start()
    {
        offSetVal = transform.position - target.position;
    }

    private void Update()
    {
        Vector3 cameraPos = target.position + offSetVal;

        transform.position = Vector3.Lerp(transform.position, cameraPos, Smoothing * Time.deltaTime);
    }
}
