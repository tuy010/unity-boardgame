using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusUI : MonoBehaviour
{
    public Slider hpUI;
    public Slider mpUI;
    public Slider expUI;

    public TextMeshProUGUI nicknameUI;
    public TextMeshProUGUI lvUI;

    public TextMeshProUGUI hptext;
    public TextMeshProUGUI mptext;
    public TextMeshProUGUI exptext;

    public TextMeshProUGUI strUI;
    public TextMeshProUGUI dexUI;
    public TextMeshProUGUI intelUI;
    public TextMeshProUGUI lukUI;

    public TextMeshProUGUI moneyUI;

    public string nickname = "player";

    public int hp;
    public int hpMax;
    public int mp;
    public int mpMax;

    public int lv;
    public int exp;
    public int expMax;

    public int str_player;
    public int dex_player;
    public int int_player;
    public int luk_player;

    public int str_up;
    public int dex_up;
    public int int_up;
    public int luk_up;

    public int money;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
        {
            nicknameUI.text = nickname;
            lvUI.text = "LV : "+lv;

            strUI.text = "STR: "+(str_player + str_up)+" (" +str_player + "+"+ str_up+")";
            dexUI.text = "DEX: " + (dex_player + dex_up) + " (" + dex_player + "+" + dex_up + ")";
            intelUI.text = "INT: " + (int_player + int_up) + " (" + int_player + "+" + int_up + ")";
            lukUI.text = "LUK: " + (luk_player + luk_up) + " (" + luk_player + "+" + luk_up + ")";
            moneyUI.text = "Money : " + money;

            hpUI.value = hp;
            mpUI.value = mp;
            expUI.value = exp;

            hpUI.maxValue = hpMax;
            mpUI.maxValue = mpMax;
            expUI.maxValue = expMax;

            hptext.text = hp + " / " + hpMax;
            mptext.text = mp + " / " + mpMax;
            exptext.text = exp + " / " + expMax;

        }
    }
}
