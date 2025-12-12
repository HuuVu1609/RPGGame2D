using UnityEngine;
using TMPro;

public class NameInputManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    //public GameObject namePanel;   

    public void OnClickOK()
    {
        string playerName = nameInput.text;

        if (playerName.Length == 0)
            return;

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();

        //namePanel.SetActive(false); 
    }

    //private void Start()
    //{
    //    if (PlayerPrefs.HasKey("PlayerName"))
    //    {
    //        namePanel.SetActive(false);
    //    }
    //}
}
