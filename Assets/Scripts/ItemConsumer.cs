using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConsumer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            item.consume(gameObject);
        }

    }
}
