using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private AudioSource audioItem;
    [SerializeField] private List<MeshRenderer> itemsToHide;
    [SerializeField] private List<GameObject> ParticlesToHide;

    private void Awake()
    {
        audioItem = GetComponent<AudioSource>();
    }

    public void consume(GameObject consumer)
    {
        switch(itemData.chooseItem)
        {
            case ItemData.typeItem.AMMO:      
                fillAmmo(consumer);
                break;
            case ItemData.typeItem.SHIELD:
                restoreShield(consumer);
                break;
            case ItemData.typeItem.HEALTH:
                restoreHealth(consumer);
                break;
            case ItemData.typeItem.KEY:
                getKey(consumer);
                break;
        }


    }
    private void getKey(GameObject consumer)
    {
        FPSController sh = consumer.GetComponent<FPSController>();
        if (sh != null)
        {
            sh.setKey();
            StartCoroutine(playAudio()); 
        }
    }
    private void fillAmmo(GameObject consumer)
    {
        Shooter sh = consumer.GetComponent<Shooter>();
        if (sh != null)
        {
            if (sh.addBullets(itemData.ammo)) { StartCoroutine(playAudio()); }
        }
    }

    private void restoreShield(GameObject consumer)
    {
        PlayerHealthSystem hs = consumer.GetComponent<PlayerHealthSystem>();
        if (hs != null)
        {
            if (hs.restoreShield(itemData.shield)) { StartCoroutine(playAudio()); }
        }
    }

    private void restoreHealth(GameObject consumer)
    {
        PlayerHealthSystem hs = consumer.GetComponent<PlayerHealthSystem>();
        if (hs != null)
        {
            if (hs.restoreHealth(itemData.health)) { StartCoroutine(playAudio()); }
        }
    }


    IEnumerator playAudio()
    {
        audioItem.Play();
        GetComponent<BoxCollider>().enabled = false;
        foreach(MeshRenderer mr in itemsToHide) { mr.enabled = false; }
        foreach(GameObject pr in ParticlesToHide) { pr.SetActive(false); }
        yield return new WaitForSecondsRealtime(audioItem.clip.length);
        Destroy(this.transform.parent.gameObject);
    }
}
