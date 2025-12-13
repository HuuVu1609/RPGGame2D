using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [Header("AudioSettings")]
    [SerializeField] private AudioSource Sound;
    [SerializeField] private AudioClip[] backgoundClip;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        PlayMusic(0);
    }
    public void PlayMusic(int index)
    {
        if (index < 0 || index >= backgoundClip.Length) return;

        Sound.Stop();
        Sound.clip = backgoundClip[index];
        Sound.Play();
    }

    public void BossSound()
    {
        PlayMusic(1);
    }
}
