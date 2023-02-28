using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ammoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void updateAmmo(int loadedBullets, int unloadedBullets)
    {
        text.text = loadedBullets + " / " + unloadedBullets;
    }
}
