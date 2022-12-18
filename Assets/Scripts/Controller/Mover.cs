using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void Start();

    //Move
    public abstract void Move(Vector3 direction, float speed, float maxSpeed, string going);

    //Rotate
    public abstract void Rotate(float turnSpeed);

    //Helicopter
    public abstract void Roll(float turnSpeed);

    public abstract void Fall(float fallSpeed);

    public abstract void Hover(float fallSpeed);

}
