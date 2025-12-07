using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    [SerializeField] private Transform cameraTran;
    [SerializeField] private float moveSpeed;

    private void FixedUpdate()
    {
        transform.position = new Vector2(cameraTran.position.x * moveSpeed, transform.position.y);
    }
}
