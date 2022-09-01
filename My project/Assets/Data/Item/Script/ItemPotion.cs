using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

[CreateAssetMenu(fileName = "New Potion", menuName = "New Item/Potion")]
public class ItemPotion : Item
{
    public ItemPotion()
    {
        itemType = ItemType.Potion;
    }

    [Header("value")]
    public int hp;
    public int mp;
    public int exp;

    public override void UseItem(GameObject target)
    {
        if(hp != 0)target.GetComponent<Player>().hp += hp;
        if (hp != 0) target.GetComponent<Player>().mp += mp;
        if (hp != 0) target.GetComponent<Player>().exp += exp;
    }
}
