using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagement : MonoBehaviour
{
    public GameObject route;

    public List<GameObject> nodeList = new List<GameObject>();
    public List<GameObject> playerList = new List<GameObject>();
    public GameObject mainCamera;
    public GameObject topdownCamera;
    public GameObject itemList;


    void Start()
    {
        FindPlayers();
        FindNodes();
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
        int cnt = 0;
        foreach (GameObject t in tmp)
        {
            playerList.Add(t);
            cnt++;
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
}
