using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    //public GameObject player;
    public Transform lookAt;
    private float smoothSpeed = 7.5f;
    private Vector3 desiredPos;
    private Vector3 offset;
    private float dist = 5.0f;
    private float yOffSet = 3.7f;

    void Start()
    {
        offset = new Vector3(0, yOffSet, -1f * dist);
        //offset = transform.position - player.transform.position;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveLeft();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveRight();
    }
    private void FixedUpdate()
    {
            desiredPos = lookAt.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
            transform.LookAt(lookAt.position + Vector3.up);
    }
    public void MoveCamRL(bool left)
    {
        if (left)
            offset = Quaternion.Euler(0, 90, 0) * offset;
        else
            offset = Quaternion.Euler(0, -90, 0) * offset;
    }
    public void MoveLeft()
    {
        MoveCamRL(false);
    }
    public void MoveRight()
    {
        MoveCamRL(true);
    }
}