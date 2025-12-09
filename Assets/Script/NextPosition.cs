using UnityEngine;

public class NextPosition : MonoBehaviour
{
    [SerializeField] private Transform nextPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerCtrl = collision.gameObject.GetComponent<PlayerController>();
            if (playerCtrl != null)
            {
                playerCtrl.transform.position = nextPos.position;
            }
        }
    }
}
