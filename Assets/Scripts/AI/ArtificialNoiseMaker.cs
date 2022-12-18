using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;

public abstract class ArtificialNoiseMaker : MonoBehaviour
{
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public float volumeDistance;
    public float currentVolumeDistance;

    public float ChangeVolume()
    {
        currentVolumeDistance += volumeDistance;
        return currentVolumeDistance;
    }
}
