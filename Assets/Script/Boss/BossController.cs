using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BossController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private Transform playerTran;

    [Header("Attack Settings")]
    [SerializeField] private float checkPlayerRange = 5f;
    [SerializeField] private float attackPlayerRange = 1f;

    [Header("Random Skill Settings")]
    [Range(0, 100)]
    [SerializeField] public int skill1Chance = 100; 
    [SerializeField] private float attackCooldown = 2f;
    private float lastAttackTime;

    [Header("Health Settings")]
    [SerializeField][Range(0, 1000)] private float maxHealth = 1000f;
    private float health;


    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        if (playerTran == null)
            playerTran = GameObject.FindGameObjectWithTag("Player").transform;

        health = maxHealth;
    }

    private void FixedUpdate()
    {
        BossCtrl();
        BossFlip();
    }
    // CHECK PLAYER
    private bool CheckPlayerRange()
    {
        if (playerTran == null) return false;
        return Vector3.Distance(transform.position, playerTran.position) <= checkPlayerRange;
    }

    private bool AttackPlayerRange()
    {
        if (playerTran == null) return false;
        return Vector3.Distance(transform.position, playerTran.position) <= attackPlayerRange;
    }

    // MOVE
    private void BossFlip()
    {
        if (transform.position.x > playerTran.position.x)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void BossMove()
    {
        if (!CheckPlayerRange()) return;

        transform.position = Vector3.MoveTowards(transform.position, playerTran.position, moveSpeed * Time.deltaTime);

        anim.SetBool("run", true);
    }

    // ATTACK
    private void BossAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;

        anim.SetBool("run", false);

        RandomAttack();

        lastAttackTime = Time.time;
    }

    private void RandomAttack()
    {
        int rand = UnityEngine.Random.Range(0, 100);

        if (rand < skill1Chance)
        {
            anim.SetTrigger("atk1");
        }
        else
        {
            anim.SetTrigger("atk2");
        }
    }

    private void BossCtrl()
    {
        if (health <= 300)
        {
            skill1Chance = 50;
            sr.color = Color.red;
        }

        if (AttackPlayerRange())
            BossAttack();
        else
            BossMove();
    }

    // DEATH && DAMAGE
    public void BossDeath()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);

        if (health > 0)
            anim.SetTrigger("hit");
        else
            anim.SetTrigger("die"); 
    }

    //GIZMOS
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkPlayerRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackPlayerRange);
    }
}
