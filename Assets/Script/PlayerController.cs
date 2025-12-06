using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("MoveSettings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float inputH;

    [Header("JumpSettings")]
    [Range(5,10)]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform checkGround;
    [SerializeField] private float checkGroundRange = 1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded;

    [Header("AttackSettings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float atkRange;
    [SerializeField] private LayerMask enemyLayer;
    private int attackCount;
    private float comboResetTimer = 0.5f;
    private float comboCounter;

    [Header("HealthSettings")]
    [SerializeField] private float health;
    private float maxHealth = 100;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        attackCount = 0;
        health = maxHealth;
    }
    private void Update()
    {
        PlayerInput();
        PlayerMove();
        PlayerFlip();
        CheckGround();
        ResetComboTimer();
        PlayerAnimation();
        PlayerGravity();
    }
    private void PlayerInput()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayerAttackCount();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Damage();
        }
    }
    private void Damage()
    {
        rb.linearVelocity = new Vector2(1000, rb.linearVelocity.y);
    }
    private void PlayerAnimation()
    {
        anim.SetFloat("MoveX", rb.linearVelocity.x);
        anim.SetFloat("MoveY", rb.linearVelocity.y);
        anim.SetBool("Jump", !isGrounded);
    }
    private void PlayerMove()
    {
        rb.linearVelocity = new Vector2(inputH * moveSpeed , rb.linearVelocity.y);
    }
    private void CheckGround() => isGrounded = Physics2D.Raycast(checkGround.position, Vector2.down, checkGroundRange, groundLayer);
    private void PlayerJump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x,jumpForce);
        }   
    }
    private void PlayerGravity()
    {
        if (rb.linearVelocity.y < 0 && !isGrounded)
        {
            rb.gravityScale = 3;
        }
        else if (rb.linearVelocity.y == 0 && isGrounded)
        {
            rb.gravityScale = 1;
        }
    }
    private void PlayerFlip()
    {
        if (inputH > 0)
        {
            //sr.flipX = false;
            //attackPoint.localPosition = new Vector3(Mathf.Abs(attackPoint.localPosition.x), attackPoint.localPosition.y, 0);
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else if (inputH < 0)
        {
            //sr.flipX = true;
            //attackPoint.localPosition = new Vector3(- Mathf.Abs(attackPoint.localPosition.x), attackPoint.localPosition.y, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void PlayerAttack(float damage)
    {
        Collider2D[] enemyCtrl = Physics2D.OverlapCircleAll(attackPoint.position, atkRange, enemyLayer);
        foreach(Collider2D enemy in enemyCtrl)
        {
            var ene = enemy.GetComponent<EnemyController>();
            if (ene != null)
                ene.TakeDamage(damage);
        }
        
    }
    private void PlayerAttackCount()
    {
        switch (attackCount)
        {
            case 0:
                anim.SetTrigger("atk1");
                PlayerAttack(10);
                break;
            case 1:
                anim.SetTrigger("atk2");
                PlayerAttack(20);
                break;
        }

        attackCount++;
        comboCounter = comboResetTimer;

        if (attackCount > 1)
            attackCount = 0;
    }

    private void ResetComboTimer()
    {
        if (comboCounter > 0)
        {
            comboCounter -= Time.deltaTime;
            if (comboCounter <= 0)
                attackCount = 0;
        }
    }
    public void TakeDamage(float damage, float knockbackForce, Transform attacker)
    {
        health = Mathf.Max(0, health - damage);
        anim.SetTrigger("hit");

        float dir = Mathf.Sign(transform.position.x - attacker.position.x);
        rb.linearVelocity = new Vector2(dir * knockbackForce, rb.linearVelocity.y);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(checkGround.position, checkGround.position + Vector3.down * checkGroundRange);
        Gizmos.DrawWireSphere(attackPoint.position, atkRange);
    }
}
