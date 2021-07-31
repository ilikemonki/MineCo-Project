using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BayatGames.SaveGameFree;

public class LockedMines : MonoBehaviour
{
    public int id;
    public IdleGame idleGame;
    public TextMeshProUGUI costText;
    public double cost;
    public Button digBtn;
    public GameObject mineFront;
    public GameObject nextMineLockButton;
    public GameObject nextMineCostText;
    public GameObject bgAnimation, bgAnimation2;
    public GameObject clickOre;

    public GameObject inventoryTab;
    public Dropdown[] dropDown;

    public void Start()
    {
        costText.text = "<sprite=0>" + idleGame.ConversionNoDecimal(cost);
    }
    //Buy the mine
    public void DigMineBtn()
    {
        if (idleGame.coins >= cost)
        {
            idleGame.coins -= cost;
            OpenMine();
            idleGame.minesUnlocked++;
            AddDropDownItem();
            if (nextMineLockButton != null)
            {
                nextMineLockButton.gameObject.SetActive(true);
            }
            if (nextMineCostText != null)
            {
                nextMineCostText.gameObject.SetActive(true);
            }
            idleGame.UpdateCurrencyText();
            idleGame.Save();
        }
    }

    //adds dropdown option to the inventories
    public void AddDropDownItem()
    {
        if (dropDown != null)
        {
            for (int i = 0; i < dropDown.Length; i++)
            {
                dropDown[i].options.Add(new Dropdown.OptionData() { text = "Mine " + id });
            }
        }
    }

    public void OpenMine()
    {
        digBtn.gameObject.SetActive(false);
        costText.gameObject.SetActive(false);
        if (mineFront != null)
        {
            mineFront.gameObject.SetActive(false);
        }
        if (inventoryTab != null)
        {
            inventoryTab.gameObject.SetActive(true);
        }
        if (bgAnimation != null)
        {
            bgAnimation.gameObject.SetActive(true);
        }
        if (bgAnimation2 != null)
        {
            bgAnimation2.gameObject.SetActive(true);
        }
        if (clickOre != null)
        {
            clickOre.gameObject.SetActive(true);
        }
    }
}
