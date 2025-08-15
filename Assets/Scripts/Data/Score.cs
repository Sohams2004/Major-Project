using System;
using OpenCover.Framework.Model;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using File = System.IO.File;

public class Score : MonoBehaviour
{
    [SerializeField] private int score = 0;
    private int coinLayer;
    [SerializeField] TextMeshProUGUI scoreText;
    
    private void Start()
    {
        coinLayer = LayerMask.NameToLayer("Coin");
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();

        
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            LoadScore();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == coinLayer)
        {
            Coins coin = other.GetComponent<Coins>();
            if (coin != null)
            {
                Debug.Log("Collected " + coin.coinValue + " coins");
                score += coin.coinValue;
                scoreText.text = "Score: " + score;
                Destroy(other.gameObject);
            }
        }
    }

    public void SaveScore()
    {
        ScoreData data = new ScoreData();
        data.score = score;
        
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/ScoreDataFile.json", json);
    }

    public void LoadScore()
    {
        Debug.Log("Loading Score Data");
        string json = File.ReadAllText(Application.dataPath + "/ScoreDataFile.json");
        ScoreData data = JsonUtility.FromJson<ScoreData>(json);
        
        score = data.score;
        scoreText.text = "Score: " + score;
    }

    private void OnDestroy()
    {
        SaveScore();
    }
}
