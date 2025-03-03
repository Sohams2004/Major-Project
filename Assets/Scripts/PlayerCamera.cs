using UnityEngine;
using Unity.Cinemachine;

public class PlayerCamera : MonoBehaviour
{
    public CinemachineCamera Camera;

    private void Awake()
    {
        Camera = FindObjectOfType<CinemachineCamera>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
        if (player != null)
        {
            Camera.Follow = player.transform;
            Camera.LookAt = player.transform;
        }
    }
}
