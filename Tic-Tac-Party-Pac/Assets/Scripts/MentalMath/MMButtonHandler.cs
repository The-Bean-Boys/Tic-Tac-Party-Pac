using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMButtonHandler : MonoBehaviour
{
    int whoseTurn; // 0 = player1, 1 = player2
    int result; //the number displayed in the top right box; the answer to the math problem.
    int operation; // the operation: 1 = addition, 2 = subtraction, 3 = multiplication, 4 = division
    int score0;
    int score1; // scores for players 1 and 2.
    public GameObject operationTextBox;
    public GameObject resultTextBox;
    public GameObject optionA;
    public GameObject optionB;
    public GameObject optionC;
    public GameObject optionD;
    public GameObject optionE;
    public GameObject optionF;
    int opA, opB, opC, opD, opE, opF;
    int count;

    // Start is called before the first frame update
    void Start()
    {
        whoseTurn = 0;
        score0 = 0;
        score1 = 0;
        count = 20;
        GenerateProblem();
    }

    void GenerateProblem()
    {
        result = Random.Range(1, 20);
        operation = Random.Range(1, 3);

        
        // setting the result box
        resultTextBox.GetComponent<Text>().text = result.ToString();

        // setting the operation box
        if (operation == 1)
            operationTextBox.GetComponent<Text>().text = "+";
        if (operation == 2)
            operationTextBox.GetComponent<Text>().text = "-";
        if (operation == 3)
            operationTextBox.GetComponent<Text>().text = "*";
        if (operation == 4)
            operationTextBox.GetComponent<Text>().text = "/";

        // setting up the buttons. need to randomize order.
        if (operation == 1)
        {
            opA = Random.Range(1, result);
            opB = result - opA;
            opC = Random.Range(1, 20);
            opD = Random.Range(1, 20);
            opE = Random.Range(1, 20);
            opF = Random.Range(1, 20);
        }
        else if (operation == 2)
        {
            opA = Random.Range(result+1, 30);
            opB = opA - result;
            opC = Random.Range(1, 30);
            opD = Random.Range(1, 30);
            opE = Random.Range(1, 30);
            opF = Random.Range(1, 30);
        }

        optionA.GetComponentInChildren<Text>().text = opA.ToString();
        optionB.GetComponentInChildren<Text>().text = opB.ToString();
        optionC.GetComponentInChildren<Text>().text = opC.ToString();
        optionD.GetComponentInChildren<Text>().text = opD.ToString();
        optionE.GetComponentInChildren<Text>().text = opE.ToString();
        optionF.GetComponentInChildren<Text>().text = opF.ToString();

    }

    public void ButtonPressed()
    {
        // working on handling when the buttons are pressed.
        count++;

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
