using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public Text scoreText;
    public Text enemyAmount;

    public static int Score;

    public List<Enemy> enemies;

    public static void AddScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        FindObjectOfType<GameManager>().UpdateScoreText();
    }

    private void Awake()
    {
        scoreText.text = "Score: 0 :^(";
        UpdateEnemyText();    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + Score + "!!";
    }

    public void UpdateEnemyText()
    {
        enemyAmount.text = "Enemies left: " + enemies.Count + "!";
    }
}
