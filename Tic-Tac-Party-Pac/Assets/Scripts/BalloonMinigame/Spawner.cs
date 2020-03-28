using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Balloon_b;
    public GameObject Balloon_g;
    public GameObject winnerText;

    // Start is called before the first frame update
    void Start()
    {
        winnerText.SetActive(false);
        InvokeRepeating("Spawning", 0, .5f);
        Invoke("StopIt", 15);
    }

    private void Spawning()
    {
        GameObject temp;
        if (Random.Range(0, 1) == 0)
        {
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

    private void StopIt()
    {
        CancelInvoke("Spawning");
        Invoke("Winner", 3);
    }

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
        winnerText.SetActive(true);
    }
}
