using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LockedSlots : MonoBehaviour
{
    public IdleGame idleGame;
    public InventorySlots invSlot;
    public Inventory anyInventory;

    public TextMeshProUGUI coinCostText;
    public double coinCost;
    public TextMeshProUGUI gemCostText;    //for the last 5 slots.
    public double gemCost;

    public Color32 darkColor = new Color32(0, 0, 0, 255);  //black text

    public void Start()
    {
        if (gemCostText != null)
        {
            gemCostText.text = "<sprite=5>\n" + idleGame.ConversionNoDecimal(gemCost);
        }
        else
        {
            coinCostText.text = "<sprite=4>\n" + idleGame.ConversionNoDecimal(coinCost);
        }
    }

    public void LockedSlotButton()
    {
        //if affordable, subtract coins and set isLocked to false;
        if (idleGame.coins >= coinCost && invSlot.isLocked)
        {
            idleGame.coins -= coinCost;
            OpenCoinSlot();
            idleGame.UpdateCurrencyText();
        }
    }

    public void OpenCoinSlot()
    {
        anyInventory.maxSpace++;
        coinCostText.gameObject.SetActive(false);
        invSlot.isLocked = false;
    }

    public void GemLockedSlotButton()
    {
        //if affordable, subtract gems and set isLocked to false;
        if (idleGame.gems >= gemCost && invSlot.isLocked)
        {
            idleGame.gems -= gemCost;
            OpenGemSlot();
            idleGame.UpdateCurrencyText();
        }
    }

    public void OpenGemSlot()
    {
        anyInventory.maxSpace++;
        gemCostText.gameObject.SetActive(false);
        invSlot.isLocked = false;
    }
}
