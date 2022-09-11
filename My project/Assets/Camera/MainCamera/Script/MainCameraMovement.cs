using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMovement : MonoBehaviour
{
    List<GameObject> playerList = new List<GameObject>();

    public int target = -1;
    // Start is called before the first frame update
    void Start()
    {
        GetPlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == -1)
        {
            transform.position = new Vector3(65f, 16f, -90f);
            transform.rotation = Quaternion.Euler(24f, -23f, 0f);
        }
        else
        {
            int tmp = 1;
            Transform player_Transform = playerList[target].GetComponent<Transform>();
            if (tmp < 10)
            {              
                transform.position = new Vector3(player_Transform.position.x + 10f, 6f, player_Transform.position.z - 10f);
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (tmp >= 10 && tmp < 19)
            {
                transform.position = new Vector3(player_Transform.position.x - 10f, 6f, player_Transform.position.z - 10f);
                transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            }
            else if (tmp >= 19 && tmp < 28)
            {
                transform.position = new Vector3(player_Transform.position.x - 10f, 6f, player_Transform.position.z + 10f);
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else if (tmp >= 28 && tmp < 37)
            {
                transform.position = new Vector3(player_Transform.position.x + 10f, 6f, player_Transform.position.z + 10f);
                transform.rotation = Quaternion.Euler(0f, 270f, 0f);
            }
            MakePlayerTranslucent();
        }    
    }

    void GetPlayerData()
    {
        playerList = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().playerList;
    }

    void MakePlayerTranslucent()
    {
        GameObject targetNode = playerList[target].GetComponent<Player>().nowNode;
        foreach (GameObject player in playerList)
        {
            GameObject playerNode = player.GetComponent<Player>().nowNode;
            if (player == null) break;
            SpriteRenderer img_SpriteRenderer = player.GetComponent<Player>().img.GetComponent<SpriteRenderer>();
            Color color = img_SpriteRenderer.color;
            if (playerList[target] != player)
            {
                img_SpriteRenderer.sortingOrder = 0;
                Node playerNode_Node = playerNode.GetComponent<Node>();
                if (targetNode == playerNode) color.a = 0.2f;
                else if (targetNode == playerNode_Node.prevNode|| targetNode == playerNode_Node.nextNode || targetNode == playerNode_Node.anotherNode) color.a = 0.5f;
                else color.a = 1f;
            }
            else
            {
                img_SpriteRenderer.sortingOrder = 1;
                color.a = 1f;
            }
            img_SpriteRenderer.color = color;
        }
    }

}
