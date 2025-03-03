using UnityEngine;
using System.Collections.Generic;


public class Corridors : MonoBehaviour
{
    [SerializeField] public List<GameObject> nodes = new List<GameObject>();

    [SerializeField] GameObject corridorPrefab; // Prefab for corridor tiles

    private void Start()
    {
        ConnectRoomsWithCorridors();
    }
    void ConnectRoomsWithCorridors()
    {
        // Connect each room to the next one in the list
        for (int i = 0; i < nodes.Count - 1; i++)
        {
            GameObject startRoom = nodes[i];
            GameObject endRoom = nodes[i + 1];
            CreateCorridor(startRoom.transform.position, endRoom.transform.position);
        }
    }

    void CreateCorridor(Vector3 start, Vector3 end)
    {
        Vector3 currentPos = start;

        // Move horizontally first, then vertically
        while (currentPos.x != end.x)
        {
            // Move towards the target X position
            currentPos.x += (end.x > currentPos.x) ? 1 : -1;

            // Spawn corridor tile
            SpawnCorridorTile(currentPos);
        }

        while (currentPos.y != end.y)
        {
            // Move towards the target Y position
            currentPos.y += (end.y > currentPos.y) ? 1 : -1;

            // Spawn corridor tile
            SpawnCorridorTile(currentPos);
        }
    }

    void SpawnCorridorTile(Vector3 position)
    {
        // Check if a corridor tile already exists at this position
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);
        bool isOccupied = false;

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Corridor") || collider.CompareTag("Room"))
            {
                isOccupied = true;
                break;
            }
        }

        if (!isOccupied)
        {
            Instantiate(corridorPrefab, position, Quaternion.identity, transform);
        }
    }
}
