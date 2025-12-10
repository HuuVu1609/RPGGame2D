using UnityEngine;

public class axBullet : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z - 10);
    }
}
