using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class FindPath : Node
{
    EnemyAI enemyAI;
    Astar aStar;
    MonoBehaviour monoBehaviour;

    public FindPath(EnemyAI enemyAI, Astar aStar, MonoBehaviour monoBehaviour)
    {
        this.enemyAI = enemyAI;
        this.aStar = aStar;
        this.monoBehaviour = monoBehaviour;
    }

    public override States RunState()
    {
        if (enemyAI.path == null || enemyAI.path.Count == 0 || enemyAI.RecalculatePath())
        {
            GameObject player = GameObject.FindWithTag("Player");

            if (player == null)
            {
                return States.Failure;
            }

            Vector2 playerPosition = player.transform.position;
            Vector2 enemyPosition = enemyAI.transform.position;
            Vector2Int playerPos = new Vector2Int((int)playerPosition.x, (int)playerPosition.y);
            Vector2Int enemyPos = new Vector2Int((int)enemyPosition.x, (int)enemyPosition.y);

            monoBehaviour.StartCoroutine(Find_Path(enemyPos, playerPos));
            return States.Running;
        }

        return States.Success;
    }

    IEnumerator Find_Path(Vector2Int startPos, Vector2Int goalPos)
    {
        List<A_Node> path = aStar.GetPath(startPos, goalPos);

        if (path != null && path.Count > 0)
        {
            enemyAI.path = path;
            yield return null;
        }

        else
        {
            enemyAI.path = new List<A_Node>();
            yield return null;
        }
    }
}
