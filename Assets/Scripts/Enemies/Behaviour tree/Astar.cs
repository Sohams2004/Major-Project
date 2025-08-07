using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class Astar : MonoBehaviour
{
    static int globalVersionNumber = 0;

    [SerializeField] Vector2Int startNodeGridPos;
    [SerializeField] Vector2Int goalNodeGridPos;

    List<A_Node> openList = new List<A_Node>();
    List<A_Node> closedList = new List<A_Node>();
    List<A_Node> finalPath = new List<A_Node>();
    List<A_Node> neighbours = new List<A_Node>();

    A_Node startNode;
    A_Node goalNode;

    Grid grid;

    void Start()
    {
        grid = FindObjectOfType<Grid>();
        print(GetPath(new Vector2Int(1,5), new Vector2Int(5,1)));
    }

    public List<A_Node> GetPath(Vector2Int startPos, Vector2Int goalPos)
    {
        if (startPos == null || goalPos == null)
        {
            Debug.LogError("Start and Goal are null");
            return new List<A_Node>();
        }

        startNode = grid.GetNode(startPos);
        goalNode = grid.GetNode(goalPos);

        if (startNode == null || goalNode == null)
        {
            Debug.LogError("Start or Goal node is null");
            return new List<A_Node>();
        }

        finalPath.Clear();
        openList.Clear();

        globalVersionNumber++;

        A_Node currentNode = startNode;
        openList.Add(currentNode);

        while (openList.Count > 0)
        {
            openList.Sort();
            currentNode = openList[0];

            if(currentNode.versionNumber < globalVersionNumber)
            {
                currentNode.GCost = 0;
                currentNode.HCost = 0;
                currentNode.versionNumber = globalVersionNumber;
                currentNode.isVisited = false;
                currentNode.parent = null;
            }

            openList.Remove(currentNode);
            currentNode.isVisited = true;

            if(currentNode == goalNode)
            {
                FindPath(currentNode);
                finalPath.Reverse();
                return finalPath;
            }

            neighbours.Clear();

            Vector2Int topNode = currentNode.gridPos + Vector2Int.up;
            if(topNode.y < grid.gridSizeY)
            {
                A_Node topNodeNode = grid.GetNode(topNode);
                neighbours.Add(grid.GetNode(topNode));
            }

            Vector2Int bottomNode = currentNode.gridPos + Vector2Int.down;
            if (bottomNode.y > 0)
            {
                A_Node bottomNodeNode = grid.GetNode(bottomNode);
                neighbours.Add(grid.GetNode(bottomNode));
            }

            Vector2Int rightNode = currentNode.gridPos + Vector2Int.right;
            if (rightNode.x < grid.gridSizeX)
            {
                A_Node rightNodeNode = grid.GetNode(rightNode);
                neighbours.Add(grid.GetNode(rightNode));
            }

            Vector2Int leftNode = currentNode.gridPos + Vector2Int.left;
            if (leftNode.x > 0)
            {
                A_Node leftNodeNode = grid.GetNode(leftNode);
                neighbours.Add(grid.GetNode(leftNode));
            }

            for (int i = 0; i < neighbours.Count; i++)
            {
                if (neighbours[i].versionNumber < globalVersionNumber)
                {
                    neighbours[i].GCost = 0;
                    neighbours[i].HCost = 0;
                    neighbours[i].versionNumber = globalVersionNumber;
                    neighbours[i].isVisited = false;
                    neighbours[i].parent = null;
                }

                if (neighbours[i].isVisited)
                {
                    continue;
                }

                int newDistance = CalculateDistance(neighbours[i].gridPos, currentNode.gridPos) + currentNode.GCost;

                if(newDistance < neighbours[i].GCost || !openList.Contains(neighbours[i]))
                {
                    neighbours[i].GCost = newDistance;
                    neighbours[i].HCost = CalculateDistance(neighbours[i].gridPos, goalNode.gridPos);
                    neighbours[i].parent = currentNode;
                    if (!openList.Contains(neighbours[i]))
                    {
                        openList.Add(neighbours[i]);
                    }
                }
            }
        }

        if(finalPath.Count == 0)
        {
            Debug.LogError("No path found");
            return new List<A_Node>();
        }

        return finalPath;
    }

    void FindPath(A_Node node)
    {
        if (node.parent != null)
        {
            finalPath.Add(node);
            FindPath(node.parent);
        }

        else
        {
            finalPath.Add(node);
        }
    }

    int CalculateDistance(Vector2Int VectorA, Vector2Int VectorB)
    {
        var x = VectorA.x - VectorB.x;
        var y = VectorA.y - VectorB.y;

        return Mathf.Abs(x + y);
    }
}
