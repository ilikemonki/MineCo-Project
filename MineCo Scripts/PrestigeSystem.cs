using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PrestigeSystem : MonoBehaviour
{
    //Text
    public TextMeshProUGUI prestigeGemText, amountText;
    //Resets
    public GameObject prestigePanel;
    public IdleGame idleGame;
    public Upgrade[] upgradeList;
    public ProgressBar expBar;
    public SellingOre sellingOre;
    public InventoryUI[] invUIList;
    public LockedMines[] lockedMines;
    public OpenInventoryButton mineTab1;
    public Achievements[] achievementsList;
    public HiringTimer hiringTimer;
    public Present present;
    public GemSpawn gemSpawn;
    public CarryOres carryOres;
    public CoinPopup coinPopup;
    public FloatingTextPool ftPool;
    //gems
    public double prestigeSystemGems;
    public double coinGemCheckPoint;
    public bool keepChecking;

    public Confirmation confirmation;
    public PopupText popupText;
    public PopupText popupPrestigeText;
    public OpenInventoryButton mineCoParent;
    public OpenInventoryButton hiringBoardParent;
    public OpenInventoryButton mineParent;
    public LoadData loadData;
    public DropDownInventories dropDownInv;

    public void OnEnable()
    {
        keepChecking = true;
        if (!idleGame.adMob.interstitialAd.IsLoaded())
        {

            if (idleGame.ads)
                idleGame.adMob.RequestInterstitialAd();
        }
        coinGemCheckPoint = 5000 * (1 + (idleGame.totalPrestigeGems / 40));
    }

    public void Update()
    {
        if (prestigePanel.activeSelf)
        {
            if (keepChecking)
            {
                GemCheckPoints();
                prestigeGemText.text = $"<color=green>{ "+" + idleGame.NormalConversion(prestigeSystemGems)}</color>" + " <sprite=5>";
                amountText.text = "Total <sprite=5> :" + idleGame.NormalConversion(idleGame.gems) +
                    $" <color=green>{"(+" + idleGame.NormalConversion(prestigeSystemGems) + ")"}</color>";
            }
        }
    }

    //Check to see how much gems will be gained.
    public void GemCheckPoints()
    {
        prestigeSystemGems = Math.Round(idleGame.totalCoins / coinGemCheckPoint);
        if (prestigeSystemGems > ((idleGame.prestigeResetCounter + 1) * 20))    //First prestige is limited to 20 gems. Increases limit by 20 each iteration.
        {
            prestigeSystemGems = (idleGame.prestigeResetCounter + 1) * 20;
            keepChecking = false;
        }
    }

    public void PrestigeReset()
    {
        idleGame.totalPrestigeGems += prestigeSystemGems;
        CloseMenus();
        idleGame.gems += prestigeSystemGems;
        ResetCurrency();
        ResetInventory();
        ResetUpgrades();
        ResetExp();
        ResetUpgradesText();
        ResetSellOre();
        ResetMineLock();
        ResetSlotCost();
        ResetCarryOres();
        ResetCoinPopup();
        ResetFloatingTextPool();
        ResetGemSpawn();
        ResetAchieves();
        hiringTimer.ResetTimer();
        present.ResetPresentTimer();
        prestigePanel.SetActive(false);
        if (idleGame.ads)
            idleGame.adMob.ShowInterstitialAd();    //show ad

        popupPrestigeText.SetPrestigeText("Prestige Reset\n+" + idleGame.NormalConversion(prestigeSystemGems) + " <sprite=5>");
        popupPrestigeText.ShowPopup();
        prestigeSystemGems = 0;
        idleGame.UpdateCurrencyText();
        idleGame.UpdateOresText();
        idleGame.prestigeResetCounter++;
        idleGame.startResetPop.PopulateAtStart();
    }

    public void PrestigeResetButton()
    {
        if (prestigeSystemGems  <= 0)
        {
            popupText.SetText("Cannot reset with\nzero prestige reset gems.");
            popupText.ShowPopup();
        }
        else if (idleGame.activateConfirmation)
        {
            confirmation.SetListeners("PrestigeReset");  //Set YesBtn onClick with Reset
            confirmation.ShowConfirmationWindow();  //Show window.
        }
        else PrestigeReset();
    }


    public void ResetCurrency()
    {
        idleGame.coins = 0; //Currency
        idleGame.copper = 0;
        idleGame.iron = 0;
        idleGame.gold = 0;
        idleGame.diamond = 0;
    }

    public void ResetUpgrades()
    {
        idleGame.clickPower = 1;
        idleGame.speedChance = 1;    //set upgrades
        idleGame.oreChance = 1;
        idleGame.expChance = 1;
        idleGame.copperPrice = 1;
        idleGame.ironPrice = 3;
        idleGame.goldPrice = 4;
        idleGame.diamondPrice = 10;
        idleGame.copperSellMultiplier = 1;
        idleGame.ironSellMultiplier = 1;
        idleGame.goldSellMultiplier = 1;
        idleGame.diamondSellMultiplier = 1;
        idleGame.sellOrePower = 1;
        idleGame.maxTraits = 0;
        idleGame.goodTraitsChance = 20;
        idleGame.badTraitsChance = 80;
        idleGame.terrainTraitsChance = 10;
    }

    public void ResetAchieves()
    {
        idleGame.minesUnlocked = 1;
        idleGame.totalCoins = 0;
        idleGame.totalMinersHired = 0;
        idleGame.terraintraitsAmountOnMiner = 0;
        idleGame.goodtraitsAmountOnMiner = 0;
        idleGame.badtraitsAmountOnMiner = 0;
        idleGame.highestSpeedOnMiner = 0;
        idleGame.highestOreOnMiner = 0;
        idleGame.highestExpOnMiner = 0;
        idleGame.amountOfMinersWorking = 0;
        idleGame.totalCopperSold = 0;
        idleGame.totalIronSold = 0;
        idleGame.totalGoldSold = 0;
        idleGame.totalDiamondSold = 0;

        for (int i = 0; i < achievementsList.Length; i++)
        {
            if (!achievementsList[i].acceptBtn.gameObject.activeSelf)
            {
                achievementsList[i].keepChecking = true;
            }
        }
    }

    public void ResetUpgradesText()
    {
        for (int i = 0; i < upgradeList.Length; i++)
        {
            upgradeList[i].UpgradeReset();
        }
    }

    public void ResetExp()
    {
        idleGame.currentExp = 0;
        idleGame.maxExp = 10;    //set exp
        idleGame.expBar.SetMaxValue(idleGame.maxExp);
        idleGame.playerLevel = 1;
        expBar.SetCurrentValue(0);
        idleGame.playerLevelText.text = "Lv: " + idleGame.playerLevel;
        idleGame.maxExpText.text = (idleGame.currentExp).ToString("f2") + "/" + idleGame.maxExp.ToString("f2") + " exp";
    }

    public void ResetSellOre()
    {
        sellingOre.ClearBtn();
        sellingOre.copperToSell = 0;
        sellingOre.ironToSell = 0;
        sellingOre.goldToSell = 0;
        sellingOre.diamondToSell = 0;
        sellingOre.copperToSellText.text = "";
        sellingOre.copperToSellImage.gameObject.SetActive(false);
        sellingOre.ironToSellText.text = "";
        sellingOre.ironToSellImage.gameObject.SetActive(false);
        sellingOre.goldToSellText.text = "";
        sellingOre.goldToSellImage.gameObject.SetActive(false);
        sellingOre.diamondToSellText.text = "";
        sellingOre.diamondToSellImage.gameObject.SetActive(false);
    }

    public void ResetMineLock()
    {
        for (int i = 0; i < lockedMines.Length; i++)
        {
            lockedMines[i].mineFront.gameObject.SetActive(true);
            lockedMines[i].inventoryTab.gameObject.SetActive(false);
            lockedMines[i].clickOre.gameObject.SetActive(false);
            if (lockedMines[i].bgAnimation != null)
            {
                lockedMines[i].bgAnimation.SetActive(false);
            }
            if (lockedMines[i].bgAnimation2 != null)
            {
                lockedMines[i].bgAnimation2.SetActive(false);
            }
            if (i == 0)
            {
                lockedMines[i].digBtn.gameObject.SetActive(true);
                lockedMines[i].costText.gameObject.SetActive(true);
            }
            else
            {
                lockedMines[i].digBtn.gameObject.SetActive(false);
                lockedMines[i].costText.gameObject.SetActive(false);
            }
        }
        mineTab1.MineTabsButton();

        //Resetting dropdown options
        if (idleGame.minesUnlocked > 1)
        {
            for (int j = 0; j < dropDownInv.dropDownList.Length; j++)
            {
                if (j == 0 || j == 8)   // mine 1 and Mine Co
                {
                    dropDownInv.dropDownList[j].options.RemoveRange(1, dropDownInv.dropDownList[j].options.Count - 1);
                }
                // hiring board and the rest of the mines
                else dropDownInv.dropDownList[j].options.RemoveRange(2, dropDownInv.dropDownList[j].options.Count - 2);
                dropDownInv.dropDownList[j].value = 0;
            }
        }
    }

    public void ResetSlotCost()
    {
        for (int i = 0; i < invUIList.Length; i++)  // the inventory
        {
            for (int j = 0; j < invUIList[i].lockedSlots.Length; j++)   //the slots
            {
                if (invUIList[i].lockedSlots[j].coinCostText != null && invUIList[i].lockedSlots[j].invSlot.isLocked == false)
                {
                    invUIList[i].lockedSlots[j].invSlot.isLocked = true;
                    invUIList[i].lockedSlots[j].coinCostText.gameObject.SetActive(true);
                    invUIList[i].inventory.maxSpace--;
                }
            }
        }
    }

    public void ResetInventory()
    {
        for (int i = 0; i < invUIList.Length; i++)
        {
            invUIList[i].ClearStats();
            invUIList[i].inventory.ClearAndDestroyAll();
            for (int j = 0; j < 10; j++)
            {
                invUIList[i].slots[j].ClearSlot();
            }

        }
    }

    public void ResetCarryOres()
    {
        if (carryOres.oresBeingCarriedPool.Count > 50)
        {
            for (int i = 50; i < carryOres.oresBeingCarriedPool.Count; i++)
            {
                carryOres.oresBeingCarriedPool[i].DestroyOreBeingCarried();
            }
            carryOres.oresBeingCarriedPool.RemoveRange(50, carryOres.oresBeingCarriedPool.Count - 50);
        }
        for (int i = 0; i < carryOres.oresBeingCarriedPool.Count; i++)
        {
            if (carryOres.oresBeingCarriedPool[i].gameObject.activeSelf)
            {
                carryOres.oresBeingCarriedPool[i].gameObject.SetActive(false);
            }
        }
    }
    public void ResetCoinPopup()
    {
        if (coinPopup.coinsPool.Count > 10)
        {
            for (int i = 10; i < coinPopup.coinsPool.Count; i++)
            {
                coinPopup.coinsPool[i].DestroyCoin();
            }
            coinPopup.coinsPool.RemoveRange(10, coinPopup.coinsPool.Count - 10);
        }
        for (int i = 0; i < coinPopup.coinsPool.Count; i++)
        {
            if (coinPopup.coinsPool[i].gameObject.activeSelf)
            {
                coinPopup.coinsPool[i].gameObject.SetActive(false);
            }
        }
    }

    public void ResetFloatingTextPool()
    {
        if (ftPool.floatingTextPool.Count > 20)
        {
            for (int i = 20; i < ftPool.floatingTextPool.Count; i++)
            {
                ftPool.floatingTextPool[i].DestroyMe();
            }
            ftPool.floatingTextPool.RemoveRange(10, ftPool.floatingTextPool.Count - 20);
        }
        for (int i = 0; i < ftPool.floatingTextPool.Count; i++)
        {
            if (ftPool.floatingTextPool[i].gameObject.activeSelf)
            {
                ftPool.floatingTextPool[i].gameObject.SetActive(false);
            }
        }
    }

    public void ResetGemSpawn()
    {
        gemSpawn.Reset();
    }

    public void CloseMenus()
    {
        if (mineCoParent.gameObject.activeSelf)
        {
            mineCoParent.OpenCloseInventoryButton();
        }
        if (hiringBoardParent.gameObject.activeSelf)
        {
            hiringBoardParent.OpenCloseHiringBoardButton();
        }
        if (mineParent.gameObject.activeSelf)
        {
            mineParent.OpenCloseMineInventoryButton();
        }
    }
}
