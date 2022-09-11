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
    [SerializeField] private int _hp;
    [SerializeField] private int _mp;
    [SerializeField] private int _exp;

    [HideInInspector]
    public int hp
    {
        get { return _hp; }
    }
    [HideInInspector]
    public int mp
    {
        get { return _mp; }
    }
    [HideInInspector]
    public int exp
    {
        get { return _exp; }
    }
    public override void UseItem(GameObject target, int slotnum)
    {
        Player target_Player= target.GetComponent<Player>();
        if(hp != 0) target_Player.hp += hp;
        if (hp != 0) target_Player.mp += mp;
        if (hp != 0) target_Player.exp += exp;
    }
}
