using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    public Transform lookAt;
    public Transform camTransform;

    private Camera cam;
    //private const float Y_ANGLE_MIN = 0.0f;
    //private const float Y_ANGLE_MAX = 50.0f;
    private float dis = 10.0f;
    private float curX = 0.0f;
    private float curY = 0.0f;
    private float sensitiveX = 4.0f;
    private float sensitiveY = 1.0f;

    private void Start()
    {
        camTransform = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        curX += Input.GetAxis("Mouse X");
        curY += Input.GetAxis("Mouse Y");

        //curY = Mathf.Clamp(curY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -dis);
        Quaternion rotation = Quaternion.Euler(curX, curY, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }
}
