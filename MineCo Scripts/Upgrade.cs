using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using BayatGames.SaveGameFree;

public class Upgrade : MonoBehaviour
{
    public Upgrade oreLevelUpgrade;
    public Text upgradeTitleText;
    public Text currentText;
    public Text upgradeLevelText;

    public Text copperCostText, ironCostText, goldCostText, diamondCostText;
    public Text coinCostText;
    public Text oreMultiplierText;
    public Text updateOreCurrentText;
    public Text maxedText;

    public IdleGame idleGame;
    public Button btn;
    public Text increaseAmountText;

    public string id;
    public bool isMinerUpgrade;
    public bool maxedUpgrade;
    public double startingCost, incrementMultiplier, incrementStart;
    public string convertedCopperString;
    public string convertedIronString;
    public string convertedGoldString;
    public string convertedDiamondString;
    public string convertedCoinString;

    //Miner Upgrades
    //List: [0]-copperCost, [1]-ironCost, [2]-goldCost, [3]-diamondCost, [4]-upgradeLevel
    public List<double> minerUpgrade = new List<double> { 0, 0, 0, 0, 0 };
    //Ore Upgrades
    //List: [0]-coinCost, [1]-upgradeLevel
    public List<double> oreUpgrade = new List<double> { 0, 0 };


    public string upgradeTitleName;

