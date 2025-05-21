using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] GameObject nodePrefab;

    [SerializeField] public int gridSizeX, gridSizeY;
    [SerializeField] int gridArraySize;

    [SerializeField] int nodeWidth, nodeHeight;

    [SerializeField] bool isWalkable;

    [SerializeField] A_Node[] nodes;

    private void Start()
    {
        gridArraySize = gridSizeX * gridSizeY;

        nodes = new A_Node[gridArraySize];

        Vector2 gameObjPos = transform.position;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                int a = x + y * gridSizeX;

                Vector2Int gridPos = new Vector2Int(x, y);
                Vector2 worldPos = new Vector2(x * nodeWidth, y * nodeHeight);
                Vector2 spawnPos = gameObjPos + worldPos;

                GameObject go = null;

                go = Instantiate(nodePrefab, spawnPos, Quaternion.identity);
                go.transform.localScale = new Vector3(nodeWidth, nodeHeight, 1);
                go.transform.parent = transform;

                isWalkable = !Physics2D.OverlapCircle(worldPos, 0.1f);

                nodes[a] = new A_Node(gridPos, worldPos, isWalkable, go);
            }
        }
    }

    public A_Node GetNode(Vector2Int gridPos)
    {
        if (gridPos.x < 0 || gridPos.x >= gridSizeX || gridPos.y < 0 || gridPos.y >= gridSizeY)
        {
            Debug.LogError($"Grid position {gridPos} is out of bounds!");
            return null;
        }

        gridPos.x = Mathf.Clamp(gridPos.x, 0, gridSizeX - 1);
        gridPos.y = Mathf.Clamp(gridPos.y, 0, gridSizeY - 1);

        int index;
        index = gridPos.x + gridPos.y * gridSizeX;

        if (index >= 0 && index < nodes.Length)
        {
            return nodes[index];
        }
        else
        {
            Debug.LogError($"Index {index} is out of range!");
            return null;
        }
    }

    public Vector2 WorldPosition(Vector2Int gridPos)
    {
        Vector2 gameObjectPos = transform.position;
        return new Vector2(gridPos.x * nodeWidth, gridPos.y * nodeHeight);
    }
}
