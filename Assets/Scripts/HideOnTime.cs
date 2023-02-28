using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnTime : MonoBehaviour
{
    [SerializeField] float m_TimeToHide;
    [SerializeField] GameObject PoolDecals;
    private void OnEnable()
    {
        PoolDecals = GameObject.Find("DecalPool");
        StartCoroutine(HideObject());
    }

    IEnumerator HideObject()
    {
        yield return new WaitForSeconds(m_TimeToHide);
        transform.SetParent(PoolDecals.transform);
        this.gameObject.SetActive(false);
    }
}
