using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorTest : MonoBehaviour
{
    public Button changer;
    // Start is called before the first frame update
    void Start()
    {
        int red = (int)(Random.value * 255);
        int green = (int)(Random.value * 255);
        int blue = (int)(Random.value * 255);
        changer.onClick.AddListener(delegate { changeColor(red, green, blue); });
    }
    public void changeColor(int red, int green, int blue)
    {
        Color c = new Vector4(red / 255f, green / 255f, blue / 255f, 1);
        GetComponent<Text>().color = c;
    }
}