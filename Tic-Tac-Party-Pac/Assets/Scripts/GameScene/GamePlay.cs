using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public int[] winningLines; // Array of 72 ints indicating which lines in winningLine[] are active (for playerprefs purposes)
    public GameObject[] bigWinLine;
    public GameObject[] winningShades;
    public Text xCountText; //The text object of the x counter
    public int xCount; //The int counter of the x counter
    public Text oCountText; //The text object of the o counter
    public int oCount; //The text object of the o counter
    public GameObject gameOverPage; // game over UI page
    public Text winnerText;
    public GameObject gameBoard; // parent object of the full game board


    // Start is called before the first frame update
    void Start()
    {
        int played = PlayerPrefs.GetInt("Playing");
        if (played == 0)
        {
            PlayerPrefs.SetInt("Playing", 1);
            GameSetup(); //On startup, calls GameSetup
        }
        else
        {
            GameRedo();
        }
    }

    // Set up new game
    public void GameSetup()
    {
        gameOverPage.SetActive(false);
        gameBoard.SetActive(true);

        turn = 0; //Sets the turn count to 0
        turns = 0; //Sets turns count to 0
        xCount = 0; //Sets x count to 0
        oCount = 0; //Sets o count to 0
        UpdateCount(); //Updates the text objects to 0 on game board
        turnIcons[0].SetActive(true);  //Sets the turn icon to true for the x picture
        turnIcons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255); //Sets the x picture to bright (brightness indicates turn)
        turnIcons[1].SetActive(true); //Sets turn icon to true for o picture
        turnIcons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 127); //Sets o picture to half brightness (not turn)
        for (int i = 0; i < spaces.Length; i++)
        {
            spaces[i].interactable = true; //Sets all the game spaces to interactable
            spaces[i].image.sprite = playIcons[3]; //Sets all the game spaces to grey tiles
        }
        for (int i = 0; i < playedCells.Length; i++)
        {
            playedCells[i] = 0; //Sets all the playedCells to not played
        }
        for (int i = 0; i < playedTiles.Length; i++)
        {
            playedTiles[i] = 0; //Sets all the playedTiles to not won
        }
        for (int i = 0; i < winningLine.Length; i++)
        {
            winningLine[i].SetActive(false);
        }
        for (int i = 0; i < winningLines.Length; i++)
        {
            winningLines[i] = 0;
        }
        for (int i = 0; i < winningShades.Length; i++)
        {
            winningShades[i].SetActive(false);
        }
        for (int i = 0; i < bigWinLine.Length; i++)
        {
            bigWinLine[i].SetActive(false);
        }
    }

    // Reload existing game
    public void GameRedo()
    {
        turn = PlayerPrefs.GetInt("Turn");
        turns = PlayerPrefs.GetInt("Turns");
        xCount = PlayerPrefs.GetInt("XCount");
        oCount = PlayerPrefs.GetInt("OCount");
        for (int i = 0; i < 81; i++)
        {
            playedCells[i] = PlayerPrefs.GetInt("PlayedCells" + i);
            if (playedCells[i] == 1)
            {
                spaces[i].image.sprite = playIcons[0];
                spaces[i].interactable = false;
            }
            else if (playedCells[i] == 2)
            {
                spaces[i].image.sprite = playIcons[1];
                spaces[i].interactable = false;
            }
            else
            {
                spaces[i].image.sprite = playIcons[3];
                spaces[i].interactable = true;
            }
        }
        for (int i = 0; i < 72; i++)
        {
            winningLines[i] = PlayerPrefs.GetInt("WinningLines");
            winningLine[i].SetActive(winningLines[i] == 1);
        }
        ReturnToTile(PlayerPrefs.GetString("LastMinigame"), PlayerPrefs.GetInt("FocusCell"));
        NextTurn();
    }

    // Checks if the minigame was won by the player attempting to win a tile, places
    // the player's icon on the cell if true
    private void ReturnToTile(string minigame, int cell)
    {
        if (PlayerPrefs.GetInt(GetMinigamePref(minigame)) != turn)
        {
            playedCells[cell] = 0; // Sets playedCells of that cell to 0 (unplayed)
            spaces[cell].image.sprite = playIcons[3]; // Sets the cell image to gray (unplayed)
            spaces[cell].interactable = true; //Turns off the interaction of the button
        }
        else
        {

        }

        SetSolutions(cell / 9);
    }

    // Called when clicked by a button, WhatButton is int of button clicked
    public void TicTacToe(int cell)
    {
        int tile = cell / 9; // Calculates the tile holding the cell
        PlayerPrefs.SetInt("FocusCell", cell); // Last clicked cell
        playedCells[cell] = turn + 1; //Sets playedCells of that button to 1 + the turn (1 for x, 2 for o)
        spaces[cell].GetComponent<Image>().sprite = null; //Sets the button image to null
        spaces[cell].image.sprite = playIcons[turn]; //Sets the image of button to the player whos turn it is
        spaces[cell].interactable = false; //Turns off the interaction of the button
        if (TileWon(tile, turn))
        {
            for (int i = 0; i < playedCells.Length; i++)
            {
                PlayerPrefs.SetInt("PlayedCells" + i, playedCells[i]);
            }
            for (int i = 0; i < winningLine.Length; i++)
            {
                PlayerPrefs.SetInt("WinningLine" + i, winningLines[i]);
            }
            PlayerPrefs.SetInt("Turn", turn);
            PlayerPrefs.SetInt("Turns", turns);
            PlayerPrefs.SetInt("XCount", xCount);
            PlayerPrefs.SetInt("OCount", oCount);

            /* choose random minigame, store the name of it and the deciding tile to grab 
             * the winner of later, then load that minigame
             */
            string minigameChoice = RandomMinigame();
            PlayerPrefs.SetString("LastMinigame", minigameChoice);
            SceneManager.LoadScene(minigameChoice);
        }
        NextTurn();
    }

    // Move to next turn
    void NextTurn()
    {
        turns++; //Increases turn count
        if (turn == 0) //If x is turn
        {
            turn++; //Increases turn to 1 (o)
            turnIcons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 127); //Sets x image to dull
            turnIcons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 255); //Sets o image to bright
        }
        else
        {
            turn--; //Decrements turn to 0 (x)
            turnIcons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255); //Sets x image to bright
            turnIcons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 127); //Sets o image to dull
        }
        UpdateCount(); //Updates the text components to the counters
    }

    // Update score displays
    void UpdateCount()
    {
        xCount = 0;
        oCount = 0;
        for (int i = 0; i < playedCells.Length; i++)
        {
            if (playedCells[i] == 1)
                xCount++;
            if (playedCells[i] == 2)
                oCount++;
        }
        xCountText.text = xCount.ToString();
        oCountText.text = oCount.ToString();
    }

    // Returns true if the given player has won the given tile
    bool TileWon(int tile, int player)
    {
        int[] solutions = GetSolutions(tile);
        for (int i = 0; i < solutions.Length; i++)
        {
            // If x won (1 + 1 + 1, three xs in a row) and x is the player
            if (solutions[i] == 1)
                return player == 0;

            // If o won (2 + 2 + 2, three os in a row) and o is the player
            else if (solutions[i] == 2)
                return player == 1;

        }

        // if neither player has won the tile, return true if given player has won more tiles than the other
        if (TileFull(tile))
        {
            return CatWon(tile, player);
        }
        return false;
    }


    // Returns true if the tile is full, false otherwise
    bool TileFull(int tile)
    {
        int count = 0;
        for (int i = tile * 9; i < tile * 9 + 9; i++)
        {
            if (playedCells[i] > 0)
                count++;
        }
        return count == 9;
    }

    // Returns an array of winning lines on GIVEN TILE
    int[] GetSolutions(int tile)
    {
        int offset = tile * 9; // Calculates the offset of tile (18 is start of 3rd tile, etc.)
        int s1 = ThreeInARow(playedCells[0 + offset], playedCells[1 + offset], playedCells[2 + offset]); // top row
        int s2 = ThreeInARow(playedCells[3 + offset], playedCells[4 + offset], playedCells[5 + offset]);  // middle row
        int s3 = ThreeInARow(playedCells[6 + offset], playedCells[7 + offset], playedCells[8 + offset]); // bottom row
        int s4 = ThreeInARow(playedCells[0 + offset], playedCells[3 + offset], playedCells[6 + offset]); // left column
        int s5 = ThreeInARow(playedCells[1 + offset], playedCells[4 + offset], playedCells[7 + offset]); // middle column
        int s6 = ThreeInARow(playedCells[2 + offset], playedCells[5 + offset], playedCells[8 + offset]); // right column
        int s7 = ThreeInARow(playedCells[0 + offset], playedCells[4 + offset], playedCells[8 + offset]); // left top to bottom right diagonal
        int s8 = ThreeInARow(playedCells[2 + offset], playedCells[4 + offset], playedCells[6 + offset]); // top right to bottom left diagonal
        return new int[] { s1, s2, s3, s4, s5, s6, s7, s8 }; //Creates an array of these values
    }

    // Returns an array of winning lines on GAME BOARD
    int[] GetSolutions()
    {
        int s1 = ThreeInARow(playedTiles[0], playedTiles[1], playedTiles[2]); // top row
        int s2 = ThreeInARow(playedTiles[3], playedTiles[4], playedTiles[5]); // middle row
        int s3 = ThreeInARow(playedTiles[6], playedTiles[7], playedTiles[8]); // bottom row
        int s4 = ThreeInARow(playedTiles[0], playedTiles[3], playedTiles[6]); // left column
        int s5 = ThreeInARow(playedTiles[1], playedTiles[4], playedTiles[7]); // middle column
        int s6 = ThreeInARow(playedTiles[2], playedTiles[5], playedTiles[8]); // right column
        int s7 = ThreeInARow(playedTiles[0], playedTiles[4], playedTiles[8]); // left top to bottom right diagonal
        int s8 = ThreeInARow(playedTiles[2], playedTiles[4], playedTiles[6]); // top right to bottom left diagonal
        return new int[] { s1, s2, s3, s4, s5, s6, s7, s8 }; //Creates an array of these values
    }

    // Saves the current solutions of the tile to winningLines[] to avoid redrawing lines 
    void SetSolutions(int tile)
    {
        int[] solutions = GetSolutions(tile);
        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == 1) //If x won (1 + 1 + 1, three xs in a row)
            {
                winningLines[i + (tile * 8)] = 1;
                winningLine[i + (tile * 8)].SetActive(true);
            }
            else if (solutions[i] == 2) //If o won (2 + 2 + 2, three os in a row)
            {
                winningLines[i + (tile * 8)] = 1;
                winningLine[i + (tile * 8)].SetActive(true);
            }
        }
    }

    // Returns 1 if the three ints match as an x value, 2 if they match as an o, 0 otherwise
    int ThreeInARow(int one, int two, int three)
    {
        if ((one == two) && (one == three))
            return one;
        return 0;
    }

    /* DEPRECATED FUNCTION
     *  some of the code still necessary and will be repurposed.
     */
    void WinnerDisplay(int indexIn, int WhatTile)
    {
        int offset = WhatTile * 9; //Offset of the tile
        int lineOffset = WhatTile * 8; //There are only 8 lines for each tile, so the lineOffset multiplier goes down by 1
        winningLine[indexIn + lineOffset].SetActive(true); //Sets the winning line of the tile + the solution tile to active to show
        winningLines[indexIn + lineOffset] = 1;
        if (turn == 0)
        {
            winningShades[WhatTile * 2].SetActive(true);
            playedTiles[WhatTile] = 1;
        }
        else
        {
            winningShades[WhatTile * 2 + 1].SetActive(true);
            playedTiles[WhatTile] = 2;
        }
        for (int i = offset; i < 9 + offset; i++)  //For all cells in the won tile
        {
            if (playedCells[i] < 0) //If it hasn't already been played 
            {
                playedCells[i] = turn + 1; //Set it to the turn + 1
                spaces[i].image.sprite = playIcons[turn]; //Sets the image of the button to the image of the player who won the tile's counter
                if (turn == 0)
                {
                    xCount++; //Increase the x Count if x won the tile
                }
                else
                {
                    oCount++; //Increase the o count if o won the tile
                }
                UpdateCount(); //Updates the counts
            }
            spaces[i].interactable = false; //Makes the button not interactable
        }
    }

    // Returns true if the given player has more cells in the given tile than the other player
    bool CatWon(int tile, int player)
    {
        int offset = tile * 9;
        int xCellCount = 0;
        int oCellCount = 0;
        for (int i = offset; i < offset + 9; i++)
        {
            if (playedCells[i] == 1)
                xCellCount++;
            else if (playedCells[i] == 2)
                oCellCount++;
        }
        if (xCellCount > oCellCount)
            return player == 0;
        else
            return player == 1;
    }

    // Returns the string of a random minigame
    string RandomMinigame()
    {
        string[] minigames = { "Balloon Minigame", "FlappyXO", "Horse Minigame", "MentalMath", "SideScroller", "SimonSays", "Trivia Minigame", "Tug-of-War Minigame" };
        return minigames[Random.Range(0, minigames.Length)];
    }

    // Returns the PlayerPrefs key for the given minigame scene name
    string GetMinigamePref(string minigame)
    {
        switch (minigame)
        {
            case "Balloon Minigame":
                return "WinnerBalloon";
            case "FlappyXO":
                return "WinnerFlappy";
            case "Horse Minigame":
                return "WinnerHorse";
            case "MentalMath":
                return "WinnerMentalMath";
            case "SideScroller":
                return "WinnerSideScroller";
            case "SimonSays":
                return "WinnerSimon";
            case "Trivia Minigame":
                return "WinnerTrivia";
            case "Tug-of-War Minigame":
                return "WinnerTug";
            default:
                return "Error";
        };
    }

    /* Call when game is over
     * @param winner - pass the value of who won
     *      0 - X wins
     *      1 - O wins
     */
    void GameOver(int winner)
    {
        for (int i = 0; i < spaces.Length; i++)
        {
            spaces[i].interactable = false;
        }
        turnIcons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        turnIcons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        if (winner == 0) { winnerText.text = "X wins!"; } else { winnerText.text = "O wins!"; }

        Invoke("DisableBoard", 3);
    }

    // Disable game board and enable game over page
    void DisableBoard()
    {
        gameBoard.SetActive(false);
        gameOverPage.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
