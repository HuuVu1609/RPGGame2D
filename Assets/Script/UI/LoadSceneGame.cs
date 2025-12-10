using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneGame : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("Game");
    }
}
