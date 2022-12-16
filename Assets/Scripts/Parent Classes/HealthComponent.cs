using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{

    public float maxHealth;
    public float currentHealth;
    public float healthPercentage;

    public GameManager gameManager;

    public Image healthBar;
    public Image Life1;
    public Image Life2;
    public Image Life3;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        currentHealth = maxHealth;
        healthPercentage = currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;
    }

    public void TakeDamage(float amount, Pawn source)
    {
        currentHealth = currentHealth - amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthPercentage = currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;
        Debug.Log(source.name + " did " + amount + " damage to " + gameObject.name);
        if (currentHealth <= 0)
        {
            Die(source); 
        }
    }

    public void Heal(float amount)
    {
        currentHealth = currentHealth + amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthPercentage = currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;

    }

    public void Die(Pawn source)
    {
        gameManager.PlayerLives--;
        Debug.Log(gameManager.PlayerLives);
        if (gameManager.PlayerLives > 0)
        {
            Debug.Log("Should respawn player");
            gameManager.OnePlayerSpawn(gameManager.tankPawnPrefab);
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
        Destroy(gameObject);
        Debug.Log("Destroyed Player");

    }

}
