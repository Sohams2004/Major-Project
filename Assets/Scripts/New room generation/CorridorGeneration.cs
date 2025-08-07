using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class CorridorGeneration : RandomWalk
{
    [SerializeField] private int corridorLength;
    [SerializeField] private int corridorCount;
    [SerializeField] [Range(0.1f, 1f)] private float roomPercentage = 0.5f;
    
    protected override void RunGeneration()
    {
        CorridorGenerate();
    }

    void CorridorGenerate()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> possibleRoomPositions = new HashSet<Vector2Int>();
        
        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, possibleRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateNewRooms(possibleRoomPositions);

        List<Vector2Int> deadEnds = LocateDeadEnds(floorPositions);
        CreateDeadEndRooms(deadEnds, roomPositions);
        
        floorPositions.UnionWith(roomPositions);

        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = IncreaseCorridorWidth(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
        }
        
        tileMapVisualizer.GenerateFloor(floorPositions);
        WallsGenerator.GenerateWalls(floorPositions, tileMapVisualizer);
    }

    List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> possibleRoomPositions)
    {
        var currentPos = startPos;
        possibleRoomPositions.Add(currentPos);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();
        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralRoomGeneration.corridors(currentPos, corridorLength);
            corridors.Add(corridor);
            currentPos = corridor[corridor.Count - 1];
            possibleRoomPositions.Add(currentPos);
            floorPositions.UnionWith(corridor);
        }
        
        return corridors;
    }
    
    HashSet<Vector2Int> CreateNewRooms(HashSet<Vector2Int> possibleRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomCount = Mathf.RoundToInt(possibleRoomPositions.Count * roomPercentage);

        List<Vector2Int> roomsToGenerate = possibleRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomCount).ToList();

        foreach (var roomPos in roomsToGenerate)
        {
            var roomFloor = RunRandomWalk(randomWalkScriptableObject, roomPos);
            roomPositions.UnionWith(roomFloor);
        }
        
        return roomPositions;
    }

    List<Vector2Int> LocateDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var floorPos in floorPositions)
        {
            int neighborCount = 0;
            foreach (var direction in Direction.directionsList)
            {
                Vector2Int neighborPos = floorPos + direction;
                if (floorPositions.Contains(neighborPos))
                {
                    neighborCount++;
                }
            }
            
            if (neighborCount == 1) 
            {
                deadEnds.Add(floorPos);
            }
        }
        
        return deadEnds;
    }

    void CreateDeadEndRooms(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPositions)
    {
        foreach (var roomPos in deadEnds)
        {
            if (roomPositions.Contains(roomPos) == false)
            {
                var roomFloor = RunRandomWalk(randomWalkScriptableObject, roomPos);
                roomPositions.UnionWith(roomFloor);
            }
        }
    }
    
    List<Vector2Int> IncreaseCorridorWidth(List<Vector2Int> corridor)
    {
        List<Vector2Int> widenedCorridor = new List<Vector2Int>();
        foreach (var position in corridor)
        {
            widenedCorridor.Add(position);
            foreach (var direction in Direction.directionsList)
            {
                Vector2Int offsetPosition = position + direction;
                if (!widenedCorridor.Contains(offsetPosition))
                {
                    widenedCorridor.Add(offsetPosition);
                }
            }
        }
        return widenedCorridor;
    }
}

