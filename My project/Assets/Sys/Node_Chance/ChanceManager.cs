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
        chanceDatas[0] = new ChanceData(0, "레벨업!","당신은 동굴에서 의문의 비약을 찾았다!", true,"마신다","마시지 않는다");
        chanceDatas[1] = new ChanceData(1, "아야!", "다쳤엉\nHp - 10", false);
    }

}


public class ChanceData : ScriptableObject
{
    public int code;
    public string name;
    public string script;

    public bool isOption;
    //false = 강제발생
    //true = 선택지
    public bool answer;
    //true = 수락
    //false = 거절
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


