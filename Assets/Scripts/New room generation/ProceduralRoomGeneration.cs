using System.Collections.Generic;
using UnityEngine;

public static class ProceduralRoomGeneration 
{
    public static HashSet<Vector2Int> rooms(Vector2Int startPos, int length)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        Vector2Int currentPos = startPos;

        for (int i = 0; i < length; i++)
        {
            roomPositions.Add(currentPos);
            
            int direction = Random.Range(0, 4);
            switch (direction)
            {
                case 0: 
                    currentPos += Vector2Int.up;
                    break;
                case 1: 
                    currentPos += Vector2Int.down;
                    break;
                case 2: 
                    currentPos += Vector2Int.left;
                    break;
                case 3: 
                    currentPos += Vector2Int.right;
                    break;
            }
        }

        return roomPositions;
    }

    public static List<Vector2Int> corridors(Vector2Int startPos, int length)
    {
        List<Vector2Int> corridorPositions = new List<Vector2Int>();
        var direction = Direction.GetDirections();
        var currentPos = startPos;
        
        for (int i = 0; i < length; i++)
        {
            currentPos += direction;
            corridorPositions.Add(currentPos);
        }
        
        return corridorPositions;
    }
    
     public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while(roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if(room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if(Random.value < 0.5f)
                {
                    if(room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }else if(room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }else if(room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

public static class Direction
{
    public static List<Vector2Int> directionsList = new List<Vector2Int>()
    {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(1, 0)
    };

    public static Vector2Int GetDirections()
    {
        return directionsList[Random.Range(0, directionsList.Count)];
    }
}