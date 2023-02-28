using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pointsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void updatePoints(int loadedBullets)
    {
        text.text = "Points: "+ loadedBullets;
    }
}
