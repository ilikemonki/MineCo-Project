using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour
{
    public Miners miner;
    public Image minerIcon;
    public CurrentSelectedSlot selectedSlot;
    public bool isLocked, hasMiner;

    public void AddMiner(Miners newMiner)
    {
        miner = newMiner;
        minerIcon.sprite = miner.sprite;
        minerIcon.enabled = true;
        hasMiner = true;
    }

    public void ClearSlot()
    {
        miner = null;
        minerIcon.sprite = null;
        minerIcon.enabled = false;
        if (selectedSlot.slotSelectedImage.gameObject.activeSelf)
            selectedSlot.slotSelectedImage.gameObject.SetActive(false);
        hasMiner = false;
    }

    //For Button
    public void SlotIsSelected()
    {
        if (miner != null && isLocked == false)
        {
            selectedSlot.CurrentSlot(this);
        }
    }

    public void FireButton()
    {
        ClearSlot();
    }
}
