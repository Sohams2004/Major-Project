using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    RoomGenerate roomGenerate;

    [SerializeField] private TextMeshProUGUI loadingText;

    private void Start()
    {
        roomGenerate = FindObjectOfType<RoomGenerate>();
    }

    IEnumerator RoomGenerateLoadingPercent()
    {
        loadingText.text = "Generating Level : " + Mathf.Round(roomGenerate.loadingPercent) + "%";

        if (roomGenerate.loadingPercent >= 100)
        {
            yield return new WaitForSeconds(1f);
            loadingText.enabled = false;
        }
    }

    private void Update()
    {
        StartCoroutine(RoomGenerateLoadingPercent());
    }
}
