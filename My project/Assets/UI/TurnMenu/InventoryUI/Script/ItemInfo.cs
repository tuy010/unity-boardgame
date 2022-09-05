using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfo : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI type;
    public TextMeshProUGUI description;
    public TextMeshProUGUI UseText;
    public TextMeshProUGUI DiscardText;
    public TextMeshProUGUI unequidText;

    public GameObject useButton;
    public GameObject discardButton;
    public GameObject unequidButton;
    public Image img;

    public GameObject EquidUI;
    public GameObject InventoryUI;

    public void CloseInfo()
    {
        if(EquidUI.activeSelf)
        {
            EquidUI.GetComponent<EquidUI>().isShowInfo = false;
            EquidUI.GetComponent<EquidUI>().ReloadEquid();
        }
        else if (InventoryUI.activeSelf)
        {
            InventoryUI.GetComponent<Inventory>().isShowInfo = false;
            InventoryUI.GetComponent<Inventory>().ReloadInventory();
        }

    }
}
