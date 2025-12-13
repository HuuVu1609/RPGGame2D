using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private Transform target;
    private RectTransform rectTransform;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        slider.maxValue = 100;
        //slider.value = enemyController.health;
    }
    private void Update()
    {
        CanvasFlip();
        if (enemyController != null)
            slider.value = enemyController.health;
    }

    private void CanvasFlip()
    {
        if (target.rotation.y == 180)
        {
            rectTransform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            rectTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
