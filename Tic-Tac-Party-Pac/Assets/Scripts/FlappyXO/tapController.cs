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

    new Rigidbody2D rigidbody;
    Quaternion downRotation;
    Quaternion forwardRotation;

    public GameObject gameManager;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0, 0, -90);
        forwardRotation = Quaternion.Euler(0, 0, 35);
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.rotation = forwardRotation;
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ScoreZone")
        {

        }

        if (col.gameObject.tag == "DeadZone")
        {
            rigidbody.simulated = false;
            forwardRotation = downRotation;
            OnPlayerDied(); //event sent to GameManager;
        }
    }
}
