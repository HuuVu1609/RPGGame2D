using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI settings")]
    [SerializeField] private Slider[] slider;
    [SerializeField] private Image[] image;
    [SerializeField] private GameObject healthBoss;
    [SerializeField] private GameObject gamePlayUI;
    [SerializeField] private GameObject pauseUI;

    [Space]
    [SerializeField] private PlayerController playerController;

    [Space]
    [SerializeField] private BossController bossController;
    [Space]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        bossController = GameObject.FindAnyObjectByType<BossController>();

        slider[0].maxValue = 100;
        slider[0].value = playerController.health;

        slider[1].maxValue = 1000;
        slider[1].value = playerController.health;
    }
    private void Update()
    {
        PlayerHealthUI();
        BossHealthUI();
    }

    //Player
    private void PlayerHealthUI()
    {
        if (slider == null) return;
        slider[0].value = playerController.health;
    }
    public IEnumerator SkillUI(int count, float time)
    {
        image[count].color = new Color(0.15f, 0.15f, 0.15f, 0.6f);
        yield return new WaitForSeconds(time);
        image[count].color = Color.white;
    }

    //Boss
    private void BossHealthUI()
    {
        if (slider == null || bossController == null) return ;
        slider[1].value = bossController.health;
    }
    public void IsBossHealthUI()
    {
        healthBoss.SetActive(true);
    }

    //Background
    public void LoadLeverUI()
    {
        if(anim == null) return;
        anim.SetTrigger("start");
    }

    //Button
    public void PauseUI()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
        gamePlayUI.SetActive(false);
    }
}
