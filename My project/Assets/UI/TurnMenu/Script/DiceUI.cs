using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceUI : MonoBehaviour
{
    public int num;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Number : " + num;
    }
}
