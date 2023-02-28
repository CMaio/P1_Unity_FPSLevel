using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHealthSystem : MonoBehaviour,HealthSystem
{
    [SerializeField] private float health = 0.0f;
    [SerializeField] private float maxHealth = 1.0f;
    private DieFunction die;
    public void takeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            die();
        }
    }


    public void setDieFunction(DieFunction die)
    {
        this.die = die;
    }



    // Start is called before the first frame update
    public void Start()
    {
        health = maxHealth;
    }
}