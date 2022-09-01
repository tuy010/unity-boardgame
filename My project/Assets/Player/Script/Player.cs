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

    public int str = 10;
    public int dex = 10;
    public int intel = 10;
    public int luk = 10;

    public int money = 100;
    //
    public int lap = 0;
    public GameObject nowNode;
    public bool isturn = false;

    public Dictionary<int, Item> inventory = new Dictionary<int, Item>();

    void Start()
    {
        GetCameraData();

        lv = 1;
        exp = 0;

        str = 10;
        dex = 10;
        intel = 10;
        luk = 10;

        money = 100;
    
        lap = 0;
        isturn = false;

        hpMax = 80 + 20 * lv + str * 4;
        mpMax = 90 + 20 * lv + intel * 4;
        expMax = 90 + 10 * lv;

        hp = hpMax;
        mp = mpMax;

        inventory.Add(1, ItemCode(0));
    }

    // Update is called once per frame
    void Update()
    {
        
        RotateImg();
        hpMax = 80 + 20 * lv + str*4;
        mpMax = 90 + 20 * lv + intel*4;
        expMax = 90 + 10 * lv;

        if (hp > hpMax) hp = hpMax;
        if (mp > mpMax) mp = mpMax;
    }

    void GetCameraData()
    {
        mainCamera = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().mainCamera;
        topdownCamera = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().topdownCamera;
    }
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

    Item ItemCode(int i)
    {
        if (GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().itemList.GetComponent<ItemList>().Items.Count > i) return GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().itemList.GetComponent<ItemList>().Items[i];
        else return null;
    }

    public void RemoveItem(int i)
    {
        inventory.Remove(i);
    }
}
