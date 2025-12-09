using UnityEngine;

public class FireBall : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerCtrl = collision.GetComponent<PlayerController>();
            if (playerCtrl != null)
                playerCtrl.TakeDamage(5f, 10f);
            Destroy(gameObject);
        }
    }
}
