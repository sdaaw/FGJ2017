using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public Enemy enemyPrefab;
    public List<Transform> spawnPoints;
    public bool isEnabled;

    private float spawnTimer = 0;
    public float spawnTime = 0;
	
	void Update () {
        if(isEnabled)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnTime)
            {
                spawnTimer = 0;
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        GameObject.Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
        FindObjectOfType<GameManager>().UpdateEnemyText();
    }
}
