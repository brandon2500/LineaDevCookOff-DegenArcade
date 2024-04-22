using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 1;

    [SerializeField] private TMPro.TextMeshProUGUI scoreText;


    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            score ++;
            scoreText.text = score.ToString();
        }
    }

    public void Restart()
    {
        score = 0;
    }

}
