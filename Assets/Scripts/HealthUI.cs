using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void updateHealth(float actualHealth, float totalHealth)
    {
        text.text = actualHealth + " / " + totalHealth;
    }
}
