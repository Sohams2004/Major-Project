using UnityEngine;

public class Chase : Node
{
    EnemyAI enemyAI;

    public Chase(EnemyAI enemyAI)
    {
        this.enemyAI = enemyAI;
    }

    public override States RunState()
    {
        if (enemyAI.path == null || enemyAI.path.Count == 0)
        {
            return States.Failure;
        }

        A_Node targetNode = enemyAI.path[0];
        Vector2 target = targetNode.worldPos;

        enemyAI.transform.position = Vector2.MoveTowards(enemyAI.transform.position, target, enemyAI.speed * Time.deltaTime);

        if (Vector2.Distance(enemyAI.transform.position, target) < 0.1f)
        {
            enemyAI.path.RemoveAt(0);
        }

        return States.Success;
    }
}
