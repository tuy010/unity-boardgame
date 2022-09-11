using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public enum NodeType
    {
        Empty,
        TownNode_1p,
        TownNode_2p,
        TownNode_3p,
        TownNode_4p,
        MonsterNode,
        ChanceNode,
        PortalNode,
        CrossNode
    }
    [Header("LinkedNode")]
    public GameObject nextNode = null;
    public GameObject prevNode = null;
    public bool isCrossroad = false;
    public GameObject anotherNode = null;

    [Header ("Node type")]
    public NodeType nodeType;

    // Start is called before the first frame update
    void Start()
    {
        switch (nodeType)
        {
            case NodeType.TownNode_1p:
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case NodeType.TownNode_2p:
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                break ;
            case NodeType.TownNode_3p:
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case NodeType.TownNode_4p:
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case NodeType.MonsterNode:
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;
            case NodeType.ChanceNode:
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case NodeType.PortalNode:
                gameObject.GetComponent<Renderer>().material.color = Color.cyan;
                break;
            case NodeType.CrossNode:
                gameObject.GetComponent<Renderer>().material.color = Color.gray;
                break;
            default:
                break;
        }
    }
}
