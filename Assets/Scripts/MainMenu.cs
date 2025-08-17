using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    [SerializeField] AudioSource buttonClickAudio;

    private void Start()
    {
        buttonClickAudio = GameObject.Find("ButtonClickAudio").GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
    }
    
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ButtonClickAudio()
    {
        buttonClickAudio.Play();
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
