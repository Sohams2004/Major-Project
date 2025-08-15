using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject player;
    public float spawnInterval = 2f;
    public float cameraMargin;
    public float minPlayerDistance;
    public Camera cam;

    private float nextSpawnTime = 0f;
    [SerializeField] int currentEnemyCount = 0;

    [SerializeField] bool isSpawningEnemies = true;
    
    RoomGenerate roomGenerate;

    private Coroutine enemyCoroutine;

    private void Awake()
    {
        roomGenerate = FindObjectOfType<RoomGenerate>();
    }

    public void StartSpawingEnemy()
    {
        Debug.Log("Start spawning enemy");
        if(!cam) 
        {
            cam = Camera.main;
        }
        
        if (enemyCoroutine == null)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        Debug.Log("Coroutine started");
        var wait = new WaitForSeconds(spawnInterval);
        while (isSpawningEnemies)
        {
            Debug.Log("Spawning enemy");
            SpawnOnFloor();
            yield return wait;
        }
    }

    bool SpawnOnFloor()
    {
        if (roomGenerate.floorTiles.Count == 0 || enemyPrefab == null || enemyPrefab.Length == 0)
        {
            return false;
        }
        
        Bounds cameraBounds = GetCameraBounds(cam, cameraMargin);
        for(int i = 0; i < roomGenerate.floorTiles.Count; i++)
        {
            Vector3Int spawnPosition = roomGenerate.floorTiles[Random.Range(0, roomGenerate.floorTiles.Count)];
            if (!cameraBounds.Contains(spawnPosition))
            {
                if (Vector3.Distance(spawnPosition, GameObject.FindGameObjectWithTag("Player").transform.position) >= minPlayerDistance && GameObject.FindGameObjectWithTag("Player") != null)
                {
                    int randomIndex = Random.Range(0, enemyPrefab.Length);
                    Instantiate(enemyPrefab[randomIndex], roomGenerate.tilemap.CellToWorld(spawnPosition), Quaternion.identity);
                    currentEnemyCount++;
                    return true;
                }
            }
        }
        return false;
    }

    static Bounds GetCameraBounds(Camera camera, float margin)
    {
        if (!camera || !camera.orthographic)
        {
            return new Bounds(Vector3.zero, Vector3.zero);
        }
        
        float screenHeight = camera.orthographicSize * 2f;
        float screenWidth = screenHeight * camera.aspect;
        var size = new Vector3(screenWidth + margin * 2f, screenHeight + margin * 2f, 50f);
        return new Bounds(camera.transform.position, size);
    }


    /*IEnumerator SpawnEnemy()
    {
        Debug.Log("Coroutine started");
        WaitForSeconds wait = new WaitForSeconds(spawnInterval);

        while (isSpawning)
        {
            yield return wait;
            if (currentEnemyCount < 5) 
            {
                int randomIndex = Random.Range(0, enemyPrefab.Length);
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);

                Instantiate(enemyPrefab[randomIndex], spawnPoints[spawnPointIndex].position, Quaternion.identity);
                currentEnemyCount++;
                nextSpawnTime = Time.time + spawnInterval;
            }
        }
    }*/


    void Update()
    {
        
    }
}
