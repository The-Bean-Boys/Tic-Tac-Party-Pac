using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TapController : MonoBehaviour
{
    // event delegate
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;

    public float tapForce = 10f;
    public float tiltSmooth = 5f; // how quickly to rotate
    public Vector3 startPos;

    new Rigidbody2D rigidbody; 
    Quaternion downRotation; // degree to stop rotating at
    Quaternion forwardRotation; // degree to start rotating at

    GameManager game; // reference to GameManager

    private void OnEnable()
    {
        // subscribe to events
        GameManager.OnGameStarted += OnGameStarted; 
        GameManager.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        // unsubscribe from events
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOver -= OnGameOver;
    }

    void OnGameStarted()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.simulated = true; // activate physics
        transform.localPosition = startPos; // move to start position (relative to parent)
        transform.rotation = Quaternion.identity; // face forward
    }

    void OnGameOver()
    {
        rigidbody.simulated = false; // remove physics
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0, 0, -90); // maximum gravity induced rotation -> straight down
        forwardRotation = Quaternion.Euler(0, 0, 35); // up tilt rotation (when tapped)
        game = GameManager.Instance;
    }

    private void Update()
    {
        if (game.startPage.activeSelf || game.gameOverPage.activeSelf) return;
        // rotate towards downRotation with indicated "speed" based on tiltSmooth scalar
        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
    }

    // when left or right buttons are clicked, they trigger this event based on which object this script is attached to
    public void Tapped()
    {
        if (game.startPage.activeSelf || game.gameOverPage.activeSelf) return;
        transform.rotation = forwardRotation; // tilt up
        rigidbody.velocity = Vector3.zero; // stop downward velocity
        rigidbody.AddForce(Vector2.up * tapForce, ForceMode2D.Force); // add upward velocity
    }

    // on collision
    private void OnTriggerEnter2D(Collider2D col)
    {
        // tags were meant for another collision to keep track of score, but thats not needed for this
        if (col.gameObject.tag == "DeadZone")
        {
            rigidbody.simulated = false;
            forwardRotation = downRotation;
            OnPlayerDied(); // event sent to GameManager;
        }
    }
}
