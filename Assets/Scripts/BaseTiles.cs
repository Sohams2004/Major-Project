using UnityEngine;

public class BaseTiles : MonoBehaviour
{
    [SerializeField] bool isRoom;
    Corridors corridors;

    [System.Obsolete]
    private void Awake()
    {
        corridors = FindObjectOfType<Corridors>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Room"))
        {
            isRoom = true;
            corridors.nodes.Add(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Room"))
        {
            isRoom = false;
            corridors.nodes.Remove(gameObject);
        }
    }
}
