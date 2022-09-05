using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EquidUI : MonoBehaviour
{
    public int target;
    List<GameObject> playerList = new List<GameObject>();

    [SerializeField] private Slot[] slots = new Slot[4];


    [SerializeField] private GameObject ItemInfoUI;
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
        for (int i = 0; i < 4; i++)
        {
            Item value = null;
            bool isUsed = playerList[target].GetComponent<Player>().equid.TryGetValue(i, out value);
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
        if (isShowInfo || isShowCheck) return;
        nowItem = n;
        isShowInfo = true;
        ItemInfoUI.SetActive(true);
        ItemInfoUI.GetComponent<ItemInfo>().useButton.SetActive(false);
        ItemInfoUI.GetComponent<ItemInfo>().discardButton.SetActive(false);
        ItemInfoUI.GetComponent<ItemInfo>().unequidButton.SetActive(true);
        ItemInfoUI.GetComponent<ItemInfo>().name.text = slots[n].item.itemName;
        ItemInfoUI.GetComponent<ItemInfo>().img.sprite = slots[n].item.itemImage;
        ItemInfoUI.GetComponent<ItemInfo>().description.text = slots[n].item.itemDescription;
        ItemInfoUI.GetComponent<ItemInfo>().unequidText.text = "해제";
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
            default:
                ItemInfoUI.GetComponent<ItemInfo>().type.text = "???";
                ItemInfoUI.GetComponent<ItemInfo>().UseText.text = "???";
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
