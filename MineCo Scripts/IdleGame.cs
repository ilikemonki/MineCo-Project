using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;
using UnityEngine.SceneManagement;

[System.Serializable]
public class IdleGame : MonoBehaviour
{
    public Text coinsText;
    public double coins;
    public Text gemsText;
    public double gems, gemsBought;
    public ProgressBar expBar;
    public double currentExp;
    public double maxExp;
    public Text maxExpText;
    public int playerLevel;
    public Text playerLevelText;
    // Ore
    public Text copperText, ironText, goldText, diamondText;
    public double copper, iron, gold, diamond;
    //Miner upgrades
    public double speedChance, oreChance, expChance, maxTraits, goodTraitsChance, badTraitsChance, terrainTraitsChance;
    public double sellOrePower;
    public double clickPower;
    //Ore upgrades
    public double copperPrice, ironPrice, goldPrice, diamondPrice;
    public double copperSellMultiplier, ironSellMultiplier, goldSellMultiplier, diamondSellMultiplier;
    //Prestige Upgrades
    public double flatSpeed, flatOre, flatExp, hiringBoardTimer, specialTraitAmount;
    public double doubleOreChance, maxTraitsPlus;
    public double gemChance, clickMultiplier;
    //Achievements variables
    public double totalCoins;
    public double totalMinersHired;
    public double terraintraitsAmountOnMiner; //Set the var to the miner with the most terrain(3).
    public double goodtraitsAmountOnMiner;  //Set this var to miner with most good traits.
    public double badtraitsAmountOnMiner;
    public double highestSpeedOnMiner;
    public double highestOreOnMiner;
    public double highestExpOnMiner;
    public double amountOfMinersWorking;
    public double minesUnlocked;
    public double totalCopperSold, totalIronSold, totalGoldSold, totalDiamondSold;
    //Settings
    public bool activateConfirmation;
    public bool showTutorial;
    public bool ads;    //admob
    public int numOfSpins;  //Wheel
    public double totalPrestigeGems, prestigeResetCounter;  //Prestige System

    public SellingOre sellingOre;
    public GameObject inventoryParent;
    public GameObject mineInvParent;
    public GameObject hiringParent;
    public AdMob adMob;
    public StartResetPop startResetPop;
    public HiringTimer hiringTimer;
    public LoadData loadData;
    public CloudSaving cloudSaving;
    public LoadingSceneInGame loadingScene;
    public GemSpawn gemSpawn;
    public SpinAd spinAd;
    public PopupText savingUIPopup;

    public void Start()
    {
        inventoryParent.SetActive(false);
        hiringParent.SetActive(false);
        mineInvParent.SetActive(false);
        expBar.SetMaxValue(maxExp);
    }

    public void CoinClicker()
    {
        coins += 1000000000;
        gems += 10000;
        copper += 1000000000;
        iron += 1000000000;
        gold += 1000000000;
        diamond += 1000000000;
        GainExp(10);
        UpdateCurrencyText();
        UpdateOresText();
    }

    //check for level and set exp text
    public void GainExp(double amount)
    {
        do
        {
            if (amount >= maxExp - currentExp)  //amount goes over maxExp. Level up
            {
                amount -= maxExp - currentExp;
                currentExp = 0;
                playerLevel += 1;
                playerLevelText.text = "Lv:" + playerLevel;
                maxExp = RoundCostConversion(maxExp * 1.20);
                expBar.SetMaxValue(maxExp);
            }
            else
            {
                currentExp += amount;
                break;
            }
        }
        while (amount > 0);
        
        expBar.SetCurrentValue(currentExp);
        maxExpText.text = CoinConversionTwoDecimal(currentExp) + "/" + CoinConversionTwoDecimal(maxExp) + " exp";
    }

    public string CoinConversionTwoDecimal(double c)
    {
        if (c >= 1E15)  //if larger/equal to quadtrillion, call 2nd conversion function.
        {
            return CoinConversionTwoDecimal2(c);
        }
        if (c >= 1E12) //trillions
        {
            return (c / 1E12).ToString("F2") + "T";
        }
        else if (c >= 1000000000) //billions
        {
            return (c / 1000000000).ToString("F2") + "B";
        }
        else if (c >= 1000000) //millions
        {
            return (c / 1000000).ToString("F2") + "M";
        }
        else if (c >= 1000) //thousands
        {
            return (c / 1000).ToString("F2") + "K";
        }
        else return c.ToString("F2");
    }

