using System.Collections.Generic;
using UnityEngine;

public static class SimpleAStar
{
    public static List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
    {
        HashSet<Vector2Int> closedSet = new();
        PriorityQueue openSet = new();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new();

        openSet.Enqueue(start, 0);
        Dictionary<Vector2Int, int> gScore = new() { [start] = 0 };

        while (openSet.Count > 0)
        {
            Vector2Int current = openSet.Dequeue();
            if (current == goal)
                return ReconstructPath(cameFrom, current);

            closedSet.Add(current);
            foreach (var neighbor in GridManager.Instance.GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor)) continue;
                int tentativeG = gScore[current] + 1;
                if (!gScore.ContainsKey(neighbor) || tentativeG < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeG;
                    int f = tentativeG + Heuristic(neighbor, goal);
                    openSet.Enqueue(neighbor, f);
                }
            }
        }
        return new(); // no path
    }

    static List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current)
    {
        List<Vector2Int> path = new() { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Insert(0, current);
        }
        return path;
    }

    static int Heuristic(Vector2Int a, Vector2Int b) => Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y); // Manhattan
}

