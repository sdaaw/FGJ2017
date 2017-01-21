using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public static GameManager gameManager;

    public Text scoreText;
    public Text enemyAmount;
    public Text toggleText;
    public Text insanityText;
    public Text massacreText;

    private float timerTime = 0;

    private int normalizeTime; //for the text shake
    public int normalizeCap;

    public int shakeValue;

    public bool sadModeReady = false;
    public static int Score;
    public float gameTimeLimit = 10;

    public List<Enemy> enemies;

    public LineRenderer line;

    public Player player;

    public SinTest sinCurve;

    public float massacreTime;
    private float massacreTimer;
    public float killCount;

    public GameObject endScreen;

    void Start()
    {
        player = FindObjectOfType<Player>();
        sinCurve = FindObjectOfType<SinTest>();
        gameManager = this;
    }

    public static void AddScore(int scoreToAdd, int multiplier)
    { 
        Score += scoreToAdd * (multiplier==0 ? 1 : multiplier);
        FindObjectOfType<GameManager>().UpdateScoreText();
    }

    void Update()
    {

        if(Input.GetKeyUp(KeyCode.F))
        {
            if(sadModeReady)
            {
                GetComponent<PhaseManager>().Shift();
                sinCurve.howSadAreYou = 2f;
                sinCurve.speed = 20f;
                timerTime = 0;
                sadModeReady = false;
            }
        }
        shakeText();
        if(timerTime > gameTimeLimit && player.currentState != PlayerState.Sad) //switch mental states?
        {
            //wtf do I do here?
            sadModeReady = true;
            insanityText.text = "Press 'F' to activate sad mode!";
        }
        else if(timerTime > gameTimeLimit && player.currentState == PlayerState.Sad)
        {
            GetComponent<PhaseManager>().Shift();
            //GetComponent<SinTest>().speed = 2;
            //GetComponent<SinTest>().howSadAreYou = 2f;
            sinCurve.howSadAreYou = 5f;
            sinCurve.speed = 2f;
            
            timerTime = 0;
        }
        else
        {
            timerTime += 1 * Time.deltaTime;
            insanityText.text = Mathf.RoundToInt(timerTime).ToString();
        }

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
    }

    void shakeText()
    {
        normalizeTime++;
        if(normalizeTime < normalizeCap)
        {
        }
        else
        {
            normalizeTime = 0;
        }
    }

    private void Awake()
    {
        scoreText.text = "Score: 0 :^(";
        UpdateEnemyText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + Score + "!!";
    }

    public void UpdateEnemyText()
    {
        if(enemyAmount != null)
            enemyAmount.text = "Enemies left: " + enemies.Count + "!";
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
