using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform playerTran;
    [SerializeField] private float moveSpeed = 10f;

    [Header("AttackSetting")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange =3f;
    [SerializeField] private LayerMask playerLayer;

    [Header("HetlhSettings")]
    [SerializeField] [Range(0f, 100f)] private float hetlh = 100f;


    private Rigidbody2D rb;
    private Animator anim;
    public float playerRange = 5f;

    // Enemy move settings
    private float direction = 1f;
    private Vector3 startPos;
    private float leftPosX;
    private float rightPosX;

    private bool isMove;
    private bool isAttack;
    private void Start()
    {
        playerTran = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        startPos = transform.position;
        leftPosX = startPos.x - 3f;
        rightPosX = startPos.x + 3f;

    }

    private void FixedUpdate()
    {
        CheckPlayer();
        EnemyMove();
    }

    public void TakeDamage(float damage)
    {
        Debug.Log($"damage: {damage}");
        hetlh = Mathf.Max(hetlh - damage, 0f);
        anim.SetTrigger("hit");
        if(hetlh <= 0f)
        {
            anim.SetTrigger("die");
        }
        isAttack = false;
        isMove = false;
    }
    public void EnemyDeath()
    {
        Destroy(this.gameObject);
    }
    private void CheckPlayer()
    {
        float dist = Vector3.Distance(transform.position, playerTran.position);
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
    public void EnemyAttack()
    {
        if (isAttack == true)
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
            var playCtrl = playerCollider.GetComponent<PlayerController>();
            if(playCtrl != null)
            {
                playCtrl.TakeDamage(10, 30, transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (playerTran == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, playerTran.position);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }
}
