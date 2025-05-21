using UnityEngine;

public class IsPlayerInRange : Node
{
    Transform player;
    Transform enemy;

    float range;

    public IsPlayerInRange(Transform player, Transform enemy, float range)
    {
        this.player = player;
        this.enemy = enemy;
        this.range = range;
    }

    public override States RunState()
    {
        float distance = Vector2.Distance(player.position, enemy.position);
        Debug.Log("Distance: " + distance);
        return distance < range ? States.Success : States.Failure;
    }
}
