using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 spawnPosition;

    private void Start()
    {
        spawnPosition = transform.position;
    }

    public void Respawn()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.linearVelocity = Vector3.zero;

        transform.position = spawnPosition;
    }
}