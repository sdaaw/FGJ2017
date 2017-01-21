using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyTimer : MonoBehaviour
{
    public float time;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time)
            Destroy(gameObject);
    }
}
