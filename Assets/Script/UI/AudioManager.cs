using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSettings")]
    [SerializeField] private AudioSource Sound;
    [SerializeField] private AudioClip backgoundClip;

    private void Start()
    {
        Sound.clip = backgoundClip;
        Sound.Play();
    }
}
