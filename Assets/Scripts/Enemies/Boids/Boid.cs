using UnityEngine;
using System.Collections;

public class Boid : MonoBehaviour
{
    public Vector2 velocity;
    public float maxVelocity;

    private void Update()
    {
        if(velocity.magnitude > maxVelocity)
        {
            velocity = velocity.normalized * maxVelocity;
        }

        transform.position = transform.position + new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime;
        transform.rotation = Quaternion.FromToRotation(Vector2.up, velocity);
    }
}
