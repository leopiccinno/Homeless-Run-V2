using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private static bool objectiveAlreadyShown = false;

    [Header("Coins")]
    [SerializeField] private int targetCoins = 3;

    [Header("UI")]
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text portalOpenedText;
    [SerializeField] private TMP_Text youDiedText;
    [SerializeField] private TMP_Text objectiveText;

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

        if (portal != null)
        {
            portal.SetActive(false);
        }

        if (portalOpenedText != null)
        {
            portalOpenedText.gameObject.SetActive(false);
        }

        if (youDiedText != null)
        {
            youDiedText.gameObject.SetActive(false);
        }

        if (objectiveText != null)
        {
            if (!objectiveAlreadyShown)
            {
                objectiveText.gameObject.SetActive(true);

                Invoke(nameof(HideObjectiveText), 4f);

                objectiveAlreadyShown = true;
            }
            else
            {
                objectiveText.gameObject.SetActive(false);
            }
        }

        UpdateCoinUI();
    }

    private void AddCoin()
    {
        collectedCoins++;

        UpdateCoinUI();

        if (collectedCoins >= targetCoins)
        {
            if (portal != null)
            {
                portal.SetActive(true);
            }

            if (portalOpenedText != null)
            {
                portalOpenedText.gameObject.SetActive(true);

                CancelInvoke(nameof(HidePortalOpenedText));
                Invoke(nameof(HidePortalOpenedText), 3f);
            }
        }
    }

    public void PlayerDied()
    {
        StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel()
    {
        if (youDiedText != null)
        {
            youDiedText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    private void HidePortalOpenedText()
    {
        if (portalOpenedText != null)
        {
            portalOpenedText.gameObject.SetActive(false);
        }
    }

    private void HideObjectiveText()
    {
        if (objectiveText != null)
        {
            objectiveText.gameObject.SetActive(false);
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