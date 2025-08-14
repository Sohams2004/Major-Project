using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float enemyHealth = 10f;
    public float enemySpeed = 2f;
    public float rotateSpeed;

    private Rigidbody2D enemyRb;

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        enemyRb.MovePosition(enemyRb.position + direction * enemySpeed * Time.deltaTime);
    }

    void RotateTowardsPlayer()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * 100 * Time.deltaTime);
    }
    
    private void Update()
    {
        if (target == null)
        {
            GetTarget();
        }
        else
        {
            RotateTowardsPlayer();
            MoveTowardsPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy collided with Player");
            Destroy(other.gameObject);
            Time.timeScale = 0;
        }
    }
}
