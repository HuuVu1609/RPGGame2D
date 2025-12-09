using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [Header("AudioSettings")]
    [SerializeField] private AudioSource enemySound;
    [SerializeField] private AudioClip enemyDeathClip;

    public void enemyDeathAudio()
    {
        enemySound.PlayOneShot(enemyDeathClip);
    }
}
