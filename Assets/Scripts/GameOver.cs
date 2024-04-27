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
    public Button claimTokenButton;
    public GameObject obstacles;

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
            obstacles.SetActive(false);

        }
    }

    public void RestartGame()
    {

        player.SetActive(true);

        gameOverPanel.SetActive(false);

        claimTokenButton.interactable = true;

        obstacles.SetActive(true);

        UpdateScoreText();

    }

    void UpdateScoreText()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
}