    public string ConversionNoDecimal(double c)
    {
        if (c >= 1E15)  //if larger/equal to quadtrillion, call 2nd conversion function.
        {
            return CoinConversionNoDecimal2(c);
        }
        if (c >= 1E12) //trillions
        {
            return (c / 1E12).ToString() + "T";
        }
        if (c >= 1000000000) //billions
        {
            return (c / 1000000000).ToString() + "B";
        }
        else if (c >= 1000000) //millions
        {
            return (c / 1000000).ToString() + "M";
        }
        else if (c >= 1000) //thousands
        {
            return (c / 1000).ToString() + "K";
        }
        else return c.ToString();
    }

    //the difference is, anything less than 1k is a flat number. Everything else is the same as coinConversion
    public string OreConversion(double c)
    {
        if (c >= 1E15)  //if larger/equal to quadtrillion, call 2nd conversion function.
        {
            return CoinConversionTwoDecimal2(c);
        }
        if (c >= 1E12) //trillions
        {
            return (c / 1E12).ToString("F2") + "T";
        }
        if (c >= 1000000000) //billions
        {
            return (c / 1000000000).ToString("F2") + "B";
        }
        else if (c >= 1000000) //millions
        {
            return (c / 1000000).ToString("F2") + "M";
        }
        else if (c >= 1000) //thousands
        {
            return (c / 1000).ToString("F2") + "K";
        }
        else return c.ToString();
    }

    public string CoinConversionTwoDecimal2(double c)
    {
        if (c >= 1E33)    //larger than nonillion
        {
            return CoinConversionTwoDecimal3(c);
        }
        if (c >= 1E30) //Nonillions
        {
            return (c / 1E30).ToString("F2") + "Non";
        }
        else if (c >= 1E27) //Octillions
        {
            return (c / 1E27).ToString("F2") + "Oct";
        }
        else if (c >= 1E24) //Septillions
        {
            return (c / 1E24).ToString("F2") + "Sp";
        }
        else if (c >= 1E21) //Sextillions
        {
            return (c / 1E21).ToString("F2") + "Sx";
        }
        else if (c >= 1E18) //Quintillions
        {
            return (c / 1E18).ToString("F2") + "Qn";
        }
        else //Quadtillions
        {
            return (c / 1E15).ToString("F2") + "Qd";
        }
    }
    public string CoinConversionTwoDecimal3(double c)
    {
        if (c >= 1E54) return "Infinite";
        else if (c >= 1E51) //Sexdecillions
        {
            return (c / 1E51).ToString("F2") + "Sxd";
        }
        else if (c >= 1E48) //Quindecillion 
        {
            return (c / 1E48).ToString("F2") + "Qnd";
        }
        else if (c >= 1E45) //Quattuordecillions
        {
            return (c / 1E45).ToString("F2") + "Qt";
        }
        else if (c >= 1E42) //Tredecillions
        {
            return (c / 1E42).ToString("F2") + "Tre";
        }
        else if (c >= 1E39) //Duodecillions
        {
            return (c / 1E39).ToString("F2") + "Duo";
        }
        else if (c >= 1E36) //Undecillions
        {
            return (c / 1E36).ToString("F2") + "Un";
        }
        else //Decillions
        {
            return (c / 1E33).ToString("F2") + "Dec";
        }
    }

    public string CoinConversionNoDecimal2(double c)
    {
        if (c >= 1E33)    //larger than nonillion
        {
            return CoinConversionTwoDecimal3(c);
        }
        if (c >= 1E30) //Nonillions
        {
            return (c / 1E30).ToString() + "Non";
        }
        else if (c >= 1E27) //Octillions
        {
            return (c / 1E27).ToString() + "Oct";
        }
        else if (c >= 1E24) //Septillions
        {
            return (c / 1E24).ToString() + "Sp";
        }
        else if (c >= 1E21) //Sextillions
        {
            return (c / 1E21).ToString() + "Sx";
        }
        else if (c >= 1E18) //Quintillions
        {
            return (c / 1E18).ToString() + "Qn";
        }
        else //Quadtillions
        {
            return (c / 1E15).ToString() + "Qd";
        }
    }
    public string CoinConversionNoDecimal3(double c)
    {
        if (c >= 1E54) return "Infinite";
        else if (c >= 1E51) //Sexdecillions
        {
            return (c / 1E51).ToString() + "Sxd";
        }
        else if (c >= 1E48) //Quindecillion 
        {
            return (c / 1E48).ToString() + "Qnd";
        }
        else if (c >= 1E45) //Quattuordecillions
        {
            return (c / 1E45).ToString() + "Qt";
        }
        else if (c >= 1E42) //Tredecillions
        {
            return (c / 1E42).ToString() + "Tre";
        }
        else if (c >= 1E39) //Duodecillions
        {
            return (c / 1E39).ToString() + "Duo";
        }
        else if (c >= 1E36) //Undecillions
        {
            return (c / 1E36).ToString() + "Un";
        }
        else //Decillions
        {
            return (c / 1E33).ToString() + "Dec";
        }
    }

