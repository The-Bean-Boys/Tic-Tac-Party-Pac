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

    SideScrollerManager Manager;

    // Start is called before the first frame update
    void Start()
    {
        Manager = SideScrollerManager.Instance;
    }

    private void OnEnable()
    {
        // subscribe to events
        SideScrollerManager.OnGameStarted += OnGameStarted;
    }

    private void OnDisable()
    {
        // unsubscribe from events
        SideScrollerManager.OnGameStarted -= OnGameStarted;
    }

    void OnGameStarted()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        GenerateRandomChallenge();
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.startPage.activeSelf || Manager.gameOverPage.activeSelf) { return; }

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
            if (currentChild.transform.position.x <= -20)
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
