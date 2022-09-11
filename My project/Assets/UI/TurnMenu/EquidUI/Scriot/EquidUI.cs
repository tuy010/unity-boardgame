using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EquidUI : MonoBehaviour
{
    public int target;
    List<GameObject> playerList = new List<GameObject>();

    [SerializeField] private Slot[] slots = new Slot[4];


    [SerializeField] private GameObject itemInfoUI;
    public bool isShowInfo = false;
    public bool isShowCheck = false;
    int nowItem = -1;

    void Start()
    {
        GetPlayerData();
    }

    void GetPlayerData()
    {
        playerList = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().playerList;
    }

    public void ReloadEquid()
    {
        Player target_Player = playerList[target].GetComponent<Player>();
        for (int i = 0, cnt = 0, size = target_Player.equid.Count; i < 4; i++)
        {
            if (cnt == size) { slots[i].item = null; continue; }
            Item value = null;
            bool isUsed = target_Player.equid.TryGetValue(i, out value);
            if (isUsed) { slots[i].item = value; cnt++; }
            else slots[i].item = null;
        }
        if (!isShowInfo)
        {
            nowItem = -1;
            isShowCheck = false;
            itemInfoUI.SetActive(false);
            //ItemCheckUI.SetActive(false);
        }
    }

    public void ShowInfo(int n)
    {
        if (isShowInfo || isShowCheck) return;
        nowItem = n;
        isShowInfo = true;       
        itemInfoUI.SetActive(true);
        ItemInfo itemInfoUI_ItemInfo = itemInfoUI.GetComponent<ItemInfo>();
        itemInfoUI_ItemInfo.useButton.SetActive(false);
        itemInfoUI_ItemInfo.discardButton.SetActive(false);
        itemInfoUI_ItemInfo.unequidButton.SetActive(true);
        itemInfoUI_ItemInfo.itemName.text = slots[n].item.itemName;
        itemInfoUI_ItemInfo.img.sprite = slots[n].item.itemImage;
        itemInfoUI_ItemInfo.description.text = slots[n].item.itemDescription;
        itemInfoUI_ItemInfo.unequidText.text = "해제";
        switch (slots[n].item.itemType)
        {
            case Item.ItemType.Weapon:
                itemInfoUI_ItemInfo.type.text = "무기";
                break;
            case Item.ItemType.Armor:
                itemInfoUI_ItemInfo.type.text = "방어구";
                break;
            case Item.ItemType.Ring:
                itemInfoUI_ItemInfo.type.text = "반지";
                break;
            default:
                itemInfoUI_ItemInfo.type.text = "???";
                itemInfoUI_ItemInfo.UseText.text = "???";
                break;
        }
        ReloadEquid();
    }
    public void CloseInfo()
    {
        isShowInfo = false;
        ReloadEquid();
    }

    public void UnequidItem()
    {
        playerList[target].GetComponent<Player>().UnequidItem(nowItem);
        CloseInfo();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
