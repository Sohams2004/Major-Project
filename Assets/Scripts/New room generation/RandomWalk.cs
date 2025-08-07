using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomWalk : AbstractGenerator
{
    [SerializeField] protected RandomWalkScriptableObject randomWalkScriptableObject;

    protected override void RunGeneration()
    {
        HashSet<Vector2Int> walkPositions = RunRandomWalk(randomWalkScriptableObject, startPos);
        tileMapVisualizer.ClearTileMap();
        tileMapVisualizer.GenerateFloor(walkPositions);
        WallsGenerator.GenerateWalls(walkPositions, tileMapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(RandomWalkScriptableObject parameters, Vector2Int position)
    {
        var currentPos = position;
        HashSet<Vector2Int> walkPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProceduralRoomGeneration.rooms(currentPos, parameters.walkLength);
            walkPositions.UnionWith(path);

            if (parameters.startIterations)
            {
                currentPos = walkPositions.ElementAt(Random.Range(0, walkPositions.Count));
            }
        }
        return walkPositions;
    }
}
