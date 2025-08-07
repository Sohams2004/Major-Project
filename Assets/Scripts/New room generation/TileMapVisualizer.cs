using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField] Tilemap floorTileMap, wallTileMap;
    [SerializeField] TileBase floorTile, wallTile;
    
    public void GenerateFloor(IEnumerable<Vector2Int> positions)
    {
        PaintTiles(positions, floorTileMap, floorTile);
    }
    
    public void PaintTiles(IEnumerable<Vector2Int> positions,Tilemap tileMap ,TileBase tile)
    {
        foreach (var position in positions)
        {
            Vector3Int tilePosition = new Vector3Int(position.x, position.y, 0);
            floorTileMap.GetComponent<Tilemap>().SetTile(tilePosition, tile);
        }
    }
    
    public void PaintWall(Vector2Int position)
    {
        Vector3Int tilePosition = new Vector3Int(position.x, position.y, 0);
        floorTileMap.GetComponent<Tilemap>().SetTile(tilePosition, wallTile);
    }
    
    public void ClearTileMap()
    {
        floorTileMap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
    }
}
