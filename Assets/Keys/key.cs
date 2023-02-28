using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeyData", menuName = "ScriptableObjects/Key", order = 1)]
public class key : ScriptableObject
{
    [SerializeField] private string id;
    bool canOpenDoor(string id)
    {
        return (this.id.CompareTo(id) == 0);
    }

    //en otro script 
    //terurn keys contains(keyData)
}
