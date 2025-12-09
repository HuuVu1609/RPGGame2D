using UnityEngine;

public class Enemy_FlayingEye : EnemyController
{
    public float diveSpeed = 10f;
    public float checkAtkRange = 1f;
    private Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;  
    }
    private void Update()
    {
        EnemyAttack();
    }
    protected override void CheckPlayer()
    {
        base.CheckPlayer();

        if (dist < playerRange )
        {
            Vector2 dir = (playerTran.position - transform.position).normalized;
            isMove = false;
            if(dist > checkAtkRange)
            {
                rb.linearVelocity = dir * diveSpeed;
            }
            else
            {
                rb.linearVelocity = dir * 0;

            }
        }
        else 
        {
            isMove = true;
            isAttack = false;
            transform.position = Vector2.MoveTowards(transform.position, startPos, diveSpeed* Time.deltaTime);
        }
    }
}
