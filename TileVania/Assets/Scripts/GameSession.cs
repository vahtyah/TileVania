using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public static GameSession instance;

    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI arrowText;
    [SerializeField] PlayerMovement player;

    int score = 0;

    void Awake()
    {
        if (instance) Destroy(gameObject);
        else instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        livesText.text = "Live: " + playerLives.ToString();
        scoreText.text = "Score: " + score.ToString();
        ChangeArrow();
    }

    public void ChangeArrow()
    {
        arrowText.text = "Arrow: " + PlayerMovement.arrow.ToString();
    }

    public void IncementScore()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void ProcessPlayerDeat()
    {
        if (playerLives > 1) TakeLife();
        else ResetGameSession();
    }

    private void TakeLife()
    {
        playerLives--;
        livesText.text = "Live: " + playerLives.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        ScenePersist.instance.ResetScenePersist();
    }

    public void OnPauseButtonClick()
    {
        if (player != null)
        {
            player.isPause = true;
        }
    }

    public void OnResumeButtonClick()
    {
        if (player != null)
        {
            player.isPause = false;
        }
    }

    public void OnBackButtonClick()
    {
        ResetGameSession();
    }

    public void BuyHealthButtonClick()
    {
        if (score < 10) return;
        score -= 10;
        scoreText.text = "Score: " + score.ToString();
        playerLives++;
        livesText.text = "Live: " + playerLives.ToString();

    }

    public void BuyArrowButtonClick()
    {
        if(score < 5) return;
        score -= 5;
        scoreText.text = "Score: " + score.ToString();
        PlayerMovement.arrow++;
        ChangeArrow();
    }

}
