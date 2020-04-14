using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SideScrollerPlayer : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate OnPlayerDied;

    public float jumpPower = 10.0f;
    public float posX;
    public Vector3 startPos;

    new Rigidbody2D rb;
    private bool isGrounded = false;

    SideScrollerManager Manager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Manager = SideScrollerManager.Instance;
    }

    private void OnEnable()
    {
        // subscribe to events
        SideScrollerManager.OnGameStarted += OnGameStarted;
        SideScrollerManager.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        // unsubscribe from events
        SideScrollerManager.OnGameStarted -= OnGameStarted;
        SideScrollerManager.OnGameOver -= OnGameOver;
    }

    void OnGameStarted()
    {
        rb.velocity = Vector3.zero;
        transform.localPosition = startPos;

        rb.simulated = true; // activate physics
    }

    void OnGameOver()
    {
        rb.simulated = false; // remove physics
    }

    void FixedUpdate()
    {
        if (Manager.startPage.activeSelf || Manager.gameOverPage.activeSelf) { return; }

        //Hit in face
        if (transform.position.x < posX-.1)
        {
            GameOver();
        }
    }

    public void Jump()
    {
        if (Manager.startPage.activeSelf || Manager.gameOverPage.activeSelf) { return; }

        if (isGrounded)
        {
            rb.AddForce(Vector3.up * (jumpPower * rb.mass * rb.gravityScale * 20.0f));
        }
    }

    void GameOver()
    {
        rb.simulated = false;
        OnPlayerDied();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Ground")
        {
            isGrounded = true;
        }
        if (col.gameObject.tag == "DeadZone")
        {
            rb.simulated = false;
            GameOver();
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
