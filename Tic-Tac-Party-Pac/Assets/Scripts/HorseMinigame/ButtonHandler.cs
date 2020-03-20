using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{

    public GameObject horse;
    public HorseHandler horseScript;

   public void OnClick()
    {
        Debug.Log("BeenClicked!");
        horseScript.move();
    }
}
