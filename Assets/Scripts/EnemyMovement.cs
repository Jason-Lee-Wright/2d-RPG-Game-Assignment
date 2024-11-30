using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float tileSize = 0.08f; // Size of each tile
    public float moveSpeed = 1f; // Speed of movement between tiles
    private Vector3 targetPosition;
    private bool isMoving = false;

    private GameObject player; // Reference to the player
    private Tilemapgenorator map;

    void Start()
    {
        map = FindObjectOfType<Tilemapgenorator>(); // Find the Tilemap generator script
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player object by tag
        targetPosition = transform.position;       // Set initial target position
    }

    void Update()
    {
        // Smoothly move to the target position
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Stop moving if we reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
        else
        {
            // Move towards the player when not already moving
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        // Calculate the direction to the player
        Vector3Int enemyGridPosition = map.tilemap.WorldToCell(transform.position);
        Vector3Int playerGridPosition = map.tilemap.WorldToCell(player.transform.position);
        Vector3Int direction = GetStepTowardsTarget(enemyGridPosition, playerGridPosition);

        // Try to move the enemy in the calculated direction
        TryMove(direction);
    }

    bool TryMove(Vector3Int direction)
    {
        // Calculate the new tile position
        Vector3Int gridPosition = map.tilemap.WorldToCell(transform.position);
        Vector3Int targetGridPosition = gridPosition + direction;

        // Check if the target tile is passable
        if (map.IsTilePassable(targetGridPosition))
        {
            targetPosition = map.tilemap.CellToWorld(targetGridPosition) + new Vector3(tileSize / 2, tileSize / 2, 0); // Offset to center on tile
            isMoving = true;
            return true; // Move was successful
        }

        return false; // Move was not possible
    }

    Vector3Int GetStepTowardsTarget(Vector3Int currentPosition, Vector3Int targetPosition)
    {
        Vector3Int direction = Vector3Int.zero;

        // Prioritize horizontal movement (x-axis)
        if (currentPosition.x < targetPosition.x)
            direction = Vector3Int.right; // Move right
        else if (currentPosition.x > targetPosition.x)
            direction = Vector3Int.left; // Move left
        else if (currentPosition.y < targetPosition.y) // Then vertical movement (y-axis)
            direction = Vector3Int.up; // Move up
        else if (currentPosition.y > targetPosition.y)
            direction = Vector3Int.down; // Move down

        return direction;
    }
}
