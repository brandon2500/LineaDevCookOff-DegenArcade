using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;

    public GameObject player;
    public Transform respawnPoint;
    public TextMeshProUGUI scoreText;

    private int score;
   
    void Start()
    {
       score = 0;
    }

    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        player.SetActive(true);

        gameOverPanel.SetActive(false);

        score = 0;
        UpdateScoreText();

    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
