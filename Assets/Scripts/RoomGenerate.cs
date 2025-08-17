using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using System;


public class RoomGenerate : MonoBehaviour
{
    public enum GridType
    {
        Floor,
        Wall,
        Empty
    }

    public GridType[,] grid;

    public List<RoomObject> rooms;

    public Vector3Int spawnLocation;

    public Tilemap tilemap;

    public CustomTile wallTile;

    public CustomTile floorTile;

    public CustomTile emptyTile;

    public GameObject player;

    public int width, height;

    public int maxRooms;

    public int tileCount;

    public int iterations;
    int maxIterations = 100000;

    public float fillPercent;
    public float waitTime;
    public float loadingPercent;
    
    public event Action OnLevelGenerated;
    public List<Vector3Int> floorTiles = new List<Vector3Int>();
    
    [SerializeField] EnemySpawn enemySpawn;
    [SerializeField] Loot loot;

    private void Start()
    {
        InitializeRoomGrid();
        print(TargetTileCount());

        fillPercent = Mathf.Clamp(fillPercent, 0.1f, 0.9f);
        
        enemySpawn = FindObjectOfType<EnemySpawn>();
        loot = FindObjectOfType<Loot>();
    }

    Vector2 GetDirection()
    {
        int random = Random.Range(0, 4);
        switch (random)
        {
            case 0:
                return Vector2.up;
            case 1:
                return Vector2.down;
            case 2:
                return Vector2.left;
            case 3:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }

    Vector3Int GetSpawnLocation()
    {
        return new Vector3Int(grid.GetLength(0) / 2, grid.GetLength(1) / 2, 0);
    }

    int TargetTileCount()
    {
        return (int)(grid.Length * fillPercent);
    }  

    void InitializeRoomGrid()
    {
        if (width <= 0 || height <= 0)
        {
            return;
        }

        grid = new GridType[width, height];

        rooms = new List<RoomObject>();

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                grid[x, y] = GridType.Empty;
            }
        }

        if (spawnLocation.x < 0 || spawnLocation.x >= width || spawnLocation.y < 0 || spawnLocation.y >= height)
        {
            return;
        }

        Vector3Int tile = spawnLocation;

        RoomObject currentRoom = new RoomObject(new Vector2(tile.x, tile.y), GetDirection(), 0.5f);
        grid[tile.x, tile.y] = GridType.Floor;
        tilemap.SetTile(tile, floorTile);
        rooms.Add(currentRoom);
        tileCount++;

