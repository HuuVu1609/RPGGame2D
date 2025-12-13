using UnityEngine;
using TMPro;

public class PlayerNameDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Transform target;
    private RectTransform rectTransform;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        string savedName = PlayerPrefs.GetString("PlayerName", "Player");
        nameText.text = savedName;
    }
    private void Update()
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
