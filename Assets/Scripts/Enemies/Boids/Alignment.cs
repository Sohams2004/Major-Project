using UnityEngine;

public class Alignment : MonoBehaviour
{
    public float radius;
    public float neighbors;

    Boid boid;

    private void Start()
    {
        boid = GetComponent<Boid>();
    }

    void Align()
    {
        var boids = FindObjectsOfType<Boid>();
        var average = Vector2.zero;
        neighbors = 0;

        for(int i = 0; i < boids.Length; i++)
        {
            Vector2 difference = boid.transform.position - gameObject.transform.position;

            if (difference.magnitude < radius)
            {
                average += difference;
                neighbors++;
            }
        }

        if (neighbors > 0)
        {
            average = average / neighbors;
            boid.velocity += Vector2.Lerp(boid.velocity, average, Time.deltaTime);
        }
    }

    private void Update()
    {
        Align();
    }
}

