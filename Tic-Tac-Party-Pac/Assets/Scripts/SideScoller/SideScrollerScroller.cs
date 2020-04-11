using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerScroller : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public GameObject[] challenges;
    public float freq = 0.5f;
    float counter = 0.0f;
    public Transform challengesSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomChallenge();
    }

    // Update is called once per frame
    void Update()
    {
        //GenerateObjects
        if (counter <= 0.0f)
        {
            GenerateRandomChallenge();
        } else
        {
            counter -= Time.deltaTime * freq;
        }

        //Scrolling
        GameObject currentChild;
        for (int i = 0; i < transform.childCount; i++)
        {
            currentChild = transform.GetChild(i).gameObject;
            ScrollChallenge(currentChild);
            if (currentChild.transform.position.x <= -15)
            {
                Destroy(currentChild);
            }
        }

    }


    void ScrollChallenge(GameObject currChallenge)
    {
        currChallenge.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
    }

    void GenerateRandomChallenge()
    {
        GameObject newChallenge = Instantiate(challenges[Random.Range(0, challenges.Length)], challengesSpawnPoint.position, Quaternion.identity) as GameObject;
        newChallenge.transform.parent = transform;
        counter = 1.0f;
    }
}
