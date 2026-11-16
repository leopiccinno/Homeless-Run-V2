using UnityEngine;

public class EnemyKillPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        GameManager.Instance.PlayerDied();
    }
}