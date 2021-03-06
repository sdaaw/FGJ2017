﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private GameManager m_gm;
    public int Health = 0;
    public int MaxHealth = 0;
    public bool isAlive = true;
    public int scoreToAdd;

    public GameObject blood;
    public Camera cam;

    public float shakeValue;

    public int bloodPerSpawn = 30;

    public bool isInvincible;
    public float invTime;

    public GameObject blöd;

    public void TakeDamage(int damage)
    {
        if (!isInvincible && isAlive)
        {
            Health -= damage;
            if (Health <= 0 && isAlive)
            {
                isAlive = false;
                Die();
            }
            else
            {
                StartCoroutine(invicibleTimer());
            }

            for (int i = 0; i < bloodPerSpawn; i++)
            {
                GameObject a = Instantiate(blood, new Vector3(transform.position.x, transform.position.y, gameObject.transform.position.z), Quaternion.identity);
                Rigidbody aR = a.GetComponent<Rigidbody>();
                a.GetComponent<destroyTimer>().time = Random.Range(15, 40);
                aR.AddForce(Vector3.forward * 100);
                m_gm.blöds.Add(a);
            }

            m_gm.CheckBlood(bloodPerSpawn);
            SoundManager.PlayASource("blood");

        }  
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
            //DecalPainter dp = FindObjectOfType<DecalPainter>();
            //dp.Paint(transform.position -transform.up, Color.red, 1, 1);

            Quaternion rRot = new Quaternion();
            rRot = Quaternion.Euler(-90, blöd.transform.rotation.y, Random.Range(0, 360));

            GameObject g = GameObject.Instantiate(blöd, transform.position - transform.up * 0.97f, rRot) as GameObject;
            g.transform.localScale = new Vector3(Random.Range(2f, 6f), Random.Range(2f, 6f), 1);


            GetComponent<Enemy>().model.SetActive(false);
            //GetComponent<Enemy>().ragdoll.SetActive(true);
            GameObject ragdoll = GameObject.Instantiate(GetComponent<Enemy>().ragdoll, transform.position, transform.rotation) as GameObject;
            if(tag != "spooky")
                ragdoll.GetComponentInChildren<Renderer>().material.mainTexture = m_gm.GetComponent<WaveManager>().textures[GetComponent<Enemy>().textureN];
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
        m_gm = FindObjectOfType<GameManager>();
    }

    IEnumerator invicibleTimer()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invTime);
        isInvincible = false;
    }
}