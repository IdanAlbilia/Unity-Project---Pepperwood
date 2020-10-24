using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    //Movement vars
    public float moveSpeed;
    public float maxSpeed = 6f;

    public float tankSpeed;
    public Rigidbody tankRigidBody;
    public float movementInput;
    public float turnInput;
    public float movementSpeed;
    public float turnSpeed;



    //Level vars
    private Vector3 input;

    // Start is called before the first frame update
    void Start()
    {
        tankRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        turnInput = Input.GetAxis("Horizontal");
        movementInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }
    private void Turn()
    {
        float turn = turnInput * turnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
        tankRigidBody.MoveRotation(tankRigidBody.rotation * turnRotation);
    }
    private void Move()
    {
        Vector3 movement = transform.forward * movementInput * tankSpeed * Time.deltaTime;

        tankRigidBody.MovePosition(tankRigidBody.position + movement);
    }

}
