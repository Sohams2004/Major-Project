using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Custom Tile", menuName = "Tiles/Custom Tile")]
public class CustomTile : Tile
{
    public Tile.ColliderType colliderType;
}
