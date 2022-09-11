using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    GameObject[] childObjects; //Ŭ����(GameObject)�� ��ü �迭 ����
    public GameObject startNode;

    void OnDrawGizmos() //�ð��� ������� ���� �Լ�, �׸� �� ����
    {
        Gizmos.color = Color.blue;

        Queue<GameObject> startNodeStack = new Queue<GameObject>();
        startNodeStack.Enqueue(startNode);
        while (startNodeStack.Count > 0)
        {
            GameObject startnode = startNodeStack.Dequeue();
            Node startnode_Node = startnode.GetComponent<Node>();
            Vector3 nowPos = startnode.transform.position;
            Gizmos.DrawSphere(nowPos, 1);

            GameObject nextnode;
            if (startnode_Node.isCrossroad) nextnode = startnode_Node.anotherNode;
            else nextnode = startnode_Node.nextNode;
            Vector3 nextPos = nextnode.transform.position;

            Gizmos.DrawLine(nowPos, nextPos);

            GameObject nownode = nextnode;
            while(nownode != null)
            {
                Node nownode_Node = nownode.GetComponent<Node>();
                nowPos = nownode.transform.position;
                Gizmos.DrawSphere(nowPos, 1);
                if (nownode_Node.isCrossroad) startNodeStack.Enqueue(nownode);
                
                nextnode = nownode_Node.nextNode;
                if (nextnode == null) break;
                nextPos = nextnode.transform.position;
                Gizmos.DrawLine(nowPos, nextPos);

                if (nextnode == startnode) break;
                else nownode = nextnode;
            }
        }
    }

}
