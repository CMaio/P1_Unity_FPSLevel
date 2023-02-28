using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveNextBanana : MonoBehaviour
{
    [SerializeField] List<GameObject> nextBanananMans;
    [SerializeField] float timeToWait;
    [SerializeField] shootingGalery ManagerGallery;

    private void OnEnable()
    {
        StartCoroutine(countToActive());
    }

    void Start()
    {
        GetComponent<DummyHealthSystem>().setDieFunction(
           () =>
           {
               activeNext(true);
           }
          );
    }

    void activeNext(bool hit)
    {
        ManagerGallery.updatePoints(hit);
        foreach (GameObject go in nextBanananMans)
        {
            go.SetActive(true);
        }
        ManagerGallery.finishGame();
        this.gameObject.SetActive(false);
    }


    IEnumerator countToActive()
    {
        yield return new WaitForSeconds(timeToWait);
        activeNext(false);
    }
}
