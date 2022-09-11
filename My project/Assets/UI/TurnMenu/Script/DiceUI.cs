using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceUI : MonoBehaviour
{
    public int num;
    public TextMeshProUGUI text;

    void Update()
    {
        text.text = "Number : " + num;
    }
}
