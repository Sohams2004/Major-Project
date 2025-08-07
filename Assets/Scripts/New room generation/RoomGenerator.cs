using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomGenerator : RandomWalk
{
    [SerializeField] private int minRoomWidth;
    [SerializeField] private int minRoomHeight;
    [SerializeField] [Range(0,10)] private int offset;
    
    [SerializeField] int mapWidth;
    [SerializeField] int mapHeight;

    [SerializeField] private bool radomWalkRooms = false;

    protected override void RunGeneration()
    {
        CreateRooms();
    }
    
    private void CreateRooms()
    {
        var roomsList = ProceduralRoomGeneration.BinarySpacePartitioning(new BoundsInt((Vector3Int) startPos, new Vector3Int(mapWidth, mapHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (radomWalkRooms)
        {
            floor = CreateRoomsRandom(roomsList);
        }
        else
        {
            floor = GenerateRooms(roomsList);
        }
        

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        tileMapVisualizer.GenerateFloor(floor);
        WallsGenerator.GenerateWalls(floor, tileMapVisualizer);
    }
    
    
    private HashSet<Vector2Int> CreateRoomsRandom(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkScriptableObject, roomCenter);
            foreach (var position in roomFloor)
            {
                if(position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) && position.y >= (roomBounds.yMin - offset) && position.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
    
    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenter)
    {
        if (roomCenter.Count == 0)
        {
            Debug.Log("No room centers to connect");
            return new HashSet<Vector2Int>();
        }
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenter[Random.Range(0, roomCenter.Count)];
        roomCenter.Remove(currentRoomCenter);

        while (roomCenter.Count > 0)
        {
            Vector2Int closestRoom = FindClosestRoom(currentRoomCenter, roomCenter);
            roomCenter.Remove(closestRoom);
            HashSet<Vector2Int> newCorridor = CreateNewCorridor(currentRoomCenter, closestRoom);
            currentRoomCenter = closestRoom;
            corridors.UnionWith(newCorridor);
        }
        
        return corridors;
    }

    private HashSet<Vector2Int> CreateNewCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);

        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            
            corridor.Add(position);
        }
        
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            
            corridor.Add(position);
        }
        
        return corridor;
    }
    
    HashSet<Vector2Int> IncreaseCorridorSize(List<Vector2Int> corridor)
    {
        HashSet<Vector2Int> newCorridor = new HashSet<Vector2Int>();
        foreach (var position in corridor)
        {
            newCorridor.Add(position);
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (Mathf.Abs(x) + Mathf.Abs(y) == 1) // Only add adjacent tiles
                    {
                        newCorridor.Add(position + new Vector2Int(x, y));
                    }
                }
            }
        }
        return newCorridor;
    }

    private Vector2Int FindClosestRoom(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float length = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float distance = Vector2.Distance(position, currentRoomCenter);
            if (distance < length)
            {
                length = distance;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> GenerateRooms(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomList)
        {
            for (int x = offset; x < room.size.x - offset; x++)
            {
                for (int y = offset; y < room.size.y - offset; y++)
                {
                    Vector2Int pos = (Vector2Int) room.min + new Vector2Int(x, y);
                    floor.Add(pos);
                }
            }
        }
        
        return floor;
    }
}
