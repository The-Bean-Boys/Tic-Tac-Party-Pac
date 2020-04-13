using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimonGameState : MonoBehaviour
{
    List<string> Sequence;
    int Index;
    string PlayerTurn;
    public GameObject PlayerText;
    public GameObject LastColor;
    public GameObject RoundText;
    public GameObject Canvas;
    SimonFinish SF;
  
    public void Start()
    {
        Sequence = new List<string>();
        Index = 0;
        PlayerTurn = "Player One";
        SF = Canvas.GetComponent<SimonFinish>();
    }

    public void CheckSequence(string Choice)
    {
        if(Choice == null)
        {
            Debug.Log("CheckSequence in SimonGameState got a null parameter");
            return;
        }
        if(!(Choice.Equals("Red") || Choice.Equals("Green") || Choice.Equals("Blue") || Choice.Equals("Yellow")))
        {
            Debug.Log("CheckSequence in SimonGameState got a bad parameter");
            return;
        }
        if(Index >= Sequence.Count)
        {
            AddOntoSequence(Choice);
            UpdateLastColor(Choice);
            ChangeTurn();
            Index = 0;
            return;
        }
        if(Choice.Equals(Sequence[Index])) {
            Index++;
        } else
        {
            if (PlayerTurn.Equals("Player One"))
            {
                SF.SimonWins("X");
                SceneManager.LoadScene("GameScene");
            }
            else
            {
                SF.SimonWins("O");
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    public void AddOntoSequence(string Choice)
    {
        Sequence.Add(Choice);
    }

    public void UpdateLastColor(string Choice)
    {
        Color Red = new Color(255/255f, 6/255f, 0);
        Color Green = new Color(4/255f, 255/255f, 47/255f);
        Color Blue = new Color(0, 60/255f, 255/255f);
        Color Yellow = new Color(250/255f, 255/255f, 0);
        switch (Choice)
        {
            case "Red":
                LastColor.GetComponent<Image>().color = Red;
                break;
            case "Green":
                LastColor.GetComponent<Image>().color = Green;
                break;
            case "Blue":
                LastColor.GetComponent<Image>().color = Blue;
                break;
            case "Yellow":
                LastColor.GetComponent<Image>().color = Yellow;
                break;
        }
    }

    public void ChangeTurn()
    {
        if (PlayerTurn.Equals("Player One"))
        {
            PlayerTurn = "Player Two";
        }
        else
        {
            PlayerTurn = "Player One";
        }
        PlayerText.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerTurn;
        RoundText.GetComponent<TMPro.TextMeshProUGUI>().text = "Round " + (Sequence.Count + 1);
    }

    public void ResetGame()
    {
        PlayerTurn = "Player One";
        Sequence.Clear();
        Index = 0;
        PlayerText.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerTurn;
        RoundText.GetComponent<TMPro.TextMeshProUGUI>().text = "Round " + (Sequence.Count + 1);
        LastColor.GetComponent<Image>().color = Color.black;
        SF.ResetGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
