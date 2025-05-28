using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpikedBall : MonoBehaviour
{
    [SerializeField] private GameObject spikedBall;       
    [SerializeField] private float speed = -5f;          
    [SerializeField] private Transform dropPoint;        

    private bool isPlayerInTrigger = false;               
    private bool hasSpawned = false;                    

    private void Update()
    {
        if (isPlayerInTrigger && !hasSpawned)
        {
            hasSpawned = true;
            SpawnSpikedBall();
        }
    }

    private void SpawnSpikedBall()
    {
        GameObject spawnBall = Instantiate(spikedBall, dropPoint.position, Quaternion.identity);

        Rigidbody2D rb = spawnBall.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(0, speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            hasSpawned = false; 
        }
    }
}
