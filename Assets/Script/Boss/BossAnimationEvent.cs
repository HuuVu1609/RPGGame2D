using UnityEngine;

public class BossAnimationEvent : MonoBehaviour
{
    private BossController bossController;

    private void Start()
    {
        bossController = GetComponentInParent<BossController>();
    }
    public void BossDealthEvent()
    {
        bossController.BossDeath();
    }

    public void BossSkill2Start()
    {
        StartCoroutine(BossAttack2.instance.BossFireBall());
    }
    public void BossEndHit()
    {
        bossController.EndHit();
    }
}
