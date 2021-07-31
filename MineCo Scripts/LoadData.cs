using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Serializers;
using System;


public class LoadData : MonoBehaviour
{
    public string fileName = "Game.dat";
    public bool savingInProgress;
    public float autoSaveTimer, autoSaveCD;
    public IdleGame idleGame;
    public Wheel wheel;
    public MinerSprites minerSprites;
    public InventoryUI[] invUIList;
    public Achievements[] achievesList;
    public Costumes[] costumesList;
    public PrestigeUpgrade[] prestigeUpgradeList;
    public Upgrade[] upgradeList;
    public PopulateHiringMiners popMiners;
    public Tutorial tutorial;
    public LockedMines[] lockedMines;
    public GameData localData, cloudData;

    public void Start()
    {
        if (idleGame != null)
        {
            SaveGame.Serializer = new SaveGameBinarySerializer();
            SaveGame.Encode = true;
            StartCoroutine(LoadSaveFile());
        }
    }
    public void Update()
    {
        if (idleGame != null)
        {
            if (!savingInProgress && !idleGame.showTutorial)
            {
                autoSaveTimer += Time.deltaTime;
                if (autoSaveTimer >= autoSaveCD)
                {
                    idleGame.SaveAndShowPopup();
                }
            }
        }
    }


    public void LoadCostumes(GameData g)
    {
        for (int i = 0; i < costumesList.Length; i++)
        {
            costumesList[i].Load(g.costumeDataList[i]);
            if (costumesList[i].isUnlocked)
            {
                costumesList[i].scrollCostumeImage.color = new Color(255, 255, 255, 255);
                minerSprites.unlockedCostumeMinerSprites.Add(costumesList[i].spriteCostume);
                minerSprites.unlockedCostumeMinerAnimatorController.Add(costumesList[i].animatorController);
                if (costumesList[i].filteredIn)
                {
                    minerSprites.costumeMinerSprites.Add(costumesList[i].spriteCostume);
                    minerSprites.costumeMinerAnimatorController.Add(costumesList[i].animatorController);
                }
            }
        }
    }

    public void LoadSlots(GameData g)
    {
        for (int i = 0; i < invUIList.Length; i++)
        {
            invUIList[i].Load(g.slotsDataList[i]);
        }
    }

    public void LoadMiner(GameData g)
    {
        int minerCounter = 0;
        for (int i = 0; i < invUIList.Length; i++)  //each inventories
        {
            for (int j = 0; j < g.slotsDataList[i].isLocked.Length; j++)     //each slots
            {
                if (g.slotsDataList[i].hasMiner[j] && invUIList[i].slots[j].miner == null)   //if has miner, populate miner.
                {
                    //populate miner and set them to the according slots.
                    invUIList[i].LoadAddMiners(popMiners.LoadMiners(g.minerDataList[minerCounter], invUIList[i].inventory), j);
                    minerCounter++;
                }
            }
        }
        idleGame.sellingOre.SetSellPile();  //set ore pile text
    }
    public IEnumerator SpawnLoadedMiners(int i)  //Spawn miners in the mines.
    {
        for (int j = 0; j < invUIList[i].slots.Length; j++)     //each slots
        {
            if (i < idleGame.minesUnlocked || i == 8)
            {
                if (invUIList[i].slots[j].hasMiner && !invUIList[i].slots[j].isLocked)
                {
                    if (!invUIList[i].slots[j].miner.gameObject.activeSelf)
                    {
                        if (i < 8)  //Set miners to be active in mines.
                        {
                            popMiners.spawnMiner.SpawnToHousing(invUIList[i].slots[j].miner, invUIList[i].slots[j].miner.inventory, true);
                        }
                        else    //Mine Co. and hiring board. Set miners to inactive.
                        {
                            if (idleGame.sellingOre.IsSellingOres())
                            {
                                popMiners.spawnMiner.SpawnToHousing(invUIList[i].slots[j].miner, invUIList[i].slots[j].miner.inventory, true);
                            }
                            else
                            {
                                popMiners.spawnMiner.SpawnToHousing(invUIList[i].slots[j].miner, invUIList[i].slots[j].miner.inventory, false);
                            }
                        }
                        yield return new WaitForSeconds(0.4f);
                    }
                }
            }
        }
    }

    public void LoadUpgrades(GameData g)
    {
        for (int i = 0; i < upgradeList.Length; i++)
        {
            upgradeList[i].Load(g.upgradesDataList[i]);
        }
    }
    public void LoadPrestigeUpgrades(GameData g)
    {
        for (int i = 0; i < prestigeUpgradeList.Length; i++)
        {
            prestigeUpgradeList[i].Load(g.prestigeUpgradeDataList[i]);
        }
    }
    public void LoadAchievements(GameData g)
    {
        for (int i = 0; i < achievesList.Length; i++)
        {
            achievesList[i].Load(g.achieveDataList[i]);
        }
    }

