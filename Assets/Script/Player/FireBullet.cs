using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private Transform playerTran;

    private void Start()
    {
        playerTran = GameObject.FindGameObjectWithTag("Player").transform;
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
        if (collision.gameObject.CompareTag("Boss"))
        {

            var bossCtrl = collision.GetComponent<BossController>();
            if(bossCtrl != null)
            {
                bossCtrl.TakeDamage(50);
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemyCtrl = collision.GetComponent<EnemyController>();
            if (enemyCtrl != null)
            {
                enemyCtrl.TakeDamage(50);
            }
        }
    }

}
