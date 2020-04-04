using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // Events that will be broadcast to other scripts
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOver;

    // Creating a static instance of this script to reference
    public static GameManager Instance;

    // To figure out which bird dies first
    public GameObject xBird;
    public GameObject oBird;

    // Different UI pages, startPage just has a playbutton for now,
    //  gameOverPage has the winner (or tie) text display
    public GameObject startPage;
    public GameObject gameOverPage;
    public Text winnerText; // text to change to display winner

    // Just an enum to easily reference which page is active
    enum PageState
    {
        None, // Game is running
        Start, // Game is waiting to be started
        GameOver, // Tie text is being displayed, also displays start screen
        FinalGameOver // One bird won, no need to display start screen
    }
    
    // Starts off the game in the game over state
    bool gameOver = true;
    public bool GameOver{ get { return gameOver; } }

    // When this object starts running
    private void Awake()
    {
        Instance = this; // Make the static referece
    }

    // When this object becomes enabled (after Awake, before Start(?))
    private void OnEnable()
    {
        TapController.OnPlayerDied += OnPlayerDied; // Subscribe to the birds' "i died" event
    }

    // When disabled
    private void OnDisable()
    {
        TapController.OnPlayerDied -= OnPlayerDied; // Unsubscribe to birds' "i died" event
    }


    // Called when a tap controller sends out "OnPlayerDied" event
    void OnPlayerDied()
    {
        gameOver = true;
        // determines which bird won, if any
        switch(!xBird.GetComponent<Rigidbody2D>().simulated) // is xBird's physics disactivated? if yes, it hit something
        {
            case (false): // xBird's physics still active
                if (oBird.GetComponent<Rigidbody2D>().simulated) // if oBird's physics also active
                {
                    winnerText.text = "Tie! Play Again"; // set game over "winnerText" to tie. Not sure how this would happen, but just being safe
                    SetPageState(PageState.GameOver); // Tie game over screen, (re)play button still enabled.
                }
                if (!oBird.GetComponent<Rigidbody2D>().simulated) // if oBird's physics aren't active
                {
                    winnerText.text = "X wins!"; // Only x bird's physics are active, so x didnt hit something but o did. x wins
                    PlayerPrefs.SetString("WinnerFlappyXO", "x"); // Stores the winner of the minigame "x" under the label "WinnerFlappyXO" in PlayerPrefs 
                    SetPageState(PageState.FinalGameOver); // Change ui page to final game over screen, because no replay needed
                }
                break;
            case (true): // xBird's physics not active
                if (oBird.GetComponent<Rigidbody2D>().simulated) // if oBird's physics are active
                {
                    winnerText.text = "O wins!";                
                    PlayerPrefs.SetString("WinnerFlappyXO", "o");
                    SetPageState(PageState.FinalGameOver);
                }
                if (!oBird.GetComponent<Rigidbody2D>().simulated) // oBird's physics not active, both birds died at the same time
                {
                    winnerText.text = "Tie! Play Again";
                    SetPageState(PageState.GameOver);
                }
                break;
        }
        OnGameOver(); // Send out event notification that the game is over.
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
