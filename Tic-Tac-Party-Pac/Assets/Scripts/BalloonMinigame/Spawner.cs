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
        Invoke("stopIt", 15);
    }

    private void Spawning()
    {
        GameObject temp;
        if (Random.Range(0, 1) == 0)
        {
            temp = Instantiate(Balloon_b, new Vector3(Random.Range(transform.position.x - 2.5f, transform.position.x + 2f), Random.Range(transform.position.y - 3, transform.position.y), transform.position.z), Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sortingLayerName = "Back Middle";
            temp.transform.SetParent(gameObject.transform);
            temp = Instantiate(Balloon_g, new Vector3(Random.Range(transform.position.x - 2f, transform.position.x + 2.5f), Random.Range(transform.position.y - 3, transform.position.y), transform.position.z), Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sortingLayerName = "Front Middle";
            temp.transform.SetParent(gameObject.transform);

        }
        else
        {
            temp = Instantiate(Balloon_g, new Vector3(Random.Range(transform.position.x - 2f, transform.position.x + 2.5f), Random.Range(transform.position.y - 3, transform.position.y), transform.position.z), Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sortingLayerName = "Back Middle";
            temp.transform.SetParent(gameObject.transform);
            temp = Instantiate(Balloon_b, new Vector3(Random.Range(transform.position.x - 2.5f, transform.position.x + 2f), Random.Range(transform.position.y - 3, transform.position.y), transform.position.z), Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sortingLayerName = "Front Middle";
            temp.transform.SetParent(gameObject.transform);

        }
    }
    
    private void stopIt()
    {
        CancelInvoke("Spawning");
    }
}
