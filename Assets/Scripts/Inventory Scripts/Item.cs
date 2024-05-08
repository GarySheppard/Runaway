using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    [Header("Properties")]
    public int id;
    public string itemName;
    //for pistol, ammo, bat = 0, and grenade and shuriken is the number of them
    public int value;
    public Sprite icon;
    public ItemType itemType;
}

public enum ItemType{Bat, Pistol, Grenade, Shuriken}
