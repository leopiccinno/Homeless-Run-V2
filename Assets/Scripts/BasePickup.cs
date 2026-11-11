using UnityEngine;

public abstract class BasePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        OnPickup();

        Destroy(gameObject);
    }

    protected abstract void OnPickup();
}