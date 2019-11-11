using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject enemyPrefab;
    public GameObject healthPrefab;
    public GameObject ammoRoundPrefab;
    public GameObject bossPrefab;

    private float spawnRange = 9.0f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(gameManager.level);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            int enemyCount = FindObjectsOfType<Enemy>().Length;
            if (gameManager.level >= 5 && enemyCount==0 && !gameManager.isBossActive)
            {
                gameManager.ActivateBoss();
                Instantiate(bossPrefab, GenerateRandomPosition(), bossPrefab.transform.rotation);
                InvokeRepeating("SpawnAfterBoss", 0, 3.0f);
            }
            else
            {
                if (enemyCount == 0 && !gameManager.isBossActive)
                {
                    //next level update here
                    gameManager.updateLevel();
                    SpawnEnemyWave(gameManager.level);
                }
            }
        }
    }

    public void SpawnEnemyWave(int enemiesToSpawn)
    {
        for(int i=0; i<enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateRandomPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    public void SpawnHealth(Vector3 pos=new Vector3())
    {
        if (gameManager.isGameActive)
        {
            Instantiate(healthPrefab, pos + new Vector3(0, 0.5f, 0), healthPrefab.transform.rotation);
        }
    }

    public void SpawnAmmo(Vector3 pos = new Vector3())
    {
        if (gameManager.isGameActive)
        {
            Instantiate(ammoRoundPrefab, pos + new Vector3(0, 0.5f, 0), ammoRoundPrefab.transform.rotation);
        }
    }

    public void SpawnAfterBoss()
    {
        SpawnAmmo(GenerateRandomPosition());
        SpawnHealth(GenerateRandomPosition());
    }
}