        StartCoroutine(GenerateFloor());

    }

    IEnumerator GenerateFloor()
    {
        while ((float)tileCount / (float)grid.Length < fillPercent && iterations < maxIterations)
        {
            iterations++;
            bool isRoomPlaced = false;
            foreach (RoomObject room in rooms)
            {
                Vector3Int currentPosition = new Vector3Int((int)room.position.x, (int)room.position.y, 0);

                if (currentPosition.x < 0 || currentPosition.x >= width || currentPosition.y < 0 || currentPosition.y >= height)
                {
                    continue;
                }

                if (grid[currentPosition.x, currentPosition.y] != GridType.Floor)
                {
                    tilemap.SetTile(currentPosition, floorTile);
                    tileCount++;
                    grid[currentPosition.x, currentPosition.y] = GridType.Floor;
                    isRoomPlaced = true;
                }
            }

            if(isRoomPlaced)
            {
                yield return new WaitForSeconds(waitTime);
            }

            ToRemove();
            ToRedirect();
            ToCreate();
            UpdatePosition();

            StartCoroutine(GenerateWalls());

            AddTilesToEmptyArea();
        }

        yield return new WaitForSeconds(1);
        LoadPlayer();
    }

    void ToRemove()
    {
        int updatedCount = rooms.Count;

        for (int i = 0; i < updatedCount; i++)
        {
            if(Random.value < rooms[i].change && rooms.Count > 1)
            {
                rooms.RemoveAt(i);
                break;
            }
        }
    }

    void ToRedirect()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (Random.value < rooms[i].change)
            {
                RoomObject currentRoom = rooms[i];
                rooms[i].direction = GetDirection();
                rooms[i] = currentRoom;
            }
        }
    }

    void ToCreate()
    {
        int updatedCount = rooms.Count;
        for (int i = 0; i < updatedCount; i++)
        {
            if(Random.value < rooms[i].change && rooms.Count < maxRooms)
            {
                Vector2 newDirection = GetDirection();
                Vector2 newPosition = rooms[i].position;

                RoomObject newRoom = new RoomObject(newPosition, newDirection, 0.5f);
                rooms.Add(newRoom);
            }
        }
    }

    void UpdatePosition()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            RoomObject currentRoom = rooms[i];
            currentRoom.position += currentRoom.direction;
            currentRoom.position.x = Mathf.Clamp(currentRoom.position.x, 1, grid.GetLength(0) - 2);
            currentRoom.position.y = Mathf.Clamp(currentRoom.position.y, 1, grid.GetLength(1) - 2);
            rooms[i] = currentRoom;
        }
    }

    IEnumerator GenerateWalls()
    {
        for (int i = 0; i < grid.GetLength(0) - 1; i++)
        {
            for(int j = 0; j < grid.GetLength(1) - 1; j++)
            {
                if (grid[i, j] == GridType.Floor)
                {
                    bool isWall = false;
                    
                    if(grid[i + 1, j] == GridType.Empty)
                    {
                        Vector3Int tile = new Vector3Int(i + 1, j, 0);
                        tilemap.SetTile(tile, wallTile);
                        grid[i + 1, j] = GridType.Wall;
                        isWall = true;
                    }

                    if (grid[i - 1, j] == GridType.Empty)
                    {
                        Vector3Int tile = new Vector3Int(i - 1, j, 0);
                        tilemap.SetTile(tile, wallTile);
                        grid[i - 1, j] = GridType.Wall;
                        isWall = true;
                    }

                    if (grid[i, j + 1] == GridType.Empty)
                    {
                        Vector3Int tile = new Vector3Int(i, j + 1, 0);
                        tilemap.SetTile(tile, wallTile);
                        grid[i, j + 1] = GridType.Wall;
                        isWall = true;
                    }

                    if (grid[i, j - 1] == GridType.Empty)
                    {
                        Vector3Int tile = new Vector3Int(i, j - 1, 0);
                        tilemap.SetTile(tile, wallTile);
                        grid[i, j - 1] = GridType.Wall;
                        isWall = true;
                    }

                    if (isWall)
                    {
                        yield return new WaitForSeconds(waitTime);
                    }
                }
            }
        }

        AreAllTilesSpawned();
    }

    void AddTilesToEmptyArea()
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] == GridType.Empty)
                {
                    Vector3Int tile = new Vector3Int(i, j, 0);
                    tilemap.SetTile(tile, emptyTile);
                }
            }
        }
    }

    void SpawnPlayer()
    {
        List<Vector3Int> floorTiles = new List<Vector3Int>();

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] == GridType.Floor)
                {
                    floorTiles.Add(new Vector3Int(i, j, 0));
                }
            }
        }

        if(floorTiles.Count > 0)
        {
            Vector3Int randomTile = floorTiles[Random.Range(0, floorTiles.Count)];
            GameObject playerInstance = Instantiate(player, tilemap.CellToWorld(randomTile) + tilemap.tileAnchor, Quaternion.identity);
        }
    }

    bool AreAllTilesSpawned()
    {
        if(tileCount > TargetTileCount())
        {
            Debug.Log("All tiles spawned");
            return true;
        }
        return false;
    }

    void LoadPlayer()
    {
        if (AreAllTilesSpawned())
        {
            AddFloorTilesInAList();
            //Debug.Log("All tiles spawned");
            SpawnPlayer();
            
            OnLevelGenerated?.Invoke();
            enemySpawn.StartSpawingEnemy();
            loot.StartSpawnLoot();
        }
    }

    void LoadingPercentage()
    {
        loadingPercent = ((float)tileCount / (float)TargetTileCount()) * 100;
        Mathf.Round(loadingPercent);
    }

    void AddFloorTilesInAList()
    {
        floorTiles.Clear();
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] == GridType.Floor)
                {
                    floorTiles.Add(new Vector3Int(i, j, 0));
                }
            }
        }
    }

    public IReadOnlyList<Vector3Int> GetFloorTiles() => floorTiles;
    
    
    private void Update()
    {
        LoadingPercentage();
    }
}
