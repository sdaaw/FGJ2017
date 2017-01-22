using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodswap : MonoBehaviour
{
    public Texture s1;
    public Texture s2;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Renderer>().material.mainTexture = (Random.Range(0, 2) == 1) ? s1 : s2;
	}
}
