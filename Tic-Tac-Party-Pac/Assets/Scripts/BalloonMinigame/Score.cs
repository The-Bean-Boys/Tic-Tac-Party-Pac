using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int score;

    // Start is called before the first frame update
    public void Start()
    {
        score = 0;
        gameObject.GetComponent<TMPro.TextMeshPro>().text = score.ToString();
        // assigns static reference(s) from the BalloonManagerStatic to their respective
        //  score text gameObject(s) 
        if (gameObject.name.Equals("X Score"))
            BalloonManagerStatic.Xtext = this;
        else
            BalloonManagerStatic.Otext = this;
    }


    // returns the score value for the score text
    public int getScore()
    {
        return score;
    }

    // increment score & update text
    public void AddScore()
    {
        score++;
        gameObject.GetComponent<TMPro.TextMeshPro>().text = score.ToString();
    }
}
