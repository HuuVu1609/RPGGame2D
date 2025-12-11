using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private EnemyController enemyController;

    private void Start()
    {
        slider.maxValue = 100;
        //slider.value = enemyController.health;
    }
    private void Update()
    {
        if (enemyController != null)
        slider.value = enemyController.health;
    }
}
