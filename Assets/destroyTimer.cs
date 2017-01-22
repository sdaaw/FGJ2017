using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyTimer : MonoBehaviour
{
    public float time;
    private float timer;

    private bool destroy;

    private void Update()
    {
        if (destroy)
            DestroyTimer();
    }

    void DestroyTimer()
    {
        timer += Time.deltaTime;
        if (timer >= time)
            Destroy(gameObject);
    }


    public void StartDestroyTimer(float dTime)
    {
        time = dTime;
        destroy = true;
    }
}
