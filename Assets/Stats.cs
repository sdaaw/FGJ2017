using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int Health = 0;
    public bool isAlive = true;

    public GameObject blood;

    public int bloodParticleAmount = 100;

    public void TakeDamage(int damage)
    {
        Debug.Log("VITTU OSU");
        Health -= damage;
        if (Health <= 0 && isAlive)
        {
            isAlive = false;
            Die();
        }

        for(int i = 0; i < bloodParticleAmount; i++)
        {
            Debug.Log("asd");
            GameObject a = Instantiate(blood, new Vector3(transform.position.x, transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            Rigidbody aR = a.GetComponent<Rigidbody>();
            aR.AddForce(Vector3.forward * 100);
        }
            
    }

    public void Die()
    {
        //play death anim and wait
        //dragdol?
        Destroy(gameObject);
    }

    private void Awake()
    {

    }

    private void FixedUpdate()
    {

    }
}