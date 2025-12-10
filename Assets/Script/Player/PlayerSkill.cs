using UnityEngine;
using System.Collections; // Add this line

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] private PlayerController playerCtrl;
    [SerializeField] private GameObject[] skillPrefab;
    [SerializeField] private Transform SkillPoint;
    [SerializeField] private float skill1Speed = 10f;
    [SerializeField] private float skill2Speed = 20f;

    public float skill1Cooldown = 20f;
    public float skill2Cooldown = 15f;
    public float skill3Cooldown = 10f;
    private float timeSkill1;
    private float timeSkill2;
    private float timeSkill3;

    private void Start()
    {
        timeSkill1 = 21; timeSkill2 =21; timeSkill3 =21;
    }
    private void Update()
    {
        timeSkill1 += Time.deltaTime;
        timeSkill2 += Time.deltaTime;
        timeSkill3 += Time.deltaTime;
    }
    public void Skill1Start()
    {
        if(timeSkill1 >= skill1Cooldown)
        {
            playerCtrl.anim.SetTrigger("fire");
            StartCoroutine(UIManager.Instance.SkillUI(0, skill1Cooldown));
            GameObject fireBullet = Instantiate(skillPrefab[0], SkillPoint.position, SkillPoint.rotation);
            var rb = fireBullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2 (skill1Speed, 0);

            timeSkill1 = 0;
        }
    }
    public void Skill2Start()
    {
        if (timeSkill2 >= skill2Cooldown)
        {
            playerCtrl.anim.SetTrigger("fire");
            StartCoroutine(UIManager.Instance.SkillUI(1, skill2Cooldown));
            GameObject axBullet = Instantiate(skillPrefab[1], SkillPoint.position, SkillPoint.rotation);
            var rb = axBullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(skill2Speed, 0);
            timeSkill2 = 0;
        }
    }
    public void Skill3Start()
    {
        if (timeSkill3 >= skill3Cooldown)
        {
            StartCoroutine(UIManager.Instance.SkillUI(2, skill3Cooldown));
            playerCtrl.health = Mathf.Min(playerCtrl.health + 10, 100);

            timeSkill3 = 0;
        }
    }
}
