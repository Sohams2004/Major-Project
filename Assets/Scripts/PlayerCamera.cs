using UnityEngine;
using Unity.Cinemachine;

public class PlayerCamera : MonoBehaviour
{
    public CinemachineCamera Camera;

    private void Awake()
    {
        Camera = FindObjectOfType<CinemachineCamera>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            SetCameraTarget(player.transform);
        }
    }

    public void SetCameraTarget(Transform target)
    {
        Camera.Follow = target;
        Camera.LookAt = target;
    }
}
