using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour {

    public bool sadState = false;

    public Light dirLight;

    public Camera cam;

    [SerializeField]
    private float shakeValue;

    private Vector3 initCamPos;

    private int shakeCount;

    private bool newSadState;

    private bool shifting = false;

	// Use this for initialization
	void Start () {

        initCamPos = cam.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyUp(KeyCode.F))
        {
            if (!sadState)
                newSadState = true;
            else
                newSadState = false;

            StartCoroutine("stateTransition", 2f);
        }

        if(shifting)
        {
            dirLight.intensity += 0.01f;
            cam.fieldOfView += 0.1f;
        }


        if(sadState)
        {
            cam.clearFlags = CameraClearFlags.Color;
            cam.backgroundColor = Color.black;

            if(!shifting)
            {
                cam.fieldOfView = 60;
                ShakeCamera();
                dirLight.intensity = 0;
            }
        }
        else
        {
            if (!shifting)
            {
                cam.fieldOfView = 60;
                dirLight.intensity = 1;
            }
            cam.clearFlags = CameraClearFlags.Skybox;
        }
		
	}


    IEnumerator stateTransition(float transitionTime)
    {
        shifting = true;
        //dirLight.intensity++;
        yield return new WaitForSeconds(transitionTime);
        shifting = false;
        sadState = newSadState;
    }

    void ShakeCamera()
    {
        cam.transform.position = new Vector3(cam.transform.position.x + Random.Range(-shakeValue, shakeValue), 
            cam.transform.position.y + Random.Range(-shakeValue, shakeValue), 
            cam.transform.position.z + Random.Range(-shakeValue, shakeValue));

        //cam.transform.position = initCamPos; //reset back to normal so it wont drift
    }
}
