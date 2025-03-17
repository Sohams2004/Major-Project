using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    public CinemachineCamera camera;
    GameObject player;

    private void Start()
    {
        camera = FindObjectOfType<CinemachineCamera>();

        player = GameObject.FindGameObjectWithTag("Player");

        SetCameraTarget(player.transform);
    }

    public void SetCameraTarget(Transform target)
    {
        camera.Follow = target;
        camera.LookAt = target;
    }
}
