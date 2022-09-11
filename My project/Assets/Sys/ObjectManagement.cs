using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagement : MonoBehaviour
{
    public GameObject route;

    public List<GameObject> nodeList = new List<GameObject>();
    public Dictionary<int, GameObject> townNodeDic = new Dictionary<int, GameObject>();
    public List<GameObject> playerList = new List<GameObject>();
    public GameObject mainCamera;
    public GameObject topdownCamera;

    [Header ("Item"),SerializeField]
    private List<Item> Item_List_tmp = new List<Item>();
    private Dictionary<int, Item> Item_Dictionary = new Dictionary<int, Item>();

    void Awake()
    {
        foreach (var item in Item_List_tmp)
        {
            Item_Dictionary.Add(item.itemCode, item);
            Debug.Log(item.itemCode + " : \"" + item.itemName + "\" Added.");
        }
        FindNodes();
        FindPlayers();
        FindCamera();
    }
    void FindNodes()
    {
        nodeList.Clear();
        Transform[] tmpNode;
        tmpNode = route.GetComponentsInChildren<Transform>();

        foreach (Transform t in tmpNode)
        {
            if (t != route.transform)
            {
                nodeList.Add(t.gameObject);
                Node.NodeType tmp = t.gameObject.GetComponent<Node>().nodeType;
                if (tmp == Node.NodeType.TownNode_1p) townNodeDic.Add(1, t.gameObject);
                else if (tmp == Node.NodeType.TownNode_2p) townNodeDic.Add(2, t.gameObject);
                else if (tmp == Node.NodeType.TownNode_3p) townNodeDic.Add(3, t.gameObject);
                else if (tmp == Node.NodeType.TownNode_4p) townNodeDic.Add(4, t.gameObject);
            }
        }
    }
    /*
    List<GameObject> nodeList = new List<GameObject> ();
    void GetNodeData()
    {
        nodeList = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().nodeList;
    }
    */
    void FindPlayers()
    {
        playerList.Clear();
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Player");
        int cnt = 1;
        foreach (GameObject t in tmp)
        {
            playerList.Add(t);
            Player t_Player = t.GetComponent<Player>();
            t_Player.code = cnt;
            GameObject tmpNode;
            if (townNodeDic.TryGetValue(cnt++, out tmpNode)) { t_Player.townNode = tmpNode; t_Player.nowNode = tmpNode; }
        }

    }
    /*
     List<GameObject> playerList = new List<GameObject>();
     void GetPlayerData()
    {
        playerList = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().playerList;
    }
     */
    void FindCamera()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        topdownCamera = GameObject.FindGameObjectWithTag("TopdownCamera");

        mainCamera.SetActive(true);
        topdownCamera.SetActive(false);
    }
    /*
    public GameObject mainCamera;
    public GameObject topdownCamera;
    void GetCameraData()
    {
        mainCamera = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().mainCamera;
        topdownCamera = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().topdownCamera;
    }
    */
    /*
    GameObject itemList;

    void GetItemListData()
    {
        itemList = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().itemList;
    }
    */

    /////////////////////
    public Item ItemCode(int i)
    {
        Item value;
        if (Item_Dictionary.TryGetValue(i, out value)) return value;
        else return null;
    }

    
}
