using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class tapController : MonoBehaviour
{

    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;

    public float tapForce = 10f;
    public float tiltSmooth = 5f;
    public Vector3 startPos;
    public int id = 0;

    public int minInput;
    public int maxInput;

    new Rigidbody2D rigidbody;
    Quaternion downRotation;
    Quaternion forwardRotation;

    GameManager game;

    private void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOver -= OnGameOver;
    }

    void OnGameStarted()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.simulated = true;
        transform.localPosition = startPos;
        transform.rotation = Quaternion.identity;
    }

    void OnGameOver()
    {
        rigidbody.simulated = false;
        forwardRotation = downRotation;
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0, 0, -90);
        forwardRotation = Quaternion.Euler(0, 0, 35);
        game = GameManager.Instance;
    }

    private void Update()
    {
        if (game.startPage.activeSelf || game.gameOverPage.activeSelf) return;
        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
    }

    public void Tapped()
    {
        if (game.startPage.activeSelf || game.gameOverPage.activeSelf) return;
        transform.rotation = forwardRotation;
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DeadZone")
        {
            rigidbody.simulated = false;
            forwardRotation = downRotation;
            OnPlayerDied(); //event sent to GameManager;
        }
    }
}
