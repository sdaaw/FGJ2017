using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public Text scoreText;

    public static int Score;

    public static void AddScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        FindObjectOfType<GameManager>().UpdateScoreText();
    }

    private void Awake()
    {
        scoreText.text = "Score: 0 :^(";
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + Score + "!!";
    }
}
