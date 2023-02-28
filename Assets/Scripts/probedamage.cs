using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class probedamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealthSystem ph = other.gameObject.GetComponent<PlayerHealthSystem>();
        if(ph != null)
            other.gameObject.GetComponent<PlayerHealthSystem>().takeDamage(2000.0f);
    }
}
