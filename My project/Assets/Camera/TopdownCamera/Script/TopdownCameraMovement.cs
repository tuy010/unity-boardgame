using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownCameraMovement : MonoBehaviour
{
    List<GameObject> playerList = new List<GameObject>();
    public int target = -1;

    void Start()
    {
        GetPlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == -1)
        {
            transform.position = new Vector3(0f, 125f, 0f);
        }
        else
        {
            transform.position = new Vector3(playerList[target].GetComponent<Transform>().position.x, 65f, playerList[target].GetComponent<Transform>().position.z);
        }
        MakePlayerTranslucent();
    }


    void GetPlayerData()
    {
        playerList = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().playerList;
    }

    void MakePlayerTranslucent()
    {
        if (gameObject.activeSelf)
        {
            foreach (GameObject player in playerList)
            {
                SpriteRenderer img_SpriteRenderer = player.GetComponent<Player>().img.GetComponent<SpriteRenderer>();
                Color color = img_SpriteRenderer.color;
                if(target != -1)
                {
                    if (playerList[target] != player)
                    {
                        img_SpriteRenderer.sortingOrder = 0;
                        color.a = 0.7f;
                    }
                    else
                    {
                        img_SpriteRenderer.sortingOrder = 1;
                        color.a = 1f;
                    }
                }
                else
                {
                    img_SpriteRenderer.sortingOrder = 0;
                    color.a = 1f;
                }
                img_SpriteRenderer.color = color;
            }
        }  
    }
}
