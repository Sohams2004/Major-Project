using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public int width = 10, height = 10;
    public bool[,] walkable;

    void Awake()
    {
        Instance = this;
        walkable = new bool[width, height];
        for (int x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
            walkable[x, y] = true; 
    }

    public List<Vector2Int> GetNeighbors(Vector2Int node)
    {
        List<Vector2Int> neighbors = new();
        Vector2Int[] dirs = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        foreach (var dir in dirs)
        {
            Vector2Int newPos = node + dir;
            if (newPos.x >= 0 && newPos.x < width &&
                newPos.y >= 0 && newPos.y < height &&
                walkable[newPos.x, newPos.y])
                neighbors.Add(newPos);
        }
        return neighbors;
    }
    
    void OnDrawGizmos()
    {
        if (walkable == null) return;

        for (int x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
        {
            Gizmos.color = walkable[x, y] ? Color.white : Color.red;
            Gizmos.DrawWireCube(new Vector3(x, y, 0), Vector3.one);
        }
    }

}