using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapUI : MonoBehaviour
{
    public GameObject SwitchButton;
    public GameObject PlayerImg;
    public GameObject MapImg;
    public TextMeshProUGUI nickname;
    public GameObject TopdownCamera;

    public int target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TopdownCamera.GetComponent<TopdownCameraMovement>().target!=-1)
        {
            MapImg.SetActive(true);
            PlayerImg.SetActive(false);
        }
        else
        {
            MapImg.SetActive(false);
            PlayerImg.SetActive(true);
        }
    }
}
