using UnityEngine;

public class BoidManager : MonoBehaviour
{
    [SerializeField] GameObject[] boids;
    [SerializeField] GameObject boid;
    [SerializeField] GameObject boidParent;

    [SerializeField] int boidCount;

    [SerializeField] Vector2 radius = new Vector2();

    private void Start()
    {
        //boid = GameObject.FindWithTag("Boid");
        //boidParent = GameObject.FindWithTag("BoidParent");
    }

    private void Awake()
    {
        boids = new GameObject[boidCount];
        for (int i = 0; i < boidCount; i++)
        {
            boids[i] = Instantiate(boid, new Vector2(Random.Range(-radius.x, radius.x), Random.Range(-radius.y, radius.y)), Quaternion.identity);
            boids[i].transform.SetParent(boidParent.transform);
        }
    }
}
