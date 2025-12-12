using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadSceneGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] Animator anim;

    public void NextScene()
    {
        StartCoroutine(ButtonCtrl());
    }
    private IEnumerator ButtonCtrl()
    {
        text.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        text.color = Color.white;
        //anim.SetBool("start",true);
        //yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game");
    }
}
