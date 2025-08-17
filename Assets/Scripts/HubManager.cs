using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubManager : MonoBehaviour
{
    [SerializeField] private GameObject gamePausePanel;
    [SerializeField] private GameObject controlPanel;

    [SerializeField] private bool isPaused;
    [SerializeField] private bool isControlPanelOpen;

    private void Start()
    {
        gamePausePanel.SetActive(false);
        controlPanel.SetActive(false);
        Time.timeScale = 1;
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
        GamePause();
    }
}
