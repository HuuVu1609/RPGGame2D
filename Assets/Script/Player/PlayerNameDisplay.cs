using UnityEngine;
using TMPro;

public class PlayerNameDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;

    void Start()
    {
        string savedName = PlayerPrefs.GetString("PlayerName", "Player");
        nameText.text = savedName;
    }
}
