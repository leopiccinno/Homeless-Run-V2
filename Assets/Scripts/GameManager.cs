using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int targetCoins = 3;

    [SerializeField] private GameObject portal;

    private int collectedCoins;

    private void OnEnable()
    {
        CoinPickup.CoinCollected += AddCoin;
    }

    private void OnDisable()
    {
        CoinPickup.CoinCollected -= AddCoin;
    }

    private void Start()
    {
        if (portal != null)
            portal.SetActive(false);
    }

    private void AddCoin()
    {
        collectedCoins++;

        Debug.Log($"Coins: {collectedCoins}/{targetCoins}");

        if (collectedCoins >= targetCoins)
        {
            if (portal != null)
                portal.SetActive(true);
        }
    }
}
