using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliMover : Mover
{
    /// <summary>
    /// Nothing to see Here:(
    /// </summary>
    private Rigidbody H_Rigidbody;

    // Start is called before the first frame update
    public override void Start()
    {
        H_Rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    public override void Move(Vector3 direction, float speed, float maxSpeed, string going)
    {
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        H_Rigidbody.AddForce(moveVector);
        /*if (transform.rotation.x < 0.1 && going == "forward")
        {
            transform.Rotate(1, 0, 0);
        }
        if (transform.rotation.x > -0.3 && going == "backward")
        {
            transform.Rotate(-1, 0, 0);
        }*/



    }

    public override void Rotate(float turnSpeed)
    {
        transform.Rotate(0, turnSpeed, 0);
    }

    public override void Roll(float turnSpeed)
    {
        if (transform.rotation.z <= .45 && transform.rotation.z >= -.45)
        {
            transform.Rotate(0, 0, turnSpeed);
            Debug.Log(transform.rotation.z);
        }
        if (transform.rotation.z > .45 && turnSpeed < 0)
        {
            transform.Rotate(0, 0, turnSpeed);
        }
        else if (transform.rotation.z < -.45 && turnSpeed > 0)
        {
            transform.Rotate(0, 0, turnSpeed);
            Debug.Log(turnSpeed);
        }
    }

    public override void Fall(float fallSpeed)
    {
        if (H_Rigidbody.velocity.y > -10)
        {
            Vector3 moveVector = Vector3.up.normalized * fallSpeed * Time.deltaTime;
            H_Rigidbody.AddForce(moveVector);
        }
        if(H_Rigidbody.velocity.x > 0 || H_Rigidbody.velocity.z > 0)
        {
            Vector3 stopVector = Vector3.forward.normalized * fallSpeed * Time.deltaTime;
            H_Rigidbody.AddForce(stopVector);
            Debug.Log("x:" + H_Rigidbody.velocity.x);
            Debug.Log("z:" + H_Rigidbody.velocity.z);
        }
        else
        {
            H_Rigidbody.velocity.Set(0, H_Rigidbody.velocity.y, 0);
        }
        if (H_Rigidbody.velocity.x < 0 || H_Rigidbody.velocity.z < 0)
        {
            Debug.Log("x:" + H_Rigidbody.velocity.x);
            Debug.Log("z:" + H_Rigidbody.velocity.z);
            Vector3 stopVector = Vector3.forward.normalized * -fallSpeed * Time.deltaTime;
            H_Rigidbody.AddForce(stopVector);
        }
        else
        {
            H_Rigidbody.velocity.Set(0, H_Rigidbody.velocity.y, 0);
        }
    }
    public override void Hover(float fallSpeed)
    {
        if (H_Rigidbody.velocity.y > 0)
        {
            Vector3 moveVector = Vector3.up * fallSpeed * Time.deltaTime;
            H_Rigidbody.AddForce(moveVector);
        }
        else if (H_Rigidbody.velocity.y < 0)
        {
            Vector3 moveVector = Vector3.up * fallSpeed * Time.deltaTime * -1;
            H_Rigidbody.AddForce(moveVector);
        }
        else
        {
            H_Rigidbody.velocity.Set(H_Rigidbody.velocity.x, 0, H_Rigidbody.velocity.z);
        }
        if (H_Rigidbody.velocity.x > 0 && H_Rigidbody.velocity.z > 0)
        {
            Vector3 stopVector = Vector3.forward.normalized * fallSpeed * Time.deltaTime;
            H_Rigidbody.AddForce(stopVector);
            Debug.Log("x:" + H_Rigidbody.velocity.x);
            Debug.Log("z:" + H_Rigidbody.velocity.z);
        }
        else
        {
            H_Rigidbody.velocity.Set(0, H_Rigidbody.velocity.y, 0);
        }
        if (H_Rigidbody.velocity.x > 0 && H_Rigidbody.velocity.z < 0)
        {
            Debug.Log("x:" + H_Rigidbody.velocity.x);
            Debug.Log("z:" + H_Rigidbody.velocity.z);
            Vector3 stopVector = Vector3.forward.normalized * -fallSpeed * Time.deltaTime;
            H_Rigidbody.AddForce(stopVector);
        }    
        else
        {
            H_Rigidbody.velocity.Set(0, H_Rigidbody.velocity.y, 0);
        }
    }
}

