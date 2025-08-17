using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rb;

    [SerializeField] private float facingDirection;
    
    [SerializeField] Animator animator;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(x, y);
        rb.linearVelocity = movement * moveSpeed;
        
        if(x > 0)
        {
            facingDirection = 1f;
        }
        else if(x < 0)
        {
            facingDirection = -1f; 
        }
        else
        {
            facingDirection = 0f; 
        }
        
        if(y > 0)
        {
            animator.SetBool("Walk up", true);
            animator.SetBool("Walk down", false);
        }
        else if(y < 0)
        {
            animator.SetBool("Walk down", true);
            animator.SetBool("Walk up", false);
        }
        else
        {
            animator.SetBool("Walk up", false);
            animator.SetBool("Walk down", false);
        }

        if (facingDirection == 1)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk left", false);
            animator.SetBool("Walk right", true);
        }
        else if (facingDirection == 0)
        {
            animator.SetBool("Walk right", false);
            animator.SetBool("Walk left", false);
            animator.SetBool("Idle", true);
        }
        
        if (facingDirection == -1)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk right", false);
            animator.SetBool("Walk left", true);
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
