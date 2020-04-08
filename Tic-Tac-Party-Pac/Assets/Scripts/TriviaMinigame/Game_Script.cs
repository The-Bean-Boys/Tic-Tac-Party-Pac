using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Game_Script : MonoBehaviour
{

    int playerOneScore;                     // Tracks P1 score
    int playerTwoScore;                     // Tracks P2 score      
    int currentIndex;                       // Where in QPrompts current question is
    int questionNumber;                     // Tracks current question number

    bool gameActive;                        // True if game is active
    int playerActive = 0;                   // 1 for P1, 2 for P2

    public GameObject questionCounter;      // Displays the current question number
    public GameObject questionPrompt;       // The question on-screen
    public GameObject timer;                // Timer displayed on screen
    public GameObject Choice1;              // Button 1
    public GameObject Choice2;              // Button 2
    public GameObject Choice3;              // Button 3
    public GameObject Choice4;              // Button 4
    public GameObject playerTwoStart;       // Button to start second game
    public GameObject winnerDisplay;        // Displays the winner of the minigame

    float timerCount;                       // How long each player gets to answer questions
    int winner;                             // Keeps track of who won

    // Array of all question prompts
    public string[] QPrompts = {"What is the capital of Michigan?", "What is the largest freshwater lake in the world?", "What kind of weapon is a falchion?", "What is the world's largest island?",
    "What is the world's largest ocean?", "What is the world's longest river?", "Approximately what is the diameter of Earth?", "What is the capital city of Spain?", "Which chess piece can only move diagonally?",
    "Which of these colors is NOT a primary color?", "When did the Cold War end?", "Which country consumes the most chocolate per capita?", "What is a duel between three people called?",
    "What was the first toy to be advertised on television?", "Kylo Ren from Star Wars is the grandson of:", "What is the smallest bone in the human body?", 
    "Which state produced nearly half of America's rice between 2014 and 2019?"};

    // Array of all corresponding question answers
    public string[] answerKey = {"Lansing", "Lake Superior", "Sword", "Greenland", "Pacific", "Nile", "8000 mi", "Madrid", "Bishop", "Green", "1991", "Switzerland", "Truel", "Mr. Potato Head", "Anakin Skywalker", 
    "Stapes", "Arkansas"};

    // 2D array of all corresponding question choices
    // 2 1 3 4 is the pattern of right answers in each sub array
    public string[,] QChoices = {{"Detroit", "Lansing", "Grand Rapids", "Ann Arbor"}, {"Lake Superior", "Lake Victoria", "Lake Huron", "Lake Michigan"}, {"Spear", "Firearm", "Sword", "Crossbow"}, 
    {"Madagascar", "Iceland", "Ireland", "Greenland"}, {"Atlantic", "Pacific", "Indian", "Arctic"}, {"Nile", "Amazon", "Mississippi", "Congo"}, {"6000 mi", "12000 mi", "8000 mi", "10000 mi"}, 
    {"Barcelona", "Seville", "Toledo", "Madrid"}, {"Rook", "Bishop", "Queen", "Pawn"}, {"Green", "Red", "Yellow", "Blue"}, {"1970", "1985", "1991", "2000"}, {"Germany", "Russia", "United States", "Switzerland"}, 
    {"Deadlock", "Truel", "Trinity", "Standoff"}, {"Mr. Potato Head", "Rocking Horse", "LEGOs", "Lincoln Logs"}, {"Han Solo", "Sheev Palpatine", "Anakin Skywalker", "Luke Skywalker"}, 
    {"Femur", "Patella", "Tibia", "Stapes"}, {"California", "Arkansas", "Iowa", "New York"}};

    public ArrayList usedQuestions;    // Store the indeces of used question prompts here
    
    // Start is called before the first frame update
    public void Start()
    {
        playerTwoStart.SetActive(false);
        winnerDisplay.SetActive(false);

        // Display as question 1
        questionCounter.GetComponent<TMPro.TextMeshProUGUI>().text = "Question #1";
        questionPrompt.GetComponent<TMPro.TextMeshProUGUI>().text = "What is the capital of Michigan?";

        // Always start off with the same question
        Choice1.GetComponent<Button>().GetComponentInChildren<Text>().text = "Detroit";
        Choice2.GetComponent<Button>().GetComponentInChildren<Text>().text = "Lansing";
        Choice3.GetComponent<Button>().GetComponentInChildren<Text>().text = "Grand Rapids";
        Choice4.GetComponent<Button>().GetComponentInChildren<Text>().text = "Ann Arbor";

        // Set buttons to be interactable
        Choice1.GetComponent<Button>().interactable = true;
        Choice2.GetComponent<Button>().interactable = true;
        Choice3.GetComponent<Button>().interactable = true;
        Choice4.GetComponent<Button>().interactable = true;

        // Variables to initialize for start of game
        currentIndex = 0;                           // Reset current index
        questionNumber = 1;                         // Set question count to 1
        timerCount = 30.0f;                         // Set timer for 30 seconds
        usedQuestions = new ArrayList();            // Initialize usedQuestions ArrayList
        usedQuestions.Add(0);                       // Add first question to list
        gameActive = true;                          // Boolean to keep track of game in progress

        // Check who is playing
        if (playerActive == 0){
            Debug.Log("Player 1 active");
            playerActive = 1;
        }
        else{
            Debug.Log("Player 2 active");
            playerActive = 2;
        }
    }

    // Do this for every button click
    public void onClick(){

        // Grab player's answer and correct answer
        string playerAnswer = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        string correctAnswer = answerKey[currentIndex];

        // If player is correct, add to score
        if (playerAnswer == correctAnswer){
            if (playerActive == 1){
                playerOneScore++;
                Debug.Log("Player 1 Score: " + playerOneScore);
            }
            else{
                playerTwoScore++;
                Debug.Log("Player 2 Score: " + playerTwoScore);
            }
        }

        // Get new question
        currentIndex = Random.Range(0, QPrompts.Length);

        // This is a new question
        if (usedQuestions.IndexOf(currentIndex) == -1){
            usedQuestions.Add(currentIndex);                        // Add the new question to usedQuestions list
        }

        // This question has been used
        else if (usedQuestions.Count != QPrompts.Length){

            // Loop until new question is found
            while (usedQuestions.IndexOf(currentIndex) != -1){
                currentIndex = Random.Range(0, QPrompts.Length);    // Search for new question
            }
            usedQuestions.Add(currentIndex);                        // Add the new question to usedQuestions list
        }

        // No new questions available
        else{
            Debug.Log("No more questions");
            gameActive = false;         // Stop the timer

            // Disable buttons, reset text
            Choice1.GetComponent<Button>().interactable = false;
            Choice1.GetComponent<Button>().GetComponentInChildren<Text>().text = "";
            Choice2.GetComponent<Button>().interactable = false;
            Choice2.GetComponent<Button>().GetComponentInChildren<Text>().text = "";
            Choice3.GetComponent<Button>().interactable = false;
            Choice3.GetComponent<Button>().GetComponentInChildren<Text>().text = "";
            Choice4.GetComponent<Button>().interactable = false;
            Choice4.GetComponent<Button>().GetComponentInChildren<Text>().text = "";

            // Display the corresponding player's score
            if (playerActive == 1){
                questionPrompt.GetComponent<TMPro.TextMeshProUGUI>().text = "Player 1 Score: " + playerOneScore;
                playerTwoStart.SetActive(true);
            }
            else{
                questionPrompt.GetComponent<TMPro.TextMeshProUGUI>().text = "Player 2 Score: " + playerTwoScore;
                calculateWinner();
                winnerDisplay.SetActive(true);

                // Display the winner
                if (winner == 1){
                    winnerDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Player 1 Wins!";
                }
                else if (winner == 2){
                    winnerDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Player 2 Wins!";
                }
            }
            return;
        }

        // Change the prompt and question #
        changeQuestion(QPrompts[currentIndex]);
        questionCounter.GetComponent<TMPro.TextMeshProUGUI>().text = "Question #" + ++questionNumber;

        // Display new choices that correspond to the new prompt
        Choice1.GetComponent<Button>().GetComponentInChildren<Text>().text = QChoices[currentIndex,0];
        Choice2.GetComponent<Button>().GetComponentInChildren<Text>().text = QChoices[currentIndex,1];
        Choice3.GetComponent<Button>().GetComponentInChildren<Text>().text = QChoices[currentIndex,2];
        Choice4.GetComponent<Button>().GetComponentInChildren<Text>().text = QChoices[currentIndex,3];
    }

    // Method to change the prompt
    public void changeQuestion(string newQuestion){
        questionPrompt.GetComponent<TMPro.TextMeshProUGUI>().text = newQuestion;
    }

    public void Update(){

        // Only update timer if game is active
        if (gameActive){
            timerCount -= Time.deltaTime;   // Subtract time from counter
            timer.GetComponent<TMPro.TextMeshProUGUI>().text = "Time: " + timerCount.ToString("F2");    // Display timer

            // Time's up
            if (timerCount <= 0){
                Debug.Log("Time's Up");
                gameActive = false;
                timer.GetComponent<TMPro.TextMeshProUGUI>().text = "TIME'S UP";

                // Disable buttons, set text to empty
                Choice1.GetComponent<Button>().interactable = false;
                Choice1.GetComponent<Button>().GetComponentInChildren<Text>().text = "";
                Choice2.GetComponent<Button>().interactable = false;
                Choice2.GetComponent<Button>().GetComponentInChildren<Text>().text = "";
                Choice3.GetComponent<Button>().interactable = false;
                Choice3.GetComponent<Button>().GetComponentInChildren<Text>().text = "";
                Choice4.GetComponent<Button>().interactable = false;
                Choice4.GetComponent<Button>().GetComponentInChildren<Text>().text = "";

                if (playerActive == 1){
                    questionPrompt.GetComponent<TMPro.TextMeshProUGUI>().text = "Player 1 Score: " + playerOneScore;
                    playerTwoStart.SetActive(true);     // Give player 2 a chance to play
                }
                else{
                    questionPrompt.GetComponent<TMPro.TextMeshProUGUI>().text = "Player 2 Score: " + playerTwoScore;
                    calculateWinner();

                    winnerDisplay.SetActive(true);

                    // Display the winner
                    if (winner == 1){
                        winnerDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Player 1 Wins!";
                    }
                    else if (winner == 2){
                        winnerDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Player 2 Wins!";
                    }
                }
                return;
            }
        }
    }

    // Calculate and save the results of the minigame
    public void calculateWinner(){
        if (playerOneScore > playerTwoScore){
            winner = 1;
            Debug.Log("Player 1 Wins!");
        }
        else if (playerTwoScore > playerOneScore){
            winner = 2;
            Debug.Log("Player 2 Wins!");
        }
        else {
            winner = 0;
            Debug.Log("Tie Game!");
        }
    }
}
