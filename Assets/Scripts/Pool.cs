using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] GameObject objectToPool;
    [SerializeField] GameObject particleToPool;
    [SerializeField] List<GameObject> pooledObjects = new List<GameObject>();
     [SerializeField]  List<GameObject> particlePooledObjects = new List<GameObject>();
    [SerializeField] int poolSize;
    // Start is called before the first frame update
    void Start()
    {
        for(int i= 0; i < poolSize; i++)
        {
            GameObject go = Instantiate(objectToPool);
            go.SetActive(false);
            pooledObjects.Add(go);
            go.transform.SetParent(this.transform);

            GameObject gp = Instantiate(particleToPool);
            gp.SetActive(false);
            particlePooledObjects.Add(gp);
            gp.transform.SetParent(this.transform);
        }
    }

    // Update is called once per frame
    public GameObject activeObject(Vector3 pos, Quaternion rot,Transform newParent)
    {
        GameObject go = getPooledObject();
        if(go != null)
        {
            go.SetActive(true);
            go.transform.position = pos;
            go.transform.rotation = rot;
            go.transform.SetParent(newParent);
        }
        return go;
    }
    public GameObject activeParticleObject(Vector3 pos, Quaternion rot, Transform newParent)
    {
        GameObject gp = getParticlePooledObject();
        if (gp != null)
        {
            gp.SetActive(true);
            gp.transform.position = pos;
            gp.transform.rotation = rot;
            gp.transform.SetParent(newParent);
        }
        return gp;
    }

    private GameObject getPooledObject()
    {
        for (var i = pooledObjects.Count - 1; i > -1; i--)
        {
            if (pooledObjects[i] == null)
            {
                pooledObjects.RemoveAt(i);
            }
        }

        foreach (GameObject go in pooledObjects)
        {
            if (!go.activeInHierarchy) {
                if (go == null)
                {
                    pooledObjects.Remove(go);
                }
                else
                {
                    return go;
                }
            }
        }


        if(pooledObjects.Count < poolSize)
        {
            GameObject go = Instantiate(objectToPool);
            go.SetActive(false);
            pooledObjects.Add(go);
            go.transform.SetParent(this.transform);
            return go;
        }
        return null;
    }

    private GameObject getParticlePooledObject()
    {
        for (var i = particlePooledObjects.Count - 1; i > -1; i--)
        {
            if (particlePooledObjects[i] == null)
            {
                particlePooledObjects.RemoveAt(i);
            }
        }

        foreach (GameObject go in particlePooledObjects)
        {
            if (!go.activeInHierarchy)
            {
                return go;
            }
        }


        if (particlePooledObjects.Count < poolSize)
        {
            GameObject go = Instantiate(particleToPool);
            go.SetActive(false);
            pooledObjects.Add(go);
            go.transform.SetParent(this.transform);
            return go;
        }
        return null;
    }
}
