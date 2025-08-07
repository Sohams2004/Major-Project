using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue : MonoBehaviour
{
    private List<(Vector2Int pos, int priority)> elements = new();
    public int Count => elements.Count;

    public void Enqueue(Vector2Int item, int priority)
    {
        elements.Add((item, priority));
    }

    public Vector2Int Dequeue()
    {
        int bestIndex = 0;
        for (int i = 1; i < elements.Count; i++)
            if (elements[i].priority < elements[bestIndex].priority)
                bestIndex = i;

        var bestItem = elements[bestIndex];
        elements.RemoveAt(bestIndex);
        return bestItem.pos;
    }
}