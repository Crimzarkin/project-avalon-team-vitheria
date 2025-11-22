using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public GameObject itemPrefab;

    public bool Compare(Item other)
    {
        if (this.name == other.name)
        {
            return true;
        }
        return false;
    }
}


