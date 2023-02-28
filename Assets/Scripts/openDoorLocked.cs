using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoorLocked : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FPSController fps = other.gameObject.GetComponent<FPSController>();
        if (fps != null)
        {
            if (fps.getKey())
                GetComponent<Animation>().Play();
        }
    }
}
