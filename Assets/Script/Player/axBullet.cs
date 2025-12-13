using UnityEngine;

public class AxBullet : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    private void Update()
    {
        transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z - 10);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //boss
        var bossCtrl = collision.GetComponent<BossController>();
        if (bossCtrl != null)
        {
            bossCtrl.TakeDamage(30);
            gameObject.SetActive(false);
        }

        //enemy
        var enemyCtrl = collision.GetComponent<EnemyController>();
        if (enemyCtrl != null)
        {
            enemyCtrl.TakeDamage(30);
            gameObject.SetActive(false);
        }
    }
}
