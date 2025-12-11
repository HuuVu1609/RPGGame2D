using UnityEngine;

public class NextLeverBoss : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.IsBossHealthUI();
            anim.SetTrigger("start");
            sr.color = Color.red;
        }
    }
}
