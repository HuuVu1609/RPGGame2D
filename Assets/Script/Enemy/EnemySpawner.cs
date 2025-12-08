using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public List<GameObject> enemyPrefabs;  
    public Transform[] spawnPoints;
    public GameObject nextLever;

    [Header("Barrier")]
    public Collider2D barrierCollider;

    private int enemyCount = 0;

    void Start()
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        enemyCount = spawnPoints.Length;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            var prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            var pos = spawnPoints[Random.Range(0, spawnPoints.Length)];

            var enemyGO = Instantiate(prefab, pos.position, Quaternion.identity);

            var enemyCtrl = enemyGO.GetComponent<EnemyController>();

            enemyCtrl.OnEnemyDead += CheckEnemyDead;
        }
    }

    private void CheckEnemyDead(EnemyController enemy)
    {
        if(nextLever == null || barrierCollider == null) return;
        enemyCount--;

        if (enemyCount <= 0)
        {
            barrierCollider.isTrigger = true;
            nextLever.SetActive(true);
        }
    }
}
