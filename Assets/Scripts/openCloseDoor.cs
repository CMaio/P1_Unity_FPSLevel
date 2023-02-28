using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openCloseDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FPSController fps = other.gameObject.GetComponent<FPSController>();
        if (fps != null)
        {
            Debug.Log("entrarrrr");
            openDoor();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        FPSController fps = other.gameObject.GetComponent<FPSController>();
        if (fps != null)
        {
            closeDoor();
        }
    }
    void openDoor()
    {
        GetComponent<Animation>().Play("openDoorAuto_clip");
    }
    void closeDoor()
    {
        GetComponent<Animation>().Play("closeDoorAuto_clip");
    }
}
