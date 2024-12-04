using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    public float tileSize = 0.08f; // Size of each tile
    public float moveSpeed = 1f; // Speed of movement between tiles

    private Vector3 targetPosition;
    private Vector3Int lastMove;
    private bool isMoving = false;
    private GameObject enemy;
    private bool hashit = false;
    private bool moved_ = false;

    private Tilemapgenorator map;
    private TurnHandler Turnswap;
    private HealthSystem healthSystem;
    private Enemymanager enemymanager;

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        map = FindObjectOfType<Tilemapgenorator>(); // Find the TestTilemap script
        Turnswap = FindObjectOfType<TurnHandler>(); // Find EnemyMovent script
        healthSystem = FindObjectOfType<HealthSystem>();
        enemymanager = FindObjectOfType<Enemymanager>();
        targetPosition = transform.position;    // Set initial target position
    }

    void Update()
    {
        // If already moving, don't accept new input
        if (!Turnswap.IsPlayerTurn() || isMoving)
        {
            // Smoothly move to the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Stop moving if we reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                if (!moved_)
                {
                    transform.position = targetPosition;
                    isMoving = false;

                    moved_ = true;

                    
                    Turnswap.PlayerFinishedTurn();
                }
            }
            return;
        }

        // Check for input and move in the corresponding direction
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            TryMove(Vector3Int.up);
            lastMove = Vector3Int.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            TryMove(Vector3Int.down);
            lastMove = Vector3Int.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TryMove(Vector3Int.left);
            lastMove = Vector3Int.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            TryMove(Vector3Int.right);
            lastMove = Vector3Int.right;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // First, try moving in the last direction (1 tile)
            if (TryMove(lastMove))
            {
                // If the first move is successful, try moving in the same direction (2 tiles)
                TryMove(lastMove * 2);
            }
        }
    }

    void AttackEnemy()
    {
        if (!hashit)
        {
            healthSystem.TakeDamage(2, "enemy");

            Turnswap.PlayerFinishedTurn();

            hashit = true;
        }
    }

    bool TryMove(Vector3Int direction)
    {
        // Calculate the new tile position
        Vector3Int gridPosition = map.tilemap.WorldToCell(transform.position);
        Vector3Int targetGridPosition = gridPosition + direction;

        Vector3Int playerGridPosition = map.tilemap.WorldToCell(transform.position);
        Vector3Int enemyGridPosition = map.tilemap.WorldToCell(enemy.transform.position);


        if (IsAdjacentToEnemy(enemyGridPosition, playerGridPosition))
        {
            hashit = false;
            AttackEnemy();

            return false;
        }

        // Check if the target tile is passable
        else if (map.IsTilePassable(targetGridPosition))
        {
            moved_ = false;

            targetPosition = map.tilemap.CellToWorld(targetGridPosition) + new Vector3(tileSize / 2, tileSize / 2, 0); // Offset to center on tile
            isMoving = true;
            return true; // Move was successful
        }

        if (map.IsDoorTile(targetGridPosition))
        {
            string mapData = map.LoadPremadeLevel();
            map.ConvertMapToTilemap(mapData);

            enemymanager.Spawn();
        }

        if (map.IsChestTile(targetGridPosition))
        {
            healthSystem.HealPlayer(1);
            map.tilemap.SetTile(targetGridPosition, map.openChestTile);
        }

        return false; // Move was not possible
    }

    bool IsAdjacentToEnemy(Vector3Int enemyPosition, Vector3Int playerPosition)
    {
        // Calculate the difference between the enemy and player positions
        int distanceX = Mathf.Abs(playerPosition.x - enemyPosition.x);
        int distanceY = Mathf.Abs(playerPosition.y - enemyPosition.y);

        // Return true if the player is 1 tile away in any direction
        return (distanceX == 1 && distanceY == 0) || (distanceX == 0 && distanceY == 1);
    }
}
