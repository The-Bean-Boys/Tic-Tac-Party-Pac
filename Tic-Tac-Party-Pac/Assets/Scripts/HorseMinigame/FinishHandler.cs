using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishHandler : MonoBehaviour
{

    bool finished;
    public GameObject winText;

    // Start is called before the first frame update
    void Start()
    {
        finished = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hitDetected");
        
        if (!finished)
        {
            TextMesh text = winText.GetComponent<TextMesh>();
            text.text = "Player " + collision.gameObject.ToString();
        }
        
        finished = true;
               
    }
}
