using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;

public class HealthComponent : MonoBehaviour
{
    #region Variables

    //Health and Regeneration Variables
    public float maxHealth;
    public float currentHealth;
    public float healthPercentage;
    public float regeneration;
    public float timeBeforeHealing;
    private float timeOfLastDamage;

    //Gets the gamemanager
    public GameManager gameManager;

    //Private character healthbar
    public UnityEngine.UI.Image healthBar;

    //Public health bar for everyone to see
    public UnityEngine.UI.Image UniversalHealthBar;

    //Score trackers
    public TextMeshProUGUI player1Score;
    public TextMeshProUGUI player2Score;
    public TextMeshProUGUI player3Score;
    public TextMeshProUGUI player4Score;

    //Sounds for getting hit and dying
    public AudioSource hit;
    public AudioSource death;

    //Timer Variables
    private float timeLeft;
    private float clockMinute;
    private float clockSeconds;
    private string clockSecondsString;
    private float matchLengthAdjusted;
    private float timeStartAdjusted;
    public TextMeshProUGUI timer;

    //Keeps track of who's who
    public int pawnNum;

    public Pawn KillboxPawn;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Sets game manager, gets timer ready, and sets current health. Also set's up score counters and health bars
        gameManager = GameManager.instance;
        matchLengthAdjusted = gameManager.matchLengthinMinutes * 60;
        Debug.Log(matchLengthAdjusted);
        currentHealth = maxHealth;
        healthPercentage = currentHealth / maxHealth;
        UniversalHealthBar.fillAmount = healthPercentage;
        if (healthBar != null)
        {
            healthBar.fillAmount = healthPercentage;
        }
        if (player1Score != null)
        {
            player1Score.text = "Player 1: " + gameManager.playerOneScore;
            player2Score.text = "Player 2: " + gameManager.playerTwoScore;
            player3Score.text = "Player 3: " + gameManager.playerThreeScore;
            player4Score.text = "Player 4: " + gameManager.playerFourSocre;
        }
    }

    //Called once per frame
    private void Update()
    {
        //Updates scores to keep them accurate
        if (player1Score != null)
        {
            player1Score.text = "Player 1: " + gameManager.playerOneScore;
            player2Score.text = "Player 2: " + gameManager.playerTwoScore;
            player3Score.text = "Player 3: " + gameManager.playerThreeScore;
            player4Score.text = "Player 4: " + gameManager.playerFourSocre;
        }
        //Updates timer on screen
        if (timer != null)
        {
            if (gameManager.timerStarted)
            {
                if (timeStartAdjusted <= 0)
                {
                    timeStartAdjusted = matchLengthAdjusted + gameManager.timerStartTime;
                    timeLeft = timeStartAdjusted;
                }
                else
                {
                    timeLeft -= Time.deltaTime;
                }
                timerToClock(timeLeft);
                timer.text = clockMinute.ToString() + ":" + clockSecondsString;
                if (timeLeft <= gameManager.timerStartTime)
                {
                    timeStartAdjusted = 0;
                    gameManager.ActivateLoserScreen();
                }
            }
        }
        //Regeneration
        if(currentHealth != maxHealth)
        {
            if(timeBeforeHealing + timeOfLastDamage < Time.time)
            {
                Heal(regeneration);
            }
        }
    }

    //Turns timer into a readable clock
    public void timerToClock(float timer)
    {
        float tempSeconds;
        float adjustedTimer = timer - gameManager.timerStartTime;
        clockMinute = Mathf.Floor(adjustedTimer / 60);
        tempSeconds = adjustedTimer / 60 - clockMinute;
        clockSeconds = Mathf.Floor(tempSeconds * 60);
        if(clockSeconds < 10)
        {
            clockSecondsString = "0" + clockSeconds.ToString();
        }
        else
        {
            clockSecondsString = clockSeconds.ToString();
        }
    }

    //Heals character
    public void Heal(float amount)
    {
        currentHealth = currentHealth + amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthPercentage = currentHealth / maxHealth;
        UniversalHealthBar.fillAmount = healthPercentage;
        if (healthBar != null)
        {
            healthBar.fillAmount = healthPercentage;
        }

    }

    //Functions that happen during or after death
    #region death

    //Applies damage to character
    public void TakeDamage(float amount, Pawn source)
    {
        hit.Play();
        timeOfLastDamage = Time.time;
        currentHealth = currentHealth - amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthPercentage = currentHealth / maxHealth;
        UniversalHealthBar.fillAmount = healthPercentage;
        if (healthBar != null)
        {
            healthBar.fillAmount = healthPercentage;
        }
        Debug.Log(source.name + " did " + amount + " damage to " + gameObject.name);
        if (currentHealth <= 0)
        {
            death.Play();
            AddPoints(source);
            if (source != KillboxPawn)
            {
                Respawn();
            }
            Die(source);
        }
    }

    //Adds points to whoever kills you
    public void AddPoints(Pawn source)
    {
        if (source.healthComponenet.pawnNum == 1)
        {
            gameManager.playerOneScore++;
        }
        else if (source.healthComponenet.pawnNum == 2)
        {
            gameManager.playerTwoScore++;
        }
        else if (source.healthComponenet.pawnNum == 3)
        {
            gameManager.playerThreeScore++;
        }
        else if (source.healthComponenet.pawnNum == 4)
        {
            gameManager.playerFourSocre++;
        }
        else if (source.healthComponenet.pawnNum == 5)
        {
            gameManager.playerTwoScore++;
        }
        else if (source.healthComponenet.pawnNum == 6)
        {
            gameManager.playerThreeScore++;
        }
        else if (source.healthComponenet.pawnNum == 7)
        {
            gameManager.playerFourSocre++;
        }
    }

    //Calls respawn functions from game manager
    public void Respawn()
    {
        if (pawnNum == 1)
        {

            Debug.Log("Should respawn player");
            gameManager.RespawnPlayerOne(gameManager.tankPawnPrefab);
            currentHealth = maxHealth;
            healthPercentage = currentHealth / maxHealth;
            healthBar.fillAmount = healthPercentage;
            UniversalHealthBar.fillAmount = healthPercentage;

        }
        else if (pawnNum == 2)
        {

            Debug.Log("Should respawn player");
            gameManager.RespawnPlayerTwo(gameManager.playerTwoPrefab);
            currentHealth = maxHealth;
            healthPercentage = currentHealth / maxHealth;
            healthBar.fillAmount = healthPercentage;
            UniversalHealthBar.fillAmount = healthPercentage;

        }
        else if (pawnNum == 3)
        {

            Debug.Log("Should respawn player");
            gameManager.RespawnPlayerThree(gameManager.playerThreePrefab);
            currentHealth = maxHealth;
            healthPercentage = currentHealth / maxHealth;
            healthBar.fillAmount = healthPercentage;
            UniversalHealthBar.fillAmount = healthPercentage;
        }
        else if (pawnNum == 4)
        {

            Debug.Log("Should respawn player");
            gameManager.RespawnPlayerFour(gameManager.playerFourPrefab);
            currentHealth = maxHealth;
            healthPercentage = currentHealth / maxHealth;
            healthBar.fillAmount = healthPercentage;
            UniversalHealthBar.fillAmount = healthPercentage;

        }
        else if (pawnNum == 5)
        {
            Debug.Log("Should respawn player");
            gameManager.RespawnAIOne(gameManager.aiTank1);
            currentHealth = maxHealth;
            healthPercentage = currentHealth / maxHealth;
            UniversalHealthBar.fillAmount = healthPercentage;

        }
        else if (pawnNum == 6)
        {
            Debug.Log("Should respawn player");
            gameManager.RespawnAITwo(gameManager.aiTank2);
            currentHealth = maxHealth;
            healthPercentage = currentHealth / maxHealth;
            UniversalHealthBar.fillAmount = healthPercentage;
        }
        else if (pawnNum == 7)
        {
            Debug.Log("Should respawn player");
            gameManager.RespawnAIThree(gameManager.aiTank3);
            currentHealth = maxHealth;
            healthPercentage = currentHealth / maxHealth;
            UniversalHealthBar.fillAmount = healthPercentage;
        }
    }

    //Destroys game object after dying
    public void Die(Pawn source)
    {
        Debug.Log("Destroyed Player" + source);
        Destroy(gameObject);
    }

    #endregion
}
