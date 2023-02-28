using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthSystem : MonoBehaviour, HealthSystem
{
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private float shield = 0.0f;
    [SerializeField] private float maxShield = 100.0f;
    private DieFunction die;

    [SerializeField] private UnityEvent<float, float, bool> damageHealthEvent;
    [SerializeField] private UnityEvent<float, float, bool> damageShieldEvent;
    [SerializeField] private UnityEvent<float, float> updateHealth;
    [SerializeField] private UnityEvent<float, float> updateShield;
    [SerializeField] private UnityEvent damageEfectEvent;


    private void Start()
    {
        updateHealth.Invoke(health, maxHealth);
        updateShield.Invoke(shield, maxShield);
    }


    public void setDieFunction(DieFunction die)
    {
        this.die = die;
    }

    public void OnEnable()
    {
        health = maxHealth;
        shield = maxShield;
    }

    public void restart()
    {
        health = maxHealth;
        shield = maxShield;
    }

    public void takeDamage(float damage)
    {
        if (shield > 0.0f)
        {
            float damageShield;
            float damageHealth;
            damageShield = damage * 0.75f;
            damageHealth = damage - damageShield;

            if (damageShield > shield)
            {
                damageShieldEvent.Invoke(shield, damageShield, false);
                damageHealth += damageShield - shield;
                shield = 0.0f;
                updateShield.Invoke(shield, maxShield);
            }
            else
            {
                damageShieldEvent.Invoke(shield, damageShield, false);
                shield -= damageShield;
                updateShield.Invoke(shield, maxShield);
            }

            damageHealthEvent.Invoke(health, damageHealth, false);
            damageEfectEvent.Invoke();
            health -= damageHealth;
            updateHealth.Invoke(health, maxHealth);

        }
        else
        {
            damageHealthEvent.Invoke(health, damage, false);
            damageEfectEvent.Invoke();
            health -= damage;
            updateHealth.Invoke(health, maxHealth);
        }
        
        isAlive();
    }

    public bool restoreShield(int shieldItem)
    {
        if (shield >= maxShield) { return false; }
        shield += shieldItem;
        shield = Mathf.Min(shield, maxShield);
        updateShield.Invoke(shield, maxShield);
        damageShieldEvent.Invoke(shield, shield, true);
        damageShieldEvent.Invoke(shield, maxShield, false);
        return true;
    }

    public bool restoreHealth(int healthItem)
    {
        if (health >= maxHealth) { return false; }
        health += healthItem;
        health = Mathf.Min(health, maxHealth);
        updateHealth.Invoke(health, maxHealth);
        damageHealthEvent.Invoke(health, health, true);
        damageHealthEvent.Invoke(health, maxHealth, false);

        return true;
    }

    public void isAlive()
    {
        if (health <= 0.0f)
        {
            die();
        }
    }
}
