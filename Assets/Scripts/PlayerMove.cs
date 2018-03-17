using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    private Rigidbody m_rb;
    private Renderer ourRend;
    private Transform camTran { set; get; }

    public Vector3 MoveVec { set; get; }
    public JoyStick joyStick;
    public float speed = 5.0f;
    public float turnSpeed = 1.0f;
    public float m_jumpVelocity = 5.0f;
    public float rotateSpeed = 2f;
    public Camera cam;


    void Start () {
        m_rb = GetComponent<Rigidbody>();
        ourRend = GetComponent<Renderer>();
        camTran = Camera.main.transform;
        m_rb.maxAngularVelocity = speed;
        m_rb.drag = turnSpeed;
    }
    private Vector3 JoyInput()
    {
        Vector3 dir = Vector3.zero;
        dir.x = joyStick.Horizontal();
        dir.z = joyStick.Vertical();
        if (dir.magnitude > 1)
        {
            dir.Normalize();
        }
        return dir;
    }
    private Vector3 RotateWithView()
    {
        if (camTran != null)
        {
            Vector3 dir = camTran.TransformDirection(MoveVec);
            dir.Set(dir.x, 0, dir.z);
            return dir.normalized * MoveVec.magnitude;
        }
        else
        {
            camTran = Camera.main.transform;
            return MoveVec;
        }
    }
    private void Move()
    {
        m_rb.AddForce((MoveVec * speed));
    }
    void Update () {
        
        MoveVec = JoyInput();
        MoveVec = RotateWithView();
        Move();

        Vector3 newVel = transform.up;
        newVel.y = m_rb.velocity.y;

        transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);
        cam.transform.Rotate(-Input.GetAxis("Mouse Y") * rotateSpeed, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            ourRend.material.color = Color.blue;
            //check if our character is grounded
            if (Mathf.Abs(m_rb.velocity.y) < 0.01f)
            {
                 newVel.y = m_jumpVelocity;
            }
        }
        //m_rb.velocity = newVel;
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ourRend.material.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {

            ourRend.material.color = Color.green;

        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            ourRend.material.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {

            ourRend.material.color = Color.green;

        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            ourRend.material.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {

            ourRend.material.color = Color.green;

        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            ourRend.material.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {

            ourRend.material.color = Color.green;

        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            ourRend.material.color = Color.red;
        }
    }
}
