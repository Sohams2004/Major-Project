using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rb;
    
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(x, y);
        rb.linearVelocity = movement * moveSpeed;

        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
    }

    void Sprint()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Sprinting");
            moveSpeed += 2;
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("Not sprinting");
            moveSpeed -= 2;
        }

    }

    private void FixedUpdate()
    {
        Movement();        
    }

    private void Update()
    {
        Sprint();
    }
}
