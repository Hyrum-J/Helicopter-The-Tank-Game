using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pickup : MonoBehaviour
{
    //how fast it moves
    public float rotationSpeed;
    public float bobSpeed;
    public AnimationCurve bobCurve;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Rotate();
        Bob();
    }

    //Rotates pickup
    protected virtual void Rotate()
    {
        transform.Rotate(0, rotationSpeed + Time.deltaTime, 0);
    }

    //Moves pickup up and down
    protected virtual void Bob()
    {
        transform.position = new Vector3(transform.position.x, (bobSpeed * bobCurve.Evaluate(Time.time % bobCurve.length)), transform.position.z);
    }

}
