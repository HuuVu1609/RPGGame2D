using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Slider[] sl;
    [SerializeField] private AudioSource[] au;
    [SerializeField] private GameObject GamePlayUI;

    private void Start()
    {
        GetAudio();
    }

    private void GetAudio()
    {
        int count = Mathf.Min(sl.Length, au.Length);

        for (int i = 0; i < count; i++)
        {
            int index = i;
            sl[index].maxValue = 1f;
            sl[index].value = au[index].volume;

            sl[index].onValueChanged.AddListener((v) =>
            {
                au[index].volume = v;
            });
        }
    }
    public void PauseContinue()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        GamePlayUI.SetActive(true);
    }
    public void PauseOut()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void PauseReplay()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        GamePlayUI.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
