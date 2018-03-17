using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickCam : MonoBehaviour {

    public JoyStick camJoyStick;

    public Transform lookAt;

    private float dis = 10.0f;
    private float curX = 0.0f;
    private float curY = 0.0f;
    private float sensitiveX = 4.0f;
    private float sensitiveY = 1.0f;
    private void Update ()
    {
        curX += camJoyStick.InputVectors.x * sensitiveX;
        curY += camJoyStick.InputVectors.z * sensitiveY;
	}
    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -dis);
        Quaternion rot = Quaternion.Euler(curY, curX, 0);
        transform.position = lookAt.position + rot * dir;
        transform.LookAt(lookAt);
    }
}
