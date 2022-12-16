using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float timeBetween;
    public Pickup prefab;
    private Pickup pickUp;
    public float timeLastPickedUp;
    private bool isPickedUp;

    // Start is called before the first frame update
    void Start()
    {
        RespawnPickup();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPickedUp == false)
        {
            timeLastPickedUp = Time.time;
            Debug.Log("Timer Reset");
        }
        if(pickUp == null)
        {
            Debug.Log("Pickup null");
            isPickedUp = true;
        }
        if (Time.time > timeLastPickedUp + timeBetween)
        {
            RespawnPickup();
        }
    }

    private void RespawnPickup()
    {
        pickUp = Instantiate(prefab, transform.position, Quaternion.identity);
        isPickedUp = false;
        timeLastPickedUp = Time.time;
        Debug.Log("PickUp Spawned" + Time.time);
    }
}
