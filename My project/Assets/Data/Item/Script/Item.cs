using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Ring,
        Potion,
        SpecialPotion,
        Scroll,
        Quest,
        ETC
    }
    [Header ("Item Info")]
    public int itemCode;
    public string itemName;
    [TextArea]
    public string itemDescription;
    public ItemType itemType;
    [Space]
    public Sprite itemImage;

    public abstract void UseItem(GameObject target);
}
