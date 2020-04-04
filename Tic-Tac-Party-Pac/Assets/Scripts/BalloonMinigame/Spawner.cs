using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // attach references of prefabs
    public GameObject Balloon_b;
    public GameObject Balloon_g;

    public GameObject winnerText;

    // Start is called before the first frame update
    void Start()
    {
        // hide winner text
        winnerText.SetActive(false);
        InvokeRepeating("Spawning", 0, .5f); // call the spawn function every .5 seconds
        Invoke("StopIt", 15); // call StopIt(), which runs the end of minigame function
    }

    private void Spawning()
    {
        // empty reference to a gameObject
        GameObject temp;

        /* random int from 0 to 1. 0 instantiates a blue balloon first, resulting in the
         * blue balloon being a layer lower than the green balloon, 1 being the opposite. 
         * If these two balloons happen to spawn on top of eachother, this method ensures
         * one balloon isn't always spawning above the other. (Higher layer balloons get
         * popped first when clicked).
         */
        if (Random.Range(0, 1) == 0)
        {
            /* set temp reference to a new instantiation of a blue and green balloon
             * blue always spawns towards the left side of the screen-ish, green to
             * the right. Their visual layer is updated to be consistent with drawing order.
             * Each balloon is set to have the spawner as a parent
             */
            temp = Instantiate(Balloon_b, new Vector3(Random.Range(transform.position.x - 2.5f, transform.position.x + 2f), Random.Range(transform.position.y - 4, transform.position.y), transform.position.z), Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sortingLayerName = "Back Middle";
            temp.transform.SetParent(gameObject.transform);
            temp = Instantiate(Balloon_g, new Vector3(Random.Range(transform.position.x - 2f, transform.position.x + 2.5f), Random.Range(transform.position.y - 4, transform.position.y), transform.position.z), Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sortingLayerName = "Front Middle";
            temp.transform.SetParent(gameObject.transform);

        }
        else
        {
            temp = Instantiate(Balloon_g, new Vector3(Random.Range(transform.position.x - 2f, transform.position.x + 2.5f), Random.Range(transform.position.y - 4, transform.position.y), transform.position.z), Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sortingLayerName = "Back Middle";
            temp.transform.SetParent(gameObject.transform);
            temp = Instantiate(Balloon_b, new Vector3(Random.Range(transform.position.x - 2.5f, transform.position.x + 2f), Random.Range(transform.position.y - 4, transform.position.y), transform.position.z), Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sortingLayerName = "Front Middle";
            temp.transform.SetParent(gameObject.transform);

        }
    }

    // When 15 seconds have passed
    private void StopIt()
    {
        // stop the above InvokeRepeating call
        CancelInvoke("Spawning");
        // call the Winner() function after 3 seconds to ensure all balloons have despawned
        Invoke("Winner", 3);
    }

    // Decide who the winner is
    private void Winner()
    {
        if (BalloonManagerStatic.Xtext.getScore() > BalloonManagerStatic.Otext.getScore())
        {
            winnerText.GetComponent<TMPro.TextMeshPro>().text = "X Wins!";
        } else if (BalloonManagerStatic.Xtext.getScore() < BalloonManagerStatic.Otext.getScore())
        {
            winnerText.GetComponent<TMPro.TextMeshPro>().text = "O Wins!";
        } else
        {
            winnerText.GetComponent<TMPro.TextMeshPro>().text = "A tie?";
        }
        winnerText.SetActive(true); // enable winner text
    }
}
