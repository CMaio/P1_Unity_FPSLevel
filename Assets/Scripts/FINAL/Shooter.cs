using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    //Variables-----------------------------------------
    /*[Header("")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform pointToShoot;*/

    [Header("Properties Bullet")]
    [SerializeField] float shootSpeed;
    [SerializeField] float maxDistance;
    [SerializeField] float bulletDamage;

    [Header("")]
    [SerializeField] Camera cam;
    [SerializeField] LayerMask shootLayerMask;

    [Header("Decals")]
    [SerializeField] Pool decalPool;
    [SerializeField] float zOfset;

    [Header("Reload")]
    [SerializeField] private int loadedBullets = 0;
    [SerializeField] private int unloadBullets = 60;
    [SerializeField] private int maxLoadedBullets = 10;
    [SerializeField] private int maxUnloadedBullets = 100;
    [SerializeField] private KeyCode reloadKey = KeyCode.R;
    [SerializeField] private UnityEvent<int, int> ammoChanged;

    [Header("Anim")]
    [SerializeField] private Animation anim;

    [Header("Audio")]
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioClip noAmmo;
    [SerializeField] private AudioClip Shoot;


    //Functions------------------------------------------
    private void Start()
    {
        ammoChanged.Invoke(loadedBullets, unloadBullets);
        audioPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(reloadKey))
        {
            reloadWeapon();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (loadedBullets > 0)
            {
                loadedBullets--;
                shootByRaycast();
            }
            else
            {
                cannotShoot();
            }
        }
    }
    void reloadWeapon()
    {
        int actualBullets = maxLoadedBullets - loadedBullets;
        int toLoad = Mathf.Min(unloadBullets, actualBullets);
        loadedBullets += toLoad;
        unloadBullets -= toLoad;
        ammoChanged.Invoke(loadedBullets, unloadBullets);
        anim.CrossFade("reload_clip", 0.3f);
        anim.CrossFadeQueued("idle_clip", 0.3f);
    }
    void cannotShoot()
    {
        anim.CrossFade("noammo_clip", 0.3f);
        anim.CrossFadeQueued("idle_clip", 0.3f);
        audioPlayer.clip = noAmmo;
        audioPlayer.Play();
        Debug.Log("No pots disparar sense bales, carrega prement R");
    }

    void shootByRaycast()
    {
        Ray r = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hitInfo;
        if(Physics.Raycast(r, out hitInfo, maxDistance, (int)shootLayerMask))
        {
            HealthSystem hs = hitInfo.transform.gameObject.GetComponent<HealthSystem>();
            if (hs != null)
            {
                hs.takeDamage(bulletDamage);
            }
            decalPool.activeObject(hitInfo.point + (hitInfo.normal * zOfset), Quaternion.LookRotation(hitInfo.normal),hitInfo.transform);
            decalPool.activeParticleObject(hitInfo.point + (hitInfo.normal * zOfset), Quaternion.LookRotation(hitInfo.normal),hitInfo.transform);
        }
        ammoChanged.Invoke(loadedBullets, unloadBullets);
        anim.CrossFade("shoot_clip", 0.3f);
        anim.CrossFadeQueued("idle_clip", 0.3f);
        audioPlayer.clip = Shoot;
        audioPlayer.Play();

    }

    public bool addBullets(int numBullets)
    {
        if (unloadBullets >= maxUnloadedBullets) { return false; }
        unloadBullets += numBullets;
        unloadBullets = Mathf.Min(unloadBullets, maxUnloadedBullets);
        ammoChanged.Invoke(loadedBullets, unloadBullets);
        return true;
    }
}
