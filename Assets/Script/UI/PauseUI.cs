using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Slider[] sl;
    [SerializeField] private AudioSource[] au;

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
}
