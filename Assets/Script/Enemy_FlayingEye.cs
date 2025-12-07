using UnityEngine;

public class Enemy_FlayingEye : EnemyController
{
    public float diveSpeed = 10;
    protected override void CheckPlayer()
    {
         base.CheckPlayer();
        if(dist < playerRange)
        {
            Vector2 dir = (playerTran.position - transform.position).normalized;
            rb.linearVelocity = dir * diveSpeed;
            if (dist < 0.5 )
            {
                diveSpeed = 0;
            }
        }
    }
}
