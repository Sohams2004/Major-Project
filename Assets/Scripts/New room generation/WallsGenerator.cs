using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class WallsGenerator
{
    public static void GenerateWalls(HashSet<Vector2Int> floorPositions, TileMapVisualizer tileMapVisualizer)
    {
        var wallPositions = FindWallDirections(floorPositions, new List<Vector2Int>
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        });

        foreach (var position in (IEnumerable<Vector2Int>)wallPositions)
        {
            tileMapVisualizer.PaintWall(position);
        }
    }
    
    private static object FindWallDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directions)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

        foreach (var position in floorPositions)
        {
            foreach (var direction in directions)
            {
                Vector2Int neighborPosition = position + direction;
                if (!floorPositions.Contains(neighborPosition))
                {
                    wallPositions.Add(neighborPosition);
                }
            }
        }

        return wallPositions;
    }
}
