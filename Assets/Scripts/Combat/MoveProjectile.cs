using System;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    private Rigidbody2D projectileRb;
    public float speed = 10f;
    
    int wallLayer;
    int enemyLayer;

    private void Start()
    {
        projectileRb = GetComponent<Rigidbody2D>();
        
        wallLayer = LayerMask.NameToLayer("Wall");
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    public void MoveObject()
    {
        projectileRb.linearVelocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.isTrigger)
            return; 
        
        if (other.gameObject.layer == enemyLayer)
        {
            Debug.Log("Projectile hit an enemy");
            Destroy(other.gameObject);
            Destroy(gameObject); 
        }
        else if (other.gameObject.layer == wallLayer)
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
