using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance { get; set; }
    public float currentHealth;
    public float maxHealth;
    public PlayerDie playerDie;
    [SerializeField] private GameObject Player;
    private int lives = 0;
    private void Awake()
    {
        currentHealth = maxHealth;
        if (Instance != null && Instance!=this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        playerDie = Player.GetComponent<PlayerDie>();
    }
    //private void Update()
    //{
    //    if (currentHealth <= 0 && lives <= 3)
    //    {
    //        currentHealth = 0;
    //        playerDie.Die();
    //        lives++;

    //    }
    //    else if (currentHealth <= 0 && lives > 3)
    //    {
    //        // gameOver
    //        Debug.Log("Game Over");
    //    }
    //}
  
}
