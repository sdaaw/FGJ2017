using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player m_player;
    public Stats stats;

    public float chillSpeed;
    public float chaseSpeed;

    public bool waiting;
    public bool moving;

    private Rigidbody m_rb;

	private void Awake ()
    {
        stats = GetComponent<Stats>();
        time = Random.Range(1, 3);
        m_player = FindObjectOfType<Player>();
        m_rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate ()
    {
        switch(m_player.currentState)
        {
            case PlayerState.Normal:
                Chill();
                break;
            case PlayerState.Sad:
                Chase();
                break;
        }
	}

    float timer = 0;
    float time = 0;

    private void Chill()
    {
        if(!moving && !waiting)
        {
           transform.Rotate(transform.rotation.x, Random.Range(0, 360), transform.rotation.z);
        }
        if (timer < time && !waiting)
        {
            moving = true;
            m_rb.velocity += transform.forward * chillSpeed * Time.deltaTime;
        }
        else if(timer >= time)
        {
            moving = false;
            StartCoroutine(AIDelay(Random.Range(1, 5)));
            timer = 0;
            time = Random.Range(1, 3);
        }

        timer += Time.deltaTime;
    }

    IEnumerator AIDelay(float delay)
    {
        waiting = true;
        yield return new WaitForSeconds(delay);
        waiting = false;
    }

    Vector3 playerChaseLookat;

    private void Chase()
    {
        playerChaseLookat = new Vector3(m_player.transform.position.x,
                                       transform.position.y,
                                       m_player.transform.position.z);

        transform.LookAt(playerChaseLookat);

        Vector3 dir = (playerChaseLookat - transform.position).normalized * chaseSpeed;
        m_rb.velocity = dir;
    }

    private enum EnemyStates
    {
        Chill,
        Chase   
    };
}
