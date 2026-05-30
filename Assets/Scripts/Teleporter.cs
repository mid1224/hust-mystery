using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TeleportTo(Transform target)
    {
        if (target != null)
        {
            playerTransform.position = target.position;
        }
    }
}