    public void OnEnable()
    {
        if (upgradeTitleName.Contains("Max Traits"))
        {
            currentText.text = "Current: " + $"<color=blue>{idleGame.maxTraits + idleGame.maxTraitsPlus}</color>";
        }
        else if (upgradeTitleName.Contains("Click Power"))
        {
            currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.OreConversion(idleGame.RoundCostConversion(idleGame.clickPower * idleGame.clickMultiplier))}</color>";
        }
    }

    public void Start()
    {
        upgradeTitleName = upgradeTitleText.text;
        if (isMinerUpgrade)
        {
            ConvertCost(minerUpgrade, true);
        }
        else ConvertCost(oreUpgrade, false);
    }

    public void StartCost()
    {
        if (isMinerUpgrade)
        {
            minerUpgrade[0] = startingCost;
        }
        else oreUpgrade[0] = startingCost;
    }


    public void Update()
    {
        if (!maxedUpgrade)
        {
            if (isMinerUpgrade)
            {
                UpdateCost(minerUpgrade);
            }
            else UpdateCoinCost(oreUpgrade);
        }
    }

    public bool CheckUpgradeRequirements(List<double> xUpgrade)
    {
        if (maxedUpgrade) return false;
        if (idleGame.copper >= xUpgrade[0] && idleGame.iron >= xUpgrade[1] && idleGame.gold >= xUpgrade[2] && idleGame.diamond >= xUpgrade[3])
        {
            if (xUpgrade[0] > 0) idleGame.copper -= xUpgrade[0];    //copper
            if (xUpgrade[1] > 0) idleGame.iron -= xUpgrade[1];      //iron
            if (xUpgrade[2] > 0) idleGame.gold -= xUpgrade[2];      //gold
            if (xUpgrade[3] > 0) idleGame.diamond -= xUpgrade[3];   //diamond
            xUpgrade[4]++;  //level
            upgradeLevelText.text = "Lv: " + xUpgrade[4];   //set level text
            idleGame.UpdateOresText();
            return true;
        }
        return false;
    }

    public void UpdateCost(List<double> xUpgrade)
    {
        if (xUpgrade[0] > 0)
        {
            if (xUpgrade[0] <= idleGame.copper) copperCostText.text = $"<color=green>{convertedCopperString}</color>";    //copper
            else copperCostText.text = $"<color=red>{convertedCopperString}</color>";
        }
        if (xUpgrade[1] > 0)
        {
            if (xUpgrade[1] <= idleGame.iron && xUpgrade[1] > 0) ironCostText.text = $"<color=green>{convertedIronString}</color>";    //iron
            else ironCostText.text = $"<color=red>{convertedIronString}</color>";
        }
        if (xUpgrade[2] > 0)
        {
            if (xUpgrade[2] <= idleGame.gold && xUpgrade[2] > 0) goldCostText.text = $"<color=green>{convertedGoldString}</color>";    //gold
            else goldCostText.text = $"<color=red>{convertedGoldString}</color>";
        }
        if (xUpgrade[3] > 0)
        {
            if (xUpgrade[3] <= idleGame.diamond && xUpgrade[3] > 0) diamondCostText.text = $"<color=green>{convertedDiamondString}</color>";    //diamond
            else diamondCostText.text = $"<color=red>{convertedDiamondString}</color>";
        }

        if (xUpgrade[0] > idleGame.copper || xUpgrade[1] > idleGame.iron || xUpgrade[2] > idleGame.gold || xUpgrade[3] > idleGame.diamond)
        {
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
        }
    }

    public void ConvertCost(List<double> xUpgrade, bool isMinerUpgrade)
    {
        if (isMinerUpgrade)
        {
            convertedCopperString = idleGame.OreConversion(xUpgrade[0]);
            convertedIronString = idleGame.OreConversion(xUpgrade[1]);
            convertedGoldString = idleGame.OreConversion(xUpgrade[2]);
            convertedDiamondString = idleGame.OreConversion(xUpgrade[3]);
        }
        else
        {
            convertedCoinString = idleGame.OreConversion(xUpgrade[0]);
        }
    }

    //Miner Upgrade Buttons
    public void UpgradeSpeed()
    {
        //check for requirements of upgrade.
        if(CheckUpgradeRequirements(minerUpgrade))
        {
            idleGame.speedChance += 0.10;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.speedChance)}</color>";    //set current text
            if (minerUpgrade[4] == 100) //max upgrade
            {
                MaxedMinerUpgrade();
                return;
            }
            //set cost
            minerUpgrade[0] = idleGame.RoundCostConversion(minerUpgrade[0] * incrementMultiplier);
            if (minerUpgrade[4] >= 10)  //Iron
            {
                if (minerUpgrade[1] == 0) minerUpgrade[1] = 30;
                minerUpgrade[1] = idleGame.RoundCostConversion(minerUpgrade[1] * incrementMultiplier);
            }
            if (minerUpgrade[4] >= 20)  //Gold
            {
                if (minerUpgrade[2] == 0) minerUpgrade[2] = 100;
                minerUpgrade[2] = idleGame.RoundCostConversion(minerUpgrade[2] * incrementMultiplier);
            }
            if (minerUpgrade[4] >= 30)  //Diamond
            {
                if (minerUpgrade[3] == 0) minerUpgrade[3] = 250;
                minerUpgrade[3] = idleGame.RoundCostConversion(minerUpgrade[3] * incrementMultiplier);
            }
            ConvertCost(minerUpgrade, true);
        }
    }

    public void UpgradeOre()
    {
        //check for requirements of upgrade.
        if (CheckUpgradeRequirements(minerUpgrade))
        {
            idleGame.oreChance += 0.10;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.oreChance)}</color>";    //set current text
            //set cost
            minerUpgrade[0] = idleGame.RoundCostConversion(minerUpgrade[0] * incrementMultiplier);
            if (minerUpgrade[4] >= 15)  //Iron
            {
                if (minerUpgrade[1] == 0) minerUpgrade[1] = 20;
                minerUpgrade[1] = idleGame.RoundCostConversion(minerUpgrade[1] * incrementMultiplier);
            }
            if (minerUpgrade[4] >= 30)  //Gold
            {
                if (minerUpgrade[2] == 0) minerUpgrade[2] = 80;
                minerUpgrade[2] = idleGame.RoundCostConversion(minerUpgrade[2] * incrementMultiplier);
            }
            if (minerUpgrade[4] >= 45)  //Diamond
            {
                if (minerUpgrade[3] == 0) minerUpgrade[3] = 400;
                minerUpgrade[3] = idleGame.RoundCostConversion(minerUpgrade[3] * incrementMultiplier);
            }
            ConvertCost(minerUpgrade, true);
        }
    }

    public void UpgradeExp()
    {
        //check for requirements of upgrade.
        if (CheckUpgradeRequirements(minerUpgrade))
        {
            idleGame.expChance += 0.10;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.expChance)}</color>";    //set current text
            //set cost
            minerUpgrade[0] = idleGame.RoundCostConversion(minerUpgrade[0] * incrementMultiplier);
            if (minerUpgrade[4] >= 15)  //Iron
            {
                if (minerUpgrade[1] == 0) minerUpgrade[1] = 20;
                minerUpgrade[1] = idleGame.RoundCostConversion(minerUpgrade[1] * incrementMultiplier);
            }
            if (minerUpgrade[4] >= 30)  //Gold
            {
                if (minerUpgrade[2] == 0) minerUpgrade[2] = 80;
                minerUpgrade[2] = idleGame.RoundCostConversion(minerUpgrade[2] * incrementMultiplier);
            }
            if (minerUpgrade[4] >= 45)  //Diamond
            {
                if (minerUpgrade[3] == 0) minerUpgrade[3] = 400;
                minerUpgrade[3] = idleGame.RoundCostConversion(minerUpgrade[3] * incrementMultiplier);
            }
            ConvertCost(minerUpgrade, true);
        }
    }
    public void UpgradeSellOreCapacity()
    {
        //check for requirements of upgrade.
        if (CheckUpgradeRequirements(minerUpgrade))
        {
            idleGame.sellOrePower += 0.2;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.sellOrePower)}</color>";    //set current text
            //set cost
            minerUpgrade[0] = idleGame.RoundCostConversion(minerUpgrade[0] * incrementMultiplier);
            if (minerUpgrade[4] >= 10)  //Iron
            {
                if (minerUpgrade[1] == 0) minerUpgrade[1] = 200;
                minerUpgrade[1] = idleGame.RoundCostConversion(minerUpgrade[1] * incrementMultiplier);
            }
            if (minerUpgrade[4] >= 15)  //Gold
            {
                if (minerUpgrade[2] == 0) minerUpgrade[2] = 700;
                minerUpgrade[2] = idleGame.RoundCostConversion(minerUpgrade[2] * incrementMultiplier);
            }
            if (minerUpgrade[4] >= 20)  //Diamond
            {
                if (minerUpgrade[3] == 0) minerUpgrade[3] = 3000;
                minerUpgrade[3] = idleGame.RoundCostConversion(minerUpgrade[3] * incrementMultiplier);
            }
            ConvertCost(minerUpgrade, true);
        }
    }
    public void UpgradeMoreTraits()
    {
        //check for requirements of upgrade.
        if (CheckUpgradeRequirements(minerUpgrade))
        {
            idleGame.maxTraits += 1;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{idleGame.maxTraits + idleGame.maxTraitsPlus}</color>";    //set current text
            if (minerUpgrade[4] == 10) //max upgrade
            {
                MaxedMinerUpgrade();
                return;
            }
            //set cost
            minerUpgrade[0] = idleGame.RoundCostConversion(minerUpgrade[0] * incrementMultiplier);
            if (minerUpgrade[4] >= 2)  //Iron
            {
                if (minerUpgrade[1] == 0) minerUpgrade[1] = 250;
                minerUpgrade[1] = idleGame.RoundCostConversion(minerUpgrade[1] * incrementMultiplier);
            }
            if (minerUpgrade[4] >= 4)  //Gold
            {
                if (minerUpgrade[2] == 0) minerUpgrade[2] = 2500;
                minerUpgrade[2] = idleGame.RoundCostConversion(minerUpgrade[2] * incrementMultiplier);
            }
            if (minerUpgrade[4] >= 6)  //Diamond
            {
                if (minerUpgrade[3] == 0) minerUpgrade[3] = 30000;
                minerUpgrade[3] = idleGame.RoundCostConversion(minerUpgrade[3] * incrementMultiplier);
            }
            ConvertCost(minerUpgrade, true);
            if (minerUpgrade[4] == 2 || minerUpgrade[4] == 4) incrementMultiplier += 1.5;
            if (minerUpgrade[4] == 6) incrementMultiplier += 4;
        }
    }
    public void UpgradeGoodTraits()
    {
        //check for requirements of upgrade.
        if (CheckUpgradeRequirements(minerUpgrade))
        {
            idleGame.goodTraitsChance += 2;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{idleGame.goodTraitsChance.ToString("F2") + "%"}</color>";    //set current text
            if (minerUpgrade[4] == 30) //max upgrade
            {
                MaxedMinerUpgrade();
                return;
            }
            //set cost
            minerUpgrade[0] = idleGame.RoundCostConversion(minerUpgrade[0] * incrementMultiplier);
            if (minerUpgrade[4] >= 5)  //Iron
            {
                if (minerUpgrade[1] == 0) minerUpgrade[1] = 70;
                minerUpgrade[1] = idleGame.RoundCostConversion(minerUpgrade[1] * incrementMultiplier);
            }
            if (minerUpgrade[4] >= 10)  //Gold
            {
                if (minerUpgrade[2] == 0) minerUpgrade[2] = 500;
                minerUpgrade[2] = idleGame.RoundCostConversion(minerUpgrade[2] * incrementMultiplier);
            }
            if (minerUpgrade[4] >= 15)  //Diamond
            {
                if (minerUpgrade[3] == 0) minerUpgrade[3] = 7000;
                minerUpgrade[3] = idleGame.RoundCostConversion(minerUpgrade[3] * incrementMultiplier);
            }
            ConvertCost(minerUpgrade, true);
            if (minerUpgrade[4] == 10 || minerUpgrade[4] == 20) incrementMultiplier += 0.2;
            if (minerUpgrade[4] == 25) incrementMultiplier += 1;
        }
    }
    public void UpgradeBadTraits()
    {
        //check for requirements of upgrade.
        if (CheckUpgradeRequirements(minerUpgrade))
        {
            idleGame.badTraitsChance -= 2;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{idleGame.badTraitsChance.ToString("F2") + "%"}</color>";    //set current text
            if (minerUpgrade[4] == 30) //max upgrade
            {
                MaxedMinerUpgrade();
                return;
            }
            //set cost
            minerUpgrade[0] = idleGame.RoundCostConversion(minerUpgrade[0] * incrementMultiplier);
            if (minerUpgrade[4] >= 5)  //Iron
            {
                if (minerUpgrade[1] == 0) minerUpgrade[1] = 70;
                minerUpgrade[1] = idleGame.RoundCostConversion(minerUpgrade[1] * incrementMultiplier);
            }
            if (minerUpgrade[4] >= 10)  //Gold
            {
                if (minerUpgrade[2] == 0) minerUpgrade[2] = 500;
                minerUpgrade[2] = idleGame.RoundCostConversion(minerUpgrade[2] * incrementMultiplier);
            }
            if (minerUpgrade[4] >= 15)  //Diamond
            {
                if (minerUpgrade[3] == 0) minerUpgrade[3] = 7000;
                minerUpgrade[3] = idleGame.RoundCostConversion(minerUpgrade[3] * incrementMultiplier);
            }
            ConvertCost(minerUpgrade, true);
            if (minerUpgrade[4] == 10 || minerUpgrade[4] == 20) incrementMultiplier += 0.2;
            if (minerUpgrade[4] == 25) incrementMultiplier += 1;
        }
    }
    public void UpgradeTerrainTraits()
    {
        //check for requirements of upgrade.
        if (CheckUpgradeRequirements(minerUpgrade))
        {
            idleGame.terrainTraitsChance += 2;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{idleGame.terrainTraitsChance.ToString("F2") + "%"}</color>";    //set current text
            if (minerUpgrade[4] == 15) //max upgrade
            {
                MaxedMinerUpgrade();
                return;
            }
            //set cost
            minerUpgrade[0] = idleGame.RoundCostConversion(minerUpgrade[0] * incrementMultiplier);
            if (minerUpgrade[4] >= 2)  //Iron
            {
                if (minerUpgrade[1] == 0) minerUpgrade[1] = 100;
                minerUpgrade[1] = idleGame.RoundCostConversion(minerUpgrade[1] * incrementMultiplier);
            }
            if (minerUpgrade[4] >= 6)  //Gold
            {
                if (minerUpgrade[2] == 0) minerUpgrade[2] = 600;
                minerUpgrade[2] = idleGame.RoundCostConversion(minerUpgrade[2] * incrementMultiplier);
            }
            if (minerUpgrade[4] >= 10)  //Diamond
            {
                if (minerUpgrade[3] == 0) minerUpgrade[3] = 10000;
                minerUpgrade[3] = idleGame.RoundCostConversion(minerUpgrade[3] * incrementMultiplier);
            }
            ConvertCost(minerUpgrade, true);
            if (minerUpgrade[4] == 5 || minerUpgrade[4] == 10) incrementMultiplier += 0.5;
            if (minerUpgrade[4] == 15) incrementMultiplier += 4;
        }
    }



    //---------------------------Ore Upgrade Buttons -----------------------------------
    public void UpgradeClickPower()
    {
        //check for requirements of upgrade.
        if (CheckOreUpgradeRequirements(oreUpgrade))
        {
            idleGame.clickPower++;
            currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.OreConversion(idleGame.RoundCostConversion(idleGame.clickPower * idleGame.clickMultiplier))}</color>";    //set current text
            //set cost
            oreUpgrade[0] = idleGame.RoundCostConversion(oreUpgrade[0] * incrementMultiplier);
            ConvertCost(oreUpgrade, false);
        }
    }
    public void UpgradeCopper()
    {
        //check for requirements of upgrade.
        if (CheckOreUpgradeRequirements(oreUpgrade))
        {
            idleGame.copperPrice = 1 + (0.1 * idleGame.copperSellMultiplier) * oreUpgrade[1];    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.copperPrice)}</color>";    //set current text
            //set cost
            oreUpgrade[0] = idleGame.RoundCostConversion(oreUpgrade[0] * incrementMultiplier);
            ConvertCost(oreUpgrade, false);
        }
    }
    public void UpgradeIron()
    {
        //check for requirements of upgrade.
        if (CheckOreUpgradeRequirements(oreUpgrade))
        {
            idleGame.ironPrice = 3 + (0.5 * idleGame.ironSellMultiplier) * oreUpgrade[1];    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.ironPrice)}</color>";    //set current text
            //set cost
            oreUpgrade[0] = idleGame.RoundCostConversion(oreUpgrade[0] * incrementMultiplier);
            ConvertCost(oreUpgrade, false);
        }
    }
    public void UpgradeGold()
    {
        //check for requirements of upgrade.
        if (CheckOreUpgradeRequirements(oreUpgrade))
        {
            idleGame.goldPrice = 4 + (1.0 * idleGame.goldSellMultiplier) * oreUpgrade[1];    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.goldPrice)}</color>";    //set current text
            //set cost
            oreUpgrade[0] = idleGame.RoundCostConversion(oreUpgrade[0] * incrementMultiplier);
            ConvertCost(oreUpgrade, false);
        }
    }
    public void UpgradeDiamond()
    {
        //check for requirements of upgrade.
        if (CheckOreUpgradeRequirements(oreUpgrade))
        {
            idleGame.diamondPrice = 10 + (2.0 * idleGame.diamondSellMultiplier) * oreUpgrade[1];    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.diamondPrice)}</color>";    //set current text
            //set cost
            oreUpgrade[0] = idleGame.RoundCostConversion(oreUpgrade[0] * incrementMultiplier);
            ConvertCost(oreUpgrade, false);
        }
    }
    public void UpgradeCopperSellMultiplier()
    {
        //check for requirements of upgrade.
        if (CheckOreUpgradeRequirements(oreUpgrade))
        {
            idleGame.copperSellMultiplier += 0.1;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.copperSellMultiplier)}</color>";    //set current text
            oreMultiplierText.text = $"<color=green>{"+" + idleGame.CoinConversionTwoDecimal(0.1 * idleGame.copperSellMultiplier)}</color>";
            idleGame.copperPrice = 1 + (0.1 * idleGame.copperSellMultiplier) * oreLevelUpgrade.oreUpgrade[1];
            updateOreCurrentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.copperPrice)}</color>";
            //set cost
            oreUpgrade[0] = idleGame.RoundCostConversion(oreUpgrade[0] * incrementMultiplier);
            ConvertCost(oreUpgrade, false);
        }
    }
    public void UpgradeIronSellMultiplier()
    {
        //check for requirements of upgrade.
        if (CheckOreUpgradeRequirements(oreUpgrade))
        {
            idleGame.ironSellMultiplier += 0.1;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.ironSellMultiplier)}</color>";    //set current text
            oreMultiplierText.text = $"<color=green>{"+" + idleGame.CoinConversionTwoDecimal(0.5 * idleGame.ironSellMultiplier)}</color>";
            idleGame.ironPrice = 3 + (0.5 * idleGame.ironSellMultiplier) * oreLevelUpgrade.oreUpgrade[1];
            updateOreCurrentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.ironPrice)}</color>";
            //set cost
            oreUpgrade[0] = idleGame.RoundCostConversion(oreUpgrade[0] * incrementMultiplier);
            ConvertCost(oreUpgrade, false);
        }
    }
    public void UpgradeGoldSellMultiplier()
    {
        //check for requirements of upgrade.
        if (CheckOreUpgradeRequirements(oreUpgrade))
        {
            idleGame.goldSellMultiplier += 0.1;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.goldSellMultiplier)}</color>";    //set current text
            oreMultiplierText.text = $"<color=green>{"+" + idleGame.CoinConversionTwoDecimal(1.0 * idleGame.goldSellMultiplier)}</color>";
            idleGame.goldPrice = 4 + (1.0 * idleGame.goldSellMultiplier) * oreLevelUpgrade.oreUpgrade[1];
            updateOreCurrentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.goldPrice)}</color>";
            //set cost
            oreUpgrade[0] = idleGame.RoundCostConversion(oreUpgrade[0] * incrementMultiplier);
            ConvertCost(oreUpgrade, false);
        }
    }
    public void UpgradeDiamondSellMultiplier()
    {
        //check for requirements of upgrade.
        if (CheckOreUpgradeRequirements(oreUpgrade))
        {
            idleGame.diamondSellMultiplier += 0.1;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.diamondSellMultiplier)}</color>";    //set current text
            oreMultiplierText.text = $"<color=green>{"+" + idleGame.CoinConversionTwoDecimal(2.0 * idleGame.diamondSellMultiplier)}</color>";
            idleGame.diamondPrice = 10 + (2.0 * idleGame.diamondSellMultiplier) * oreLevelUpgrade.oreUpgrade[1];
            updateOreCurrentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.diamondPrice)}</color>";
            //set cost
            oreUpgrade[0] = idleGame.RoundCostConversion(oreUpgrade[0] * incrementMultiplier);
            ConvertCost(oreUpgrade, false);

        }
    }

    public bool CheckOreUpgradeRequirements(List<double> xUpgrade)
    {
        if (idleGame.coins >= xUpgrade[0])
        {
            idleGame.coins -= xUpgrade[0];
            xUpgrade[1]++; //level
            upgradeLevelText.text = "Lv: " + xUpgrade[1];   //set level text
            idleGame.UpdateCurrencyText();
            return true;
        }
        return false;
    }

    public void UpdateCoinCost(List<double> xUpgrade)
    {
        if (xUpgrade[0] <= idleGame.coins) coinCostText.text = $"<color=green>{convertedCoinString}</color>";    //copper
        else coinCostText.text = $"<color=red>{convertedCoinString}</color>";

        if (xUpgrade[0] > idleGame.coins)
        {
            btn.GetComponent<Button>().interactable = false;
        }
        else
        {
            btn.GetComponent<Button>().interactable = true;
        }
    }

    public void MaxedMinerUpgrade()
    {
        maxedUpgrade = true;
        copperCostText.text = "";
        ironCostText.text = "";
        goldCostText.text = "";
        diamondCostText.text = "";
        maxedText.gameObject.SetActive(true);
        btn.gameObject.SetActive(false);
    }

    public void MaxedOreUpgrade()
    {
        maxedUpgrade = true;
        coinCostText.text = "";
        maxedText.gameObject.SetActive(true);
        btn.gameObject.SetActive(false);
    }

    public void UpgradeReset()
    {
        upgradeTitleName = upgradeTitleText.text;
        incrementMultiplier = incrementStart;
        StartCost();

        if (isMinerUpgrade)
        {
            for (int i = 1; i < 5; i++)
            {
                minerUpgrade[i] = 0;
            }
            ConvertCost(minerUpgrade, true);
            ironCostText.text = "0";
            goldCostText.text = "0";
            diamondCostText.text = "0";
        }
        else
        {
            oreUpgrade[1] = 0;
            ConvertCost(oreUpgrade, false);
        }
        switch (upgradeTitleName)
        {
            //Miner Upgrades Updates
            case "Increase Speed Roll":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.speedChance.ToString("F2")}</color>";
                break;
            case "Increase Ore Roll":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.oreChance.ToString("F2")}</color>";
                break;
            case "Increase Exp Roll":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.expChance.ToString("F2")}</color>";
                break;
            case "Max Traits":
                currentText.text = "Current: " + $"<color=blue>{idleGame.maxTraits + idleGame.maxTraitsPlus}</color>";
                break;
            case "Increase Good Trait Chance":
                currentText.text = "Current: " + $"<color=blue>{idleGame.goodTraitsChance.ToString("F2") + "%"}</color>";
                break;
            case "Decrease Bad Trait Chance":
                currentText.text = "Current: " + $"<color=blue>{idleGame.badTraitsChance.ToString("F2") + "%"}</color>";
                break;
            case "Increase Terrain Trait Chance":
                currentText.text = "Current: " + $"<color=blue>{idleGame.terrainTraitsChance.ToString("F2") + "%"}</color>";
                break;
            case "Sell More Ore":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.sellOrePower.ToString("F2")}</color>";
                break;
            //Ore Upgrades Update
            case "Click Power":
                currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.RoundCostConversion(idleGame.clickPower * idleGame.clickMultiplier).ToString()}</color>";
                break;
            case "Copper Sell Price":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.copperPrice.ToString("F2")}</color>";
                break;
            case "Iron Sell Price":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.ironPrice.ToString("F2")}</color>";
                break;
            case "Gold Sell Price":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.goldPrice.ToString("F2")}</color>";
                break;
            case "Diamond Sell Price":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.diamondPrice.ToString("F2")}</color>";
                break;
            case "Copper Price Multiplier":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.copperSellMultiplier.ToString("F2")}</color>";
                oreMultiplierText.text = $"<color=green>{"+" + (0.20).ToString()}</color>";
                break;
            case "Iron Price Multiplier":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.ironSellMultiplier.ToString("F2")}</color>";
                oreMultiplierText.text = $"<color=green>{"+" + (0.50).ToString()}</color>";
                break;
            case "Gold Price Multiplier":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.goldSellMultiplier.ToString("F2")}</color>";
                oreMultiplierText.text = $"<color=green>{"+" + (1.00).ToString()}</color>";
                break;
            case "Diamond Price Multiplier":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.diamondSellMultiplier.ToString("F2")}</color>";
                oreMultiplierText.text = $"<color=green>{"+" + (2.00).ToString()}</color>";
                break;
            default:
                Debug.Log("Error: Did not find " + upgradeTitleName + " title. (Upgrade.cs)");
                break;
        }
        upgradeLevelText.text = "Lv: 0";
        if (maxedUpgrade)
        {
            maxedUpgrade = false;
            maxedText.gameObject.SetActive(false);
            btn.gameObject.SetActive(true);
        }
    }

    public void Load(GameData.UpgradesSaveData u)
    {
        upgradeTitleName = upgradeTitleText.text;
        maxedUpgrade = u.maxedUpgrade;
        incrementMultiplier = u.incrementMultiplier;
        incrementStart = u.incrementStart;
        //Set Text
        if (isMinerUpgrade)
        {
            minerUpgrade = u.minerUpgrade;
            ConvertCost(minerUpgrade, true);
            upgradeLevelText.text = "Lv: " + minerUpgrade[4];   //set level text
        }
        else
        {
            oreUpgrade = u.oreUpgrade;
            ConvertCost(oreUpgrade, false);
            upgradeLevelText.text = "Lv: " + oreUpgrade[1];   //set level text
        }

        switch (upgradeTitleName)
        {
            //Miner Upgrades Updates
            case "Increase Speed Roll":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.speedChance)}</color>";
                break;
            case "Increase Ore Roll":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.oreChance)}</color>";
                break;
            case "Increase Exp Roll":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.expChance)}</color>";
                break;
            case "Max Traits":
                currentText.text = "Current: " + $"<color=blue>{idleGame.maxTraits + idleGame.maxTraitsPlus}</color>";
                break;
            case "Increase Good Trait Chance":
                currentText.text = "Current: " + $"<color=blue>{idleGame.goodTraitsChance.ToString("F2") + "%"}</color>";
                break;
            case "Decrease Bad Trait Chance":
                currentText.text = "Current: " + $"<color=blue>{idleGame.badTraitsChance.ToString("F2") + "%"}</color>";
                break;
            case "Increase Terrain Trait Chance":
                currentText.text = "Current: " + $"<color=blue>{idleGame.terrainTraitsChance.ToString("F2") + "%"}</color>";
                break;
            case "Sell More Ore":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.sellOrePower)}</color>";
                break;
            //Ore Upgrades Update
            case "Click Power":
                currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.OreConversion(idleGame.RoundCostConversion(idleGame.clickPower * idleGame.clickMultiplier))}</color>";    //set current text
                break;
            case "Copper Sell Price":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.copperPrice)}</color>";
                break;
            case "Iron Sell Price":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.ironPrice)}</color>";
                break;
            case "Gold Sell Price":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.goldPrice)}</color>";
                break;
            case "Diamond Sell Price":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.diamondPrice)}</color>";
                break;
            case "Copper Price Multiplier":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.copperSellMultiplier)}</color>";
                oreMultiplierText.text = $"<color=green>{"+" + idleGame.CoinConversionTwoDecimal(0.2 * idleGame.copperSellMultiplier)}</color>";
                updateOreCurrentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.copperPrice)}</color>";
                break;
            case "Iron Price Multiplier":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.ironSellMultiplier)}</color>";
                oreMultiplierText.text = $"<color=green>{"+" + idleGame.CoinConversionTwoDecimal(0.5 * idleGame.ironSellMultiplier)}</color>";
                updateOreCurrentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.ironPrice)}</color>";
                break;
            case "Gold Price Multiplier":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.goldSellMultiplier)}</color>";
                oreMultiplierText.text = $"<color=green>{"+" + idleGame.CoinConversionTwoDecimal(1 * idleGame.goldSellMultiplier)}</color>";
                updateOreCurrentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.goldPrice)}</color>";
                break;
            case "Diamond Price Multiplier":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.diamondSellMultiplier)}</color>";
                oreMultiplierText.text = $"<color=green>{"+" + idleGame.CoinConversionTwoDecimal(2 * idleGame.diamondSellMultiplier)}</color>";
                updateOreCurrentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.diamondPrice)}</color>";
                break;
            default:
                Debug.Log("Error: Did not find " + upgradeTitleName + " title. (Upgrade.cs)");
                break;
        }
        if (maxedUpgrade)
        {
            if (isMinerUpgrade) MaxedMinerUpgrade();
            else MaxedOreUpgrade();
        }
    }
}