    //Round coin/gem cost
    public double RoundCostConversion(double c)
    {
        c = Math.Round(c);
        if (c < 1000) //999 and below, return
        {
            return c;
        }
        string s = c.ToString();
        int length = s.Length;
        int subAmount = 0;
        if (c >= 1E15)
        {
            int index = s.IndexOf("E");
            double multiplier = Convert.ToDouble("1" + s.Substring(index));
            c = Convert.ToDouble(s.Substring(0, index));
            s = c.ToString("F2");
            return Convert.ToDouble(s) * multiplier;
        }
        else if (length % 3 == 1)
        {
            s = s.Substring(0, 3);
            subAmount = 3;
        }
        else if (length % 3 == 2)
        {
            s = s.Substring(0, 4);
            subAmount = 4;
        }
        else if (length % 3 == 0)
        {
            s = s.Substring(0, 5);
            subAmount = 5;
        }
        for (int i = 0; i < length - subAmount; i++)
        {
            s += "0";
        }
        return Convert.ToDouble(s);
    }

    //full whole numbers with commas
    public string NormalConversion(double c)
    {
        return c.ToString("N0");
    }

    public void Save()
    {
        StartCoroutine(SaveGame());
    }
    public IEnumerator SaveGame()
    {
        if (!loadData.savingInProgress)
        {
            loadData.savingInProgress = true;
            loadData.autoSaveTimer = 0;
            cloudSaving.CloudSave();
            StartCoroutine(loadData.SaveLocalFile());
            yield return new WaitUntil(() => cloudSaving.cloudDoneSaving);
            cloudSaving.cloudDoneSaving = false;
        }
    }
    public void SaveAndShowPopup()
    {
        Save();
        savingUIPopup.ShowPopup();
    }

    public IEnumerator ExitAndSaveGame()
    {
        loadingScene.ShowLoadingScreen();
        loadData.savingInProgress = true;
        loadData.autoSaveTimer = 0;
        cloudSaving.CloudSave();
        StartCoroutine(loadData.SaveLocalFile());
        yield return new WaitUntil(() => cloudSaving.cloudDoneSaving);
        cloudSaving.cloudDoneSaving = false;
        yield return new WaitForSeconds(1);
        AsyncOperation loadingProgress = SceneManager.LoadSceneAsync(0);
    }

