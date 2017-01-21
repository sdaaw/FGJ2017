using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int Health = 0;
    public bool isAlive = true;
    public int scoreToAdd;

    public GameObject blood;
    public Camera cam;

    public float shakeValue;

    public int bloodParticleAmount = 35;

    public bool isInvincible;
    public float invTime;

    public void TakeDamage(int damage)
    {
        if (isInvincible)
            return;

        Health -= damage;
        if (Health <= 0 && isAlive)
        {
            isAlive = false;
            Die();
        }

        for(int i = 0; i < bloodParticleAmount; i++)
        {
            GameObject a = Instantiate(blood, new Vector3(transform.position.x, transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            Rigidbody aR = a.GetComponent<Rigidbody>();
            aR.AddForce(Vector3.forward * 100);
        }
        StartCoroutine(invicibleTimer());
    }

    public void Die()
    {
        //play death anim and wait
        //dragdol
        GameManager.AddScore(scoreToAdd, (int)GameManager.gameManager.killCount);
        GetComponent<Renderer>().material.color = Color.gray;
        isAlive = false;
        if(GetComponent<Enemy>())
        {
            GetComponent<Enemy>().model.SetActive(false);
            //GetComponent<Enemy>().ragdoll.SetActive(true);
            GameObject.Instantiate(GetComponent<Enemy>().ragdoll, transform.position, transform.rotation);
            FindObjectOfType<GameManager>().RefreshMassacre();
            Destroy(gameObject);
        }
        else if(GetComponent<Player>())
        {
            Player p = GetComponent<Player>();
            p.gameObject.SetActive(false);
            FindObjectOfType<CameraController>().enabled = false;
            GameManager.gameManager.EndGame();
        }
        //Destroy(gameObject);
    }

    private void Awake()
    {
    }

    private void FixedUpdate()
    {

    }

    IEnumerator invicibleTimer()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invTime);
        isInvincible = false;
    }
}