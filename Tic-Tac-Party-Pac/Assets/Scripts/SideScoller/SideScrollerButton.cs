using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideScrollerButton : MonoBehaviour
{
    public SideScrollerPlayer player;

    private bool pressed = false;

    // Update is called once per frame
    void Update()
    {
        // While button is held down
        if (pressed)
        {
            player.Jump();
        }
    }

    // When button is pressed
    public void OnMouseDown()
    {
        pressed = true;
    }

    // Once button is no longer pressed
    public void OnMouseUp()
    {
        pressed = false;
    }
}
