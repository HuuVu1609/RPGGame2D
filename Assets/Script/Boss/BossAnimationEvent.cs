using UnityEngine;
using System.Collections;
public class BossAnimationEvent : MonoBehaviour
{
    private BossController bossController;

    private void Start()
    {
        bossController = GetComponentInParent<BossController>();
    }
    private void BossDeathEvent()
    {
        bossController.BossDeath();
        UIManager.Instance.WinUI();
    }

    public void BossSkill2Start()
    {
        StartCoroutine(BossAttack2.instance.BossFireBall());
    }
}
