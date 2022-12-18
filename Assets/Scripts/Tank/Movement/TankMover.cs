using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TankMover : Mover
{
    //The rigidbody of the tank
    private Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    public override void Start()
    {
        m_Rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    //Moves the tank in the direction you choose
    public override void Move(Vector3 direction, float speed, float maxSpeed, string going)
    {
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + moveVector);
    }

    //Rotates the tank either left or right
    public override void Rotate(float turnSpeed)
    {
        transform.Rotate(0, turnSpeed, 0);
    }

    //RIP Helicopter
    public override void Roll(float turnSpeed)
    {
        Debug.Log("Not for Tank");
    }

    public override void Fall(float fallSpeed)
    {
        Debug.Log("Not for Tank");
    }

    public override void Hover(float fallSpeed)
    {
        Debug.Log("Not for Tank");
    }

}
