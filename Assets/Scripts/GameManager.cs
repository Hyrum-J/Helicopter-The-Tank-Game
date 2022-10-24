using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool pawnPicker = true;
    public static GameManager instance;

    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;
    public GameObject heliPawnPrefab;
    public Transform playerSpawnTransform;

    public List<PlayerController> players;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        if (pawnPicker == true)
        {
            pawnPicker = false;
            SpawnPlayer(tankPawnPrefab);
        }
        else
        {
            pawnPicker = true;
            SpawnPlayer(heliPawnPrefab);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnPlayer(GameObject pawn)
    {
       
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(pawn, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;

    }

}
