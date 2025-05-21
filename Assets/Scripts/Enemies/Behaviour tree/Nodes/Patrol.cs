using UnityEngine;

public class Patrol : Node
{
    EnemyAI enemyAI;
    Transform[] waypoints;
    int currentWaypoint = 0;

    public Patrol(EnemyAI enemyAI, Transform[] waypoints)
    {
        this.enemyAI = enemyAI;
        this.waypoints = waypoints;
    }

    public override States RunState()
    {
        if (Vector2.Distance(enemyAI.transform.position, waypoints[currentWaypoint].position) < 0.1f)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
        enemyAI.transform.position = Vector2.MoveTowards(enemyAI.transform.position, waypoints[currentWaypoint].position, enemyAI.speed * Time.deltaTime);
        return States.Running;
    }
}
