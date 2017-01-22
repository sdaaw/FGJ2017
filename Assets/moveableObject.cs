using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveableObject : MonoBehaviour {

    private Rigidbody rbody;
    public float power;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody>();
	}
	
    private void OnTriggerEnter(Collider col)
    {
        weapon w = col.GetComponent<weapon>();
        if(w != null)
        {
            rbody.AddForce(w.transform.root.forward * power, ForceMode.Impulse);
        }
    }
}
