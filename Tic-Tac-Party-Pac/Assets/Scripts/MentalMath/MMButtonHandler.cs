using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject xstart;
    public GameObject ostart;
    public GameObject finalMessage;
    int opA, opB, opC, opD, opE, opF;
    int count; //number of buttons pressed
    int sum;
    float timeLeft;
    bool timerActive;
    bool needsRematch;

    // Start is called before the first frame update
    void Start()
    {
        xstart.SetActive(true);
        ostart.SetActive(false);
        finalMessage.SetActive(false);
        timerActive = false;
        needsRematch = false;

        score = new int[] { 0, 0 }; // x, y

        
    }

    public void xstarted()
    {
        xstart.SetActive(false);
        timeLeft = 30.0f;
        timerActive = true;
        whoseTurn = 0;
        GenerateProblem();
    }

    public void ostarted()
    {
        ostart.SetActive(false);
        timeLeft = 30.0f;
        timerActive = true;
        whoseTurn = 1;
        GenerateProblem();
    }

    public void finalMsgClicked()
    {
        if(needsRematch)
        {
            Start(); //Restarts game
        }
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
            int temp = Random.Range(1, result);
            int[] r = RandomizeButtons(temp, result - temp, Random.Range(1, 20), Random.Range(1, 20), Random.Range(1, 20), Random.Range(1, 20));
            opA = r[0];
            opB = r[1];
            opC = r[2];
            opD = r[3];
            opE = r[4];
            opF = r[5];
        }
        else if (operation == 2)
        {
            result = Random.Range(1, 20);
            int temp = Random.Range(result + 1, 30);
            int[] r = RandomizeButtons(temp, temp - result, Random.Range(1, 30), Random.Range(1, 30), Random.Range(1, 30), Random.Range(1, 30));
            opA = r[0];
            opB = r[1];
            opC = r[2];
            opD = r[3];
            opE = r[4];
            opF = r[5];
        }
        else if (operation == 3)
        {
            opA = Random.Range(1, 13);
            opB = Random.Range(1, 13);
            result = opA * opB;

            int[] r = RandomizeButtons(opA, opB, Random.Range(1, 13), Random.Range(1, 13), Random.Range(1, 13), Random.Range(1, 13));

            opA = r[0];
            opB = r[1];
            opC = r[2];
            opD = r[3];
            opE = r[4];
            opF = r[5];
        }
        else if (operation == 4)
        {
            result = Random.Range(1, 13);
            opA = Random.Range(1, 13);
            opB = result * opA;

            int[] r = RandomizeButtons(opA, opB, Random.Range(1, 13), Random.Range(1, 13) * Random.Range(1, 13), Random.Range(1, 13), Random.Range(1, 13) * Random.Range(1, 13));

            opA = r[0];
            opB = r[1];
            opC = r[2];
            opD = r[3];
            opE = r[4];
            opF = r[5];
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
                //division 
                //To avoid problems with integer division, the way this method works for division is a little different.
                //Remember that this function is trying to find button1 / button2 = result. Equivalently, result * button2 = button1.
                //To achieve this:
                //When the first button is pressed, we store "result" into sum. We then overwrite result with button1.
                // Button1 (the first button the user presses) is the result of our multiplication problem.
                // For the second button, we will multiply that by the old result and see if it matches button1 (which is stored in result)
                if (count == 0)
                {
                    sum = result;
                    result = value;
                }
                else
                    sum *= value;
                break;
        }


        // working on handling when the buttons are pressed.
        count++;
        if (count == 2)
        {
            if (sum == result)
            {
                score[whoseTurn]++;
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



    int[] RandomizeButtons(int a, int b, int c, int d, int e, int f)
    {
        ArrayList entryArray = new ArrayList();
        entryArray.Add(a);
        entryArray.Add(b);
        entryArray.Add(c);
        entryArray.Add(d);
        entryArray.Add(e);
        entryArray.Add(f);

        int a2, b2, c2, d2, e2, f2;

        int rand = Random.Range(0, 6);
        a2 = (int) entryArray[rand];
        entryArray.RemoveAt(rand);

        rand = Random.Range(0, 5);
        b2 = (int)entryArray[rand];
        entryArray.RemoveAt(rand);

        rand = Random.Range(0, 4);
        c2 = (int)entryArray[rand];
        entryArray.RemoveAt(rand);

        rand = Random.Range(0, 3);
        d2 = (int)entryArray[rand];
        entryArray.RemoveAt(rand);

        rand = Random.Range(0, 2);
        e2 = (int)entryArray[rand];
        entryArray.RemoveAt(rand);

        f2 = (int) entryArray[0];

        return new int[] { a2, b2, c2, d2, e2, f2 };


    }


    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            timeLeft -= Time.deltaTime;

            if(timeLeft < 0)
            {
                timerActive = false;
                if (whoseTurn == 0)
                {
                    //now it is player 2's turn
                    ostart.SetActive(true);
                }
                else
                {
                    evaluateGame();
                }
            }
        }
    }


    void evaluateGame()
    {
        finalMessage.SetActive(true);
        if (score[0] > score[1])
        {
            finalMessage.GetComponentInChildren<Text>().text = "X Wins!";
            PlayerPrefs.SetInt("WinnerMentalMath", 0);
            Invoke("MinigameOver", 5);
        }
        else if (score[0] < score[1])
        {
            finalMessage.GetComponentInChildren<Text>().text = "O Wins!";
            PlayerPrefs.SetInt("WinnerMentalMath", 1);
            Invoke("MinigameOver", 5);
        }
        else
        {
            needsRematch = true;
            finalMessage.GetComponentInChildren<Text>().text = "Tied Game!\nClick to\nPlay Again";
        }
    }

    private void MinigameOver()
    {
        SceneManager.LoadScene("GameScene");
    }
}