    public void Load(GameData ig)
    {
        //ads = ig.ads;
        coins = ig.coins;
        gems = ig.gems;
        gemsBought = ig.gemsBought;
        currentExp = ig.currentExp;
        maxExp = ig.maxExp;
        playerLevel = ig.playerLevel;
        // Ore
        copper = ig.copper;
        iron = ig.iron;
        gold = ig.gold;
        diamond = ig.diamond;
        //Miner upgrades
        speedChance = ig.speedChance;
        oreChance = ig.oreChance;
        expChance = ig.expChance;
        maxTraits = ig.maxTraits;
        goodTraitsChance = ig.goodTraitsChance;
        badTraitsChance = ig.badTraitsChance;
        terrainTraitsChance = ig.terrainTraitsChance;
        sellOrePower = ig.sellOrePower;
        clickPower = ig.clickPower;
        //Ore upgrades
        copperPrice = ig.copperPrice;
        ironPrice = ig.ironPrice;
        goldPrice = ig.goldPrice;
        diamondPrice = ig.diamondPrice;
        copperSellMultiplier = ig.copperSellMultiplier;
        ironSellMultiplier = ig.ironSellMultiplier;
        goldSellMultiplier = ig.goldSellMultiplier;
        diamondSellMultiplier = ig.diamondSellMultiplier;
        //Prestige Upgrades
        flatSpeed = ig.flatSpeed;
        flatOre = ig.flatOre;
        flatExp = ig.flatExp;
        hiringBoardTimer = ig.hiringBoardTimer;
        specialTraitAmount = ig.specialTraitAmount;
        doubleOreChance = ig.doubleOreChance;
        maxTraitsPlus = ig.maxTraitsPlus;
        gemChance = ig.gemChance;
        clickMultiplier = ig.clickMultiplier;
        //Achievements variables
        totalCoins = ig.totalCoins;
        totalMinersHired = ig.totalMinersHired;
        terraintraitsAmountOnMiner = ig.terraintraitsAmountOnMiner;
        goodtraitsAmountOnMiner = ig.goodtraitsAmountOnMiner;
        badtraitsAmountOnMiner = ig.badtraitsAmountOnMiner;
        highestSpeedOnMiner = ig.highestSpeedOnMiner;
        highestOreOnMiner = ig.highestOreOnMiner;
        highestExpOnMiner = ig.highestExpOnMiner;
        amountOfMinersWorking = ig.amountOfMinersWorking;
        minesUnlocked = ig.minesUnlocked;
        totalCopperSold = ig.totalCopperSold;
        totalIronSold = ig.totalIronSold;
        totalGoldSold = ig.totalGoldSold;
        totalDiamondSold = ig.totalDiamondSold;
        //Settings
        activateConfirmation = ig.activateConfirmation;
        numOfSpins = ig.numOfSpins;
        totalPrestigeGems = ig.totalPrestigeGems;
        prestigeResetCounter = ig.prestigeResetCounter;

        showTutorial = ig.showTutorial;
        hiringTimer.isNowHiring = ig.isNowHiring;
        if (ig.isNowHiring)
        {
            hiringTimer.ShowNowHiring();
        }
        else
        {
            hiringTimer.currentTime = ig.currentHiringTimer;
        }
        //Selling Ores
        sellingOre.copperToSell = ig.copperToSell;
        sellingOre.ironToSell = ig.ironToSell;
        sellingOre.goldToSell = ig.goldToSell;
        sellingOre.diamondToSell = ig.diamondToSell;
        //Set Exp Text
        expBar.SetMaxValue(maxExp);
        playerLevelText.text = "Lv:" + playerLevel;
        expBar.SetCurrentValue(currentExp);
        maxExpText.text = CoinConversionTwoDecimal(currentExp) + "/" + CoinConversionTwoDecimal(maxExp) + " exp";
        UpdateCurrencyText();
        UpdateOresText();
        if (ig.showGem)
        {
            gemSpawn.spawnTimer = gemSpawn.gemCD - 2;   // if gemSpawn is true, spawn it in 2 secs.
        }
        if (ig.showSpinAd)
        {
            spinAd.timer = spinAd.cd - 2;   // if spinAd is true, spawn it in 2 secs.
        }
        if (!ig.reRollBtn)
        {
            loadData.popMiners.rerollBtn.interactable = false;
        }
        loadData.minerSprites.angelicConstumeToggle.isOn = ig.angelicToggle;
        loadData.minerSprites.demonicConstumeToggle.isOn = ig.demonicToggle;
    }

    public void UpdateCurrencyText()
    {
        //Coins
        coinsText.text = CoinConversionTwoDecimal(coins);
        //Gems
        gemsText.text = NormalConversion(gems);
    }
    public void UpdateOresText()
    {
        //Ore
        copperText.text = OreConversion(copper);
        ironText.text = OreConversion(iron);
        goldText.text = OreConversion(gold);
        diamondText.text = OreConversion(diamond);
    }

    //Spawns miners to sell ore.
    //Originally in SellingOre script but since it is an IEnum, exitting out of store will cancel spawning.
    public IEnumerator ActivateSellMiners()
    {
        if (sellingOre.IsSellingOres())
        {
            for (int i = 0; i < sellingOre.inventory.miners.Count; i++)
            {
                if (!sellingOre.inventory.miners[i].gameObject.activeSelf)
                {
                    sellingOre.inventory.miners[i].moveRight = true;
                    sellingOre.inventory.miners[i].spriteRend.flipX = false;
                    sellingOre.inventory.miners[i].gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.3f);
                }
            }
        }
    }

    public void SellBtn()
    {
        if (sellingOre.copperBeingSoldAmount + sellingOre.ironBeingSoldAmount + sellingOre.goldBeingSoldAmount + sellingOre.diamondBeingSoldAmount > 0)
        {
            //subtract ores
            copper -= sellingOre.copperBeingSoldAmount;
            iron -= sellingOre.ironBeingSoldAmount;
            gold -= sellingOre.goldBeingSoldAmount;
            diamond -= sellingOre.diamondBeingSoldAmount;
            UpdateOresText();
            //ores to sell, carried by miners
            sellingOre.copperToSell += sellingOre.copperBeingSoldAmount;
            sellingOre.ironToSell += sellingOre.ironBeingSoldAmount;
            sellingOre.goldToSell += sellingOre.goldBeingSoldAmount;
            sellingOre.diamondToSell += sellingOre.diamondBeingSoldAmount;
            sellingOre.SetSellPile();
            //activate miners to carry ores.
            StartCoroutine(ActivateSellMiners());
            sellingOre.ClearBtn();
            sellingOre.popupText.SetCoinReward("Sold");
            sellingOre.popupText.ShowPopup();
            Save();
        }
    }
}
