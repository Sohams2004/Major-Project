using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class AbstractGenerator : MonoBehaviour
{
    [SerializeField] protected TileMapVisualizer tileMapVisualizer;
    [SerializeField] protected Vector2Int startPos = Vector2Int.zero;

    public void Generate()
    {
        tileMapVisualizer.ClearTileMap();
        RunGeneration();
    }

    protected abstract void RunGeneration();

}
