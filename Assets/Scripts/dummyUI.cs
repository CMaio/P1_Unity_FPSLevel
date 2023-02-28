using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dummyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void updateDummys(int loadedBullets,int unloadedbullets)
    {
        text.text = "Dummy's: " + loadedBullets + " / " + unloadedbullets;
    }
}
