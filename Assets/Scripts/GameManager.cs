using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Coins")]
    [SerializeField] private int targetCoins = 3;

    [Header("UI")]
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text levelClearedText;
    [SerializeField] private TMP_Text youDiedText;

    [Header("Portal")]
    [SerializeField] private GameObject portal;

    private int collectedCoins;

    private void Awake()
    {
        Instance = this;
    }

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

        portal.SetActive(false);

        if (levelClearedText != null)
            levelClearedText.gameObject.SetActive(false);

        if (youDiedText != null)
            youDiedText.gameObject.SetActive(false);

        UpdateCoinUI();
    }

    private void AddCoin()
    {
        collectedCoins++;

        UpdateCoinUI();

        if (collectedCoins >= targetCoins)
        {
            portal.SetActive(true);

            if (levelClearedText != null)
                levelClearedText.gameObject.SetActive(true);
        }
    }

    public void PlayerDied(GameObject player)
    {
        collectedCoins = 0;

        UpdateCoinUI();

        if (portal != null)
            portal.SetActive(false);

        if (youDiedText != null)
        {
            youDiedText.gameObject.SetActive(true);

            CancelInvoke(nameof(HideDeathText));
            Invoke(nameof(HideDeathText), 2f);
        }

        PlayerRespawn respawn =
            player.GetComponent<PlayerRespawn>();

        if (respawn != null)
            respawn.Respawn();
    }

    private void HideDeathText()
    {
        if (youDiedText != null)
            youDiedText.gameObject.SetActive(false);
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text =
                $"{collectedCoins}/{targetCoins} Coins";
        }
    }
}