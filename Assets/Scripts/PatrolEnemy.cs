using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 8f;
    [SerializeField] private float waitTime = 1f;

    private Transform target;
    private Animator animator;

    private bool waiting;

    private void Start()
    {
        animator = GetComponent<Animator>();
        target = pointB;
    }

    private void Update()
{
    if (waiting)
        return;

    transform.position = Vector3.MoveTowards(
        transform.position,
        target.position,
        moveSpeed * Time.deltaTime
    );

    Vector3 direction =
        target.position - transform.position;

    direction.y = 0f;

    if (direction.sqrMagnitude > 0.01f)
    {
        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
    }

    if (Vector3.Distance(
            transform.position,
            target.position) <= 0.1f)
    {
        StartCoroutine(ChangeDirection());
    }

    if (animator != null)
    {
        animator.SetFloat("Speed", 1f);
    }
}

    private System.Collections.IEnumerator ChangeDirection()
    {
        waiting = true;

        if (animator != null)
        {
            animator.SetFloat("Speed", 0f);
        }

        yield return new WaitForSeconds(waitTime);

        target =
            target == pointA
            ? pointB
            : pointA;

        waiting = false;
    }
}