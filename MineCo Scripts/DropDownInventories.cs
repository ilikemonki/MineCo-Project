using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownInventories : MonoBehaviour
{
    public Inventory[] inventoryList;
    public Dropdown[] dropDownList;

    public Inventory SetInvToSendTo(string t)    //Sets which inventory to send to based on what drop down item is chosen.
    {
        switch (t)
        {
            //0-7 is mines, last is mine Co.
            case "Mine 1":
                return inventoryList[0];
            case "Mine 2":
                return inventoryList[1];
            case "Mine 3":
                return inventoryList[2];
            case "Mine 4":
                return inventoryList[3];
            case "Mine 5":
                return inventoryList[4];
            case "Mine 6":
                return inventoryList[5];
            case "Mine 7":
                return inventoryList[6];
            case "Mine 8":
                return inventoryList[7];
            default:
                return inventoryList[8];
        }
    }
}



