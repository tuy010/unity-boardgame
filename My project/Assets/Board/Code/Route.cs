using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    GameObject[] childObjects; //클래스(GameObject)의 객체 배열 생성
    List<GameObject> nodeList = new List<GameObject> (); //객체를 담는 리스트 동적 생성

    void OnDrawGizmos() //시각적 디버깅을 위한 함수, 그림 툴 느낌
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
