using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    private bool playerTurn = true; // Indicates if it's the player's turn
    private EnemyMovement[] enemies; // References to all enemies

    void Start()
    {
        // Find all enemies in the scene
        enemies = FindObjectsOfType<EnemyMovement>();
    }

    public void PlayerFinishedTurn()
    {
        playerTurn = false;

        // Trigger all enemies to take their turn
        foreach (var enemy in enemies)
        {
            enemy.TakeTurn();
        }

        // Once all enemies finish their turn, return control to the player
        Invoke(nameof(StartPlayerTurn), 0.1f); // Small delay for smoother transitions
    }

    void StartPlayerTurn()
    {
        playerTurn = true;
    }

    public bool IsPlayerTurn()
    {
        return playerTurn;
    }
}
