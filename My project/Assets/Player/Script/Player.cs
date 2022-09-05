using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <Camera>
    public GameObject mainCamera;
    public GameObject topdownCamera;
    public bool isTopdownView = false;
    public bool isBottomdownView = false;

    public GameObject itemList;

    /// <PlayerObject>
    public GameObject img;

    /// <PlayerData>
    public int code;
    public string nickname;

    public int hp = 100;
    public int hpMax = 100;
    public int mp = 100;
    public int mpMax = 100;

    public int lv = 1;
    public int exp = 0;
    public int expMax = 100;

    public int str_player = 10;
    public int dex_player = 10;
    public int int_player = 10;
    public int luk_player = 10;

    public int str_up = 0;
    public int dex_up = 0;
    public int int_up = 0;
    public int luk_up = 0;

    public int str_total = 0;
    public int dex_total = 0;
    public int int_total = 0;
    public int luk_total = 0;

    public int money = 100;
    //
    public int lap = 0;
    public GameObject nowNode;
    public bool isturn = false;

    public Dictionary<int, Item> inventory = new Dictionary<int, Item>(); //0~14

    public Dictionary<int, Item> equid = new Dictionary<int, Item>();

    void Start()
    {
        GetCameraData();
        GetItemListData();

        lv = 1;
        exp = 0;

        str_player = 10;
        dex_player = 10;
        int_player = 10;
        luk_player = 10;

        money = 100;
    
        lap = 0;
        isturn = false;

        CalStatus();

        hpMax = 80 + 20 * lv + str_player * 4;
        mpMax = 90 + 20 * lv + int_player * 4;
        expMax = 90 + 10 * lv;

        hp = hpMax;
        mp = mpMax;


        inventory.Add(0, itemList.GetComponent<ItemList>().ItemCode(100));
        inventory.Add(1, itemList.GetComponent<ItemList>().ItemCode(101));
        inventory.Add(2, itemList.GetComponent<ItemList>().ItemCode(0));
        inventory.Add(3, itemList.GetComponent<ItemList>().ItemCode(0));
    }

    // Update is called once per frame
    void Update()
    {
        
        RotateImg();
        CalStatus();
        hpMax = 80 + 20 * lv + str_total * 4;
        mpMax = 90 + 20 * lv + int_total * 4;
        expMax = 90 + 10 * lv;

        if (hp > hpMax) hp = hpMax;
        if (mp > mpMax) mp = mpMax;
    }

    //start
    void GetCameraData()
    {
        mainCamera = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().mainCamera;
        topdownCamera = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().topdownCamera;
    }
    void GetItemListData()
    {
        itemList = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().itemList;
    }

    //update
    void RotateImg()
    {
        if (mainCamera.activeSelf && !topdownCamera.activeSelf && isTopdownView)
        {
            img.GetComponent<SpriteRenderer>().flipX = true;
            img.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            img.transform.position = new Vector3(img.transform.position.x, 6.18f, img.transform.position.z);
            isTopdownView = false;
        }
        else if (!mainCamera.activeSelf && topdownCamera.activeSelf && !isTopdownView)
        {
            img.transform.rotation = Quaternion.Euler(90f, 0, 45f);
            img.transform.position = new Vector3(img.transform.position.x, 0.7f, img.transform.position.z);
            img.GetComponent<SpriteRenderer>().flipX = false;
            img.transform.localScale = Vector3.one;
            isTopdownView = true;
        }
        if(!isTopdownView)
        {
            img.transform.LookAt(mainCamera.transform.position);
        }
            
    }
    void CalStatus()
    {
        str_up = 0;
        dex_up = 0;
        int_up = 0;
        luk_up = 0;

        Item tmp;
        if (equid.TryGetValue(0, out tmp))
        {
            ItemWeapon a = (ItemWeapon)tmp;
            str_up += a.str;
            dex_up += a.dex;
            int_up += a.intel;
            luk_up += a.luk;
        }


        str_total = str_player + str_up;
        dex_total = dex_player + dex_up;
        int_total = int_player + int_up;    
        luk_total = luk_player + dex_up;
    }

    //functions
    public void RemoveItem(int i)
    {
        inventory.Remove(i);
    }
    public void EquidItem(int i)
    {
        Item newEquidment;
        Item oldEquidment;
        bool isAlreadyEquid = false;
        int slot = -1;

        if (!inventory.TryGetValue(i, out newEquidment)) return;
        
        switch(newEquidment.itemType)
        {
            case Item.ItemType.Weapon:
                slot = 0;
                if (equid.TryGetValue(slot, out oldEquidment))
                {
                    equid.Remove(slot);
                    isAlreadyEquid = true;
                }
                break;
            case Item.ItemType.Armor:
                slot = 1;
                if (!equid.TryGetValue(slot, out oldEquidment))
                {
                    equid.Remove(slot);
                    isAlreadyEquid = true;
                }
                break;
            case Item.ItemType.Ring:
                if(equid.TryGetValue(2, out oldEquidment))
                {
                    if(equid.TryGetValue(3, out oldEquidment))
                    {
                        Debug.Log("two ring");
                        return;
                    }
                    else
                    {
                        slot = 3;
                    }
                }
                else
                {
                    slot = 2;
                }
                break;
            default:    
                return;
        }


        inventory.Remove(i);
        equid.Remove(slot);
        equid.Add(slot, newEquidment);
        if (isAlreadyEquid)
        {
            inventory.Add(i, oldEquidment);
        }
    }
    public void UnequidItem(int slot)
    {
        int emptyslot=-1;
        for(int i = 0; i < 15; i++)
        {
            if (!inventory.ContainsKey(i))
            {
                emptyslot = i;
                break;
            } 
            if(i == 14)
            {
                Debug.Log("!");
                return;
            }
        }
        Item tmp;
        if (emptyslot != -1&& equid.TryGetValue(slot, out tmp))
        {
            inventory.Add(emptyslot, tmp);
            equid.Remove(slot);
        }
    }
}
