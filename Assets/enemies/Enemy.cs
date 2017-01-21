using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player m_player;
    public Stats stats;

    public int dmg;

    public float chillSpeed;
    public float chaseSpeed;

    public bool waiting;
    public bool moving;

    private float m_swingTimer;
    public float swingTime;

    private Rigidbody m_rb;

    public GameObject model;
    public GameObject ragdoll;

    public float speed;
    public Animator anim;


	private void Awake ()
    {
        stats = GetComponent<Stats>();
        time = Random.Range(1, 3);
        m_player = FindObjectOfType<Player>();
        m_rb = GetComponent<Rigidbody>();

        FindObjectOfType<GameManager>().enemies.Add(this);
    }

    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void FixedUpdate ()
    {
        anim.SetFloat("speed", speed);
        if (stats.isAlive)
        {
            switch (m_player.currentState)
            {
                case PlayerState.Normal:
                    Chill();
                    break;
                case PlayerState.Sad:
                    Chase();
                    break;
            }
        }

        /*if (m_isGrounded && m_yVelocity < 0)
        {
            m_yVelocity = -0.1f;
        }
        else
        {
            m_yVelocity += Physics.gravity.y * m_fallingSpeed * Time.deltaTime;
        }*/

        speed = m_rb.velocity.sqrMagnitude;
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

        if(Vector3.Distance(transform.position, m_player.transform.position) < 2)
        {
            //do melee stuff
            Attack();
        }
        if (m_swingTimer >= swingTime + 2)
            m_swingTimer = 0;
        m_swingTimer += Time.deltaTime;
    }

    void Attack()
    {
        if(m_swingTimer >= swingTime)
        {
            m_swingTimer = 0;
            m_player.stats.TakeDamage(dmg);
        }
    }

    private void OnDestroy()
    {
        if(FindObjectOfType<GameManager>())
        {
            FindObjectOfType<GameManager>().enemies.Remove(this);
            FindObjectOfType<GameManager>().UpdateEnemyText();
        } 
    }

    private enum EnemyStates
    {
        Chill,
        Chase   
    };
}
