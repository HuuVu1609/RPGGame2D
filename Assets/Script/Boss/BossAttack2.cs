using System.Collections;
using UnityEngine;

public class BossAttack2 : MonoBehaviour
{
    public static BossAttack2 instance;

    [SerializeField] private Transform atk2Tran;
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private int bulletCount;
    [SerializeField] private float spawnRange;
    [SerializeField] private float spawnTime = 0.5f;
    private void Start()
    {
        if(instance == null)
            instance = this;
    }

    //ATTACK
    public IEnumerator BossFireBall()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 spawnPos = new Vector2(atk2Tran.position.x + Random.Range(-spawnRange, spawnRange), atk2Tran.position.y);
            Instantiate(fireBallPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }
        bulletCount++;
    }

    //GIZMOS
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(atk2Tran.position, spawnRange);
    }
}
