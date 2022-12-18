using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Variables for this project. Warning there are a lot. Open at own risk. 
    #region Variables
    //The Game Manager itself
    public static GameManager instance;

    //Player Spawn locations
    protected Transform playerSpawnTransform;
    public GameObject[] playerSpawns;

    //Player COntrollers
    public GameObject playerControllerPrefab;
    public GameObject player2ControllerPrefab;
    public GameObject player3Controller;
    public GameObject player4Controller;

    //Prefabs for players
    public GameObject tankPawnPrefab;
    public GameObject playerTwoPrefab;
    public GameObject playerThreePrefab;
    public GameObject playerFourPrefab;

    //Controllers and Prefabs for AI
    public GameObject aiController;
    public GameObject aiController2;
    public GameObject aiTank1;
    public GameObject aiTank2;
    public GameObject aiTank3;

    //Each players camera
    private Camera playerOneCamera;
    private Camera playerTwoCamera;
    private Camera playerThreeCamera;
    private Camera playerFourCamera;

    //Dropdown for player selection
    public Dropdown DropdownMenu;

    //Audio mixer to set volume
    public AudioMixer mainAudioMixer;

    //UI Sliders for volume
    public UnityEngine.UI.Slider mainVolumeSlider;
    public UnityEngine.UI.Slider FXSlider;
    public UnityEngine.UI.Slider musicSlider;

    //Backgrond music
    public AudioSource mainMenuMusic;
    public AudioSource gameMusic;

    //RIP Helicopter (2022-2022) you will be missed buddy :(
    public GameObject heliPawnPrefab;

    //UI Menu objects
    public GameObject MainMenuStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject ControlsScreenStateObject;
    public GameObject PauseScreenStateObject;
    public GameObject MainGameStateObject;
    public GameObject VictoryScreenStateObject;
    public GameObject LoserScreenStateObject;

    //Whether game is paused or not
    public bool pauseManager = false;

    //List of players
    public List<PlayerController> players;

    //Players Scores
    public int playerOneScore;
    public int playerTwoScore;
    public int playerThreeScore;
    public int playerFourSocre;

    //Endgame player score presentations
    public TextMeshProUGUI player1Score;
    public TextMeshProUGUI player2Score;
    public TextMeshProUGUI player3Score;
    public TextMeshProUGUI player4Score;

    //Number of players function
    public bool onePlayerGame;
    public bool twoPlayerGame;
    public bool threePlayerGame;
    public bool fourPlayerGame;

    //Timer variables
    public float timerStartTime;
    public bool timerStarted;
    public float matchLengthinMinutes;

    #endregion

    //Makes it so manager doesn't get destroyed
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

    //Player And AI Spawning
    #region Spawn

    //Spawns the correct amount of players
    public void SpawnPlayer()
    {
        if (onePlayerGame)
        {
            OnePlayerChangeCamera();
            OnePlayerSpawn(tankPawnPrefab, aiTank1, aiTank2, aiTank3);
        }
        else if (twoPlayerGame)
        {
            TwoPlayerChangeCamera();
            TwoPlayerSpawn(tankPawnPrefab, playerTwoPrefab, aiTank2, aiTank3);
        }
        else if (threePlayerGame)
        {
            ThirdPlayerChangeCamera();
            ThreePlayerSpawn(tankPawnPrefab, playerTwoPrefab, playerThreePrefab, aiTank3);
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

    //Sets camera for One Player Game
    public void OnePlayerChangeCamera()
    {
        playerOneCamera = tankPawnPrefab.GetComponentInChildren<Camera>();
        playerOneCamera.rect = new Rect(0, 0, 1, 1);
    }

    //Sets cameras for two player games
    public void TwoPlayerChangeCamera()
    {
        playerOneCamera = tankPawnPrefab.GetComponentInChildren<Camera>();
        playerOneCamera.rect = new Rect(0, 0, 1, 0.5f);

        playerTwoCamera = playerTwoPrefab.GetComponentInChildren<Camera>();
        playerTwoCamera.rect = new Rect(0, 0.5f, 1, 0.5f);
    }

    //Sets cameras for three player games
    public void ThirdPlayerChangeCamera()
    {
        playerOneCamera = tankPawnPrefab.GetComponentInChildren<Camera>();
        playerOneCamera.rect = new Rect(0, 0, 0.5f, 0.5f);

        playerTwoCamera = playerTwoPrefab.GetComponentInChildren<Camera>();
        playerTwoCamera.rect = new Rect(0.5f, 0, 0.5f, 0.5f);

        playerThreeCamera = playerThreePrefab.GetComponentInChildren<Camera>();
        playerThreeCamera.rect = new Rect(0.25f, 0.5f, 0.5f, 0.5f);
    }

    //Sets cameras for four player games
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

    //Spawns in one player and three AI
    public void OnePlayerSpawn(GameObject pawn, GameObject pawnTwo, GameObject pawnThree, GameObject pawnFour)
    {
        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        Debug.Log(playerSpawnTransform);
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(pawn, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;

        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjTwo = Instantiate(aiController, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjTwo = Instantiate(pawnTwo, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerTwo = newPlayerObjTwo.GetComponent<Controller>();
        Pawn newPawnTwo = newPawnObjTwo.GetComponent<Pawn>();

        newControllerTwo.pawn = newPawnTwo;

        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjThree = Instantiate(aiController, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjThree = Instantiate(pawnThree, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerThree = newPlayerObjThree.GetComponent<Controller>();
        Pawn newPawnThree = newPawnObjThree.GetComponent<Pawn>();

        newControllerThree.pawn = newPawnThree;

        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjFour = Instantiate(aiController2, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjFour = Instantiate(pawnFour, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerFour = newPlayerObjFour.GetComponent<Controller>();
        Pawn newPawnFour = newPawnObjFour.GetComponent<Pawn>();

        newControllerFour.pawn = newPawnFour;
    }

    //Spawns in two players and two AI
    public void TwoPlayerSpawn(GameObject pawn, GameObject pawnTwo, GameObject pawnThree, GameObject pawnFour)
    {
        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(pawn, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;

        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjTwo = Instantiate(player2ControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjTwo = Instantiate(pawnTwo, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerTwo = newPlayerObjTwo.GetComponent<Controller>();
        Pawn newPawnTwo = newPawnObjTwo.GetComponent<Pawn>();

        newControllerTwo.pawn = newPawnTwo;

        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjThree = Instantiate(aiController, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjThree = Instantiate(pawnThree, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerThree = newPlayerObjThree.GetComponent<Controller>();
        Pawn newPawnThree = newPawnObjThree.GetComponent<Pawn>();

        newControllerThree.pawn = newPawnThree;

        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjFour = Instantiate(aiController2, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjFour = Instantiate(pawnFour, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerFour = newPlayerObjFour.GetComponent<Controller>();
        Pawn newPawnFour = newPawnObjFour.GetComponent<Pawn>();

        newControllerFour.pawn = newPawnFour;
    }

    //Spawns in three players and one AI
    public void ThreePlayerSpawn(GameObject pawn, GameObject pawnTwo, GameObject pawnThree, GameObject pawnFour)
    {
        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(pawn, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;

        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjTwo = Instantiate(player2ControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjTwo = Instantiate(pawnTwo, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerTwo = newPlayerObjTwo.GetComponent<Controller>();
        Pawn newPawnTwo = newPawnObjTwo.GetComponent<Pawn>();

        newControllerTwo.pawn = newPawnTwo;

        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjThree = Instantiate(player3Controller, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjThree = Instantiate(pawnThree, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerThree = newPlayerObjThree.GetComponent<Controller>();
        Pawn newPawnThree = newPawnObjThree.GetComponent<Pawn>();

        newControllerThree.pawn = newPawnThree;

        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjFour = Instantiate(aiController2, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjFour = Instantiate(pawnFour, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerFour = newPlayerObjFour.GetComponent<Controller>();
        Pawn newPawnFour = newPawnObjFour.GetComponent<Pawn>();

        newControllerFour.pawn = newPawnFour;
    }

    //Spawns in four players and no AI
    public void FourPlayerSpawn(GameObject pawn, GameObject pawnTwo, GameObject pawnThree, GameObject pawnFour)
    {
        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(pawn, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;

        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjTwo = Instantiate(player2ControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjTwo = Instantiate(pawnTwo, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerTwo = newPlayerObjTwo.GetComponent<Controller>();
        Pawn newPawnTwo = newPawnObjTwo.GetComponent<Pawn>();

        newControllerTwo.pawn = newPawnTwo;

        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjThree = Instantiate(player3Controller, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjThree = Instantiate(pawnThree, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerThree = newPlayerObjThree.GetComponent<Controller>();
        Pawn newPawnThree = newPawnObjThree.GetComponent<Pawn>();

        newControllerThree.pawn = newPawnThree;

        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjFour = Instantiate(player4Controller, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjFour = Instantiate(pawnFour, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerFour = newPlayerObjFour.GetComponent<Controller>();
        Pawn newPawnFour = newPawnObjFour.GetComponent<Pawn>();

        newControllerFour.pawn = newPawnFour;
    }

    //Gets input from dropdown menu, and sets the amount of players
    public void HandleDropdownInput()
    {
        if (DropdownMenu.value == 0)
        {
            onePlayer();
        }
        else if (DropdownMenu.value == 1)
        {
            twoPlayer();
        }
        else if (DropdownMenu.value == 2)
        {
            threePlayer();
        }
        else if (DropdownMenu.value == 3)
        {
            fourPlayer();
        }
    }
    #endregion

    //Respawning Players and AI
    #region Respawning

    //Respawns player one
    public void RespawnPlayerOne(GameObject pawn)
    {
        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        Debug.Log(playerSpawnTransform);
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(pawn, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
    }

    //Respawns player Two
    public void RespawnPlayerTwo(GameObject pawnTwo)
    {
        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjTwo = Instantiate(player2ControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjTwo = Instantiate(pawnTwo, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerTwo = newPlayerObjTwo.GetComponent<Controller>();
        Pawn newPawnTwo = newPawnObjTwo.GetComponent<Pawn>();

        newControllerTwo.pawn = newPawnTwo;
    }

    //Respawns Player Three
    public void RespawnPlayerThree(GameObject pawnThree)
    {
        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjThree = Instantiate(player3Controller, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjThree = Instantiate(pawnThree, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerThree = newPlayerObjThree.GetComponent<Controller>();
        Pawn newPawnThree = newPawnObjThree.GetComponent<Pawn>();

        newControllerThree.pawn = newPawnThree;
    }

    //Respawns Player Four
    public void RespawnPlayerFour(GameObject pawnFour)
    {
        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        GameObject newPlayerObjFour = Instantiate(player4Controller, Vector3.zero, Quaternion.identity);
        GameObject newPawnObjFour = Instantiate(pawnFour, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newControllerFour = newPlayerObjFour.GetComponent<Controller>();
        Pawn newPawnFour = newPawnObjFour.GetComponent<Pawn>();

        newControllerFour.pawn = newPawnFour;
    }

    //Respawns the first AI
    public void RespawnAIOne(GameObject pawn)
    {
        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        Debug.Log(playerSpawnTransform);
        GameObject newPlayerObj = Instantiate(aiController, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(pawn, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
    }

    //Respawns the second AI
    public void RespawnAITwo(GameObject pawn)
    {
        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        Debug.Log(playerSpawnTransform);
        GameObject newPlayerObj = Instantiate(aiController, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(pawn, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
    }

    //Respawns the third AI
    public void RespawnAIThree(GameObject pawn)
    {
        playerSpawnTransform = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;
        Debug.Log(playerSpawnTransform);
        GameObject newPlayerObj = Instantiate(aiController2, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(pawn, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
    }

    #endregion

    //Changes the UI
    #region UI State Machine
    //Sets all states to not active
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

    //Sets Main Menu to Active
    public void ActivateMainMenuScreen()
    {
        DeactivateAllStates();
        playerOneScore = 0;
        playerTwoScore = 0;
        playerThreeScore = 0;
        playerFourSocre = 0;
        players.Clear();
        timerStarted = false;
        timerStartTime = Time.time;
        MainMenuStateObject.SetActive(true);
        mainMenuMusic.Play();
    }

    //Sets Options Screen to Active
    public void ActivateOptionsScreen()
    {
        DeactivateAllStates();
        OptionsScreenStateObject.SetActive(true);
    }

    //Sets Pause to Active
    public void ActivatePauseScreen()
    {
        pauseManager = !pauseManager;
        PauseScreenStateObject.SetActive(pauseManager);
    }

    //Sets Controls to Active
    public void ActivateControlsScreen()
    {
        DeactivateAllStates();
        ControlsScreenStateObject.SetActive(true);
    }

    //Starts the main Game
    public void ActivateMainGameScreen()
    {
        DeactivateAllStates();
        mainMenuMusic.Stop();
        MainGameStateObject.SetActive(true);
        timerStarted = true;
        timerStartTime = Time.time;
        gameMusic.Play();
        SpawnPlayer();
    }

    //Screen of Victory
    public void ActivateVictoryScreen()
    {
        DeactivateAllStates();
        VictoryScreenStateObject.SetActive(true);
    }

    //Game over Screen
    public void ActivateLoserScreen()
    {
        DeactivateAllStates();
        timerStarted = false;
        player1Score.text = playerOneScore.ToString();
        player2Score.text = playerTwoScore.ToString();
        player3Score.text = playerThreeScore.ToString();
        player4Score.text = playerFourSocre.ToString();
        LoserScreenStateObject.SetActive(true);
    }

    #endregion

    //Sound functions
    #region Sound

    //Main Volume Changer
    public void OnMainVolumeChange()
    {
        float newVolume = mainVolumeSlider.value;

        if (newVolume <= 0)
        {
            newVolume = -80;
        }
        else
        {
            newVolume = Mathf.Log10(newVolume);
            newVolume *= 20;
        }

        mainAudioMixer.SetFloat("mainVolume", newVolume);
    }

    //Sound Effects Volume Value Changer
    public void OnFXVolumeChange()
    {
        float newVolume = FXSlider.value;

        if(newVolume <= 0) 
        {
            newVolume = -80;
        }
        else
        {
            newVolume = Mathf.Log10(newVolume);
            newVolume *= 20;
        }

        mainAudioMixer.SetFloat("FXVolume", newVolume);
    }

    //Music Volume Changer
    public void OnMusicVolumeChange()
    {
        float newVolume = musicSlider.value;

        if (newVolume <= 0)
        {
            newVolume = -80;
        }
        else
        {
            newVolume = Mathf.Log10(newVolume);
            newVolume *= 20;
        }

        mainAudioMixer.SetFloat("musicVolume", newVolume);
    }

    #endregion

    //Sets Amount of players
    #region playerStates

    //One Player Game
    public void onePlayer()
    {
        onePlayerGame = true;
        twoPlayerGame = false;
        threePlayerGame = false;
        fourPlayerGame = false;
    }

    //Two Player Game
    public void twoPlayer()
    {
        onePlayerGame = false;
        twoPlayerGame = true;
        threePlayerGame = false;
        fourPlayerGame = false;
    }

    //Three Player Game
    public void threePlayer()
    {
        onePlayerGame = false;
        twoPlayerGame = false;
        threePlayerGame = true;
        fourPlayerGame = false;
    }

    //Four Player Game
    public void fourPlayer()
    {
        onePlayerGame = false;
        twoPlayerGame = false;
        threePlayerGame = false;
        fourPlayerGame = true;
    }

    #endregion

}
