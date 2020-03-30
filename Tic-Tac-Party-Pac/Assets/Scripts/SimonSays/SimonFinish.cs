using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonFinish : MonoBehaviour
{
    public GameObject Game;
    public GameObject Win;
    GameObject WinText;
    int tmp = 0;
    int change = 60;
    public void Start()
    {
        WinText = Win.transform.GetChild(0).gameObject;
        ResetGame();
    }

    public void Update()
    {
        if(Win.activeSelf)
        {
            tmp++;
            if(tmp >= change)
            {
                WinText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(Random.value, Random.value, Random.value);
                tmp = 0;
            }
        }
    }


    public void SimonWins(string Winner)
    {
        WinText.GetComponent<TMPro.TextMeshProUGUI>().text = Winner + " Wins";
        Game.SetActive(false);
        Win.SetActive(true);
    }

    public void ResetGame()
    {
        Game.SetActive(true);
        Win.SetActive(false);
    }
}
