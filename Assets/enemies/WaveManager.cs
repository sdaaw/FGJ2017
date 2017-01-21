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

    public Text waveText;
    public Text timerText;
    public Text toggleText;

    public Player player;

    public SinTest sinCurve;


	// Use this for initialization
	void Start () {

        player = FindObjectOfType<Player>();
        sinCurve = FindObjectOfType<SinTest>();

        isOnWave = true;

    }
	
	// Update is called once per frame
	void Update () {



        if(isOnWave)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                player.currentState = PlayerState.Sad;
                timerText.text = "O BOI NICE MEDS";
                //GetComponent<PhaseManager>().Shift();
            }
            if(player.currentState == PlayerState.Sad)
            {
                sinCurve.howSadAreYou = 2f;
                sinCurve.speed = 20f;
                waveTimer += 1 * Time.deltaTime;
                timerText.text = Mathf.RoundToInt(waveTimer).ToString();
                if (waveTimer > waveTimeCap)
                {
                    NextWave();
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

    void NextWave()
    {
        //GetComponent<PhaseManager>().Shift();
        player.currentState = PlayerState.Normal;
        currWave = currWave + 1;
        isOnWave = true;
        waveTimer = 0;
        waveTimeCap += 5;
    }

    void OnWave(int wave)
    {

    }
}
