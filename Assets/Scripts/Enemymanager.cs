using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymanager : MonoBehaviour
{
    public GameObject Enemy;
    public Vector3 EnemySpawn;

    private Vector3 OffScreenSpawn;

    private HealthSystem healthSystem;
    private Tilemapgenorator tilemapgenorator;

    // Start is called before the first frame update
    void Start()
    {
        OffScreenSpawn = Enemy.transform.position;
        healthSystem = GetComponent<HealthSystem>();
        tilemapgenorator = GetComponent<Tilemapgenorator>();
    }

    // keeping incase i need later
    void Update()
    {
        
    }

    public void Spawn()
    {
        healthSystem.EHealth = 10; // just in case the health was not rest the first time

        Enemy.transform.position = EnemySpawn;
    }

    public void DeadE()
    {
        if (healthSystem.EHealth >= 0) // this is the first area where enemy health should be reset
        {
            Enemy.transform.position = OffScreenSpawn; 
        }
    }
}
