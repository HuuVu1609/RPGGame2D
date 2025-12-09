using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Slider slider;
    [SerializeField] private PlayerController playerController;
    
    [SerializeField] private Animator anim;


    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    private void Start()
    {
        slider.maxValue = 100;
        slider.value = playerController.health;
    }
    private void Update()
    {
        PlayerHealthUI();
    }

    private void PlayerHealthUI()
    {
        slider.value = playerController.health;
    }
    public void LoadLeverUI()
    {
        anim.SetTrigger("start");
    }
}
