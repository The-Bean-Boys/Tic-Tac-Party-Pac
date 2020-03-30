using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonHandler : MonoBehaviour
{
    public GameObject canvas;
    SimonGameState Game;

    public void Start()
    {
        Game = canvas.GetComponent<SimonGameState>();
    }

    public void Clicked(string Choice)
    {
        Game.CheckSequence(Choice);
    }
}
