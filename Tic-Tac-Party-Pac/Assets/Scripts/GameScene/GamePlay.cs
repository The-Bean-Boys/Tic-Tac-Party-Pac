using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    public int turn; //0 is x, 1 is o
    public int turns;
    public GameObject[] turnIcons; //0 x, 1 o
    public Sprite[] playIcons; //0 x icon, 1 y icon
    public Button[] spaces;
    public int[] playedCells;


    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }
    
    void GameSetup()
    {
        turn = 0;
        turns = 0;
        turnIcons[0].SetActive(true);
        turnIcons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        turnIcons[1].SetActive(true);
        turnIcons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 127);
        for(int i = 0; i < spaces.Length; i++)
        {
            spaces[i].interactable = true;
        }
        for(int i = 0; i < playedCells.Length; i++)
        {
            playedCells[i] = -100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TicTacToe(int WhatButton)
    {
        spaces[WhatButton].GetComponent<Image>().sprite = null;
        spaces[WhatButton].image.sprite = playIcons[turn];
        spaces[WhatButton].interactable = false;

        playedCells[WhatButton] = turn + 1;

        turns++;

        WinnerCheck();
        if (turn == 0)
        {
            turn++;
            turnIcons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 127);
            turnIcons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        } else
        {
            turn--;
            turnIcons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            turnIcons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 127);
        }
        
    }

    void WinnerCheck()
    {
        int s1 = playedCells[0] + playedCells[1] + playedCells[2];
        int s2 = playedCells[3] + playedCells[4] + playedCells[5];
        int s3 = playedCells[6] + playedCells[7] + playedCells[8];
        int s4 = playedCells[0] + playedCells[3] + playedCells[6];
        int s5 = playedCells[1] + playedCells[4] + playedCells[7];
        int s6 = playedCells[2] + playedCells[5] + playedCells[8];
        int s7 = playedCells[0] + playedCells[4] + playedCells[8];
        int s8 = playedCells[2] + playedCells[4] + playedCells[6];
        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };
        for(int i = 0; i < solutions.Length; i++)
        {
            if(solutions[i] == 3)
            {
                spaces[7].image.sprite = playIcons[2];
                Debug.Log("SDF");
            } else if(solutions[i] == 6)
            {
                spaces[4].image.sprite = playIcons[2];
            }
        }
    }

    void WinnerDisplay(int indexIn)
    {
        if(turn == 0)
        {
            Debug.Log("Player X Wins");
        }
        else
        {
            Debug.Log("Player O Wins");
        }
    }
}
