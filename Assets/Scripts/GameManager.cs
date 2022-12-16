using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager instance;

    protected Transform playerSpawnTransform;
    public GameObject[] playerSpawns;

    public GameObject playerControllerPrefab;
    public GameObject playerXboxControllerPrefab;

    public GameObject tankPawnPrefab;
    public GameObject playerTwoPrefab;
    public GameObject playerThreePrefab;
    public GameObject playerFourPrefab;

    private Camera playerOneCamera;
    private Camera playerTwoCamera;
    private Camera playerThreeCamera;
    private Camera playerFourCamera;

    public Dropdown DropdownMenu;

    public GameObject heliPawnPrefab;

    public GameObject MainMenuStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject ControlsScreenStateObject;
    public GameObject PauseScreenStateObject;
    public GameObject MainGameStateObject;
    public GameObject VictoryScreenStateObject;
    public GameObject LoserScreenStateObject;

    public bool pauseManager = false;

    public List<PlayerController> players;

    public bool onePlayerGame;
    public bool twoPlayerGame;
    public bool threePlayerGame;
    public bool fourPlayerGame;

    public int PlayerLives;

    #endregion
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
        onePlayer();
        ActivateMainMenuScreen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Spawn
    public void SpawnPlayer()
    {
        if(onePlayerGame)
        {
            OnePlayerChangeCamera();
            OnePlayerSpawn(tankPawnPrefab);
        }
        else if (twoPlayerGame)
        {
            TwoPlayerChangeCamera();
            TwoPlayerSpawn(tankPawnPrefab, playerTwoPrefab);
        }
        else if (threePlayerGame)
        {
            ThirdPlayerChangeCamera();
            ThreePlayerSpawn(tankPawnPrefab, playerTwoPrefab, playerThreePrefab);
        }
        else if (fourPlayerGame)
        {
            FourPlayerChangeCamera();
            FourPlayerSpawn(tankPawnPrefab, playerTwoPrefab, playerThreePrefab, playerFourPrefab);
        }
        else
        {
            Debug.LogError("Failed to assign Players");
        }
        Debug.Log("Spawning new player");
    }

    public void OnePlayerChangeCamera()
    {
        playerOneCamera = tankPawnPrefab.GetComponentInChildren<Camera>();
        playerOneCamera.rect = new Rect(0, 0, 1, 1);
    }

    public void TwoPlayerChangeCamera() 
    {
        playerOneCamera = tankPawnPrefab.GetComponentInChildren<Camera>();
        playerOneCamera.rect = new Rect(0, 0, 1, 0.5f);

        playerTwoCamera = playerTwoPrefab.GetComponentInChildren<Camera>();
        playerTwoCamera.rect = new Rect(0, 0.5f, 1, 0.5f);
    }

    public void ThirdPlayerChangeCamera() 
    {
        playerOneCamera = tankPawnPrefab.GetComponentInChildren<Camera>();
        playerOneCamera.rect = new Rect(0, 0, 0.5f, 0.5f);

        playerTwoCamera = playerTwoPrefab.GetComponentInChildren<Camera>();
        playerTwoCamera.rect = new Rect(0.5f, 0, 0.5f, 0.5f);

        playerThreeCamera = playerThreePrefab.GetComponentInChildren<Camera>();
        playerThreeCamera.rect = new Rect(0.25f, 0.5f, 0.5f, 0.5f);
    }

    public void FourPlayerChangeCamera() 
    {
        playerOneCamera = tankPawnPrefab.GetComponentInChildren<Camera>();
        playerOneCamera.rect = new Rect(0, 0, 0.5f, 0.5f);

        playerTwoCamera = playerTwoPrefab.GetComponentInChildren<Camera>();
        playerTwoCamera.rect = new Rect(0.5f, 0, 0.5f, 0.5f);

        playerThreeCamera = playerThreePrefab.GetComponentInChildren<Camera>();
        playerThreeCamera.rect = new Rect(0, 0.5f, 0.5f, 0.5f);

        playerFourCamera = playerFourPrefab.GetComponentInChildren<Camera>();
        playerFourCamera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
    }

    public void OnePlayerSpawn(GameObject pawn)
    {
        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(pawn, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
    }

    public void TwoPlayerSpawn(GameObject pawn, GameObject pawnTwo)
    {
        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(pawn, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;

        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjTwo = Instantiate(playerXboxControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjTwo = Instantiate(pawnTwo, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerTwo = newPlayerObjTwo.GetComponent<Controller>();
        Pawn newPawnTwo = newPawnObjTwo.GetComponent<Pawn>();

        newControllerTwo.pawn = newPawnTwo;
    }

    public void ThreePlayerSpawn(GameObject pawn, GameObject pawnTwo, GameObject pawnThree)
    {
        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(pawn, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;

        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjTwo = Instantiate(playerXboxControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjTwo = Instantiate(pawnTwo, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerTwo = newPlayerObjTwo.GetComponent<Controller>();
        Pawn newPawnTwo = newPawnObjTwo.GetComponent<Pawn>();

        newControllerTwo.pawn = newPawnTwo;

        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjThree = Instantiate(playerXboxControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjThree = Instantiate(pawnThree, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerThree = newPlayerObjThree.GetComponent<Controller>();
        Pawn newPawnThree = newPawnObjThree.GetComponent<Pawn>();

        newControllerThree.pawn = newPawnThree;
    }

    public void FourPlayerSpawn(GameObject pawn, GameObject pawnTwo, GameObject pawnThree, GameObject pawnFour)
    {
        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(pawn, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;

        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjTwo = Instantiate(playerXboxControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjTwo = Instantiate(pawnTwo, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerTwo = newPlayerObjTwo.GetComponent<Controller>();
        Pawn newPawnTwo = newPawnObjTwo.GetComponent<Pawn>();

        newControllerTwo.pawn = newPawnTwo;

        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjThree = Instantiate(playerXboxControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjThree = Instantiate(pawnThree, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerThree = newPlayerObjThree.GetComponent<Controller>();
        Pawn newPawnThree = newPawnObjThree.GetComponent<Pawn>();

        newControllerThree.pawn = newPawnThree;

        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjFour = Instantiate(playerXboxControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjFour = Instantiate(pawnFour, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerFour = newPlayerObjFour.GetComponent<Controller>();
        Pawn newPawnFour = newPawnObjFour.GetComponent<Pawn>();

        newControllerFour.pawn = newPawnFour;
    }

    public void HandleDropdownInput()
    {
        if(DropdownMenu.value == 0)
        {
            onePlayer();
        }
        else if(DropdownMenu.value == 1) 
        {
            twoPlayer();
        }
        else if(DropdownMenu.value == 2)
        {
            threePlayer();
        }
        else if(DropdownMenu.value == 3)
        {
            fourPlayer();
        }
    }
    #endregion

    #region UI State Machine
    private void DeactivateAllStates()
    {
        MainMenuStateObject.SetActive(false);
        OptionsScreenStateObject.SetActive(false);
        ControlsScreenStateObject.SetActive(false);
        PauseScreenStateObject.SetActive(false);
        MainGameStateObject.SetActive(false);
        VictoryScreenStateObject.SetActive(false);
        LoserScreenStateObject.SetActive(false);
    }

    public void ActivateMainMenuScreen()
    {
        DeactivateAllStates();
        MainMenuStateObject.SetActive(true);
    }

    public void ActivateOptionsScreen()
    {
        DeactivateAllStates();
        OptionsScreenStateObject.SetActive(true);
    }

    public void ActivatePauseScreen()
    {
        pauseManager = !pauseManager;
        PauseScreenStateObject.SetActive(pauseManager);
    }

    public void ActivateControlsScreen()
    {
        DeactivateAllStates();
        ControlsScreenStateObject.SetActive(true);
    }

    public void ActivateMainGameScreen()
    {
        DeactivateAllStates();
        MainGameStateObject.SetActive(true);
        SpawnPlayer();
    }
    public void ActivateVictoryScreen()
    {
        DeactivateAllStates();
        VictoryScreenStateObject.SetActive(true);
    }
    public void ActivateLoserScreen()
    {
        DeactivateAllStates();
       LoserScreenStateObject.SetActive(true);
    }

    #endregion

    #region playerStates
    public void onePlayer()
    {
        onePlayerGame = true;
        twoPlayerGame = false;
        threePlayerGame = false;
        fourPlayerGame = false;
    }

    public void twoPlayer()
    {
        onePlayerGame = false;
        twoPlayerGame = true;
        threePlayerGame = false;
        fourPlayerGame = false;
    }

    public void threePlayer()
    {
        onePlayerGame = false;
        twoPlayerGame = false;
        threePlayerGame = true;
        fourPlayerGame = false;
    }

    public void fourPlayer()
    {
        onePlayerGame = false;
        twoPlayerGame = false;
        threePlayerGame = false;
        fourPlayerGame = true;
    }

    #endregion

}
