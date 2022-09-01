using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public enum NodeType
    {
        TownNode,
        MonsterNode,
        ChanceNode,
    }
    [Header ("LinkedNode")]
    public GameObject nextNode;
    public GameObject prevNode;
    public bool isCrossroad = false;
    public GameObject anotherNode;

    [Header ("Node type")]
    public NodeType nodeType;

    //const int TOWNNODE = 0;
    //const int BATTLENODE = 1;
    //const int CHANCENODE = 2;
    //const int BOXNODE = 3;


    // Start is called before the first frame update
    void Start()
    {
        switch (nodeType)
        {
            case NodeType.TownNode:
                break;
            case NodeType.MonsterNode:
                break;
            case NodeType.ChanceNode:
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
