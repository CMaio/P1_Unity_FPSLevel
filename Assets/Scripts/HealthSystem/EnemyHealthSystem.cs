using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    [SerializeField] private float health = 0.0f;
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private Enemy enemy;
    private DieFunction die;
    bool died = false;

    public void setDieFunction(DieFunction die)
    {
        this.die = die;
    }

    public void Start()
    {
        enemy = GetComponent<Enemy>();
        health = maxHealth;
    }

    public void damageDone(float damage)
    {
        health -= damage;
        if (isAlive())
        {
            enemy.SetHITstate();
        }
    }

    private bool isAlive()
    {
        if (health <= 0.0f && !died)
        {
            died = true;
            die();
            return false;
        }
        
        return true;
    }
}
