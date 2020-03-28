using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonHandler : MonoBehaviour
{

    public Sprite newSprite;
    bool popped = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!popped)
            transform.position = new Vector2(transform.position.x, transform.position.y + .08f);
        if (transform.position.y > 10)
            Die();
    }

    private void OnMouseDown()
    {
        popped = true;
        GetComponent<SpriteRenderer>().sprite = newSprite;
        Invoke("Die", .5f);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
