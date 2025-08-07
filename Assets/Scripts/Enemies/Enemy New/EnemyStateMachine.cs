using UnityEngine;
using System.Collections.Generic;


public class EnemyStateMachine : MonoBehaviour
{
    enum State
    {
        Idle,
        Alert,
        Chase,
        Attack
    }
    State currentState = State.Idle;
    
    public Transform player;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public float moveSpeed = 2f;

    private List<Vector2Int> path = new();
    private int pathIndex = 0;
    private Vector2Int currentPosition => Vector2Int.RoundToInt(transform.position);
    
    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                if (Vector2.Distance(transform.position, player.position) < detectionRange)
                {
                    currentState = State.Chase;
                }
                break;

            case State.Alert:
                Alert();
                break;

            case State.Chase:
                ChasePlayer();
                break;

            case State.Attack:
                AttackPlayer();
                break;
        }
    }

    void Alert()
    {
        
    }

    void ChasePlayer()
    {
        
    }
    
    void AttackPlayer()
    {
       
    }
}
