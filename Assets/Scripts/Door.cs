using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animation anim;
    bool isOpen = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Player>() && !isOpen)
        {
            anim.CrossFade("door_Open");
        }
    }
}
