using UnityEngine;

public class Separation : MonoBehaviour
{
    public float radius;
    public float neighbors;

    Boid boid;

    private void Start()
    {
        boid = GetComponent<Boid>();
    }

    void Separate()
    {
        var boids = FindObjectsOfType<Boid>();
        var average = Vector2.zero;
        neighbors = 0;

        for (int i = 0; i < boids.Length; i++)
        {
            if(boids[i] != boid)
            {
                Vector2 difference = boid.transform.position - gameObject.transform.position;

                if (difference.magnitude < radius)
                {
                    average -= difference;
                    neighbors++;
                }
            } 
        }

        if (neighbors > 0)
        {
            average = average / neighbors;
            boid.velocity -= Vector2.Lerp(Vector2.zero, average, average.magnitude / radius);
        }
    }

    private void Update()
    {
        Separate();
    }
}
