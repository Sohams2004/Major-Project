using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour
{
    [SerializeField] private GameObject[] lootPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] float cameraMargin = 1f;
    public float minPlayerDistance;
    public Camera cam;

    private float nextSpawnTime = 0f;
    [SerializeField] bool isSpawningLoot = true;
    [SerializeField] private int currentLootCount = 0;

    RoomGenerate roomGenerate;
    private Coroutine lootCoroutine;
    
    private void Awake()
    {
        roomGenerate = FindObjectOfType<RoomGenerate>();
    }

    public void StartSpawnLoot()
    {
        if(!cam) 
        {
            cam = Camera.main;
        }
        
        if (lootCoroutine == null)
        {
            StartCoroutine(SpawnLoot());
        }
    }

    IEnumerator SpawnLoot()
    {
        var wait = new WaitForSeconds(spawnInterval);
        while (isSpawningLoot)
        {
            SpawnLootOnFloor();
            yield return wait;
        }
    }

    bool SpawnLootOnFloor()
    {
        if (roomGenerate.floorTiles.Count == 0 || lootPrefab == null || lootPrefab.Length == 0)
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
                    int randomIndex = Random.Range(0, lootPrefab.Length);
                    Instantiate(lootPrefab[randomIndex], roomGenerate.tilemap.CellToWorld(spawnPosition), Quaternion.identity);
                    currentLootCount++;
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

}
