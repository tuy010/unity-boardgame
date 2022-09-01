using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int target;
    List<GameObject> playerList = new List<GameObject>();

    [SerializeField]
    private Slot[] slots;

    [SerializeField] private GameObject slotList;

    [SerializeField] private GameObject ItemInfoUI;
    //[SerializeField] private GameObject ItemCheckUI;
    public bool isShowInfo = false;
    public bool isShowCheck = false;
    int nowItem = -1;

    private void OnValidate()
    {
        slots = slotList.GetComponentsInChildren<Slot>();

    }

    void Start()
    {
        GetPlayerData();
    }

    void GetPlayerData()
    {
        playerList = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().playerList;
    }

    public void ReloadInventory()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            Item value = null;
            bool isUsed = playerList[target].GetComponent<Player>().inventory.TryGetValue(i, out value);
            if (isUsed) slots[i].item = value;
            else slots[i].item = null;
        }
        if (!isShowInfo)
        {
            nowItem = -1;
            isShowCheck = false;
            ItemInfoUI.SetActive(false);
            //ItemCheckUI.SetActive(false);
        }
    }
    public void ShowInfo(int n)
    {
        if (isShowInfo||isShowCheck) return;
        nowItem = n;
        isShowInfo = true;
        ItemInfoUI.SetActive(true);
        ItemInfoUI.GetComponent<ItemInfo>().name.text = slots[n].item.itemName;
        ItemInfoUI.GetComponent<ItemInfo>().img.sprite = slots[n].item.itemImage;
        ItemInfoUI.GetComponent<ItemInfo>().description.text = slots[n].item.itemDescription;
        switch (slots[n].item.itemType)
        {
            case Item.ItemType.Weapon:
                ItemInfoUI.GetComponent<ItemInfo>().type.text = "무기";
                break;
            case Item.ItemType.Armor:
                ItemInfoUI.GetComponent<ItemInfo>().type.text = "방어구";
                break;
            case Item.ItemType.Ring:
                ItemInfoUI.GetComponent<ItemInfo>().type.text = "반지";
                break;
            case Item.ItemType.Potion:
                ItemInfoUI.GetComponent<ItemInfo>().type.text = "포션(소모품)";
                break;
            case Item.ItemType.SpecialPotion:
                ItemInfoUI.GetComponent<ItemInfo>().type.text = "비약(소모품)";
                break;
            case Item.ItemType.Scroll:
                ItemInfoUI.GetComponent<ItemInfo>().type.text = "스크롤(소모품)";
                break;
            case Item.ItemType.Quest:
                ItemInfoUI.GetComponent<ItemInfo>().type.text = "퀘스트 용품";
                break;
            case Item.ItemType.ETC:
                ItemInfoUI.GetComponent<ItemInfo>().type.text = "기타";
                break;
            default:
                ItemInfoUI.GetComponent<ItemInfo>().type.text = "???";
                break;
        }
        ReloadInventory();
    }
    public void CloseInfo()
    {
        isShowInfo = false;
        ReloadInventory();
    }
    public void UseItem()
    {
        slots[nowItem].item.UseItem(playerList[target]);
        playerList[target].GetComponent<Player>().RemoveItem(nowItem);
        isShowInfo = false;
        ReloadInventory();
    }
    public void DiscardItem()
    {
        playerList[target].GetComponent<Player>().RemoveItem(nowItem);
        isShowInfo = false;
        ReloadInventory();
    }
}
