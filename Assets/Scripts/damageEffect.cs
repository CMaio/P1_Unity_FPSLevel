using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageEffect : MonoBehaviour
{
    [SerializeField] Animation anim;

    public void activeDamage()
    {
        anim.CrossFade("damageEffect", 0.3f);
    }
}
