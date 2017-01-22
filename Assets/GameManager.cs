using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public static GameManager gameManager;

    public Text scoreText;
    public Text enemyAmount;
    public Text massacreText;

    public static int Score;

    public List<Enemy> enemies;


    public float massacreTime;
    private float massacreTimer;
    public float killCount;

    public GameObject endScreen;

    void Start()
    {
        gameManager = this;
        Score = 0;
        Time.timeScale = 1;
    }

    public static void AddScore(int scoreToAdd, int multiplier)
    {
        Score += scoreToAdd * (multiplier==0 ? 1 : multiplier);
        FindObjectOfType<GameManager>().UpdateScoreText();
    }

    void Update()
    {
        if(massacreTimer > 0)
        {
            massacreTimer -= Time.deltaTime;
            
        }

        if (massacreTimer < 0)
        {
            massacreTimer = 0;
            killCount = 0;
        }

        massacreText.text = killCount + "x " + massacreTimer.ToString("F2") + "s";

        if (Input.GetKey(KeyCode.Escape))
            Application.LoadLevel(0);
    }

    public List<GameObject> blöds;
    public int maxBlood = 300;

    public void CheckBlood(float bloodPerSpawn)
    {
        if (blöds.Count > maxBlood)
        {
            for (int j = 0; j < bloodPerSpawn; j++)
            {
                GameObject b = blöds[j];
                blöds.Remove(b);
                b.GetComponent<destroyTimer>().StartDestroyTimer(Random.Range(1, 6));
            }
        }
    }


    private void Awake()
    {
        scoreText.text = 0 + " :(";
        UpdateEnemyText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = Score + "!!";
    }

    public void UpdateEnemyText()
    {
        if(enemyAmount != null)
            enemyAmount.text = enemies.Count + "!";
    }

    public void RefreshMassacre()
    {
        massacreTimer = massacreTime;
        killCount++;
    }

    public void EndGame()
    {
        endScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        //endScreen.GetComponent<Animator>().Play()
    }

    public void Reload()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
