using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    public float alertRange;
    public float chaseRange;
    public float attackRange;
    public float speed;

    float pathRecalculationtimer = 0f;
    float pathRecalculationInterval = 0.5f;

    public Transform player;
    public Transform[] patrolPoints;

    Vector2 lastPlayerPosition;

    public List<A_Node> path = new List<A_Node>();

    Node root;

    Astar astar;

    private void Start()
    {
        astar = FindObjectOfType<Astar>();

        if (astar == null)
        {
            Debug.LogError("Astar not found");
            return;
        }

        root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new IsPlayerInRange(player, gameObject.transform, attackRange),
                new Attack(this)
            }),

            new Sequence(new List<Node>
            {
                new IsPlayerInRange(player, gameObject.transform, chaseRange),
                new FindPath(this, astar, this),
                new Chase(this)
            }),

            new Patrol(this, patrolPoints)
        });
    }

    public bool RecalculatePath()
    {
        pathRecalculationtimer += Time.deltaTime;

        if (pathRecalculationtimer >= pathRecalculationInterval)
        {
            float distanceMoved = Vector2.Distance(lastPlayerPosition, player.position);
            return distanceMoved > 2f;
        }

        return false;
    }

    private void Update()
    {
        lastPlayerPosition = player.position;
        root.RunState();
    }
}
