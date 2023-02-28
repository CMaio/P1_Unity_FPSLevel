using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private UnityEvent restartEvent;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UnityEvent<float, float> updateHealth;
    [SerializeField] private UnityEvent<float, float> updateShield;

    private void Start()
    {
        GetComponent<PlayerHealthSystem>().setDieFunction(
            () =>
            {
                gameManager.GameOver();
                restartEvent.Invoke();
                updateHealth.Invoke(100.0f, 100.0f);
                updateShield.Invoke(100.0f, 100.0f);
            }
           );
    }
}

