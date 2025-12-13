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

    [Header("RollSettings")]
    [SerializeField] private float rollSpeed = 10f;
    [SerializeField] private Collider2D playerCollider;
    private int rollFlip = 1;
    private bool isRoll;

    [Header("AttackSettings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float atkRange;
    [SerializeField] private LayerMask enemyLayer;
    private int attackCount;
    private float comboResetTimer = 0.5f;
    private float comboCounter;

    [Header("HealthSettings")]
    [SerializeField] private float maxHealth;
    public float health { get; set; }
    

    [Header("Skill")]
    [SerializeField] private PlayerSkill skill;

    private Rigidbody2D rb;
    public Animator anim { get; private set; }
    private SpriteRenderer sr;
    private bool isdeath;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();

        attackCount = 0;
        health = maxHealth;
        isdeath = false;
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
        //input
        if(isdeath) return;
        inputH = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            PlayerJump();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            PlayerAttackCount();

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            PlayerRollLayerStart();
            anim.SetTrigger("roll");
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skill.Skill1Start();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
            skill.Skill2Start();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            skill.Skill3Start();

    }

    // ANIMATION
    private void PlayerAnimation()
    {
        anim.SetFloat("MoveX", rb.linearVelocity.x);
        anim.SetFloat("MoveY", rb.linearVelocity.y);
        anim.SetBool("Jump", !isGrounded);
    }

    //MOVE, JUMP && ROLL
    private void PlayerMove()
    {
        if (isRoll) return;
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
        else if (rb.linearVelocity.y >= 0 && isGrounded)
        {
            rb.gravityScale = 1;
        }
    }
    public void PlayerRollStart()
    {
         isRoll = true;
         rb.linearVelocity = new Vector2(rollSpeed * rollFlip, 0);

    }
    private void PlayerRollLayerStart()
    {
        playerCollider.excludeLayers = enemyLayer;
        rb.excludeLayers = enemyLayer;
    }
    public void PlayerRollEnd()
    {
        isRoll = false;
        playerCollider.excludeLayers = 0;
        rb.excludeLayers = 0;
        rb.linearVelocity = new Vector2(0 , 0);
    }

    // FLIP
    private void PlayerFlip()
    {
        if (inputH > 0)
        {
            //sr.flipX = false;
            //attackPoint.localPosition = new Vector3(Mathf.Abs(attackPoint.localPosition.x), attackPoint.localPosition.y, 0);
            transform.rotation = Quaternion.Euler(0,0,0);
            rollFlip = 1;
        }
        else if (inputH < 0)
        {
            //sr.flipX = true;
            //attackPoint.localPosition = new Vector3(- Mathf.Abs(attackPoint.localPosition.x), attackPoint.localPosition.y, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rollFlip = -1;
        }
    }

    // ATTACK
    private void PlayerAttack(float damage)
    {
        Collider2D[] enemyCtrl = Physics2D.OverlapCircleAll(attackPoint.position, atkRange, enemyLayer);
        foreach(Collider2D enemy in enemyCtrl)
        {
            var ene = enemy.GetComponent<EnemyController>();
            var boss = enemy.GetComponent<BossController>();
            if (ene != null)
                ene.TakeDamage(damage);
            if (boss != null)
                boss.TakeDamage(damage);
        }
        
    }
    private void PlayerAttackCount()
    {
        switch (attackCount)
        {
            case 0:
                anim.SetTrigger("atk1");
                PlayerAttack(5);
                break;
            case 1:
                anim.SetTrigger("atk2");
                PlayerAttack(10);
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

    // DAMAGE && DEATH
    public void TakeDamage(float damage, float knockbackForce)
    {
        health = Mathf.Max(0, health - damage);
        anim.SetTrigger("hit");
        rb.linearVelocity = new Vector2(-knockbackForce, rb.linearVelocity.y);
        if( health <= 0)
        {
            isdeath = true;
            gameObject.layer = 0;
            anim.SetTrigger("die");

        }
    }

    // GIZMOS
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(checkGround.position, checkGround.position + Vector3.down * checkGroundRange);
        Gizmos.DrawWireSphere(attackPoint.position, atkRange);
    }


    // COLLIDER
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Load"))
        {
            UIManager.Instance.LoadLeverUI();
        }
    }
}
