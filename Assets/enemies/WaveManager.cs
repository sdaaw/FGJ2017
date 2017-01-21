using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaveManager : MonoBehaviour {

    public int currWave;
    public bool isOnWave;
    public int waveEnemyCount = 3;

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
                player.UpdateBgSound();
            }
            if(player.currentState == PlayerState.Sad)
            {

                sinCurve.howSadAreYou = 2f;
                sinCurve.speed = 20f;
                waveTimer += 1 * Time.deltaTime;
                if(!lastWave)
                    timerText.text = "Time: " + Mathf.RoundToInt(waveTimer).ToString() + "/" + waveTimeCap;
                else
                    waveText.text = "Final rampage: " + (int)waveTimer + "/" + waveTimeCap;

                if (waveTimer > waveTimeCap)
                {
                    if(lastWave)
                    {
                        player.stats.Die();
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
        for (int i = 0; i < waveEnemyCount; i++)
        {
            Vector3 offsetPos = new Vector3(spawnPoints[Random.Range(0, spawnPoints.Count)].position.x + Random.Range(-10, 10),
                spawnPoints[Random.Range(0, spawnPoints.Count)].position.y,
                spawnPoints[Random.Range(0, spawnPoints.Count)].position.z + Random.Range(-10, 10));


            GameObject.Instantiate(enemyPrefab, offsetPos, Quaternion.identity);
            FindObjectOfType<GameManager>().UpdateEnemyText();
        }
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
            player.stats.Health = player.stats.MaxHealth;
            waveTimer = 0;
            player.currentState = PlayerState.Normal;
            player.UpdateBgSound();
            currWave = currWave + 1;
            isOnWave = true;
            waveTimeCap += 5;
            waveEnemyCount += 3;
            SpawnEnemy();
        }
        //GetComponent<PhaseManager>().Shift();
    }
}
