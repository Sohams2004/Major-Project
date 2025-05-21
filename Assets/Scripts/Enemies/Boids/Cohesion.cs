using UnityEngine;

public class Cohesion : MonoBehaviour
{
    public float radius;
    public float neighbors;

    Boid boid;

    private void Start()
    {
        boid = GetComponent<Boid>();
    }

    void Cohere()
    {
        var boids = FindObjectsOfType<Boid>();
        var average = Vector2.zero;
        neighbors = 0;

        for (int i = 0; i < boids.Length; i++)
        {
            Vector2 difference = boids[i].transform.position - gameObject.transform.position;

            if (difference.magnitude < radius)
            {
                average += difference;
                neighbors++;
            }
        }
        if (neighbors > 0)
        {
            average = average / neighbors;
            boid.velocity += Vector2.Lerp(Vector2.zero, average, average.magnitude/radius);
        }
    }

    private void Update()
    {
        Cohere();
    }
}
