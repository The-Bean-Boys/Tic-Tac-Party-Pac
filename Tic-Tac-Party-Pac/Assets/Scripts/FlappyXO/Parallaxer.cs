using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Code tutorial provided by Renaissance Coders:
 * https://www.youtube.com/watch?v=A-GkNM8M5p8
 */

public class Parallaxer : MonoBehaviour
{
    // Object to be Parallax'd
    class PoolObject
    {
        public Transform transform; // Reference it's transform
        public bool inUse; // is it on screen

        // Constructor, must have transform
        public PoolObject (Transform t)
        {
            transform = t; 
        }
        public void Use()
        {
            inUse = true;
        }
        public void Dispose()
        {
            inUse = false;
        }
    }

    [System.Serializable]
    // only used by pipes, sets a Y range in which to spawn the object
    public struct YSpawnRange
    {
        public float min;
        public float max;
    }

    public GameObject Prefab; // Prefab to Parallax for this script
    public int poolSize; // max objects to have loaded at once
    public float shiftSpeed; // how quickly it scrolls across the screen
    public float spawnRate; // how often (in seconds) to spawn a new PoolObject of the prefab

    public YSpawnRange ySpawnRange; // referencable range
    public Vector3 defaultSpawnPos; // default (off screen) spawn coords
    public bool spawnImmediate; // should this prefab be on the screen to start? (background, not pipes)
    public Vector3 immediateSpawnPos; // if yes ^, what coords
    public Vector2 targetAspectRatio; // which aspect ratio are we set for?

    float spawnTimer; 
    float targetAspect; // actual aspect ratio decimal
    PoolObject[] poolObjects; // Array of spawned objects
    GameManager game; // reference to gamemanager script

    private void Awake()
    {
        Configure(); // set up parallax
    }

    private void Start()
    {
        game = GameManager.Instance; //get reference to static instance
    }

    private void OnEnable()
    {
        GameManager.OnGameOver += OnGameOver; // subscribe to event
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= OnGameOver; // unsubscribe
    }

    void OnGameOver()
    {
        for (int i = 0; i < poolObjects.Length; i++)
        {
            poolObjects[i].Dispose();
            poolObjects[i].transform.position = Vector3.one * 1000; // move them off screen
        }
        if (spawnImmediate)
        {
            SpawnImmediate(); // spawn immediate prefabs (stationary)
        }
    }

    private void Update()
    {
        if (game.GameOver) return;

        Shift();

        // calculates timer to determine if its time to spawn another PoolObject
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnRate)
        {
            Spawn();
            spawnTimer = 0;
        }
    }

    // initial configuration
    void Configure()
    {
        targetAspect = targetAspectRatio.x / targetAspectRatio.y;
        poolObjects = new PoolObject[poolSize];
        for (int i = 0; i < poolObjects.Length; i++)
        {
            GameObject go = Instantiate(Prefab) as GameObject; // instantiate a prefab
            Transform t = go.transform; // get transform of newly instantiated prefab
            t.SetParent(transform); // set new inst. prefab's parent to its spawner obj
            t.position = Vector3.one * 1000; // move (way) off screen to start
            poolObjects[i] = new PoolObject(t); // new poolobject with this object's transform
        }

        if (spawnImmediate)
        {
            SpawnImmediate();
        }
    }

    // "spawn" one of the poolobjects in the default coords position
    void Spawn()
    {
        Transform t = GetPoolObject();
        if (t == null) return; //if true, the poolSize is too small
        Vector3 pos = Vector3.zero; // zero out coords
        pos.x = (defaultSpawnPos.x * Camera.main.aspect) / targetAspect; // make sure x position is off screen no matter the "real" aspect ratio
        pos.y = Random.Range(ySpawnRange.min, ySpawnRange.max);
        t.position = pos;
    }

    // "spawn" one of the poolobjects directly at "0,0" because it should be on screen to start
    //  i.e. environment objects: the ground, clouds, stars
    void SpawnImmediate()
    {
        Transform t = GetPoolObject();
        if (t == null) return; //if true, the poolSize is too small
        Vector3 pos = Vector3.zero;
        pos.x = (immediateSpawnPos.x * Camera.main.aspect) / targetAspect;
        pos.y = Random.Range(ySpawnRange.min, ySpawnRange.max);
        t.position = pos;
        Spawn();
    }

    // move to the left by this script's shiftSpeed
    void Shift()
    {
        for (int i = 0; i < poolObjects.Length; i++)
        {
            poolObjects[i].transform.position += -Vector3.right * shiftSpeed * Time.deltaTime;
            CheckDisposeObject(poolObjects[i]); // check if off screen
        }
    }

    // check if poolobject off screen
    void CheckDisposeObject(PoolObject poolObject)
    {
        // if off screen (no matter the "real" aspect)
        if (poolObject.transform.position.x < (-defaultSpawnPos.x * Camera.main.aspect) / targetAspect)
        {
            poolObject.Dispose(); // mark as "unused" (can be respawned "safely" -- currently off screen)
            poolObject.transform.position = Vector3.one * 1000; // move offscreen
        }
    }

    // return the transform of a currently unused poolObject
    Transform GetPoolObject()
    {
        for (int i = 0; i < poolObjects.Length; i++)
        {
            if (!poolObjects[i].inUse)
            {
                poolObjects[i].Use();
                return poolObjects[i].transform;
            }
        }
        return null;
    }
}
