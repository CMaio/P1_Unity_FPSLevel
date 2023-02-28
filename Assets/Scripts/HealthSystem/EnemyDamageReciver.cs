using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReciver : MonoBehaviour,HealthSystem
{
    [SerializeField] EnemyHealthSystem healthManager;
    [SerializeField] int multiplayer = 1;
    public void takeDamage(float damage)
    {
        float totalDamage;
        totalDamage = damage * multiplayer;
        Debug.Log("Damage done: " + totalDamage + " in area: " + gameObject.name);
        healthManager.damageDone(totalDamage);
    }
}
