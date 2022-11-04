using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public static GameSession instance;

    [SerializeField] int playerLives = 3;

    void Start()
    {
        if(instance) Destroy(gameObject);
        else instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ProcessPlayerDeat()
    {
        if (playerLives > 1) TakeLife();
        else ResetGameSession();
    }

    private void TakeLife()
    { 
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        print("Tuan");
        Destroy(gameObject);
    }
}
