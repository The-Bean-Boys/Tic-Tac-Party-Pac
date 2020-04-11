using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerPlayer : MonoBehaviour
{
    public float jumpPower = 10.0f;
    public float posX;
    private Rigidbody2D rb;
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * (jumpPower * rb.mass * rb.gravityScale * 20.0f));
        }
        //Hit in face
        if (transform.position.x < posX)
        {
            GameOver();
        }
    }

    private void Update()
    {
        Debug.Log(rb.velocity);
    }

    void GameOver()
    {
        Debug.Log("Game Over");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
