using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private AudioSource portalAudioSource;
    
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            portalAudioSource = GameObject.Find("Portal").GetComponent<AudioSource>();
        }
    }

    public void SceneChange()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex + 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Portal"))
        {
            portalAudioSource.Play();
            gameObject.SetActive(false);
            Invoke("SceneChange", 2f);
        }
    }
}
