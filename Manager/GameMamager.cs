using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMamager : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;

    public EntityRespawner playerRespawner;
    private float respawnStartTime;

    private CinemachineVirtualCamera CVC;
    private HealthBar healthBar;
    public TextMeshProUGUI livesCounter;
    private int lives = 1;

    private States states;
    public int killEnemy = 0;
    public TextMeshProUGUI killCounter;
    // GameOver
    [SerializeField]private GameObject gameOver;
    [SerializeField]private GameObject results;
    [SerializeField]private Player player;

    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI monstersDefeatedText;
    private TextMeshProUGUI survivalTimeText;

    void Start()
    {
        CVC = GameObject.Find("PlayerCamera")?.GetComponent<CinemachineVirtualCamera>();
        healthBar = GameObject.Find("HealthBarContainer")?.GetComponent<HealthBar>();

        scoreText = results.transform.Find("Score")?.GetComponent<TextMeshProUGUI>();
        monstersDefeatedText = results.transform.Find("MonstersDefeated")?.GetComponent<TextMeshProUGUI>();
        survivalTimeText = results.transform.Find("SurvivalTime")?.GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {
        CheckRespawn();
        livesCounter.text = "X " + lives.ToString();
    }

    public void Respawn()
    {
        if (lives <= 0) return;

        respawnStartTime = Time.time;
        //playerRespawner.isRespawningPlayer = true;
        Debug.Log("Player will respawn...");
    }

    private void CheckRespawn()
    {
        // Player respawn
        if (playerRespawner.isRespawningPlayer && Time.time >= respawnStartTime + playerRespawner.respawnTime && lives > 0)
        {
            playerRespawner.isRespawningPlayer = false;

            GameObject newPlayer = Instantiate(playerRespawner.playerPrefabs[0], playerRespawner.playerRespawnPoints[0].position, playerRespawner.playerRespawnPoints[0].rotation);
            newPlayer.name = playerRespawner.playerPrefabs[0].name;

            if (CVC != null)
                CVC.m_Follow = newPlayer.transform;

            states = newPlayer.transform.Find("Core/States")?.GetComponent<States>();
            if (states != null)
            {
                states.currentHealth = states.maxHealth;
                healthBar.States = states;
            }
            lives--;
        }

        // Enemy respawn
        else if (playerRespawner.enemiesToRespawn.Count > 0 && Time.time >= respawnStartTime + playerRespawner.respawnTime)
        {
            var enemyData = playerRespawner.enemiesToRespawn.Dequeue();

            if (enemyData.prefab != null && enemyData.respawnPoint != null)
            {
                GameObject newEnemy = Instantiate(enemyData.prefab, enemyData.respawnPoint.position, Quaternion.identity);
                newEnemy.name = enemyData.prefab.name;

                states = newEnemy.transform.Find("Core/States")?.GetComponent<States>();
                if (states != null)
                {
                    states.currentHealth = states.maxHealth;
                }
                
            }
            else
            {
                Debug.LogWarning("Enemy data invalid for respawn.");
            }
        }

        // Game Over
        else if (lives <= 0 && !playerRespawner.isRespawningPlayer)
        {
            gameOver.SetActive(true);
            scoreText.text = "Score: " + (killEnemy*10).ToString();
            monstersDefeatedText.text = "Monsters Defeated: " + killEnemy.ToString();
            float time = Time.time;
            float survivalTime = time - player.gameStartTime;
            survivalTimeText.text = "Survival Time: " + FormatTime(survivalTime);
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
