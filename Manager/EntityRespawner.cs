using System.Collections.Generic;
using UnityEngine;

public class EntityRespawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyRespawnData
    {
        public string enemyName;
        public GameObject prefab;
        public Transform respawnPoint;
    }

    public GameObject[] playerPrefabs;
    public Transform[] playerRespawnPoints;

    public float respawnTime = 3f;

    public bool isRespawningPlayer = false;

    // Queue lưu enemy chờ respawn
    public Queue<EnemyRespawnData> enemiesToRespawn = new Queue<EnemyRespawnData>();
}
