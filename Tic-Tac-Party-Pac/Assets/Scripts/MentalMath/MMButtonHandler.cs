using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMButtonHandler : MonoBehaviour
{
    int whoseTurn; // 0 = player1, 1 = player2
    int result; //the number displayed in the top right box; the answer to the math problem.
    int operation; // the operation: 1 = addition, 2 = subtraction, 3 = multiplication, 4 = division
    int[] score;
    public GameObject operationTextBox;
    public GameObject resultTextBox;
    public GameObject optionA;
    public GameObject optionB;
    public GameObject optionC;
    public GameObject optionD;
    public GameObject optionE;
    public GameObject optionF;
    int opA, opB, opC, opD, opE, opF;
    int count; //number of buttons pressed
    int sum;

    // Start is called before the first frame update
    void Start()
    {
        whoseTurn = 0;
        score = new int[] { 0, 0 };
        //count = 20;
        GenerateProblem();
    }

    void GenerateProblem()
    {
        
        operation = Random.Range(1, 5);

        sum = 0;
        count = 0;

        
        

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
            result = Random.Range(1, 20);
            opA = Random.Range(1, result);
            opB = result - opA;
            opC = Random.Range(1, 20);
            opD = Random.Range(1, 20);
            opE = Random.Range(1, 20);
            opF = Random.Range(1, 20);
        }
        else if (operation == 2)
        {
            result = Random.Range(1, 20);
            opA = Random.Range(result+1, 30);
            opB = opA - result;
            opC = Random.Range(1, 30);
            opD = Random.Range(1, 30);
            opE = Random.Range(1, 30);
            opF = Random.Range(1, 30);
        }
        else if (operation == 3)
        {
            opA = Random.Range(1, 13);
            opB = Random.Range(1, 13);
            result = opA * opB;
            opC = Random.Range(1, 13);
            opD = Random.Range(1, 13);
            opE = Random.Range(1, 13);
            opF = Random.Range(1, 13);
        }
        else if (operation == 4)
        {
            result = Random.Range(1, 13);
            opA = Random.Range(1, 13);
            opB = result * opA;
            
            opC = Random.Range(1, 13);
            opD = Random.Range(1, 13) * Random.Range(1, 13);
            opE = Random.Range(1, 13);
            opF = Random.Range(1, 13) * Random.Range(1, 13);
        }

        // setting the result box
        resultTextBox.GetComponent<Text>().text = result.ToString();

        optionA.GetComponentInChildren<Text>().text = opA.ToString();
        optionB.GetComponentInChildren<Text>().text = opB.ToString();
        optionC.GetComponentInChildren<Text>().text = opC.ToString();
        optionD.GetComponentInChildren<Text>().text = opD.ToString();
        optionE.GetComponentInChildren<Text>().text = opE.ToString();
        optionF.GetComponentInChildren<Text>().text = opF.ToString();

    }

    //handles the things that need to happen, regardless of which button is pressed. called from each ButtonXPressed() method
    public void ButtonPressed(int value)
    {
        switch (operation)
        {
            case 1:
                //addition
                sum += value;
                break;
            case 2:
                //subtraction
                if (count == 0)
                    sum = value;
                else
                    sum -= value;
                break;
            case 3:
                //multiplication
                if (count == 0)
                    sum = 1;
                sum *= value;
                break;
            case 4:
                //division - fix this to not allow for erroneous correct answers (integer division)
                if (count == 0)
                    sum = value;
                else
                    sum /= value;
                break;
        }


        // working on handling when the buttons are pressed.
        count++;
        if (count == 2)
        {
            if (sum == result)
            {
                score[whoseTurn]++;
                Debug.Log("CORRECT!");
            }

            GenerateProblem(); //this method resets sum and count.
        }
    }

    public void ButtonAPressed()
    {
        int value = int.Parse(optionA.GetComponentInChildren<Text>().text);
        ButtonPressed(value);
    }

    public void ButtonBPressed()
    {
        int value = int.Parse(optionB.GetComponentInChildren<Text>().text);
        ButtonPressed(value);
    }

    public void ButtonCPressed()
    {
        int value = int.Parse(optionC.GetComponentInChildren<Text>().text);
        ButtonPressed(value);
    }

    public void ButtonDPressed()
    {
        int value = int.Parse(optionD.GetComponentInChildren<Text>().text);
        ButtonPressed(value);
    }

    public void ButtonEPressed()
    {
        int value = int.Parse(optionE.GetComponentInChildren<Text>().text);
        ButtonPressed(value);
    }

    public void ButtonFPressed()
    {
        int value = int.Parse(optionF.GetComponentInChildren<Text>().text);
        ButtonPressed(value);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
