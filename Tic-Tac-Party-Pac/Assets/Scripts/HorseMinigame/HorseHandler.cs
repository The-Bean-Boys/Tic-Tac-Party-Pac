using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseHandler : MonoBehaviour
{

    new Rigidbody2D rigidbody2D;
    public float acceleration = 1.0f;
    float speed = 10.0f;

    /* Start is called before the first frame update
     */
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        speed = 10.0f;
    }

    /* This function is called every time the side of the screen the horse
     * belongs to is clicked.
     */
    public void move()
    {
        speed += 1.0f;
    }

    /* Update is called every frame update
     */
    private void Update()
    {
        rigidbody2D.velocity = transform.right * speed * acceleration;
    }

}
