using System;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    private Rigidbody2D projectileRb;
    public float speed = 10f;

    private void Start()
    {
        projectileRb = GetComponent<Rigidbody2D>();
    }

    public void MoveObject(Vector2 shootDirection)
    {
        Vector2 direction = shootDirection.normalized;
        projectileRb.linearVelocity = direction * speed;
    }
}
