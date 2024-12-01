using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 10;
    public GameObject HealthBar;

    public void TakeDamage(int Damage)
    {
        health -= Damage;

        health = Mathf.Clamp(health, 0, 10);
        HealthBar.transform.localScale = new Vector3( (float)health / 10, 0.1f, 0.1f);
    }
}
