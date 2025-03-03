using UnityEngine;

public class RoomObject 
{
    public Vector2 position;
    public Vector2 direction;

    public float change;

    public RoomObject(Vector2 position, Vector2 direction, float change)
    {
        this.position = position;
        this.direction = direction;
        this.change = change;
    }
}
