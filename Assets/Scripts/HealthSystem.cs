using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int health, EHealth = 10;
    public GameObject HealthBar, EHealthBar;

    public void TakeDamage(int Damage, string tag)
    {
        if (tag == "player")
        {
            health -= Damage;

            health = Mathf.Clamp(health, 0, 10);
            HealthBar.transform.localScale = new Vector3( (float)health / 10, 1.0f, 1.0f);
        }

        if (tag == "enemy")
        {
            EHealth -= Damage;

            EHealth = Mathf.Clamp(EHealth, 0, 10);
            EHealthBar.transform.localScale = new Vector3((float)EHealth / 10, 1.0f, 1.0f);
        }
    }
}
