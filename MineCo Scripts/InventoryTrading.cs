using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTrading : MonoBehaviour
{
    public Inventory mineHouse;
    public Inventory inventory;
    public Inventory hiringBoard;
    public GameObject mineParent;
    public OpenInventoryButton openInventoryButton;
    public string openedInventoryName;
    public Vector3 spawn1Pos;
    public Vector3 spawn2Pos;
    public Vector3 returnToPos;
    public Vector3 returnToPos2;

    // Trading: inv->mine, hiring->inv, hiring->mine
    public void SortInventory()
    {
        openedInventoryName = openInventoryButton.inventory.name;
        if (openedInventoryName.Contains("Inventory"))
        {
            inventory = openInventoryButton.inventory;
        }
        else if (openedInventoryName.Contains("Hiring"))
        {
            hiringBoard = openInventoryButton.inventory;
        }
    }

    public void SetOpenInvButtonObject(OpenInventoryButton oib)
    {
        this.openInventoryButton = oib;
        SortInventory();
        
    }

    public void SetOpenInvButtonObjectNull(OpenInventoryButton oib)
    {
        openedInventoryName = oib.name;
        if (openedInventoryName.Contains("Inventory"))
        {
            inventory = null;
        }
        else if (openedInventoryName.Contains("Hiring"))
        {
            hiringBoard = null;
        }
    }

    public void SetMinesTabs(OpenInventoryButton oib)
    {
        this.openInventoryButton = oib;
        mineHouse = openInventoryButton.inventory;
    }
    public void RemoveMinesTabs()
    {
        mineHouse = null;
    }

    
    public void SetInventoryPosition()  //Set Inventory Above Hiring Board
    {
        iTween.MoveTo(inventory.gameObject, iTween.Hash("position", spawn2Pos, "islocal", true, "time", 1f));
    }

    public void ResetInventoryPosition()    //Set Inventory back down
    {
        iTween.MoveTo(inventory.gameObject, iTween.Hash("position", spawn1Pos, "islocal", true, "time", 1f));
   }

    public void SetMineInventoryPosition()
    {
        iTween.MoveTo(mineParent.gameObject, iTween.Hash("position", spawn2Pos, "islocal", true, "time", 1f));
    }

    public void ResetMineInventoryPosition()
    {
        iTween.MoveTo(mineParent.gameObject, iTween.Hash("position", spawn1Pos, "islocal", true, "time", 1f));
    }

    public void SetHiringInventoryPosition()
    {
        iTween.MoveTo(hiringBoard.gameObject, iTween.Hash("position", spawn1Pos, "islocal", true, "time", 1f));
    }

    public void ReturnMineToStartPos()
    {
        iTween.MoveTo(mineParent.gameObject, iTween.Hash("position", returnToPos, "islocal", true, "time", 0.3f, "oncomplete", "DisableMineParent", "oncompletetarget", this.gameObject));
    }

    public void ReturnMineToStartPos2()
    {
        iTween.MoveTo(mineParent.gameObject, iTween.Hash("position", returnToPos2, "islocal", true, "time", 0.3f, "oncomplete", "DisableMineParent", "oncompletetarget", this.gameObject));
    }

    public void DisableMineParent()
    {
        mineParent.gameObject.SetActive(false);
    }

}
