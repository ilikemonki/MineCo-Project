using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentSelectedSlot : MonoBehaviour
{
    public InventorySlots currentSlotSelected;
    public InventoryUI invUI;
    public Inventory invToSendTo;
    public SpawnMiner spawnMiner;
    public SellingOre sellingOre;
    public Image slotSelectedImage;
    public PopupText popupText;
    public Confirmation confirmation;
    public Text dropDownText;
    public DropDownInventories dropDownInv;


    public void OnDisable()
    {
        currentSlotSelected = null;
        slotSelectedImage.gameObject.SetActive(false);
        popupText.HidePopup();
    }
    public void CurrentSlot(InventorySlots slot)
    {
        if (currentSlotSelected == slot)    //if the selected slot is the same one being selected
        {
            currentSlotSelected.miner.Boosted();
        }
        else
        {
            this.currentSlotSelected = slot;
            slotSelectedImage.transform.position = new Vector3(slot.transform.position.x, slot.transform.position.y, slot.transform.position.z);
            slotSelectedImage.gameObject.SetActive(true);
            invUI.displayTraitDescription.Hide();
            invUI.PopulateStats(currentSlotSelected.miner);
            invUI.PopulateTraits(currentSlotSelected.miner.goodTraits, 
                currentSlotSelected.miner.badTraits, 
                currentSlotSelected.miner.terrainTraits, 
                currentSlotSelected.miner.specialTraits);     //display traits
            currentSlotSelected.miner.Boosted();
        }
    }

    //Fire button. Deletes miner.
    public void FireButton()
    {
        if (currentSlotSelected != null && sellingOre.idleGame.activateConfirmation)
        {
            confirmation.SetCSS(this);
            confirmation.SetListeners("FireConfirmationFunction");  //Set YesBtn onClick with FireFunction
            confirmation.ShowConfirmationWindow();  //Show window.
        }
        else if (currentSlotSelected != null && !sellingOre.idleGame.activateConfirmation)
        {
            FireConfirmationFunction();
        }
    }

    public void FireConfirmationFunction()
    {
        CheckOresBeingCarried();
        currentSlotSelected.miner.DestroyMiner();
        invUI.inventory.Remove(currentSlotSelected.miner);
        currentSlotSelected = null;
        invUI.ClearStats();
    }

    public void CheckOresBeingCarried()
    {
        if (currentSlotSelected.miner.oreBeingSoldAmount > 0)
        {
            if (currentSlotSelected.miner.copperBeingCarried)
            {
                sellingOre.copperToSell += currentSlotSelected.miner.oreBeingSoldAmount;
                currentSlotSelected.miner.copperBeingCarried = false;
            }
            else if (currentSlotSelected.miner.ironBeingCarried)
            {
                sellingOre.ironToSell += currentSlotSelected.miner.oreBeingSoldAmount;
                currentSlotSelected.miner.ironBeingCarried = false;
            }
            else if (currentSlotSelected.miner.goldBeingCarried)
            {
                sellingOre.goldToSell += currentSlotSelected.miner.oreBeingSoldAmount;
                currentSlotSelected.miner.goldBeingCarried = false;
            }
            else
            {
                sellingOre.diamondToSell += currentSlotSelected.miner.oreBeingSoldAmount;
                currentSlotSelected.miner.diamondBeingCarried = false;
            }
            currentSlotSelected.miner.oreBeingSoldAmount = 0;
        }
        if (currentSlotSelected.miner.oreBeingCarried != null)
        {
            currentSlotSelected.miner.oreBeingCarried.gameObject.SetActive(false);
        }
    }


    public void SetInvToSendTo()    //Sets which inventory to send to based on what drop down item is chosen.
    {
        invToSendTo = dropDownInv.SetInvToSendTo(dropDownText.text);
    }

    public void SendButton()
    {
        if (currentSlotSelected != null && dropDownText.text.Equals("Mine Co")) //sending to MineCo
        {
            if (invToSendTo.miners.Count < invToSendTo.maxSpace)
            {
                CheckOresBeingCarried();
                currentSlotSelected.miner.gameObject.SetActive(false);
                invToSendTo.Add(currentSlotSelected.miner);
                //send to inv
                if (sellingOre.IsSellingOres()) //if ores are being sold.
                {
                    spawnMiner.SpawnToHousing(currentSlotSelected.miner, invToSendTo, true);
                }
                else    //sent to MineCo but no ores selling, don't spawn.
                {
                    spawnMiner.SpawnToHousing(currentSlotSelected.miner, invToSendTo, false);
                }

                invUI.inventory.Remove(currentSlotSelected.miner);
                currentSlotSelected = null;
                invUI.ClearStats();
            }
            else if (invToSendTo.miners.Count >= invToSendTo.maxSpace)
            {
                popupText.SetText("Mine is full.");
                popupText.ShowPopup();
            }
        }
        else if (currentSlotSelected != null && !dropDownText.text.Equals("Mine Co")) //sending to mines
        {
            if (invToSendTo.miners.Count < invToSendTo.maxSpace)
            {
                CheckOresBeingCarried();
                invToSendTo.Add(currentSlotSelected.miner);
                //send to inv
                spawnMiner.SpawnToHousing(currentSlotSelected.miner, invToSendTo, true);
                invUI.inventory.Remove(currentSlotSelected.miner);
                currentSlotSelected = null;
                invUI.ClearStats();
            }
            else if (invToSendTo.miners.Count >= invToSendTo.maxSpace)
            {
                popupText.SetText("Mine is full.");
                popupText.ShowPopup();
            }
        }
    }
}
