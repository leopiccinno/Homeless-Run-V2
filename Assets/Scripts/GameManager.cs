using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Coins")]
    [SerializeField] private int targetCoins = 3;

    [Header("UI")]
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text levelClearedText;
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

        if (levelClearedText != null)
        {
            levelClearedText.gameObject.SetActive(false);
        }

        if (youDiedText != null)
        {
            youDiedText.gameObject.SetActive(false);
        }

        if (objectiveText != null)
        {
            if (PlayerPrefs.GetInt("IntroShown", 0) == 0)
            {
                objectiveText.gameObject.SetActive(true);

                Invoke(nameof(HideObjectiveText), 4f);

                PlayerPrefs.SetInt("IntroShown", 1);
                PlayerPrefs.Save();
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

            if (levelClearedText != null)
            {
                levelClearedText.gameObject.SetActive(true);
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

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
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