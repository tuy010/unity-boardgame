using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ChanceManager : MonoBehaviour
{
    const int EVENTAMOUNT = 2;
    public List<ChanceData> chanceDatas;

    void Start()
    {
        chanceDatas[0] = new ChanceData(0, "������!","����� �������� �ǹ��� ����� ã�Ҵ�!", true,"���Ŵ�","������ �ʴ´�");
        chanceDatas[1] = new ChanceData(1, "�ƾ�!", "���ƾ�\nHp - 10", false);
    }

}


public class ChanceData : ScriptableObject
{
    public int code;
    public string name;
    public string script;

    public bool isOption;
    //false = �����߻�
    //true = ������
    public bool answer;
    //true = ����
    //false = ����
    public string optionOne;
    public string optionTwo;


    public ChanceData(int code, string name, string script, bool isOption = false)
    {
        this.code = code;
        this.name = name;
        this.script = script;
        this.isOption = false;
    }

    public ChanceData(int code, string name, string script, bool isOption, string optionOne, string optionTwo) : this(code, name, script)
    {
        this.isOption = isOption;
        this.optionOne = optionOne;
        this.optionTwo = optionTwo;
    }
}


