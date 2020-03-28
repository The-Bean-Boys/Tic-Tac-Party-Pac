using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Balloon_b;
    public GameObject Balloon_g;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawning", 0, 1);
    }

    private void Spawning()
    {
        GameObject temp;
        if (Random.Range(0, 1) == 0)
        {
            temp = Instantiate(Balloon_b, new Vector3(Random.Range(transform.position.x - 2.5f, transform.position.x + 2f), Random.Range(transform.position.y - 3, transform.position.y), transform.position.z), Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sortingLayerName = "Back Middle";
            temp = Instantiate(Balloon_g, new Vector3(Random.Range(transform.position.x - 2f, transform.position.x + 2.5f), Random.Range(transform.position.y - 3, transform.position.y), transform.position.z), Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sortingLayerName = "Front Middle";
        } else
        {
            temp = Instantiate(Balloon_g, new Vector3(Random.Range(transform.position.x - 2f, transform.position.x + 2.5f), Random.Range(transform.position.y - 3, transform.position.y), transform.position.z), Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sortingLayerName = "Back Middle";
            temp = Instantiate(Balloon_b, new Vector3(Random.Range(transform.position.x - 2.5f, transform.position.x + 2f), Random.Range(transform.position.y - 3, transform.position.y), transform.position.z), Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sortingLayerName = "Front Middle";
        }
    }
}
