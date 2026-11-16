using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform platformsParent;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float rotationSpeed = 12f;

    [Header("Teleport")]
    [SerializeField] private float teleportInterval = 5f;
    [SerializeField] private float teleportHeight = 1f;
    [SerializeField] private float minimumTeleportDistance = 15f;

    private Rigidbody rb;
    private Animator animator;

    private float teleportTimer;

    public bool CanChase { get; set; }

    private Vector3 spawnPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        spawnPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (!CanChase)
        {
            rb.linearVelocity = Vector3.zero;

            if (animator != null)
            {
                animator.SetFloat("Speed", 0f);
            }

            return;
        }

        teleportTimer += Time.fixedDeltaTime;

        if (teleportTimer >= teleportInterval)
        {
            TeleportNearPlayer();
            teleportTimer = 0f;
        }

        MoveTowardsPlayer();

        if (transform.position.y < -10f)
        {
            Respawn();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;

        direction.y = 0f;

        if (direction.magnitude > 1f)
        {
            direction.Normalize();

            rb.linearVelocity = new Vector3(
                direction.x * moveSpeed,
                rb.linearVelocity.y,
                direction.z * moveSpeed
            );

            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );

            if (animator != null)
            {
                animator.SetFloat("Speed", 1f);
            }
        }
        else
        {
            rb.linearVelocity = Vector3.zero;

            if (animator != null)
            {
                animator.SetFloat("Speed", 0f);
            }
        }
    }

    private void TeleportNearPlayer()
    {
        List<Transform> validPlatforms =
            new List<Transform>();

        foreach (Transform platform in platformsParent)
        {
            float distance =
                Vector3.Distance(
                    platform.position,
                    player.position);

            if (distance >= minimumTeleportDistance)
            {
                validPlatforms.Add(platform);
            }
        }

        if (validPlatforms.Count == 0)
            return;

        Transform randomPlatform =
            validPlatforms[
                Random.Range(0, validPlatforms.Count)
            ];

        rb.linearVelocity = Vector3.zero;

        transform.position =
            randomPlatform.position +
            Vector3.up * teleportHeight;
    }

    private void Respawn()
    {
        rb.linearVelocity = Vector3.zero;
        transform.position = spawnPosition;
    }
}
