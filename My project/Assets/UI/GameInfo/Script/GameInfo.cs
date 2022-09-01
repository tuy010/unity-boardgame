using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameInfo : MonoBehaviour
{
    public int turn;
    public TextMeshProUGUI turnText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(turn == -1)
        {
            turnText.gameObject.SetActive(false);
        }
        else
        {
            turnText.gameObject.SetActive(true);
            turnText.text = "Turn : " + turn;
        }
    }
}
