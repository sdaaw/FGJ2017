using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public Text scoreText;
    public Text enemyAmount;
    public Text toggleText;
    public Text insanityText;

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

    void Start()
    {
        player = FindObjectOfType<Player>();
        sinCurve = FindObjectOfType<SinTest>();
    }

    public static void AddScore(int scoreToAdd)
    { 
        Score += scoreToAdd;
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
        enemyAmount.text = "Enemies left: " + enemies.Count + "!";
    }
}
