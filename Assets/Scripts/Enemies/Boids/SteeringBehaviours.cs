using UnityEngine;

public class SteeringBehaviours : MonoBehaviour
{
    [SerializeField] float seekVelocity;
    [SerializeField] float fleeVelocity;

    [SerializeField] bool isSeeking;
    [SerializeField] bool isFleeing;

    [SerializeField] float detectionRadius;

    [SerializeField] GameObject player;

    [SerializeField] Rigidbody2D playerRb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    void Seek()
    {
        if (isSeeking)
        {
            Vector2 velocity = (player.transform.position - transform.position).normalized * seekVelocity;
            Vector2 steeringForce = velocity - playerRb.linearVelocity;
            playerRb.linearVelocity = steeringForce;
        }

        else
        {
            playerRb.linearVelocity = Vector2.zero;
        }
    }

    void Flee()
    {
        if (isFleeing)
        {
            Vector2 velocity = (transform.position - player.transform.position).normalized * fleeVelocity;
            Vector2 steeringForce = velocity - playerRb.linearVelocity;
            playerRb.linearVelocity = steeringForce;
        }
        else
        {
            playerRb.linearVelocity = Vector2.zero;
        }
    }

    private void Update()
    {
        Seek();
        Flee();
    }
}
