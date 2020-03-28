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
}
