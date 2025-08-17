using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    RoomGenerate roomGenerate;

    [SerializeField] private TextMeshProUGUI loadingText;

    [SerializeField] private GameObject gamePausePanel;
    [SerializeField] private GameObject controlPanel;

    [SerializeField] private bool isPaused;
    [SerializeField] private bool isControlPanelOpen;

    private void Start()
    {
        roomGenerate = FindObjectOfType<RoomGenerate>();
        gamePausePanel.SetActive(false);
        controlPanel.SetActive(false);
        Time.timeScale = 1;
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

    void GamePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                isPaused = true;
                Time.timeScale = 0;
                AudioListener.pause = true;
                gamePausePanel.SetActive(true);
            }
            
            else if (isPaused && isControlPanelOpen)
            {
                CloseControlPanel();
            }

            else
            {
                isPaused=false;
                Time.timeScale = 1;
                AudioListener.pause = false;
                gamePausePanel.SetActive(false);
            }
        }
    }
    
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        gamePausePanel.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene(0);
    }
    
    public void ControlPanel()
    {
        isControlPanelOpen = true;
        gamePausePanel.SetActive(false);
        controlPanel.SetActive(true);
    }
    
    public void CloseControlPanel()
    {
        isControlPanelOpen = false;
        controlPanel.SetActive(false);
        gamePausePanel.SetActive(true);
    }

    private void Update()
    {
        StartCoroutine(RoomGenerateLoadingPercent());
        GamePause();
    }
}
