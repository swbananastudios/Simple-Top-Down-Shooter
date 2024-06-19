using System.Collections;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private void OnEnable() 
    {
        GameManager.GameOverAction += OnGameOver;
        PlayerHealth.PlayerDamageAction += UpdateHealthText;
    }

    private void OnDisable() 
    {
        GameManager.GameOverAction -= OnGameOver;
        PlayerHealth.PlayerDamageAction -= UpdateHealthText;
    }

    public void Initialize()
    {
        gameplayUI.SetActive(true);
        gameOverUI.SetActive(false);

        UpdateHealthText(gameManager.playerHealth.health);
        StartCoroutine(UpdateTimeText());
    }

    private IEnumerator UpdateTimeText() 
    {
        while (gameManager.remainingTime > 0) {
            timerText.text = gameManager.remainingTime.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    public void UpdateScoreText(int score) 
    {
        GetComponent<AudioSource>().Play();
        scoreText.text = $"Score: {score}";
    }

    public void UpdateFinalScoreText(int finalScore)
    {
        finalScoreText.text = $"Score: {finalScore}";
    }

    public void UpdateHealthText(int health)
    {
        healthText.text = $"Health: {health}";
    }

    public void OnGameOver()
    {
        gameplayUI.SetActive(false);
        gameOverUI.SetActive(true);

        finalScoreText.text = $"Score: {gameManager.score}";
    }
}
