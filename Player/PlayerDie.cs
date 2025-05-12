using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerDie : MonoBehaviour
{
    private float currentHealth;
    [SerializeField]
    private GameObject deathChunkParticles, deathBlookParticles;

    private GameMamager GM;
    public int lives = 0;
    void Start()
    {
        currentHealth = States.Instance.currentHealth;
        GM = GameObject.Find("GameMamager").GetComponent<GameMamager>();
    }
    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0 && lives <= 3)
        {
            Die();
            lives++;
            Debug.Log("Player died. Lives left: " + (3 - lives));
        }
       
        else if (currentHealth <= 0 && lives > 3)
        {
            // gameOver
        }
    }

    public void Die()
    {
        Instantiate(deathChunkParticles, transform.position, deathChunkParticles.transform.rotation);
        Instantiate(deathBlookParticles, transform.position, deathBlookParticles.transform.rotation);
        GM.Respawn();
        Destroy(gameObject);
    }
}
