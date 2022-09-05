using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    [SerializeField]
    public List<Item> Item_List_tmp = new List<Item>();
    public Dictionary<int, Item> Item_Dictionary = new Dictionary<int, Item>();

    void Awake()
    {
        foreach (var item in Item_List_tmp)
        {
            Item_Dictionary.Add(item.itemCode, item);
            Debug.Log(item.itemCode + " : \"" + item.itemName + "\" Added.");
        }
    }

    public Item ItemCode(int i)
    {
        Item value;
        if (Item_Dictionary.TryGetValue(i, out value)) return value;
        else return null;
    }
}
