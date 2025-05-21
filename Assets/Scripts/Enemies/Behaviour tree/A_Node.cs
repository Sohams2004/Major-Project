using System;
using UnityEngine;

public class A_Node : IComparable
{
    public A_Node parent;

    public GameObject nodeGameObject { get; private set; }


    public bool isVisited = false;
    public int versionNumber;

    bool isWalkable;
    int gCost, hCost, Fcost;

    public Vector2Int gridPos { get; private set; }
    public Vector2 worldPos { get; private set; }   

    public bool IsWalkable
    {
        get => isWalkable;

        set => isWalkable = value;
    }

    public int GCost
    {
        get => gCost;
        set => gCost = value;
    }

    public int HCost
    {
        get => hCost;
        set => hCost = value;
    }

    public int FCost
    {
        get => Fcost;
        set => Fcost = value;
    }

    public A_Node(Vector2Int gridPos, Vector2 worldPos, bool isWalkable, GameObject nodeGameObject)
    {
        this.gridPos = gridPos;
        this.worldPos = worldPos;
        this.isWalkable = isWalkable;
        this.nodeGameObject = nodeGameObject;
    }

    public int CompareTo(object obj)
    {
        A_Node node = (A_Node)obj;
        if (FCost < node.FCost)
        {
            return -1;
        }
        else if (FCost > node.FCost)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
