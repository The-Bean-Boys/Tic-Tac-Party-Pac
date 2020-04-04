using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOver;

    public static GameManager Instance;

    public GameObject xBird;
    public GameObject oBird;

    public GameObject startPage;
    public GameObject gameOverPage;
    public Text winnerText;

    enum PageState
    {
        None,
        Start,
        GameOver,
        FinalGameOver
    }
    
    bool gameOver = true;

    public bool GameOver{ get { return gameOver; } }

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        tapController.OnPlayerDied += OnPlayerDied;
    }

    private void OnDisable()
    {
        tapController.OnPlayerDied -= OnPlayerDied;
    }


    // Called when a tap controller sends out "OnPlayerDied" event
    void OnPlayerDied()
    {
        gameOver = true;
        // determines which bird won, if any
        switch(!xBird.GetComponent<Rigidbody2D>().simulated)
        {
            case (false):
                if (oBird.GetComponent<Rigidbody2D>().simulated)
                {
                    winnerText.text = "Tie! Play Again";
                    SetPageState(PageState.GameOver);
                }
                if (!oBird.GetComponent<Rigidbody2D>().simulated)
                {
                    winnerText.text = "X wins!";
                    PlayerPrefs.SetString("WinnerFlappyXO", "x");
                    SetPageState(PageState.FinalGameOver);
                }
                break;
            case (true):
                if (oBird.GetComponent<Rigidbody2D>().simulated)
                {
                    winnerText.text = "O wins!";                
                    PlayerPrefs.SetString("WinnerFlappyXO", "o");
                    SetPageState(PageState.FinalGameOver);
                }
                if (!oBird.GetComponent<Rigidbody2D>().simulated)
                {
                    winnerText.text = "Tie! Play Again";
                    SetPageState(PageState.GameOver);
                }
                break;
        }
        OnGameOver();
    }

    // changes which UI elements are active based on the pagestate
    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                gameOverPage.SetActive(false);
                startPage.SetActive(false);
                break;
            case PageState.Start:
                startPage.SetActive(true);
                break;
            case PageState.GameOver:
                gameOverPage.SetActive(true);
                startPage.SetActive(true);
                break;
            case PageState.FinalGameOver:
                gameOverPage.SetActive(true);
                startPage.SetActive(false);
                break;
        }
    }

    // sends out OnGameStarted event and sets game to active pagestate
    public void StartGame()
    {
        OnGameStarted();
        gameOver = false;
        SetPageState(PageState.None);
    }

}
