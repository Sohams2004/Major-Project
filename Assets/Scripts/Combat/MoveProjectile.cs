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

    public void MoveObject()
    {
        projectileRb.linearVelocity = transform.up * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Projectile hit an enemy");
            Destroy(other.gameObject); 
            Destroy(gameObject); 
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Projectile hit a wall");
            Destroy(gameObject); 
        }
    }

    private void Update()
    {
        MoveObject();
    }
}
