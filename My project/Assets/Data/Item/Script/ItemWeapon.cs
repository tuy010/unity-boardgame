using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "New Item/Weapon")]
public class ItemWeapon : Item
{
    public ItemWeapon()
    {
        itemType = ItemType.Weapon;
    }

    [Header("value")]
    public int Str;
    public int Dex;
    public int Int;
    public int Luk;

    public override void UseItem(GameObject target)
    {
        //equid Weapon;
    }
}
