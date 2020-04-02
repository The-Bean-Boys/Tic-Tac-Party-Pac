using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;

    public static GameManager Instance;

    public GameObject startPage;
    public GameObject gameOverPage;
    public Text winnerText;

    enum PageState
    {
        None,
        Start,
        GameOver
    }
    
    bool gameOver = false;

    public bool GameOver{ get { return gameOver; } }

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        tapController.OnPlayerDied += OnPlayerDied;
    }

    void OnPlayerDied()
    {
        gameOver = true;
        PlayerPrefs.SetString("WinnerFlappyXO", "x"); 
    }

    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                gameOverPage.SetActive(false);
                startPage.SetActive(false);
                break;
            case PageState.Start:
                gameOverPage.SetActive(false);
                startPage.SetActive(true);
                break;
            case PageState.GameOver:
                gameOverPage.SetActive(true);
                startPage.SetActive(false);
                break;
        }
    }



    public void StartGame()
    {
        OnGameStarted();
        gameOver = false;
        SetPageState(PageState.None);
    }

}
