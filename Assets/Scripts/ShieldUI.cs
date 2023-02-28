using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShieldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void updateShield(float actualShield, float totalShield)
    {
        text.text = actualShield + " / " + totalShield;
    }
}
