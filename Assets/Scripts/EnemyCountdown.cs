using TMPro;
using UnityEngine;

public class EnemyCountdown : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private EnemyAI enemy;

    private static float savedTime = 30f;
    private static bool enemyReleased = false;

    private float timer;

    private void Start()
    {
        if (enemyReleased)
        {
            enemy.CanChase = true;

            if (countdownText != null)
            {
                countdownText.gameObject.SetActive(false);
            }

            return;
        }

        timer = savedTime;
        enemy.CanChase = false;
    }

    private void Update()
    {
        if (enemyReleased)
            return;

        timer -= Time.deltaTime;

        savedTime = timer;

        if (countdownText != null)
        {
            countdownText.text =
                "Enemy arrives in: " +
                Mathf.CeilToInt(timer);
        }

        if (timer <= 0f)
        {
            enemyReleased = true;

            enemy.CanChase = true;

            if (countdownText != null)
            {
                countdownText.gameObject.SetActive(false);
            }
        }
    }
}
