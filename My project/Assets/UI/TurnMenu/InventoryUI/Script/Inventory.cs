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
        for (int i = 0; i < slots.Length; i++)
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
        ItemInfo tmp = ItemInfoUI.GetComponent<ItemInfo>();
        tmp.useButton.SetActive(true);
        tmp.discardButton.SetActive(true);
        tmp.unequidButton.SetActive(false);
        tmp.itemName.text = slots[n].item.itemName;
        tmp.img.sprite = slots[n].item.itemImage;
        tmp.description.text = slots[n].item.itemDescription;
        switch (slots[n].item.itemType)
        {
            case Item.ItemType.Weapon:
                tmp.type.text = "무기";
                tmp.UseText.text = "장착";
                break;
            case Item.ItemType.Armor:
                tmp.type.text = "방어구";
                tmp.UseText.text = "장착";
                break;
            case Item.ItemType.Ring:
                tmp.type.text = "반지";
                tmp.UseText.text = "장착";
                break;
            case Item.ItemType.Potion:
                tmp.type.text = "포션(소모품)";
                tmp.UseText.text = "사용";
                break;
            case Item.ItemType.SpecialPotion:
                tmp.type.text = "비약(소모품)";
                tmp.UseText.text = "사용";
                break;
            case Item.ItemType.Scroll:
                tmp.type.text = "스크롤(소모품)";
                tmp.UseText.text = "사용";
                break;
            case Item.ItemType.Quest:
                tmp.type.text = "퀘스트 용품";
                tmp.UseText.text = "???";
                break;
            case Item.ItemType.ETC:
                tmp.type.text = "기타";
                tmp.UseText.text = "???";
                break;
            default:
                tmp.type.text = "???";
                tmp.UseText.text = "???";
                break;
        }
        ReloadInventory();
    }
    public void UseItem()
    {
        slots[nowItem].item.UseItem(playerList[target],nowItem);
        if (slots[nowItem].item.itemType==Item.ItemType.Potion|| slots[nowItem].item.itemType == Item.ItemType.SpecialPotion|| slots[nowItem].item.itemType == Item.ItemType.Scroll) playerList[target].GetComponent<Player>().RemoveItem(nowItem);
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
