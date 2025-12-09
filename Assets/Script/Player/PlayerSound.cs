using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [Header("AudioSettings")]
    [SerializeField] private AudioSource playerSound;
    [SerializeField] private AudioClip foolClip;
    [SerializeField] private AudioClip jupmClip;
    [SerializeField] private AudioClip atkClip;
    public void FootStep()
    {
        playerSound.PlayOneShot(foolClip);
    }
    public void JumpAudio()
    {
        playerSound.PlayOneShot(jupmClip);
    }
    public void AttackAudio()
    {
        playerSound.PlayOneShot(atkClip);
    }
}
