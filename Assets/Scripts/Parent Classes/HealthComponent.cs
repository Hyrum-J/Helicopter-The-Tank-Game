using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class HealthComponent : MonoBehaviour
{
    #region Variables
    public float maxHealth;
    public float currentHealth;
    public float healthPercentage;

    public GameManager gameManager;

    public Image healthBar;
    public Image Life1;
    public Image Life2;
    public Image Life3;

    public TextMeshProUGUI player1Score;
    public TextMeshProUGUI player2Score;
    public TextMeshProUGUI player3Score;
    public TextMeshProUGUI player4Score;

    public AudioSource hit;
    public AudioSource death;

    public int pawnNum;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        currentHealth = maxHealth;
        healthPercentage = currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;
        player1Score.text = "Player 1: " + gameManager.playerOneScore;
        player2Score.text = "Player 2: " + gameManager.playerTwoScore;
        player3Score.text = "Player 3: " + gameManager.playerThreeScore;
        player4Score.text = "Player 4: " + gameManager.playerFourSocre;
    }

    private void Update()
    {
        if (player1Score != null)
        {
            player1Score.text = "Player 1: " + gameManager.playerOneScore;
            player2Score.text = "Player 2: " + gameManager.playerTwoScore;
            player3Score.text = "Player 3: " + gameManager.playerThreeScore;
            player4Score.text = "Player 4: " + gameManager.playerFourSocre;
        }
    }

    public void TakeDamage(float amount, Pawn source)
    {
        hit.Play();
        currentHealth = currentHealth - amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthPercentage = currentHealth / maxHealth;
        if(healthBar != null)
        {
            healthBar.fillAmount = healthPercentage;
        }
        Debug.Log(source.name + " did " + amount + " damage to " + gameObject.name);
        if (currentHealth <= 0)
        {
            death.Play();
            AddPoints(source);
            gameManager.PlayerLives--;
            if (gameManager.PlayerLives > 0)
            {
                Respawn();
                Die(source);
            }
            else
            {
                gameManager.ActivateLoserScreen();
            }
        }
    }

    public void Heal(float amount)
    {
        currentHealth = currentHealth + amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthPercentage = currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;

    }

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

    public void Respawn()
    {
        if (pawnNum == 1)
        {

            Debug.Log("Should respawn player");
            gameManager.RespawnPlayerOne(gameManager.tankPawnPrefab);
            currentHealth = maxHealth;
            healthPercentage = currentHealth / maxHealth;
            healthBar.fillAmount = healthPercentage;
            if (gameManager.PlayerLives == 2)
            {
                Life3.fillAmount = 0f;
            }
            else if (gameManager.PlayerLives == 1)
            {
                Life2.fillAmount = 0f;
            }
            else
            {
                Life1.fillAmount = 0f;

            }

        }
        else if (pawnNum == 2)
        {

            Debug.Log("Should respawn player");
            gameManager.RespawnPlayerTwo(gameManager.playerTwoPrefab);
            currentHealth = maxHealth;
            healthPercentage = currentHealth / maxHealth;
            healthBar.fillAmount = healthPercentage;
            if (gameManager.PlayerLives == 2)
            {
                Life3.fillAmount = 0;
            }
            else if (gameManager.PlayerLives == 1)
            {
                Life2.fillAmount = 0;
            }
            else
            {
                Life1.fillAmount = 0;
                gameManager.ActivateLoserScreen();
            }

        }
        else if (pawnNum == 3)
        {

            Debug.Log("Should respawn player");
            gameManager.RespawnPlayerThree(gameManager.playerThreePrefab);
            currentHealth = maxHealth;
            healthPercentage = currentHealth / maxHealth;
            healthBar.fillAmount = healthPercentage;
            if (gameManager.PlayerLives == 2)
            {
                Life3.fillAmount = 0;
            }
            else if (gameManager.PlayerLives == 1)
            {
                Life2.fillAmount = 0;
            }
            else
            {
                Life1.fillAmount = 0;
            }

        }
        else if (pawnNum == 4)
        {

            Debug.Log("Should respawn player");
            gameManager.RespawnPlayerFour(gameManager.playerFourPrefab);
            currentHealth = maxHealth;
            healthPercentage = currentHealth / maxHealth;
            healthBar.fillAmount = healthPercentage;
            if (gameManager.PlayerLives == 2)
            {
                Life3.fillAmount = 0;
            }
            else if (gameManager.PlayerLives == 1)
            {
                Life2.fillAmount = 0;
            }
            else
            {
                Life1.fillAmount = 0;
            }

        }
        else if (pawnNum == 5)
        {
            Debug.Log("Should respawn player");
            gameManager.RespawnAIOne(gameManager.aiTank1);
            currentHealth = maxHealth;
            healthPercentage = currentHealth / maxHealth;
            
        }
        else if (pawnNum == 6)
        {
            Debug.Log("Should respawn player");
            gameManager.RespawnAITwo(gameManager.aiTank2);
            currentHealth = maxHealth;
            healthPercentage = currentHealth / maxHealth;
        }
        else if (pawnNum == 7)
        {
            Debug.Log("Should respawn player");
            gameManager.RespawnAIThree(gameManager.aiTank3);
            currentHealth = maxHealth;
            healthPercentage = currentHealth / maxHealth;
        }
    }

    public void Die(Pawn source)
    {
        Destroy(gameObject);
        Debug.Log("Destroyed Player");
    }

}