    public void LoadMines()
    {
        for (int i = 0; i < lockedMines.Length; i++)    //locked mines
        {
            if (idleGame.minesUnlocked > 1 && lockedMines[i].id <= idleGame.minesUnlocked)
            {
                lockedMines[i].OpenMine();
                if (lockedMines[i].id - idleGame.minesUnlocked == 0)
                {
                    if (lockedMines[i].nextMineLockButton != null)
                    {
                        lockedMines[i].nextMineLockButton.gameObject.SetActive(true);
                    }
                    if (lockedMines[i].nextMineCostText != null)
                    {
                        lockedMines[i].nextMineCostText.gameObject.SetActive(true);
                    }
                }
                for (int j = 0; j < lockedMines[i].dropDown.Length; j++)
                {
                    lockedMines[i].dropDown[j].options.Add(new Dropdown.OptionData() { text = "Mine " + lockedMines[i].id });
                }
            }
        }
    }

    //Check which save file to load. Also does the tutorial and loading screen.
    public IEnumerator LoadSaveFile()
    {
        SaveGame.Serializer = new SaveGameBinarySerializer();
        SaveGame.Encode = true;
        idleGame.cloudSaving.CloudLoad();   //get file from cloud and set cloudData. Takes a sec.
        yield return new WaitUntil(() => idleGame.cloudSaving.cloudDoneLoading);
        if (SaveGame.Exists(fileName) && Social.localUser.authenticated && idleGame.cloudSaving.cloudFileExist)
        {
            localData = SaveGame.Load<GameData>(fileName);
            yield return new WaitForSeconds(1f);
            if ((localData.saveDate.Date > cloudData.saveDate.Date))
                LoadAll(localData);
            else if (localData.saveDate.Date == cloudData.saveDate.Date)
                if ((cloudData.saveDate - localData.saveDate).TotalSeconds <= 60 && (cloudData.saveDate - localData.saveDate).TotalSeconds >= -60 || (localData.saveDate - cloudData.saveDate).TotalSeconds > 0)
                    LoadAll(localData);
            else
                LoadAll(cloudData);
        }
        else if (SaveGame.Exists(fileName))
        {
            localData = SaveGame.Load<GameData>(fileName);
            yield return new WaitForSeconds(1f);
            LoadAll(localData);
        }
        else if (Social.localUser.authenticated && idleGame.cloudSaving.cloudFileExist)
        {
            LoadAll(cloudData);
        }

        //If new game, show tutorial
        if (idleGame.showTutorial)
        {
            tutorial.gameObject.SetActive(true);
            idleGame.hiringTimer.isNowHiring = true;
            idleGame.hiringTimer.popHiringMiners.Populate();
            idleGame.hiringTimer.ShowNowHiring();
        }
        StartCoroutine(idleGame.loadingScene.ClearLoadingScreen(1.0f));
        idleGame.cloudSaving.cloudDoneLoading = false;
    }

    public IEnumerator SaveLocalFile()
    {
        localData = new GameData(idleGame, invUIList, achievesList, costumesList, prestigeUpgradeList, upgradeList, minerSprites);
        yield return new WaitForSeconds(0.5f);
        SaveGame.Save<GameData>(fileName, localData);
        savingInProgress = false;
    }
    public bool ClearLocalSave()
    {
        if (SaveGame.Exists(fileName))
        {
            SaveGame.Clear();
            return true;
        }
        else return false;
    }
    public void ClearLocalSave2()
    {
        if (SaveGame.Exists(fileName))
        {
            SaveGame.Clear();
        }
    }

    public void LoadAll(GameData gameData)
    {
        idleGame.Load(gameData);
        LoadMines();
        LoadCostumes(gameData);
        LoadUpgrades(gameData);
        LoadPrestigeUpgrades(gameData);
        LoadSlots(gameData);
        LoadAchievements(gameData);

        LoadMiner(gameData);
        for (int i = 0; i < invUIList.Length - 1; i++)  //each inventories
        {
            StartCoroutine(SpawnLoadedMiners(i));
        }
    }
    public GameData GetGameData()
    {
        cloudData = new GameData(idleGame, invUIList, achievesList, costumesList, prestigeUpgradeList, upgradeList, minerSprites);
        return cloudData;
    }

    public void SetCloudData(GameData g)
    {
        cloudData = g;
        if (idleGame != null) idleGame.cloudSaving.cloudDoneLoading = true;
    }

}
