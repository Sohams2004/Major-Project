using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseGrid : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject playerRoomSpawn ;
    [SerializeField] GameObject[] objprefabs;

    [SerializeField] int gridArraySize;

    [SerializeField] public int gridNodeCountX;
    [SerializeField] public int gridNodeCountY;

    [SerializeField] float nodeWidth;
    [SerializeField] float nodeHeight;

    [SerializeField] float spacingx;
    [SerializeField] float spacingy;

    [SerializeField] int numeberOfNodesToSpawn;
    [SerializeField] int numeberOfPlayerRoomNodesToSpawn;

    Dictionary<Vector2Int, GameObject> spawnedObjects = new Dictionary<Vector2Int, GameObject>();

    private void Start()
    {
        GenerateGrid();
        PlayerRoomSpawn();
        SpawnPrefabs();
    }

    void GenerateGrid()
    {
        gridArraySize = gridNodeCountX * gridNodeCountY;

        Vector3 gameObjPos = gameObject.transform.position;

        for (int y = 0; y < gridNodeCountY; y++)
        {
            for (int x = 0; x < gridNodeCountX; x++)
            {
                int a = x + y * gridNodeCountX;

                Vector3Int gridPos = new Vector3Int(x, y, 0);
                Vector3 worldPos = new Vector3(x * (nodeWidth + spacingx), y * (nodeHeight + spacingy), 0);
                Vector3 spawnPos = gameObjPos + worldPos;


                GameObject tile = null;

                tile = Instantiate(tilePrefab, spawnPos, Quaternion.identity);
                tile.transform.localScale = new Vector3(nodeWidth, nodeHeight, 1);
                tile.transform.parent = gameObject.transform;
                //tile.SetActive(false);
            }
        }
    }

    void SpawnPrefabs()
    {
        int maxPossiblePrefabs = gridNodeCountX * gridNodeCountY;
        if (numeberOfNodesToSpawn > maxPossiblePrefabs)
        {
            numeberOfNodesToSpawn = maxPossiblePrefabs;
        }

        for (int i = 0; i < numeberOfNodesToSpawn; i++)
        {
            int randomX, randomY;
            Vector2Int randomPos;

            do
            {
                randomX = Random.Range(0, gridNodeCountX);
                randomY = Random.Range(0, gridNodeCountY);
                randomPos = new Vector2Int(randomX, randomY);
            } 
            
            while (spawnedObjects.ContainsKey(randomPos));


            Vector3 worldPos = new Vector3(randomX * (nodeWidth + spacingx), randomY * (nodeHeight + spacingy), 0);

            Vector3 spawnPos = gameObject.transform.position + worldPos;

            GameObject randomPrefab = objprefabs[Random.Range(0, objprefabs.Length)];
            GameObject prefabs = Instantiate(randomPrefab, spawnPos, Quaternion.identity);
            spawnedObjects.Add(randomPos, prefabs);
        }
    }

    void PlayerRoomSpawn()
    {
        for(int i = 0; i < numeberOfPlayerRoomNodesToSpawn; i++)
        {
            int randomX, randomY;
            Vector2Int randomPos;
            do
            {
                randomX = Random.Range(0, gridNodeCountX);
                randomY = Random.Range(0, gridNodeCountY);
                randomPos = new Vector2Int(randomX, randomY);
            }

            while (spawnedObjects.ContainsKey(randomPos));
            Vector3 worldPos = new Vector3(randomX * (nodeWidth + spacingx), randomY * (nodeHeight + spacingy), 0);
            Vector3 spawnPos = gameObject.transform.position + worldPos;
            GameObject playerRoom = Instantiate(playerRoomSpawn, spawnPos, Quaternion.identity);
            spawnedObjects.Add(randomPos, playerRoom);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < spawnedObjects.Count; i++)
            {
                KeyValuePair<Vector2Int, GameObject> pair = spawnedObjects.ElementAt(i);
                Destroy(pair.Value);
            }
            spawnedObjects.Clear();
            PlayerRoomSpawn();
            SpawnPrefabs();
        }
    }
}

