using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    GameObject[] childObjects; //Ŭ����(GameObject)�� ��ü �迭 ����
    List<GameObject> nodeList = new List<GameObject> (); //��ü�� ��� ����Ʈ ���� ����

    void OnDrawGizmos() //�ð��� ������� ���� �Լ�, �׸� �� ����
    {
        Gizmos.color = Color.blue;

        GetNodeData();

        for(int i = 0; i < nodeList.Count; i++)
        {
            Vector3 currentPos = nodeList[i].transform.position;
            Gizmos.DrawSphere(currentPos, 1);
            if(i>0)
            {
                Vector3 prevPos = nodeList[i-1].transform.position;
                Gizmos.DrawLine(prevPos, currentPos);

            }
        }
    }

   void GetNodeData()
    {
        nodeList = GameObject.FindGameObjectWithTag("Sys").GetComponent<ObjectManagement>().nodeList;
    }
}
