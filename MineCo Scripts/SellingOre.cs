using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SellingOre : MonoBehaviour
{
    public IdleGame idleGame;
    public Button btnOne, btnTen, btnHundred, btnHalf, btnMax;
    public Inventory inventory;
    public PopupText popupText;

    public double amountSelected;
    public bool halfIsSelected, maxIsSelected;
    //Ores being sold
    public Text copperBeingSoldAmountText, ironBeingSoldAmountText, goldBeingSoldAmountText, diamondBeingSoldAmountText;
    //total coin per ore
    public Text copperTotalText, ironTotalText, goldTotalText, diamondTotalText;
    //ore selling price
    public Text copperPriceText, ironPriceText, goldPriceText, diamondPriceText;
    public double copperBeingSoldAmount, ironBeingSoldAmount, goldBeingSoldAmount, diamondBeingSoldAmount;
    public double copperTotal, ironTotal, goldTotal, diamondTotal;

    public double copperToSell, ironToSell, goldToSell, diamondToSell;
    public Text copperToSellText, ironToSellText, goldToSellText, diamondToSellText;
    public GameObject copperToSellImage, ironToSellImage, goldToSellImage, diamondToSellImage;

    public Text totalText;
    public void Start()
    {
        amountSelected = 1;
        btnOne.GetComponent<Button>().interactable = false;
    }

    public void OnEnable()
    {
        copperTotal = copperBeingSoldAmount * idleGame.copperPrice;
        copperTotalText.text = copperTotal.ToString("F2");
        ironTotal = ironBeingSoldAmount * idleGame.ironPrice;
        ironTotalText.text = ironTotal.ToString("F2");
        goldTotal = goldBeingSoldAmount * idleGame.ironPrice;
        goldTotalText.text = goldTotal.ToString("F2");
        diamondTotal = diamondBeingSoldAmount * idleGame.ironPrice;
        diamondTotalText.text = diamondTotal.ToString("F2");
        copperPriceText.text = "x" + idleGame.copperPrice.ToString("F2");
        ironPriceText.text = "x" + idleGame.ironPrice.ToString("F2");
        goldPriceText.text = "x" + idleGame.goldPrice.ToString("F2");
        diamondPriceText.text = "x" + idleGame.diamondPrice.ToString("F2");
        UpdateTotal();
    }

    public void OneAmountSelectedBtn()
    {
        amountSelected = 1;
        maxIsSelected = false;
        halfIsSelected = false;
        btnOne.GetComponent<Button>().interactable = false;
        btnTen.GetComponent<Button>().interactable = true;
        btnHundred.GetComponent<Button>().interactable = true;
        btnHalf.GetComponent<Button>().interactable = true;
        btnMax.GetComponent<Button>().interactable = true;
    }
    public void TenAmountSelectedBtn()
    {
        amountSelected = 10;
        maxIsSelected = false;
        halfIsSelected = false;
        btnOne.GetComponent<Button>().interactable = true;
        btnTen.GetComponent<Button>().interactable = false;
        btnHundred.GetComponent<Button>().interactable = true;
        btnHalf.GetComponent<Button>().interactable = true;
        btnMax.GetComponent<Button>().interactable = true;
    }
    public void HundredAmountSelectedBtn()
    {
        amountSelected = 100;
        maxIsSelected = false;
        halfIsSelected = false;
        btnOne.GetComponent<Button>().interactable = true;
        btnTen.GetComponent<Button>().interactable = true;
        btnHundred.GetComponent<Button>().interactable = false;
        btnHalf.GetComponent<Button>().interactable = true;
        btnMax.GetComponent<Button>().interactable = true;
    }
    public void HalfAmountSelectedBtn()
    {
        maxIsSelected = false;
        halfIsSelected = true;
        btnOne.GetComponent<Button>().interactable = true;
        btnTen.GetComponent<Button>().interactable = true;
        btnHundred.GetComponent<Button>().interactable = true;
        btnHalf.GetComponent<Button>().interactable = false;
        btnMax.GetComponent<Button>().interactable = true;
    }
    public void MaxAmountSelectedBtn()
    {
        maxIsSelected = true;
        halfIsSelected = false;
        btnOne.GetComponent<Button>().interactable = true;
        btnTen.GetComponent<Button>().interactable = true;
        btnHundred.GetComponent<Button>().interactable = true;
        btnHalf.GetComponent<Button>().interactable = true;
        btnMax.GetComponent<Button>().interactable = false;
    }

    public void AddCopperBtn()
    {
        if (maxIsSelected)
        {
            copperBeingSoldAmount = idleGame.copper;
        }
        else if (halfIsSelected)
        {
            copperBeingSoldAmount = Math.Round(idleGame.copper / 2);
        }
        else
        {
            if (copperBeingSoldAmount + amountSelected <= idleGame.copper)
            {
                copperBeingSoldAmount += amountSelected;
            }
            else
            {
                copperBeingSoldAmount = idleGame.copper;
            }
        }
        copperBeingSoldAmountText.text = idleGame.OreConversion(copperBeingSoldAmount);
        copperTotal = copperBeingSoldAmount * idleGame.copperPrice;
        copperTotalText.text = idleGame.CoinConversionTwoDecimal(copperTotal);
        UpdateTotal();
    }
    public void SubtractCopperBtn()
    {
        if (maxIsSelected || halfIsSelected)
        {
            copperBeingSoldAmount = 0;
        }
        else
        {
            if (copperBeingSoldAmount - amountSelected >= 0)
            {
                copperBeingSoldAmount -= amountSelected;
            }
            else
            {
                copperBeingSoldAmount = 0;
            }
        }
        copperBeingSoldAmountText.text = idleGame.OreConversion(copperBeingSoldAmount);
        copperTotal = copperBeingSoldAmount * idleGame.copperPrice;
        copperTotalText.text = idleGame.CoinConversionTwoDecimal(copperTotal);
        UpdateTotal();
    }

    public void AddIronBtn()
    {
        if (maxIsSelected)
        {
            ironBeingSoldAmount = idleGame.iron;
        }
        else if (halfIsSelected)
        {
            ironBeingSoldAmount = Math.Round(idleGame.iron / 2);
        }
        else
        {
            if (ironBeingSoldAmount + amountSelected <= idleGame.iron)
            {
                ironBeingSoldAmount += amountSelected;
            }
            else
            {
                ironBeingSoldAmount = idleGame.iron;
            }
        }
        ironBeingSoldAmountText.text = idleGame.OreConversion(ironBeingSoldAmount);
        ironTotal = ironBeingSoldAmount * idleGame.ironPrice;
        ironTotalText.text = idleGame.CoinConversionTwoDecimal(ironTotal);
        UpdateTotal();
    }
    public void SubtractIronBtn()
    {
        if (maxIsSelected || halfIsSelected)
        {
            ironBeingSoldAmount = 0;
        }
        else
        {
            if (ironBeingSoldAmount - amountSelected >= 0)
            {
                ironBeingSoldAmount -= amountSelected;
            }
            else
            {
                ironBeingSoldAmount = 0;
            }
        }
        ironBeingSoldAmountText.text = idleGame.OreConversion(ironBeingSoldAmount);
        ironTotal = ironBeingSoldAmount * idleGame.ironPrice;
        ironTotalText.text = idleGame.CoinConversionTwoDecimal(ironTotal);
        UpdateTotal();
    }

    public void AddGoldBtn()
    {
        if (maxIsSelected)
        {
            goldBeingSoldAmount = idleGame.gold;
        }
        else if (halfIsSelected)
        {
            goldBeingSoldAmount = Math.Round(idleGame.gold / 2);
        }
        else
        {
            if (goldBeingSoldAmount + amountSelected <= idleGame.gold)
            {
                goldBeingSoldAmount += amountSelected;
            }
            else
            {
                goldBeingSoldAmount = idleGame.gold;
            }
        }
        goldBeingSoldAmountText.text = idleGame.OreConversion(goldBeingSoldAmount);
        goldTotal = goldBeingSoldAmount * idleGame.goldPrice;
        goldTotalText.text = idleGame.CoinConversionTwoDecimal(goldTotal);
        UpdateTotal();
    }
    public void SubtractGoldBtn()
    {
        if (maxIsSelected || halfIsSelected)
        {
            goldBeingSoldAmount = 0;
        }
        else
        {
            if (goldBeingSoldAmount - amountSelected >= 0)
            {
                goldBeingSoldAmount -= amountSelected;
            }
            else
            {
                goldBeingSoldAmount = 0;
            }
        }
        goldBeingSoldAmountText.text = idleGame.OreConversion(goldBeingSoldAmount);
        goldTotal = goldBeingSoldAmount * idleGame.goldPrice;
        goldTotalText.text = idleGame.CoinConversionTwoDecimal(goldTotal);
        UpdateTotal();
    }

    public void AddDiamondBtn()
    {
        if (maxIsSelected)
        {
            diamondBeingSoldAmount = idleGame.diamond;
        }
        else if (halfIsSelected)
        {
            diamondBeingSoldAmount = Math.Round(idleGame.diamond / 2);
        }
        else
        {
            if (diamondBeingSoldAmount + amountSelected <= idleGame.diamond)
            {
                diamondBeingSoldAmount += amountSelected;
            }
            else
            {
                diamondBeingSoldAmount = idleGame.diamond;
            }
        }
        diamondBeingSoldAmountText.text = idleGame.OreConversion(diamondBeingSoldAmount);
        diamondTotal = diamondBeingSoldAmount * idleGame.diamondPrice;
        diamondTotalText.text = idleGame.CoinConversionTwoDecimal(diamondTotal);
        UpdateTotal();
    }
    public void SubtractDiamondBtn()
    {
        if (maxIsSelected || halfIsSelected)
        {
            diamondBeingSoldAmount = 0;
        }
        else
        {
            if (diamondBeingSoldAmount - amountSelected >= 0)
            {
                diamondBeingSoldAmount -= amountSelected;
            }
            else
            {
                diamondBeingSoldAmount = 0;
            }
        }
        diamondBeingSoldAmountText.text = idleGame.OreConversion(diamondBeingSoldAmount);
        diamondTotal = diamondBeingSoldAmount * idleGame.diamondPrice;
        diamondTotalText.text = idleGame.CoinConversionTwoDecimal(diamondTotal);
        UpdateTotal();
    }

    public void ClearBtn()
    {
        copperBeingSoldAmount = 0; ironBeingSoldAmount = 0; goldBeingSoldAmount = 0; diamondBeingSoldAmount = 0;
        copperBeingSoldAmountText.text = copperBeingSoldAmount.ToString();
        ironBeingSoldAmountText.text = ironBeingSoldAmount.ToString();
        goldBeingSoldAmountText.text = goldBeingSoldAmount.ToString();
        diamondBeingSoldAmountText.text = diamondBeingSoldAmount.ToString();

        copperTotal = 0; ironTotal = 0; goldTotal = 0; diamondTotal = 0;
        totalText.text = "0";
        copperTotalText.text = copperTotal.ToString();
        ironTotalText.text = ironTotal.ToString();
        goldTotalText.text = goldTotal.ToString();
        diamondTotalText.text = diamondTotal.ToString();
    }

    public void SetSellPile()
    {
        //Set ores to sell text
        if (copperToSell > 0)
        {
            copperToSellText.text = idleGame.OreConversion(copperToSell);
            copperToSellImage.gameObject.SetActive(true);
        }
        if (ironToSell > 0)
        {
            ironToSellText.text = idleGame.OreConversion(ironToSell);
            ironToSellImage.gameObject.SetActive(true);
        }
        if (goldToSell > 0)
        {
            goldToSellText.text = idleGame.OreConversion(goldToSell);
            goldToSellImage.gameObject.SetActive(true);
        }
        if (diamondToSell > 0)
        {
            diamondToSellText.text = idleGame.OreConversion(diamondToSell);
            diamondToSellImage.gameObject.SetActive(true);
        }
    }


    public bool IsSellingOres()
    {
        if (copperToSell > 0 || ironToSell > 0 || goldToSell > 0 || diamondToSell > 0)
        {
            return true;
        }
        else return false;
    }

    public void UpdateTotal()
    {
        totalText.text = idleGame.CoinConversionTwoDecimal(copperTotal + +ironTotal + goldTotal + diamondTotal);
    }
}
