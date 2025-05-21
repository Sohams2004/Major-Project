using UnityEngine;

public class Attack : Node
{
    EnemyAI enemyAI;

    float attackCoolDown = 1f;

    float attackTimer = 0f; 

    public Attack(EnemyAI enemyAI)
    {
        this.enemyAI = enemyAI;
    }

    public override States RunState()
    {
        if (Time.time - attackTimer > attackCoolDown)
        {
            attackTimer = Time.time;
            return States.Success;
        }

        return States.Running;

    }
}

