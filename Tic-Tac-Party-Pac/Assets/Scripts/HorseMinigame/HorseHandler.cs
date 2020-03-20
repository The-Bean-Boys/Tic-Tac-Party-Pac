using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseHandler : MonoBehaviour
{

    new Rigidbody2D rigidbody2D;
    float speed;


    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        speed = 10.0f;
    }


    public void move()
    {
        speed += .5f;
    }

    private void Update()
    {
        rigidbody2D.velocity = transform.right * speed;
    }

}
