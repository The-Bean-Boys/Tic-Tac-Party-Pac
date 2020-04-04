using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonHandler : MonoBehaviour
{
    // sprite stored is the "popped" sprite
    public Sprite newSprite;
    bool popped = false;

    // Update is called once per frame
    void Update()
    {
        // if the balloon hasn't yet been clicked, constantly move it up
        if (!popped)
            transform.position = new Vector2(transform.position.x, transform.position.y + .03f);

        // if balloon goes out of bounds without being clicked, go ahead and kill it so it doesn't clog up resources
        if (transform.position.y > 10)
            Die();
    }

    // works well as an "on tap" as well
    private void OnMouseDown()
    {
        // if not yet popped
        if (!popped)
        {
            // if the gameObject is the X assigned object
            if (tag == "XAsset")
                // increment x's score
                BalloonManagerStatic.Xtext.AddScore();
            else
                // otherwise increment o's score
                BalloonManagerStatic.Otext.AddScore();
            popped = true;
            GetComponent<SpriteRenderer>().sprite = newSprite; // set sprite to "popped" sprite
            Invoke("Die", .5f); // kill after half a second
        }
    }


    // Method simply destroys the instance of the gameObject
    private void Die()
    {
        Destroy(gameObject);
    }
}
