using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Game_Script : MonoBehaviour
{

    int playerOneScore;
    int playerTwoScore;
    int currentIndex;
    int questionNumber;

    bool gameActive;

    public GameObject questionPrompt;   // The question on-screen
    public GameObject timer;            // Timer displayed on screen

    float timerCount;     // How long each player gets to answer questions

    public string[] QPrompts = {"What is the capital of Michigan?", "What is the largest freshwater lake in the world?", "What kind of weapon is a falchion?", "What is the world's largest island?",
    "What is the world's largest ocean?", "What is the world's longest river?", "Approximately what is the diameter of Earth?", "What is the capital city of Spain?", "Which chess piece can only move diagonally?",
    "Which of these colors is NOT a primary color?", "When did the Cold War end?", "Which country consumes the most chocolate per capita?", "What is a duel between three people called?",
    "What was the first toy to be advertised on television?", "Kylo Ren from Star Wars is the grandson of:", "What is the smallest bone in the human body?", 
    "Which state produced nearly half of America's rice between 2014 and 2019?"};

    public string[] answerKey = {"Lansing", "Lake Superior", "Sword", "Greenland", "Pacific", "Nile", "8000 mi", "Madrid", "Bishop", "Green", "1991", "Switzerland", "Truel", "Mr. Potato Head", "Anakin Skywalker", 
    "Stapes", "Arkansas"};

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
        // Display as question 1
        GameObject.Find("questionCounter").GetComponent<TMPro.TextMeshProUGUI>().text = "Question #1";

        // Always start off with the same question
        GameObject.Find("Choice1").GetComponentInChildren<Text>().text = "Detroit";
        GameObject.Find("Choice2").GetComponentInChildren<Text>().text = "Lansing";
        GameObject.Find("Choice3").GetComponentInChildren<Text>().text = "Grand Rapids";
        GameObject.Find("Choice4").GetComponentInChildren<Text>().text = "Ann Arbor";

        currentIndex = 0;                           // Reset current index
        questionNumber = 1;                         // Set question count to 1
        timerCount = 30.0f;                         // Set timer for 30 seconds

        usedQuestions = new ArrayList();            // Initialize usedQuestions ArrayList
        usedQuestions.Add(0);                       // Add first question to list

        gameActive = true;                          // Boolean to keep track of game in progress
    }

    // Do this for every button click
    public void onClick(){

        // Grab player's answer and correct answer
        string playerAnswer = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        string correctAnswer = answerKey[currentIndex];

        // If player is correct, add to score
        if (playerAnswer == correctAnswer){
            playerOneScore++;
        }

        // Get new question
        currentIndex = Random.Range(0, 17);

        // This is a new question
        if (usedQuestions.IndexOf(currentIndex) == -1){
            usedQuestions.Add(currentIndex);                        // Add the new question to usedQuestions list
        }

        // This question has been used
        else if (usedQuestions.Count != QPrompts.Length){

            // Loop until new question is found
            while (usedQuestions.IndexOf(currentIndex) != -1){
                currentIndex = Random.Range(0, 17);                 // Search for new question
            }
            usedQuestions.Add(currentIndex);                        // Add the new question to usedQuestions list
        }

        // No new questions available
        else{
            Debug.Log("No more questions");
            gameActive = false;
            return;
        }

        // Change the prompt and question #
        changeQuestion(QPrompts[currentIndex]);
        GameObject.Find("questionCounter").GetComponent<TMPro.TextMeshProUGUI>().text = "Question #" + ++questionNumber;

        // Display new choices that correspond to the new prompt
        GameObject.Find("Choice1").GetComponentInChildren<Text>().text = QChoices[currentIndex,0];
        GameObject.Find("Choice2").GetComponentInChildren<Text>().text = QChoices[currentIndex,1];
        GameObject.Find("Choice3").GetComponentInChildren<Text>().text = QChoices[currentIndex,2];
        GameObject.Find("Choice4").GetComponentInChildren<Text>().text = QChoices[currentIndex,3];
    }

    // Method to change the prompt
    public void changeQuestion(string newQuestion){
        questionPrompt.GetComponent<TMPro.TextMeshProUGUI>().text = newQuestion;
    }

    public void Update(){
        if (gameActive){
            timerCount -= Time.deltaTime;
            GameObject.Find("timer").GetComponent<TMPro.TextMeshProUGUI>().text = "Time: " + timerCount.ToString("F2");
            if (timerCount <= 0){
                Debug.Log("Time's Up");
                gameActive = false;
                GameObject.Find("timer").GetComponent<TMPro.TextMeshProUGUI>().text = "TIME'S UP";
                //GameObject.Find("Choice1").setActive(false);
                //GameObject.Find("Choice2").interactable = false;
                //GameObject.Find("Choice3").interactable = false;
                //GameObject.Find("Choice4").interactable = false;
                return;
            }
        }
    }
}
