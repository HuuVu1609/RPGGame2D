using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected Transform playerTran;
    [SerializeField] private float moveSpeed = 5f;

    [Header("AttackSetting")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange =3f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] protected float playerRange = 5f;
    [SerializeField] private float playerDamage = 5f;
    [SerializeField] private float knockbackForce = 30;
    protected float dist;

    [Header("HetlhSettings")]
    [SerializeField] [Range(0f, 100f)] private float maxHealth = 100f;
    public float health { get; private set; }

    [Header("DamagedSettings")]
    [SerializeField] private float klockbackForce;

    public event Action<EnemyController> OnEnemyDead;

    protected Rigidbody2D rb;
    protected Animator anim;

    // Enemy move settings
    private float direction = 1f;
    private Vector3 startPos;
    private float leftPosX;
    private float rightPosX;

    protected bool isMove;
    protected bool isAttack;
    protected virtual void Start()
    {
        playerTran = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        startPos = transform.position;
        leftPosX = startPos.x - 3f;
        rightPosX = startPos.x + 3f;

        health = maxHealth;

    }

    private void FixedUpdate()
    {
        CheckPlayer();
        EnemyMove();
    }
    // DAMAGE && DEATH
    public void TakeDamage(float damage)
    {
        Debug.Log($"damage: {damage}");
        health = Mathf.Max(health - damage, 0f);
        if(transform.position.x > playerTran.position.x)
        {
           rb.linearVelocity = new Vector2(- klockbackForce, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(klockbackForce, rb.linearVelocity.y);
        }
        anim.SetTrigger("hit");
        if(health <= 0f)
        {
            rb.linearVelocity = Vector3.zero;
            gameObject.layer = 0;
            anim.SetTrigger("die");
        }
        isAttack = false;
        isMove = false;
    }
    public void EnemyDeath()
    {
        OnEnemyDead?.Invoke(this);
        Destroy(this.gameObject);
    }

    //CHECK PLAYER
    protected virtual void CheckPlayer()
    {
        dist = Vector3.Distance(transform.position, playerTran.position);
        if (dist < playerRange)
        {
            isAttack = true;
            isMove = false;
            if (playerTran.position.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                direction = -1;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                direction = 1;
            }   
        }
        else
        {
            isAttack = false;
            isMove = true;
        }
        anim.SetBool("attack", isAttack);
    }

    // MOVE
    private void EnemyMove()
    {
        if (isMove == true)
        {
            rb.linearVelocity = new Vector2(moveSpeed * direction, rb.linearVelocity.y);

            if (transform.position.x > rightPosX)
            {
                direction = -1;
                transform.rotation = Quaternion.Euler(0, 180,0);
            }
            else if (transform.position.x < leftPosX)
            {
                direction = 1;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

    }

    // ATTACK
    public virtual void EnemyAttack()
    {
        if (isAttack == true)
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
            if (playerCollider != null)
            {
                var playCtrl = playerCollider.GetComponent<PlayerController>();
            
                if(playCtrl != null)
                {
                    playCtrl.TakeDamage(playerDamage, knockbackForce);
                }
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        if (playerTran == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.right * playerRange);


        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
