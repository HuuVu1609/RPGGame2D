using UnityEngine;

public class NextLeverBoss : MonoBehaviour
{
    [SerializeField] private Collider2D isCollider;
    [SerializeField] private Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollider.isTrigger = false;
            anim.SetBool("start",true);
            AudioManager.Instance.BossSound();
            UIManager.Instance.IsBossHealthUI();
        }
    }
}
