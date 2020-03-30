using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeHandler : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    float speed;

    /* Start is called before the first frame update
     */
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        speed = 0.0f;
    }

    /* This function is called every time a click is registered
     */
    public void move(string player)
    {
        if (player == "LButton"){
            speed -= 5.0f;
        }
        else{
            speed += 5.0f;
        }
    }

    /* Update is called every frame update
     */
    private void Update()
    {
        rigidbody2D.velocity = transform.right * speed;
    }
}
