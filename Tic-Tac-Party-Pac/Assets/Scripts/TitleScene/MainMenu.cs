using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button cont;
    public Button newGame;

    public void Start()
    {
        if(PlayerPrefs.GetInt("Playing") == 1)
        {
            cont.interactable = false;
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("GameScene");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Playing", 0);
    }

    public void Continue()
    {
        SceneManager.LoadScene("GameScene");
        PlayerPrefs.SetInt("Playing", 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}
