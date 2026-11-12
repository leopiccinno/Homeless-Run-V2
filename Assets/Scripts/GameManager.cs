using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField] private int targetCoins = 3;

    [Header("UI")]
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text levelClearedText;

    [Header("Portal")]
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
        collectedCoins = 0;

        if (portal != null)
        {
            portal.SetActive(false);
        }

        if (levelClearedText != null)
        {
            levelClearedText.gameObject.SetActive(false);
        }

        UpdateCoinUI();
    }

    private void AddCoin()
    {
        collectedCoins++;

        UpdateCoinUI();

        Debug.Log($"Coins: {collectedCoins}/{targetCoins}");

        if (collectedCoins >= targetCoins)
        {
            Debug.Log("LEVEL CLEARED!");

            if (portal != null)
            {
                portal.SetActive(true);
            }

            if (levelClearedText != null)
            {
                levelClearedText.gameObject.SetActive(true);
            }
        }
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = $"{collectedCoins}/{targetCoins} Coins";
        }
    }
}