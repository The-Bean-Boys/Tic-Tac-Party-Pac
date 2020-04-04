using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    public int turn; //0 is x, 1 is o
    public int turns; //How many turns have been played
    public GameObject[] turnIcons; //0 x, 1 o, large x and o images above the counters to signal turn
    public Sprite[] playIcons; //0 x icon, 1 o icon, 2 is orange shading, 3 is regular grey tile, images to fill into tiles
    public Button[] spaces; //An array of the 81 buttons, starting top-left, going right then down one row, same for cells and tiles
    public int[] playedCells; //An array of 81 ints, -100 indicates unplayed, 1 is x played, 2 is o played. To keep track of which cells have been played to who
    public int[] playedTiles; //An array of 9, -100 indicates unplayed, 1 is x played, 2 is o played. To keep track of which tiles have been won by who
    public GameObject[] winningLine; //An array of 72 lines to indicate which rows have been won in each won tile
    public Text xCountText; //The text object of the x counter
    public int xCount; //The int counter of the x counter
    public Text oCountText; //The text object of the o counter
    public int oCount; //The text object of the o counter

    // Start is called before the first frame update
    void Start()
    {
        GameSetup(); //On startup, calls GameSetup
    }
    
    void GameSetup()
    {
        turn = 0; //Sets the turn count to 0
        turns = 0; //Sets turns count to 0
        xCount = 0; //Sets x count to 0
        oCount = 0; //Sets o count to 0
        UpdateCount(); //Updates the text objects to 0 on game board
        turnIcons[0].SetActive(true);  //Sets the turn icon to true for the x picture
        turnIcons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255); //Sets the x picture to bright (brightness indicates turn)
        turnIcons[1].SetActive(true); //Sets turn icon to true for o picture
        turnIcons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 127); //Sets o picture to half brightness (not turn)
        for(int i = 0; i < spaces.Length; i++)
        {
            spaces[i].interactable = true; //Sets all the game spaces to interactable
            spaces[i].image.sprite = playIcons[3]; //Sets all the game spaces to grey tiles
        }
        for(int i = 0; i < playedCells.Length; i++)
        {
            playedCells[i] = -100; //Sets all the playedCells to not played (-100)
        }
        for(int i = 0; i < playedTiles.Length; i++)
        {
            playedTiles[i] = -100; //Sets all the playedTiles to not won (-100)
        }
    }
    
    public void TicTacToe(int WhatButton) //Called when clicked by a button, WhatButton is int of button clicked
    {
        int WhatTile = WhatButton / 9; //Calculates the tile holding the cell
        
        spaces[WhatButton].GetComponent<Image>().sprite = null; //Sets the button image to null
        spaces[WhatButton].image.sprite = playIcons[turn]; //Sets the image of button to the player whos turn it is
        spaces[WhatButton].interactable = false; //Turns off the interaction of the button

        playedCells[WhatButton] = turn + 1; //Sets playedCells of that button to 1 + the turn (1 for x, 2 for o)

        turns++; //Increases turn count
        
        WinnerCheck(WhatTile); //Checks if someone has won the tile
        if (turn == 0) //If x is turn
        {
            xCount++; //Increases x count of cells
            turn++; //Increases turn to 1 (o)
            turnIcons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 127); //Sets x image to dull
            turnIcons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 255); //Sets o image to bright
        } else
        {
            oCount++; //Increases o count of cells
            turn--; //Decrements turn to 0 (x)
            turnIcons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255); //Sets x image to bright
            turnIcons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 127); //Sets o image to dull
        }
        UpdateCount(); //Updates the text components to the counters
    }

    void UpdateCount()
    {
        xCountText.text = xCount.ToString(); //Sets the text of the GameObject to the string version of the count of x
        oCountText.text = oCount.ToString(); //Sets the text of the GameObject to the string version of the count of o
    }

    void WinnerCheck(int WhatTile)
    {
        
        int offset = WhatTile * 9; //Calculates the offset of tile (18 is start of 3rd tile, etc.)
        int s1 = playedCells[0 + offset] + playedCells[1 + offset] + playedCells[2 + offset]; //Calculates values of top row
        int s2 = playedCells[3 + offset] + playedCells[4 + offset] + playedCells[5 + offset]; //Calculates values of middle row
        int s3 = playedCells[6 + offset] + playedCells[7 + offset] + playedCells[8 + offset]; //Calculates values of bottom row
        int s4 = playedCells[0 + offset] + playedCells[3 + offset] + playedCells[6 + offset]; //Calculates values of left column
        int s5 = playedCells[1 + offset] + playedCells[4 + offset] + playedCells[7 + offset]; //Calculates values of middle column
        int s6 = playedCells[2 + offset] + playedCells[5 + offset] + playedCells[8 + offset]; //Calculates values of right column
        int s7 = playedCells[0 + offset] + playedCells[4 + offset] + playedCells[8 + offset]; //Calculates values of left top to bottom right diagonal
        int s8 = playedCells[2 + offset] + playedCells[4 + offset] + playedCells[6 + offset]; //Calculates values of top right to bottom left diagonal
        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 }; //Creates an array of these values
        for(int i = 0; i < solutions.Length; i++) 
        {
            if(solutions[i] == 3) //If x won (1 + 1 + 1, three xs in a row)
            {
                WinnerDisplay(i, WhatTile); //Displays winner of the tile with the solution and the tile of solution

            } else if(solutions[i] == 6) //If o won (2 + 2 + 2, three os in a row)
            {
                WinnerDisplay(i, WhatTile); //Displays winner of the tile with the solution and the tile of solution
            }
        }
    }

    void WinnerDisplay(int indexIn, int WhatTile)
    {
        int offset = WhatTile * 9; //Offset of the tile
        int lineOffset = WhatTile * 8; //There are only 8 lines for each tile, so the lineOffset multiplier goes down by 1
        winningLine[indexIn + lineOffset].SetActive(true); //Sets the winning line of the tile + the solution tile to active to show
        for(int i = offset; i < 9 + offset; i++)  //For all cells in the won tile
        {
            if(playedCells[i] < 0) //If it hasn't already been played 
            {
                playedCells[i] = turn + 1; //Set it to the turn + 1
                spaces[i].image.sprite = playIcons[turn]; //Sets the image of the button to the image of the player who won the tile's counter
                if(turn == 0)
                {
                    xCount++; //Increase the x Count if x won the tile
                } else
                {
                    oCount++; //Increase the o count if o won the tile
                }
                UpdateCount(); //Updates the counts
            }
            spaces[i].interactable = false; //Makes the button not interactable
        }
    }
}
