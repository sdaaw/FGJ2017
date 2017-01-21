using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int Health = 0;
    public bool isAlive = true;
    public int scoreToAdd;

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0 && isAlive)
        {
            isAlive = false;
            Die();
        }
            
    }

    public void Die()
    {
        //play death anim and wait
        //dragdol
        GameManager.AddScore(scoreToAdd);
        Destroy(gameObject);
    }

    private void Awake()
    {

    }

    private void FixedUpdate()
    {

    }
}