using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliMover : Mover
{
    private Rigidbody H_Rigidbody;

    // Start is called before the first frame update
    public override void Start()
    {
        H_Rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    public override void Move(Vector3 direction, float speed, float maxSpeed, string going)
    {
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        H_Rigidbody.AddForce(H_Rigidbody.position + moveVector);
        if (transform.rotation.x < 0.1 && going == "forward")
        {
              transform.Rotate(1, 0, 0);
        }
        if (transform.rotation.x > -0.3  && going == "backward")
        {
             transform.Rotate(-1, 0, 0);
        }

    }

    public override void Rotate(float turnSpeed)
    {
        if (transform.rotation.y <= 0.45 && transform.rotation.y >= -0.45)
        {
            transform.Rotate(0, turnSpeed, 0);
        }
        if (transform.rotation.y > .45 && turnSpeed < 0)
        {
            transform.Rotate(0, turnSpeed, 0);
        }
        else if (transform.rotation.y < -0.45 && turnSpeed > 0)
        {
            transform.Rotate(0, turnSpeed, 0);
            Debug.Log(turnSpeed);
        }
    }

    public override void Roll(float turnSpeed)
    {
        if (transform.rotation.z <= .45 && transform.rotation.z >= -.45)
        {
            transform.Rotate(0, 0, turnSpeed);
            Debug.Log(transform.rotation.z);
        }
        if(transform.rotation.z > .45 && turnSpeed < 0)
        {
            transform.Rotate(0, 0, turnSpeed);
        }
        else if(transform.rotation.z < -.45 && turnSpeed > 0)
        {
            transform.Rotate(0, 0, turnSpeed);
            Debug.Log(turnSpeed);
        }
    }

    public override void Fall(Vector3 direction, float speed)
    {
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        H_Rigidbody.MovePosition(H_Rigidbody.position - moveVector);
    }

}

