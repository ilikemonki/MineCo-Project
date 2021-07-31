using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;

public class PrestigeUpgrade : MonoBehaviour
{
    public string id;
    public Text upgradeTitleText;
    public Text currentText;
    public Text levelText;
    public Text gemCostText;

    public IdleGame idleGame;
    public Text increaseAmountText;
    public Text maxedText;

    public bool maxedUpgrade;
    public string upgradeTitleName;
    public string gemConvertedText;
    public double gemStartingCost, gemCostIncrement;
    public Text clickPowerText;
    public Toggle demonicConstumeToggle, angelicConstumeToggle;

    //[0]-gemCost, [1]-Level
    public List<double> pUpgrade = new List<double> { 0, 0 };

    public void Start()
    {
        upgradeTitleName = upgradeTitleText.text;
        CostConvertText(pUpgrade[0]);
    }

    public void GemCostStart()
    {
        pUpgrade[0] = gemStartingCost;
    }

    public void OnEnable()
    {
        if (upgradeTitleName.Contains("Max Traits"))
            currentText.text = "Current: " + $"<color=blue>{idleGame.maxTraits.ToString("F0") + "(+" + idleGame.maxTraitsPlus.ToString("F0") + ")"}</color>";
        else if (upgradeTitleName.Contains("Increase Flat Ore"))
            currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.OreConversion(idleGame.flatOre)}</color>";
        else if (upgradeTitleName.Contains("Increase Flat Exp"))
            currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.OreConversion(idleGame.flatExp)}</color>";
    }
    public void Update()
    {
        if (!maxedUpgrade)
        {
            CheckUpdates(pUpgrade);
        }
    }

    //Plus and minus Buttons for Prestige Upgrades.
    public void ClickUpgradePlus()
    {
        //check for requirements of upgrade.
        if (CheckPUPlusRequirements(pUpgrade))
        {
            idleGame.clickMultiplier += 0.5;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.clickMultiplier)}</color>";    //set current text
            clickPowerText.text = $"<color=Green>{"+" + idleGame.CoinConversionTwoDecimal(idleGame.clickMultiplier)}</color>";
            //set cost
            pUpgrade[0] = pUpgrade[0] + gemCostIncrement;
            CostConvertText(pUpgrade[0]);
        }
    }
    public void ClickUpgradeMinus()
    {
        //check for requirements of upgrade.
        if (CheckPUMinusRequirements(pUpgrade))
        {
            idleGame.clickMultiplier -= 0.5;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.clickMultiplier)}</color>";    //set current text
            //set cost
            pUpgrade[0] = pUpgrade[0] - gemCostIncrement;
            CostConvertText(pUpgrade[0]);
        }
    }

    public void SpeedUpgradePlus()
    {
        if (pUpgrade[1] != 10)   //max lv 10
        {
            //check for requirements of upgrade.
            if (CheckPUPlusRequirements(pUpgrade))
            {
                idleGame.flatSpeed += 1;    //increase modifier
                currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.flatSpeed.ToString("F0")}</color>";    //set current text
                //set cost
                pUpgrade[0] = pUpgrade[0] + gemCostIncrement;
                CostConvertText(pUpgrade[0]);
                if (pUpgrade[1] == 10)
                {
                    MaxedUpgrade();
                }
            }
        }
    }
    public void SpeedUpgradeMinus()
    {
        //check for requirements of upgrade.
        if (CheckPUMinusRequirements(pUpgrade))
        {
            idleGame.flatSpeed -= 1;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.flatSpeed.ToString("F0")}</color>";    //set current text
            //set cost
            pUpgrade[0] = pUpgrade[0] - gemCostIncrement;
            CostConvertText(pUpgrade[0]);
            if (maxedUpgrade)
            {
                UnmaxedUpgrade();
            }
        }
    }

    public void OreUpgradePlus()
    {
        //check for requirements of upgrade.
        if (CheckPUPlusRequirements(pUpgrade))
        {
            idleGame.flatOre += 4;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.OreConversion(idleGame.flatOre)}</color>";    //set current text
            //set cost
            pUpgrade[0] = pUpgrade[0] + gemCostIncrement;
            CostConvertText(pUpgrade[0]);
        }
    }
    public void OreUpgradeMinus()
    {
        //check for requirements of upgrade.
        if (CheckPUMinusRequirements(pUpgrade))
        {
            idleGame.flatOre -= 4;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.OreConversion(idleGame.flatOre)}</color>";    //set current text
            //set cost
            pUpgrade[0] = pUpgrade[0] - gemCostIncrement;
            CostConvertText(pUpgrade[0]);
        }
    }

    public void ExpUpgradePlus()
    {
        //check for requirements of upgrade.
        if (CheckPUPlusRequirements(pUpgrade))
        {
            idleGame.flatExp += 2;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.OreConversion(idleGame.flatExp)}</color>";    //set current text
            //set cost
            pUpgrade[0] = pUpgrade[0] + gemCostIncrement;
            CostConvertText(pUpgrade[0]);
        }
    }
    public void ExpUpgradeMinus()
    {
        //check for requirements of upgrade.
        if (CheckPUMinusRequirements(pUpgrade))
        {
            idleGame.flatExp -= 2;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.OreConversion(idleGame.flatExp)}</color>";    //set current text
            //set cost
            pUpgrade[0] = pUpgrade[0] - gemCostIncrement;
            CostConvertText(pUpgrade[0]);
        }
    }

    public void DoubleOreUpgradePlus()
    {
        if (pUpgrade[1] != 20)
        {
            //check for requirements of upgrade.
            if (CheckPUPlusRequirements(pUpgrade))
            {
                idleGame.doubleOreChance += 1;    //increase modifier
                currentText.text = "Current: " + $"<color=blue>{idleGame.doubleOreChance.ToString("F2") + "%"}</color>";    //set current text
                //set cost
                pUpgrade[0] = pUpgrade[0] + gemCostIncrement;
                CostConvertText(pUpgrade[0]);
                if (pUpgrade[1] == 20)
                {
                    MaxedUpgrade();
                }
            }
        }
    }
    public void DoubleOreUpgradeMinus()
    {
        //check for requirements of upgrade.
        if (CheckPUMinusRequirements(pUpgrade))
        {
            idleGame.doubleOreChance -= 1;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{idleGame.doubleOreChance.ToString("F2") + "%"}</color>";    //set current text
            //set cost
            pUpgrade[0] = pUpgrade[0] - gemCostIncrement;
            CostConvertText(pUpgrade[0]);
            if (maxedUpgrade)
            {
                UnmaxedUpgrade();
            }
        }
    }

    public void GemUpgradePlus()
    {
        if (pUpgrade[1] != 10)
        {
            //check for requirements of upgrade.
            if (CheckPUPlusRequirements(pUpgrade))
            {
                idleGame.gemChance += 0.2;    //increase modifier
                currentText.text = "Current: " + $"<color=blue>{idleGame.gemChance.ToString("F2") + "%"}</color>";    //set current text
                //set cost
                pUpgrade[0] = pUpgrade[0] + gemCostIncrement;
                CostConvertText(pUpgrade[0]);
                if (pUpgrade[1] == 10)
                {
                    MaxedUpgrade();
                }
            }
        }
    }
    public void GemUpgradeMinus()
    {
        //check for requirements of upgrade.
        if (CheckPUMinusRequirements(pUpgrade))
        {
            idleGame.gemChance -= 0.2;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{idleGame.gemChance.ToString("F2") + "%"}</color>";    //set current text
            //set cost
            pUpgrade[0] = pUpgrade[0] - gemCostIncrement;
            CostConvertText(pUpgrade[0]);
            if (maxedUpgrade)
            {
                UnmaxedUpgrade();
            }
        }
    }

    public void HiringTimerUpgradePlus()
    {
        if (pUpgrade[1] != 12)
        {
            //check for requirements of upgrade.
            if (CheckPUPlusRequirements(pUpgrade))
            {
                idleGame.hiringBoardTimer -= 5;    //increase modifier
                var minutes = Mathf.FloorToInt((int)idleGame.hiringBoardTimer / 60);
                var seconds = Mathf.FloorToInt((int)idleGame.hiringBoardTimer % 60);
                if (seconds < 10)
                {
                    currentText.text = "Current: " + $"<color=blue>{minutes.ToString() + ":0" + seconds.ToString()}</color>";
                }
                else currentText.text = "Current: " + $"<color=blue>{minutes.ToString() + ":" + seconds.ToString()}</color>";

                //set cost
                pUpgrade[0] = pUpgrade[0] + gemCostIncrement;
                CostConvertText(pUpgrade[0]);
                if (pUpgrade[1] == 12)
                {
                    MaxedUpgrade();
                }
            }
        }
    }
    public void HiringTimerUpgradeMinus()
    {
        //check for requirements of upgrade.
        if (CheckPUMinusRequirements(pUpgrade))
        {
            idleGame.hiringBoardTimer += 5;    //increase modifier
            var minutes = Mathf.FloorToInt((int)idleGame.hiringBoardTimer / 60);
            var seconds = Mathf.FloorToInt((int)idleGame.hiringBoardTimer % 60);
            if (seconds < 10)
            {
                currentText.text = "Current: " + $"<color=blue>{minutes.ToString() + ":0" + seconds.ToString()}</color>";
            }
            else currentText.text = "Current: " + $"<color=blue>{minutes.ToString() + ":" + seconds.ToString()}</color>";
            
            //set cost
            pUpgrade[0] = pUpgrade[0] - gemCostIncrement;
            CostConvertText(pUpgrade[0]);
            if (maxedUpgrade)
            {
                UnmaxedUpgrade();
            }
        }
    }

    public void MaxTraitsUpgradePlus()
    {
        if (pUpgrade[1] != 5)
        {
            //check for requirements of upgrade.
            if (CheckPUPlusRequirements(pUpgrade))
            {
                //increase modifier
                idleGame.maxTraitsPlus += 1;
                currentText.text = "Current: " + $"<color=blue>{idleGame.maxTraits.ToString("F0") + "(+" + idleGame.maxTraitsPlus.ToString("F0") + ")"}</color>";
                //set cost
                pUpgrade[0] = pUpgrade[0] + gemCostIncrement;
                CostConvertText(pUpgrade[0]);
                if (pUpgrade[1] == 5)
                {
                    MaxedUpgrade();
                }
            }
        }
    }
    public void MaxTraitsUpgradeMinus()
    {
        //check for requirements of upgrade.
        if (CheckPUMinusRequirements(pUpgrade))
        {
            //increase modifier
            idleGame.maxTraitsPlus -= 1;
            currentText.text = "Current: " + $"<color=blue>{idleGame.maxTraits.ToString("F0") + "(+" + idleGame.maxTraitsPlus.ToString("F0") + ")"}</color>";
            //set cost
            pUpgrade[0] = pUpgrade[0] - gemCostIncrement;
            CostConvertText(pUpgrade[0]);
            if (maxedUpgrade)
            {
                UnmaxedUpgrade();
            }
        }
    }

    public void SpecialTraitUpgradePlus()
    {
        if (pUpgrade[1] != 2)
        {
            //check for requirements of upgrade.
            if (CheckPUPlusRequirements(pUpgrade))
            {
                idleGame.specialTraitAmount += 1;    //increase modifier
                currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.specialTraitAmount.ToString("F0") + " (15% Chance)"}</color>";    //set current text
                //set cost
                pUpgrade[0] = pUpgrade[0] + gemCostIncrement;
                CostConvertText(pUpgrade[0]);
                if (pUpgrade[1] == 2)
                {
                    MaxedUpgrade();
                }

                demonicConstumeToggle.interactable = true;
                angelicConstumeToggle.interactable = true;
            }
        }
    }
    public void SpecialTraitUpgradeMinus()
    {
        //check for requirements of upgrade.
        if (CheckPUMinusRequirements(pUpgrade))
        {
            idleGame.specialTraitAmount -= 1;    //increase modifier
            currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.specialTraitAmount.ToString("F0") + " (15% Chance)"}</color>";    //set current text
            //set cost
            pUpgrade[0] = pUpgrade[0] - gemCostIncrement;
            CostConvertText(pUpgrade[0]);
            if (maxedUpgrade)
            {
                UnmaxedUpgrade();
            }
        }
        if (pUpgrade[1] == 0)
        {
            demonicConstumeToggle.interactable = false;
            angelicConstumeToggle.interactable = false;
        }
    }

    public bool CheckPUPlusRequirements(List<double> xUpgrade)  //Check gem requirement. Increase level and increment var
    {
        if (idleGame.gems >= xUpgrade[0])
        {
            idleGame.gems -= xUpgrade[0];    //gems
            xUpgrade[1]++;  //level up
            levelText.text = "Lv: " + xUpgrade[1];   //set level text
            idleGame.UpdateCurrencyText();
            return true;
        }
        return false;
    }

    public bool CheckPUMinusRequirements(List<double> xUpgrade)  //Decrease level and decrement var
    {
        if (xUpgrade[1] >= 1)
        {
            idleGame.gems += xUpgrade[0] - gemCostIncrement;    //gems
            xUpgrade[1]--;  //level up
            levelText.text = "Lv: " + xUpgrade[1];   //set level text
            idleGame.UpdateCurrencyText();
            return true;
        }
        return false;
    }

    public void MaxedUpgrade()
    {
        maxedUpgrade = true;
        gemCostText.text = "";
        increaseAmountText.gameObject.SetActive(false);
        maxedText.gameObject.SetActive(true);
    }

    public void UnmaxedUpgrade()
    {
        maxedUpgrade = false;
        increaseAmountText.gameObject.SetActive(true);
        maxedText.gameObject.SetActive(false);
    }

    public void CheckUpdates(List<double> xUpgrade)  //Decrease level and decrement var
    {
        if (idleGame.gems >= xUpgrade[0])
        {
            gemCostText.text = $"<color=green>{gemConvertedText}</color>";
        }
        else gemCostText.text = $"<color=red>{gemConvertedText}</color>";
    }

    public void CostConvertText(double gemCost)
    {
        gemConvertedText = idleGame.NormalConversion(gemCost);
    }

    public void PrestigeSettingsReset()
    {
        upgradeTitleName = upgradeTitleText.text;
        GemCostStart();
        pUpgrade[1] = 0;
        CostConvertText(pUpgrade[0]);
        switch (upgradeTitleName)
        {
            case "Click Multiplier":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.clickMultiplier.ToString("F2")}</color>";
                break;
            case "Increase Flat Speed":
                currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.flatSpeed.ToString("F0")}</color>";
                break;
            case "Increase Flat Ore":
                currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.flatOre.ToString("F0")}</color>";
                break;
            case "Increase Flat Exp":
                currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.flatExp.ToString("F0")}</color>";
                break;
            case "Double Ore Chance":
                currentText.text = "Current: " + $"<color=blue>{idleGame.doubleOreChance.ToString("F2") + "%"}</color>";
                break;
            case "Gem Chance":
                currentText.text = "Current: " + $"<color=blue>{idleGame.gemChance.ToString("F2") + "%"}</color>";
                break;
            case "Lower Hiring Timer":
                currentText.text = "Current: " + $"<color=blue>{"2:00"}</color>";
                break;
            case "Max Traits +":
                currentText.text = "Current: " + $"<color=blue>{idleGame.maxTraits.ToString("F0") + "(+" + idleGame.maxTraitsPlus.ToString("F0") + ")"}</color>";
                break;
            case "Max Special Trait":
                currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.specialTraitAmount.ToString("F0") + " (15% Chance)"}</color>";
                demonicConstumeToggle.interactable = false;
                angelicConstumeToggle.interactable = false;
                break;
            default:
                break;
        }
        levelText.text = "Lv. 0";
        if (maxedUpgrade)
        {
            UnmaxedUpgrade();
        }
    }
    public void Load(GameData.PrestigeUpgradesSaveData p)
    {
        upgradeTitleName = upgradeTitleText.text;
        maxedUpgrade = p.maxedUpgrade;
        pUpgrade = p.pUpgrade;

        CostConvertText(pUpgrade[0]);
        levelText.text = "Lv: " + pUpgrade[1];
        switch (upgradeTitleName)
        {
            case "Click Multiplier":
                currentText.text = "Current: " + $"<color=blue>{"x" + idleGame.CoinConversionTwoDecimal(idleGame.clickMultiplier)}</color>";
                break;
            case "Increase Flat Speed":
                currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.flatSpeed.ToString("F0")}</color>";
                break;
            case "Increase Flat Ore":
                currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.OreConversion(idleGame.flatOre)}</color>";
                break;
            case "Increase Flat Exp":
                currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.OreConversion(idleGame.flatExp)}</color>";
                break;
            case "Double Ore Chance":
                currentText.text = "Current: " + $"<color=blue>{idleGame.doubleOreChance.ToString("F2") + "%"}</color>";
                break;
            case "Gem Chance":
                currentText.text = "Current: " + $"<color=blue>{idleGame.gemChance.ToString("F2") + "%"}</color>";
                break;
            case "Lower Hiring Timer":
                var minutes = Mathf.FloorToInt((int)idleGame.hiringBoardTimer / 60);
                var seconds = Mathf.FloorToInt((int)idleGame.hiringBoardTimer % 60);
                if (seconds < 10)
                {
                    currentText.text = "Current: " + $"<color=blue>{minutes.ToString() + ":0" + seconds.ToString()}</color>";
                }
                else currentText.text = "Current: " + $"<color=blue>{minutes.ToString() + ":" + seconds.ToString()}</color>";
                break;
            case "Max Traits +":
                currentText.text = "Current: " + $"<color=blue>{idleGame.maxTraits.ToString("F0") + "(+" + idleGame.maxTraitsPlus.ToString("F0") + ")"}</color>";
                break;
            case "Max Special Trait":
                currentText.text = "Current: " + $"<color=blue>{"+" + idleGame.specialTraitAmount.ToString("F0") + " (15% Chance)"}</color>";
                if (pUpgrade[1] > 0)
                {
                    demonicConstumeToggle.interactable = true;
                    angelicConstumeToggle.interactable = true;
                }
                break;
            default:
                break;
        }

        if (maxedUpgrade)
        {
            MaxedUpgrade();
        }
    }
}
