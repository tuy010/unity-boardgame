using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManagement : MonoBehaviour
{
    List<GameObject> playerList = new List<GameObject>();
    List<GameObject> nodeList = new List<GameObject>();
    
    public List<GameObject> overlappingNodes = new List<GameObject>();
    public List<GameObject> activatedNodes = new List<GameObject>();


    void Start()
    {
        GetPlayerData();
        GetNodeData();
    }

    // Update is called once per frame
    void Update()
    {
        ChangePlayersPosition();
    }

    void GetPlayerData()
    {
        playerList = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().playerList;
    }
    void GetNodeData()
    {
        nodeList = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().nodeList;
    }

    void ChangePlayersPosition()
    {
        overlappingNodes.Clear();
        activatedNodes.Clear();

        foreach (GameObject t in playerList)
        {
            bool isoverlap = false;
            for (int i = 0; i < activatedNodes.Count; i++)
            {
                if (activatedNodes[i] == t.GetComponent<Player>().nowNode)
                {
                    isoverlap = true;
                    if (overlappingNodes.Count == 0)
                    {
                        overlappingNodes.Add(t.GetComponent<Player>().nowNode);
                    }
                    else
                    {
                        for (int j = 0; j < overlappingNodes.Count; j++)
                        {
                            if (overlappingNodes[j] == t.GetComponent<Player>().nowNode) break;
                            else if ((j+1) == overlappingNodes.Count) overlappingNodes.Add(t.GetComponent<Player>().nowNode);
                        }
                    }
                }
            }
            if (!isoverlap) activatedNodes.Add(t.GetComponent<Player>().nowNode);
        }


        foreach (GameObject i in activatedNodes)
        {
            bool isoverlap = false;
            if(overlappingNodes.Count != 0)
            {
                foreach (GameObject j in overlappingNodes)
                {
                    if (i == j)
                    {
                        List<GameObject> overlapPlayer = new List<GameObject>();
                        foreach (GameObject t in playerList)
                        {
                            if (t.GetComponent<Player>().nowNode == i) overlapPlayer.Add(t);
                        }
                        int cnt = 0;
                        foreach (GameObject t in overlapPlayer)
                        {
                            Vector3 prevPos = t.transform.position;
                            Vector3 newPos = i.transform.position;
                            switch (cnt)
                            {
                                case 0:
                                    newPos.x -= 5;
                                    break;
                                case 1:
                                    newPos.x += 5;
                                    break;
                                case 2:
                                    newPos.z -= 5;
                                    break;
                                case 3:
                                    newPos.z += 5;
                                    break;
                            }
                            t.transform.position = Vector3.MoveTowards(prevPos, newPos, 100f * Time.deltaTime);
                            cnt++;
                        }
                        isoverlap = true;
                    }
                }
            }
            if(!isoverlap)
            {
                List<GameObject> notoverlapPlayer = new List<GameObject>();
                foreach (GameObject t in playerList)
                {
                    if (t.GetComponent<Player>().nowNode == i) notoverlapPlayer.Add(t);
                }
                foreach (GameObject t in notoverlapPlayer)
                {
                    Vector3 prevPos = t.transform.position;
                    Vector3 newPos = i.transform.position;
                    t.transform.position = Vector3.MoveTowards(prevPos, newPos, 100f*Time.deltaTime);
                }
            }
        }
    }
}

