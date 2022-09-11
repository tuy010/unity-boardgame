using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeEvnet_Chance : MonoBehaviour
{
    GameObject target = null;
    bool isEvent = false;
    bool EventEnd = false;
    int turnStep = 0;

    public List<ChanceData> chanceDatas;
    int chanceAmount = -1;
    int chanceCode = -1;

    public GameObject EventUI;

    void Start()
    {
        chanceDatas = this.GetComponent<ChanceManager>().chanceDatas;
        chanceAmount = chanceDatas.Count;
    }

    void Update()
    {
        if (isEvent&&turnStep==0)
        {
            turnStep++;
            chanceCode = Random.Range(0, chanceAmount);
        }
        else if(isEvent&&turnStep==1)
        {
            EventUI.SetActive(true);
            EventUI.GetComponent<ChanceUI>().eventName.text = chanceDatas[chanceCode].chanceName;
            EventUI.GetComponent<ChanceUI>().script.text = chanceDatas[chanceCode].script;
            turnStep++;
            if (!chanceDatas[chanceCode].isOption)
            {
                EventUI.GetComponent<ChanceUI>().endButton.SetActive(true);
                EventUI.GetComponent<ChanceUI>().yesButton.SetActive(false);
                EventUI.GetComponent<ChanceUI>().noButton.SetActive(false);

                Event(chanceDatas[chanceCode], target);
            }
            else
            {
                EventUI.GetComponent<ChanceUI>().endButton.SetActive(false);
                EventUI.GetComponent<ChanceUI>().yesButton.SetActive(true);
                EventUI.GetComponent<ChanceUI>().yesText.text = chanceDatas[chanceCode].optionOne;
                EventUI.GetComponent<ChanceUI>().noButton.SetActive(true);
                EventUI.GetComponent<ChanceUI>().noText.text = chanceDatas[chanceCode].optionTwo;
            }
        }
        else if (isEvent && turnStep == 3)
        {
            EventUI.GetComponent<ChanceUI>().endButton.SetActive(true);
            EventUI.GetComponent<ChanceUI>().yesButton.SetActive(false);
            EventUI.GetComponent<ChanceUI>().noButton.SetActive(false);

            Event(chanceDatas[chanceCode], target);
            turnStep++;
        }
        else if(isEvent&&EventEnd&&turnStep==99)
        {
            isEvent = false;
            EventEnd = false;
            turnStep=0;
            target = null;
            GetComponent<TurnSystem>().turnProgress = TurnSystem.TurnProgress.TurnEnd;
            EventUI.SetActive(false);
        }
        
    }

    public void Chance(GameObject target = null)
    {
        isEvent = true;
        this.target = target;
        turnStep = 0;
    }

    public void ButtonChanceCheck()
    {
        turnStep = 99;
        EventEnd = true;
    }

    public void ButtonChanceYes()
    {
        chanceDatas[chanceCode].answer = true;
        turnStep=3;
    }
    public void ButtonChanceNo()
    {
        chanceDatas[chanceCode].answer = false;
        turnStep = 3;
    }

    void Event(ChanceData data, GameObject target)
    {
        if (!data.isOption)
        {
            switch (data.code)
            {
                case 1:
                    Event_1(target);
                    break;
                default:
                    break;
            }
        }
        else if (data.isOption)
        {
            switch (data.code)
            {
                case 0:
                    Event_0(target, data.answer);
                    break;
                default:
                    break;
            }
        }
    }

    void Event_0(GameObject target, bool answer)
    {
        if (answer)
        {
            EventUI.GetComponent<ChanceUI>().script.text = "포션을 마시자, 당신은 이전보다 강해진 느낌을 받습니다.\nLv + 1";
            target.GetComponent<Player>().lv += 1;
        }
        else if (!answer)
        {
            EventUI.GetComponent<ChanceUI>().script.text = "당신은 포션을 마시지 않고, 게이같이 도망쳤습니다.";
        }
    }
    void Event_1(GameObject target)
    {
        target.GetComponent<Player>().hp -= 10;
    }
}