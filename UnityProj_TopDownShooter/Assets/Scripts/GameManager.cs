using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameUI gameUI;
    public PlayerHealth playerHealth;

    public float totalTime = 60f;
    public float remainingTime;
    public bool isGameOver = false;
    public int score = 0;
    public static Action GameOverAction;

    private void Start() 
    {
        remainingTime = totalTime;
        gameUI.Initialize();
        StartCoroutine(StartTimer());
    }
    
    private void OnEnable() 
    {
        EnemyHealth.EnemyDeathAction += IncreaseScore;
        PlayerHealth.PlayerDeathAction += OnGameOver;
    }

    private void OnDisable() 
    {
        EnemyHealth.EnemyDeathAction -= IncreaseScore;
        PlayerHealth.PlayerDeathAction -= OnGameOver;
    }

    private IEnumerator StartTimer() 
    {
        while (remainingTime > 0) {
            remainingTime--;
            yield return new WaitForSeconds(1f);
        }

        OnGameOver();
    }

    private void IncreaseScore() 
    {
        score++;
        gameUI.UpdateScoreText(score);
    }



    private void OnGameOver()
    {
        GetComponent<AudioSource>().Play();
        isGameOver = true;
        GameOverAction?.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
