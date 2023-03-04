using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "New Weapon", menuName = "New Item/Weapon")]
public class ItemWeapon : Item
{
    public ItemWeapon()
    {
        itemType = ItemType.Weapon;
    }

    [Header("value")]
    [SerializeField] private int _str;
    [SerializeField] private int _dex;
    [SerializeField] private int _int;
    [SerializeField] private int _luk;

    [HideInInspector]
    public int str
    {
        get { return _str; }
    }
    [HideInInspector]
    public int dex
    {
        get { return _dex; }
    }
    [HideInInspector]
    public int intel
    {
        get { return _int; }
    }
    [HideInInspector]
    public int luk
    {
        get { return _luk; }
    }

    public override void UseItem(GameObject target,int slotnum)
    {
        target.GetComponent<Player>().EquidItem(slotnum);
        //equid Weapon;
    }
}
