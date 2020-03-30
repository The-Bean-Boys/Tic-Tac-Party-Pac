using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Handler : MonoBehaviour
{
    public GameObject rope;
    public RopeHandler ropeScript;
    public void OnClick()
    {
        string playerClicked = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("Button: " + playerClicked);
        ropeScript.move(playerClicked);
    }
}
