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
    private WinCounter WinCounter;

    // Start is called before the first frame update
    void Start()
    {
        OffScreenSpawn = Enemy.transform.position;
        healthSystem = GetComponent<HealthSystem>();
        tilemapgenorator = GetComponent<Tilemapgenorator>();
        WinCounter = GetComponent<WinCounter>();
    }

    // keeping incase i need later
    void Update()
    {
        
    }

    public void Spawn()
    {
        healthSystem.EHealth = 10; // just in case the health was not rest the first time
        healthSystem.EHealthBar.transform.localScale = new Vector3(1, 1, 1);

        Enemy.transform.position = EnemySpawn;
    }

    public void DeadE()
    {
        if (healthSystem.EHealth <= 0) // this is the first area where enemy health should be reset
        {
            healthSystem.EHealth = 10;
            Enemy.transform.position = OffScreenSpawn;
            WinCounter.AddCount();
        }
    }
}
