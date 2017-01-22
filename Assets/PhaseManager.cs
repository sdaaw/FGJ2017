using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{

    public bool sadState = false;

    public Light dirLight;

    public Camera cam;

    public ParticleSystem sadParticle;

    public Player player;

    [SerializeField]
    private float shakeValue;

    private Vector3 initCamPos;

    private int shakeCount;

    private bool newSadState;

    private bool shifting = false;

    public Material chillsb;
    public Material insanesb;


    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player>();
        initCamPos = cam.transform.position;
        sadParticle.gameObject.SetActive(false);
        player.currentState = PlayerState.Normal;

    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyUp(KeyCode.F)) //remove this ok?
        {
            /*if (player.currentState == PlayerState.Normal)
                player.currentState = PlayerState.Sad;
            else if(player.currentState == PlayerState.Sad)
                player.currentState = PlayerState.Normal;
            */
            //StartCoroutine("stateTransition", 2f);
        }

        if (shifting)
        {
            dirLight.intensity += 0.01f;
            cam.fieldOfView += 0.1f;
        }
        if(player.currentState == PlayerState.Sad)
        {
            if(RenderSettings.skybox != insanesb)
                RenderSettings.skybox = insanesb;
            //cam.clearFlags = CameraClearFlags.Color;
            //cam.backgroundColor = Color.black;
            sadParticle.gameObject.SetActive(true);

            if (!shifting)
            {
                cam.fieldOfView = 60;
                ShakeCamera();
                //dirLight.intensity = 0;

                Enemy[] enemies = FindObjectsOfType<Enemy>();
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].tag != "spooky")
                        enemies[i].model.transform.GetChild(1).GetComponent<Renderer>().material.mainTexture = enemies[i].sadTexture;
                }

            }
        }
        else if(player.currentState == PlayerState.Normal)
        {
            if (!shifting)
            {
                cam.fieldOfView = 60;
                dirLight.intensity = 1;
                List<GameObject> spooks = new List<GameObject>();
                Enemy[] enemies = FindObjectsOfType<Enemy>();
                for(int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].tag == "spooky")
                        spooks.Add(enemies[i].gameObject);
                }

                for(int i = 0; i < spooks.Count; i++)
                {
                    Destroy(spooks[i]);
                }
            }
            sadParticle.gameObject.SetActive(false);
            //cam.clearFlags = CameraClearFlags.Skybox;
            if (RenderSettings.skybox != chillsb)
                RenderSettings.skybox = chillsb;
        }

    }

    public void Shift()
    {
        StartCoroutine("stateTransition", 2f);
    }


    IEnumerator stateTransition(float transitionTime)
    {
        shifting = true;
        //dirLight.intensity++;
        yield return new WaitForSeconds(transitionTime);
        if (player.currentState == PlayerState.Normal)
        {
            player.currentState = PlayerState.Sad;
        }
        else
        {
            player.currentState = PlayerState.Normal;
        }
        shifting = false;
        //sadState = newSadState;
    }

    void ShakeCamera()
    {
        cam.transform.position = new Vector3(cam.transform.position.x + Random.Range(-shakeValue, shakeValue),
            cam.transform.position.y + Random.Range(-shakeValue, shakeValue),
            cam.transform.position.z + Random.Range(-shakeValue, shakeValue));

        //cam.transform.position = initCamPos; //reset back to normal so it wont drift
    }
}
