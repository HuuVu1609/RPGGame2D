using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private PlayerController controller;

    private void Start()
    {
        controller = GetComponentInParent<PlayerController>();
    }
    public void StartPlayerRoll()
    {
        controller.PlayerRollStart();
    }
    public void EndPlayerRoll()
    {
        controller.PlayerRollEnd();
    }
}
