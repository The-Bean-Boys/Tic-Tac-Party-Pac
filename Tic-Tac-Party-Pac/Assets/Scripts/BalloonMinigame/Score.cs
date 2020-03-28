using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        if (gameObject.name.Equals("X Score"))
            BalloonManagerStatic.Xtext = this;
        else
            BalloonManagerStatic.Otext = this;
    }

    public int getScore()
    {
        return score;
    }

    public void AddScore()
    {
        score++;
        gameObject.GetComponent<TMPro.TextMeshPro>().text = score.ToString();
    }
}
