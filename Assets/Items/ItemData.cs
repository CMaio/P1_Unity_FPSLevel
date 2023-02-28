using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "itemType", menuName = "Items/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public enum typeItem { 
        AMMO,
        SHIELD,
        HEALTH,
        KEY
    }
    public int ammo;
    public int shield;
    public int health;

    public typeItem chooseItem;
   
}
