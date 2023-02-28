using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingGalery : MonoBehaviour
{
    [SerializeField] private GameObject bananaManStart;
    [SerializeField] private List<GameObject> allBananaMans;
    [SerializeField] private GameObject galleryHUD;
    [SerializeField] private dummyUI textDummy;
    [SerializeField] private pointsUI textPoints;
    [SerializeField] private GameObject finishedGame;
    [SerializeField] private GameObject newZone;
    [SerializeField] private Animation doors;



    [Header("Points Gallery")]
    [SerializeField] private int points;
    [SerializeField] private int dummyHits;
    [SerializeField] private int dummyNoHits;
    int totalDummys;
    bool activeNewZone;

    private void Awake()
    {
        totalDummys = allBananaMans.Count;
        activeNewZone = false;
    }
    public void updatePoints(bool hit)
    {
        if (!hit)
        {
            dummyNoHits += 1;
        }else {
            dummyHits += 1;
            dummyNoHits += 1;
            points += 1;
        }

        textDummy.updateDummys(dummyHits,dummyNoHits);
        textPoints.updatePoints(points);
    }
    
    
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        galleryHUD.SetActive(true);
        bananaManStart.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            restart();
        }
    }

    private void restart()
    {
        dummyHits = 0;
        dummyNoHits = 0;
        points = 0;
        textDummy.updateDummys(dummyHits, dummyNoHits);
        textPoints.updatePoints(points);
        setOffBannanas();
        finishedGame.SetActive(false);
        bananaManStart.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        dummyHits = 0;
        dummyNoHits = 0;
        points = 0;
        textDummy.updateDummys(dummyHits, dummyNoHits);
        textPoints.updatePoints(points);
        finishedGame.SetActive(false);
        galleryHUD.SetActive(false);
        setOffBannanas();
    }

    private void setOffBannanas()
    {
        foreach (GameObject bm in allBananaMans)
        {
            bm.SetActive(false);
        }
    }

    public void finishGame()
    {
        if (dummyNoHits >= totalDummys)
        {
            finishedGame.SetActive(true);

            if (!activeNewZone && dummyHits > 4)
            {
                activeNewZone = true;
                newZone.SetActive(true);
                doors.Play();
            }
        }

    }
}
