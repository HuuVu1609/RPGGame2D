using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private Transform playerTran;

    private void Start()
    {
        playerTran = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(gameObject, 10f );
    }

    private void Update()
    {
        CheckPosPlayer();
    }

    private void CheckPosPlayer()
    {
        if (playerTran != null)
        {
            if (transform.position.x > playerTran.position.x)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //boss
        var bossCtrl = collision.GetComponent<BossController>();
        if(bossCtrl != null)
        {
                bossCtrl.TakeDamage(50);
        }

        //enemy
        var enemyCtrl = collision.GetComponent<EnemyController>();
        if (enemyCtrl != null)
        {
            enemyCtrl.TakeDamage(50);
        }
    }

}
