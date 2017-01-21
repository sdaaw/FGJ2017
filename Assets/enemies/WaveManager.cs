using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaveManager : MonoBehaviour {

    public int currWave;
    public bool isOnWave;

    private float waveTimer;
    public float waveTimeCap;

    public bool isOnPause;
    public bool lastWave = false;

    public Text waveText;
    public Text timerText;
    public Text toggleText;

    private Player player;

    private SinTest sinCurve;

    private float spawnTimer = 0;
    public float spawnTime = 0;

    public Enemy enemyPrefab;
    public List<Transform> spawnPoints;

    private GameManager gm;

    public bool isEnabled;


    // Use this for initialization
    void Start () {

        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
        sinCurve = FindObjectOfType<SinTest>();
        isOnWave = true;
        currWave = 1;


    }
	
	// Update is called once per frame
	void Update () {



        if(isOnWave)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                player.currentState = PlayerState.Sad;
                timerText.text = "O BOI NICE MEDS";
                waveText.text = "Wave: " + currWave.ToString();
                //GetComponent<PhaseManager>().Shift();
            }
            if(player.currentState == PlayerState.Sad)
            {
                spawnTimer += Time.deltaTime;
                if (spawnTimer >= spawnTime)
                {
                    spawnTimer = 0;
                    SpawnEnemy();
                }

                sinCurve.howSadAreYou = 2f;
                sinCurve.speed = 20f;
                waveTimer += 1 * Time.deltaTime;
                if(!lastWave)
                    timerText.text = Mathf.RoundToInt(waveTimer).ToString();
                else
                    waveText.text = "Final rampage: " + (int)waveTimer + "/" + waveTimeCap;

                if (waveTimer > waveTimeCap)
                {
                    if(lastWave)
                    {
                        Time.timeScale = 0;
                        timerText.text = "Captured by the police!";
                        waveText.text = "Final score: " + GameManager.Score;
                    }
                    else
                    {
                        NextWave();
                    }
                }
            }
            else
            {
                timerText.text = "Press 'F' to toggle sad mode!";
                sinCurve.howSadAreYou = 5f;
                sinCurve.speed = 2f;
            }
        }
    }

    void SpawnEnemy()
    {
        GameObject.Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
        FindObjectOfType<GameManager>().UpdateEnemyText();
    }

    void NextWave()
    {
        if(gm.enemies.Count > 0)
        {
            waveTimer = 0;
            waveTimeCap = 30;
            lastWave = true;
            timerText.text = "You left a witness, police is on the way!";
            waveText.text = "Final rampage: " + waveTimer + "/" + waveTimeCap;
        }
        else
        {
            player.currentState = PlayerState.Normal;
            currWave = currWave + 1;
            isOnWave = true;
            waveTimeCap += 5;
        }
        //GetComponent<PhaseManager>().Shift();
    }

    void OnWave(int wave)
    {

    }
}
