using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDamage : MonoBehaviour
{
    [SerializeField] float damage;

    private void OnEnable()
    {
        StartCoroutine(hide());
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealthSystem hs = other.GetComponent<PlayerHealthSystem>();

        if( hs != null)
        {
            hs.takeDamage(damage);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    IEnumerator hide()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
}
