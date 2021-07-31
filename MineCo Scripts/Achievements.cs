using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Achievements : MonoBehaviour
{
    public string id;
    public IdleGame idleGame;
    public Text achieveTitleText;
    public Text currentAmountText;
    public TextMeshProUGUI gemAmountext;
    public int gemAmount, gemStart;
    public Button acceptBtn;
    public bool keepChecking;
    public bool maxedAchievementReached;
    public PopupWindow popupWindow;
    public GameObject storeCopper;
    public Text maxedText;
    public PopupText popupRewardText;
    public int achieveCounter;  //record number of achieves claimed for this achievement
    public double achievementToReach, achievementToReachStart;

    public void Start()
    {
        gemStart = gemAmount;
        achievementToReachStart = achievementToReach;
    }

    public void Update()
    {
        if (!maxedAchievementReached)
        {
            UpdateCurrentAmount();  //update current amount
                                    //keep checking if variable hit the Reached amount and show button
            if (keepChecking)
            {
                if (idleGame.playerLevel >= achievementToReach && achieveTitleText.text.Contains("Reach Level"))
                {
                    DisplayButton();
                }
                else if (idleGame.totalMinersHired >= achievementToReach && achieveTitleText.text.Contains("Total Miners"))
                {
                    DisplayButton();
                }
                else if (idleGame.amountOfMinersWorking >= achievementToReach && achieveTitleText.text.Contains("Miners Working"))
                {
                    DisplayButton();
                }
                else if (idleGame.highestSpeedOnMiner >= achievementToReach && achieveTitleText.text.Contains("Speed"))
                {
                    DisplayButton();
                }
                else if (idleGame.highestOreOnMiner >= achievementToReach && achieveTitleText.text.Contains("Ore"))
                {
                    DisplayButton();
                }
                else if (idleGame.highestExpOnMiner >= achievementToReach && achieveTitleText.text.Contains("Exp"))
                {
                    DisplayButton();
                }
                else if (idleGame.badtraitsAmountOnMiner >= achievementToReach && achieveTitleText.text.Contains("Bad"))
                {
                    DisplayButton();
                }
                else if (idleGame.goodtraitsAmountOnMiner >= achievementToReach && achieveTitleText.text.Contains("Good"))
                {
                    DisplayButton();
                }
                else if (idleGame.terraintraitsAmountOnMiner >= achievementToReach && achieveTitleText.text.Contains("Terrain"))
                {
                    DisplayButton();
                }
                else if (idleGame.minesUnlocked >= achievementToReach && achieveTitleText.text.Contains("Unlock Mine"))
                {
                    DisplayButton();
                }
                else if (idleGame.totalCoins >= achievementToReach && achieveTitleText.text.Contains("Total Coins"))
                {
                    DisplayButton();
                }
                else if (idleGame.totalCopperSold >= achievementToReach && achieveTitleText.text.Contains("Total Copper Sold"))
                {
                    DisplayButton();
                }
                else if (idleGame.totalIronSold >= achievementToReach && achieveTitleText.text.Contains("Total Iron Sold"))
                {
                    DisplayButton();
                }
                else if (idleGame.totalGoldSold >= achievementToReach && achieveTitleText.text.Contains("Total Gold Sold"))
                {
                    DisplayButton();
                }
                else if (idleGame.totalDiamondSold >= achievementToReach && achieveTitleText.text.Contains("Total Diamond Sold"))
                {
                    DisplayButton();
                }
            }
        }
    }

    public void DisplayButton()
    {
        acceptBtn.interactable = true;
        keepChecking = false;
    }

    public void UpdateCurrentAmount()
    {
        if (achieveTitleText.text.Contains("Reach Level"))
        {
            SetCurrentAmountText(idleGame.playerLevel, achievementToReach);
        }
        else if (achieveTitleText.text.Contains("Total Miners"))
        {
            SetCurrentAmountText(idleGame.totalMinersHired, achievementToReach);
        }
        else if (achieveTitleText.text.Contains("Miners Working"))
        {
            SetCurrentAmountText(idleGame.amountOfMinersWorking, achievementToReach);
        }
        else if (achieveTitleText.text.Contains("Speed"))
        {
            SetCurrentAmountText(idleGame.highestSpeedOnMiner, achievementToReach);
        }
        else if (achieveTitleText.text.Contains("Ore"))
        {
            SetCurrentAmountText(idleGame.highestOreOnMiner, achievementToReach);
        }
        else if (achieveTitleText.text.Contains("Exp"))
        {
            SetCurrentAmountText(idleGame.highestExpOnMiner, achievementToReach);
        }
        else if (achieveTitleText.text.Contains("Bad"))
        {
            SetCurrentAmountText(idleGame.badtraitsAmountOnMiner, achievementToReach);
        }
        else if (achieveTitleText.text.Contains("Good"))
        {
            SetCurrentAmountText(idleGame.goodtraitsAmountOnMiner, achievementToReach);
        }
        else if (achieveTitleText.text.Contains("Terrain"))
        {
            SetCurrentAmountText(idleGame.terraintraitsAmountOnMiner, achievementToReach);
        }
        else if (achieveTitleText.text.Contains("Unlock Mine"))
        {
            SetCurrentAmountText(idleGame.minesUnlocked, achievementToReach);
        }
        else if (achieveTitleText.text.Contains("Total Coins"))
        {
            if (idleGame.totalCoins >= achievementToReach)
            {
                currentAmountText.text = $"<color=Green>{idleGame.CoinConversionTwoDecimal(idleGame.totalCoins)}</color>" + "/" + idleGame.CoinConversionTwoDecimal(achievementToReach);
            }
            else currentAmountText.text = $"<color=Red>{idleGame.CoinConversionTwoDecimal(idleGame.totalCoins)}</color>" + "/" + idleGame.CoinConversionTwoDecimal(achievementToReach);
        }
        else if (achieveTitleText.text.Contains("Total Copper Sold"))
        {
            if (idleGame.totalCopperSold >= achievementToReach)
            {
                currentAmountText.text = $"<color=Green>{idleGame.OreConversion(idleGame.totalCopperSold)}</color>" + "/" + idleGame.OreConversion(achievementToReach);
            }
            else currentAmountText.text = $"<color=Red>{idleGame.OreConversion(idleGame.totalCopperSold)}</color>" + "/" + idleGame.OreConversion(achievementToReach);
        }
        else if (achieveTitleText.text.Contains("Total Iron Sold"))
        {
            if (idleGame.totalIronSold >= achievementToReach)
            {
                currentAmountText.text = $"<color=Green>{idleGame.OreConversion(idleGame.totalIronSold)}</color>" + "/" + idleGame.OreConversion(achievementToReach);
            }
            else currentAmountText.text = $"<color=Red>{idleGame.OreConversion(idleGame.totalIronSold)}</color>" + "/" + idleGame.OreConversion(achievementToReach);
        }
        else if (achieveTitleText.text.Contains("Total Gold Sold"))
        {
            if (idleGame.totalGoldSold >= achievementToReach)
            {
                currentAmountText.text = $"<color=Green>{idleGame.OreConversion(idleGame.totalGoldSold)}</color>" + "/" + idleGame.OreConversion(achievementToReach);
            }
            else currentAmountText.text = $"<color=Red>{idleGame.OreConversion(idleGame.totalGoldSold)}</color>" + "/" + idleGame.OreConversion(achievementToReach);
        }
        else if (achieveTitleText.text.Contains("Total Diamond Sold"))
        {
            if (idleGame.totalDiamondSold >= achievementToReach)
            {
                currentAmountText.text = $"<color=Green>{idleGame.OreConversion(idleGame.totalDiamondSold)}</color>" + "/" + idleGame.OreConversion(achievementToReach);
            }
            else currentAmountText.text = $"<color=Red>{idleGame.OreConversion(idleGame.totalDiamondSold)}</color>" + "/" + idleGame.OreConversion(achievementToReach);
        }
    }

    public void SetCurrentAmountText(double currentAmount, double amountToReach)
    {
        if (currentAmount >= amountToReach)
        {
            currentAmountText.text = $"<color=Green>{idleGame.OreConversion(currentAmount)}</color>" + "/" + amountToReach;
        }
        else currentAmountText.text = $"<color=Red>{idleGame.OreConversion(currentAmount)}</color>" + "/" + amountToReach;
    }

    
    public void Reward()
    {
        keepChecking = true;
        idleGame.gems += gemAmount;
        popupRewardText.SetGemReward("+" + gemAmount + " <sprite=5>");
        popupRewardText.ShowPopup();
        achieveCounter++;
        acceptBtn.interactable = false;
        idleGame.UpdateCurrencyText();
    }

    public void SetGemText()
    {
        gemAmountext.text = gemAmount + "<sprite=0>";
    }

    public void LevelAchievementBtn()
    {
        if (achieveCounter % 4 == 0)    //every 4 claims, increase flat exp
        {
            idleGame.flatExp += 0.5;
            popupWindow.SetText("Reached level " + idleGame.OreConversion(achievementToReach) + ".\n+0.5 Flat EXP");
        }
        Reward();   //Get gem reward.
        achievementToReach += 5;    //update next achievement goal
        achieveTitleText.text = "Reach Level " + idleGame.OreConversion(achievementToReach);    //update title.
        if (achieveCounter % 2 == 0)   //increase gem amount every 10 levels.
        {
            gemAmount++;
        }
        SetGemText();   //set gem amount text.
    }

    public void TotalMinersAchievementBtn()
    {
        Reward();
        achievementToReach += 10;
        achieveTitleText.text = "Hire " + idleGame.OreConversion(achievementToReach) + " Total Miners";
        SetGemText();
    }

    public void MinersWorkingAchievementBtn()
    {
        Reward();
        if (achievementToReach == 90)
        {
            Maxed();
        }
        else
        {
            achievementToReach += 5;
            achieveTitleText.text = achievementToReach + " Miners Working";
            if (achieveCounter % 3 == 0)
            {
                gemAmount++;
            }
            SetGemText();
        }
    }

    public void MinerSpeedAchievementBtn()
    {
        Reward();
        if (achievementToReach == 50)
        {
            Maxed();
        }
        else
        {
            achievementToReach += 5;
            achieveTitleText.text = "Hire Miner With " + idleGame.OreConversion(achievementToReach) + "+" + " Speed";
            if (achieveCounter % 2 == 0)
            {
                gemAmount++;
            }
        }
        SetGemText();
    }
    public void MinerOreAchievementBtn()
    {
        Reward();
        achievementToReach += 25;
        achieveTitleText.text = "Hire Miner With " + idleGame.OreConversion(achievementToReach) + "+" + " Ore";
        gemAmount = 1;  //Delete after publish update 1.0.1
        SetGemText();
    }
    public void MinerExpAchievementBtn()
    {
        Reward();
        achievementToReach += 10;
        achieveTitleText.text = "Hire Miner With " + idleGame.OreConversion(achievementToReach) + "+" + " Exp";
        gemAmount = 1; //Delete after publish update 1.0.1
        SetGemText();
    }

    public void BadTraitsAchievementBtn()
    {
        Reward();
        if (achievementToReach == 10)
        {
            Maxed();
        }
        else
        {
            achievementToReach += 2;
            achieveTitleText.text = "Hire Miner With " + achievementToReach + "+" + " Bad Traits";
            gemAmount += 2;
            SetGemText();
        }
    }
    public void GoodTraitsAchievementBtn()
    {
        Reward();
        if (achievementToReach == 10)
        {
            Maxed();
        }
        else
        {
            achievementToReach += 2;
            achieveTitleText.text = "Hire Miner With " + achievementToReach + "+" + " Good Traits";
            gemAmount += 2;
            SetGemText();
        }
    }
    public void TerrainTraitsAchievementBtn()
    {
        Reward();
        if (achievementToReach == 3)
        {
            Maxed();
        }
        else
        {
            achievementToReach += 1;
            achieveTitleText.text = "Hire Miner With " + achievementToReach + "+" + " Terrain Traits";
            gemAmount += 5;
            SetGemText();
        }
    }
    public void UnlockMinesAchievementBtn()
    {
        if (achievementToReach >= 5)    //Increase copper output.
        {
            switch (achievementToReach)
            {
                case 5:
                    storeCopper.tag = "StoreDoubleCopperOre";
                    popupWindow.SetText("Unlocked Mine 5.\nx1.5 Copper mined.");
                    break;
                case 6:
                    storeCopper.tag = "StoreTripleCopperOre";
                    popupWindow.SetText("Unlocked Mine 6.\nx2 Copper mined.");
                    break;
                case 7:
                    storeCopper.tag = "StoreQuadCopperOre";
                    popupWindow.SetText("Unlocked Mine 7.\nx3 Copper mined.");
                    break;
                case 8:
                    storeCopper.tag = "StorePentaCopperOre";
                    popupWindow.SetText("Unlocked Mine 8.\nx4 Copper mined.");
                    break;
                default:
                    break;
            }
        }
        Reward();
        if (achievementToReach == 8)
        {
            Maxed();
        }
        else
        {
            achievementToReach += 1;
            achieveTitleText.text = "Unlock Mine " + achievementToReach;
            gemAmount++;
            SetGemText();
        }
    }

    public void CoinsAchievementBtn()
    {
        Reward();
        achievementToReach *= 2;
        achieveTitleText.text = idleGame.CoinConversionTwoDecimal(achievementToReach) + " Total Coins";
        SetGemText();
    }

    public void CopperSoldAchievementBtn()
    {
        if (achieveCounter % 3 == 0)
        {
            idleGame.flatOre += 0.25;
            popupWindow.SetText("Sold " + idleGame.OreConversion(achievementToReach) + " Copper ores.\n+0.25 Flat Ore");
        }
        Reward();
        achievementToReach *= 5;
        achieveTitleText.text = idleGame.OreConversion(achievementToReach) + " Total Copper Sold";
        if (achieveCounter % 3 == 0)
        {
            gemAmount++;
        }
        SetGemText();
    }
    public void IronSoldAchievementBtn()
    {
        if (achieveCounter % 3 == 0)
        {
            idleGame.flatOre += 0.25;
            popupWindow.SetText("Sold " + idleGame.OreConversion(achievementToReach) + " Iron ores.\n+0.25 Flat Ore");
        }
        Reward();
        achievementToReach *= 5;
        achieveTitleText.text = idleGame.OreConversion(achievementToReach) + " Total Iron Sold";
        if (achieveCounter % 3 == 0)
        {
            gemAmount++;
        }
        SetGemText();
    }
    public void GoldSoldAchievementBtn()
    {
        if (achieveCounter % 3 == 0)
        {
            idleGame.flatOre += 0.25;
            popupWindow.SetText("Sold " + idleGame.OreConversion(achievementToReach) + " Gold ores.\n+0.25 Flat Ore");
        }
        Reward();
        achievementToReach *= 5;
        achieveTitleText.text = idleGame.OreConversion(achievementToReach) + " Total Gold Sold";
        if (achieveCounter % 3 == 0)
        {
            gemAmount++;
        }
        SetGemText();
    }
    public void DiamondSoldAchievementBtn()
    {
        if (achieveCounter % 3 == 0)
        {
            idleGame.flatOre += 0.25;
            popupWindow.SetText("Sold " + idleGame.OreConversion(achievementToReach) + " Diamond ores.\n+0.25 Flat Ore");
        }
        Reward();
        achievementToReach *= 5;
        achieveTitleText.text = idleGame.OreConversion(achievementToReach) + " Total Diamond Sold";
        if (achieveCounter % 3 == 0)
        {
            gemAmount++;
        }
        SetGemText();
    }

    public void Maxed()
    {
        maxedAchievementReached = true;
        currentAmountText.gameObject.SetActive(false);
        maxedText.gameObject.SetActive(true);
        acceptBtn.gameObject.SetActive(false);
    }

    public void ResetAchievements()
    {
        gemAmount = gemStart;
        achieveCounter = 1;
        achievementToReach = achievementToReachStart;
        if (achieveTitleText.text.Contains("Reach Level"))
        {
            achieveTitleText.text = "Reach Level " + achievementToReach;
        }
        else if (achieveTitleText.text.Contains("Total Miners"))
        {
            achieveTitleText.text = "Hire " + achievementToReach + " Total Miners";
        }
        else if (achieveTitleText.text.Contains("Miners Working"))
        {
            achieveTitleText.text = achievementToReach + " Miners Working";
        }
        else if (achieveTitleText.text.Contains("Speed"))
        {
            achieveTitleText.text = "Hire Miner With " + achievementToReach + "+" + " Speed";
        }
        else if (achieveTitleText.text.Contains("Ore"))
        {
            achieveTitleText.text = "Hire Miner With " + achievementToReach + "+" + " Ore";
        }
        else if (achieveTitleText.text.Contains("Exp"))
        {
            achieveTitleText.text = "Hire Miner With " + achievementToReach + "+" + " Exp";
        }
        else if (achieveTitleText.text.Contains("Bad"))
        {
            achieveTitleText.text = "Hire Miner With " + achievementToReach + "+" + " Bad Traits";
        }
        else if (achieveTitleText.text.Contains("Good"))
        {
            achieveTitleText.text = "Hire Miner With " + achievementToReach + "+" + " Good Traits";
        }
        else if (achieveTitleText.text.Contains("Terrain"))
        {
            achieveTitleText.text = "Hire Miner With " + achievementToReach + "+" + " Terrain Traits";
        }
        else if (achieveTitleText.text.Contains("Unlock Mine"))
        {
            storeCopper.tag = "StoreCopperOre";
            achieveTitleText.text = "Unlock Mine " + achievementToReach;
        }
        else if (achieveTitleText.text.Contains("Total Coins"))
        {
            achieveTitleText.text = idleGame.CoinConversionTwoDecimal(achievementToReach) + " Total Coins";
        }
        else if (achieveTitleText.text.Contains("Total Copper Sold"))
        {
            achieveTitleText.text = idleGame.OreConversion(achievementToReach) + " Total Copper Sold";
        }
        else if (achieveTitleText.text.Contains("Total Iron Sold"))
        {
            achieveTitleText.text = idleGame.OreConversion(achievementToReach) + " Total Iron Sold";
        }
        else if (achieveTitleText.text.Contains("Total Gold Sold"))
        {
            achieveTitleText.text = idleGame.OreConversion(achievementToReach) + " Total Gold Sold";
        }
        else if (achieveTitleText.text.Contains("Total Diamond Sold"))
        {
            achieveTitleText.text = idleGame.OreConversion(achievementToReach) + " Total Diamond Sold";
        }
        keepChecking = true;
        if (maxedAchievementReached)
        {
            currentAmountText.gameObject.SetActive(true);
            maxedText.gameObject.SetActive(false);
            acceptBtn.gameObject.SetActive(true);
            maxedAchievementReached = false;
        }
    }
    public void Load(GameData.AchieveSaveData a)
    {
        gemAmount = a.gemAmount;
        gemStart = a.gemStart;
        //keepChecking = a.keepChecking;
        maxedAchievementReached = a.maxedAchievementReached;
        if (maxedAchievementReached)
        {
            keepChecking = false;
        }
        else
            keepChecking = true;
        achieveCounter = a.achieveCounter;
        achievementToReach = a.achievementToReach;
        achievementToReachStart = a.achievementToReachStart;

        if (achieveTitleText.text.Contains("Reach Level"))
        {
            achieveTitleText.text = "Reach Level " + idleGame.OreConversion(achievementToReach);
        }
        else if (achieveTitleText.text.Contains("Total Miners"))
        {
            achieveTitleText.text = "Hire " + idleGame.OreConversion(achievementToReach) + " Total Miners";
        }
        else if (achieveTitleText.text.Contains("Miners Working"))
        {
            achieveTitleText.text = idleGame.OreConversion(achievementToReach) + " Miners Working";
        }
        else if (achieveTitleText.text.Contains("Speed"))
        {
            achieveTitleText.text = "Hire Miner With " + idleGame.OreConversion(achievementToReach) + "+" + " Speed";
        }
        else if (achieveTitleText.text.Contains("Ore"))
        {
            achieveTitleText.text = "Hire Miner With " + idleGame.OreConversion(achievementToReach) + "+" + " Ore";
            gemAmount = 1; //Delete after publish update 1.0.1
        }
        else if (achieveTitleText.text.Contains("Exp"))
        {
            achieveTitleText.text = "Hire Miner With " + idleGame.OreConversion(achievementToReach) + "+" + " Exp";
            gemAmount = 1; //Delete after publish update 1.0.1
        }
        else if (achieveTitleText.text.Contains("Bad"))
        {
            achieveTitleText.text = "Hire Miner With " + achievementToReach + "+" + " Bad Traits";
        }
        else if (achieveTitleText.text.Contains("Good"))
        {
            achieveTitleText.text = "Hire Miner With " + achievementToReach + "+" + " Good Traits";
        }
        else if (achieveTitleText.text.Contains("Terrain"))
        {
            achieveTitleText.text = "Hire Miner With " + achievementToReach + "+" + " Terrain Traits";
        }
        else if (achieveTitleText.text.Contains("Unlock Mine"))
        {
            if (achievementToReach >= 5)
            {
                switch (achievementToReach)
                {
                    case 5:
                        storeCopper.tag = "StoreDoubleCopperOre";
                        break;
                    case 6:
                        storeCopper.tag = "StoreTripleCopperOre";
                        break;
                    case 7:
                        storeCopper.tag = "StoreQuadCopperOre";
                        break;
                    case 8:
                        storeCopper.tag = "StorePentaCopperOre";
                        break;
                    default:
                        break;
                }
            }
            achieveTitleText.text = "Unlock Mine " + achievementToReach;
        }
        else if (achieveTitleText.text.Contains("Total Coins"))
        {
            achieveTitleText.text = idleGame.CoinConversionTwoDecimal(achievementToReach) + " Total Coins";
        }
        else if (achieveTitleText.text.Contains("Total Copper Sold"))
        {
            achieveTitleText.text = idleGame.OreConversion(achievementToReach) + " Total Copper Sold";
        }
        else if (achieveTitleText.text.Contains("Total Iron Sold"))
        {
            achieveTitleText.text = idleGame.OreConversion(achievementToReach) + " Total Iron Sold";
        }
        else if (achieveTitleText.text.Contains("Total Gold Sold"))
        {
            achieveTitleText.text = idleGame.OreConversion(achievementToReach) + " Total Gold Sold";
        }
        else if (achieveTitleText.text.Contains("Total Diamond Sold"))
        {
            achieveTitleText.text = idleGame.OreConversion(achievementToReach) + " Total Diamond Sold";
        }
        if (maxedAchievementReached)
        {
            Maxed();
        }
        SetGemText();
    }
}
