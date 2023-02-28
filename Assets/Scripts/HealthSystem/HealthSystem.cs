using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void DieFunction();

public interface HealthSystem
{
    public void takeDamage(float damage);
}
