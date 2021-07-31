using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BayatGames.SaveGameFree;

public class Settings : MonoBehaviour
{
    public IdleGame idleGame;
    public Slider bgMusicSlider;
    public Slider sfxSlider;
    public Text bgMusicPercentText, sfxPercentText;
    public Sounds sounds;
    public GameObject mainPage;
    public GameObject aboutPage;
    public GameObject helpPage;
    public Text aboutBodyText;

    public GameObject[] helpPageList;
    public int helpPageNum;
    public Toggle confirmationToggle;
    public Confirmation confirmation;
    public PrestigeSystem prestige;
    public PrestigeUpgrade[] prestigeUpgradeList;
    public Achievements[] achievements;
    public MinerSprites minerSprites;
    public Wheel wheel;
    public SpinAd spinAd;

    public void Update()
    {
        if (Application.isMobilePlatform)
        {
            if (mainPage.activeSelf)
            {
                if (Input.touchCount >= 1)
                {
                    if (Input.touches[0].phase == TouchPhase.Moved)
                    {
                        UpdateSounds();
                    }
                    else if (Input.touches[0].phase == TouchPhase.Ended) Save();
                }
            }
        }
        else
        {
            if (mainPage.activeSelf)
            {
                if (Input.GetMouseButton(0))
                {
                    UpdateSounds();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    Save();
                }
            }
        }
    }

    public void UpdateSounds()
    {
        bgMusicPercentText.text = (bgMusicSlider.value * 100).ToString("F0") + "%";
        sounds.bgAudioSource.volume = bgMusicSlider.value;

        sfxPercentText.text = (sfxSlider.value * 100).ToString("F0") + "%";
        for (int i = 0; i < sounds.sfxSources.Count; i++)
        {
            sounds.sfxSources[i].volume = sfxSlider.value;
        }
    }

    public void OnEnable()
    {
        AboutBackButton();
        helpPage.SetActive(false);
        idleGame.SaveAndShowPopup();
    }

    public void AboutButton()   //send to About page
    {
        mainPage.SetActive(false);
        aboutPage.SetActive(true);
    }

    public void AboutBackButton() //return from About page
    {
        mainPage.SetActive(true);
        aboutPage.SetActive(false);
    }

    public void HelpButton()   //send to Help page
    {
        mainPage.SetActive(false);
        helpPage.SetActive(true);
    }
    public void HelpBackButton() //return from Help page
    {
        mainPage.SetActive(true);
        helpPage.SetActive(false);
    }

    public void BackArrowButton()
    {
        if (helpPageNum >= 1)
        {
            helpPageList[helpPageNum].SetActive(false);
            helpPageNum--;
            helpPageList[helpPageNum].SetActive(true);
        }
    }

    public void ForwardArrowButton()
    {
        if (helpPageNum < helpPageList.Length - 1)
        {
            helpPageList[helpPageNum].SetActive(false);
            helpPageNum++;
            helpPageList[helpPageNum].SetActive(true);
        }
    }

    public void ConfirmToggle()
    {
        if (confirmationToggle.isOn)
        {
            idleGame.activateConfirmation = false;
        }
        else idleGame.activateConfirmation = true;
    }

    //Resets the Game.
    public void ResetGame()
    {
        sounds.StartBG();
        prestige.CloseMenus();
        idleGame.gems = idleGame.gemsBought;
        prestige.ResetCurrency();
        prestige.ResetInventory();
        prestige.ResetUpgrades();
        prestige.ResetExp();
        prestige.ResetUpgradesText();
        prestige.ResetSellOre();
        prestige.ResetMineLock();
        ResetAllSlotCost();
        ResetPrestigeUpgrades();
        ResetAchievementsGained();
        prestige.hiringTimer.ResetTimer();
        prestige.present.ResetPresentTimer();
        prestige.ResetCarryOres();
        prestige.ResetCoinPopup();
        prestige.ResetFloatingTextPool();
        prestige.ResetGemSpawn();
        prestige.ResetAchieves();
        ResetCostumes();
        idleGame.UpdateCurrencyText();
        idleGame.UpdateOresText();
        spinAd.Reset();

        idleGame.startResetPop.PopulateAtStart();
        gameObject.SetActive(false);
    }

    public void ResetGameButton()
    {
        confirmation.SetListeners("ResetGame");  //Set YesBtn onClick with FireFunction
        confirmation.SetText("Are you sure?\nBought gems will be returned.");
        confirmation.ShowConfirmationWindow();  //Show window.
    }

    public void ResetPrestigeUpgrades()
    {
        idleGame.prestigeResetCounter = 0;
        idleGame.clickMultiplier = 1;
        idleGame.flatSpeed = 0; idleGame.flatOre = 0; idleGame.flatExp = 0;
        idleGame.hiringBoardTimer = 120;
        idleGame.specialTraitAmount = 0;
        idleGame.doubleOreChance = 0; idleGame.maxTraitsPlus = 0;
        idleGame.totalPrestigeGems = 0;
        idleGame.gemChance = 0;
        for (int i = 0; i < prestigeUpgradeList.Length; i++)
        {
            prestigeUpgradeList[i].PrestigeSettingsReset();
        }
    }

    public void ResetAchievementsGained()
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            achievements[i].ResetAchievements();
        }
    }

    public void ResetAllSlotCost()
    {
        for (int i = 0; i < prestige.invUIList.Length; i++)  // the inventory
        {
            for (int j = 0; j < prestige.invUIList[i].lockedSlots.Length; j++)   //the slots
            {
                prestige.invUIList[i].lockedSlots[j].invSlot.isLocked = true;
                if (prestige.invUIList[i].lockedSlots[j].coinCostText != null)
                {
                    prestige.invUIList[i].lockedSlots[j].coinCostText.gameObject.SetActive(true);
                }
                else
                {
                    prestige.invUIList[i].lockedSlots[j].gemCostText.gameObject.SetActive(true);
                }
            }
            prestige.invUIList[i].inventory.maxSpace = 1;
        }
    }

    public void ResetCostumes()
    {
        minerSprites.costumeMinerSprites.Clear();
        minerSprites.costumeMinerAnimatorController.Clear();
        for (int i = 0; i < wheel.costumeT1List.Count; i++)
        {
            wheel.costumeT1List[i].isUnlocked = false;
            wheel.costumeT1List[i].filteredIn = false;
            wheel.costumeT1List[i].scrollCostumeImage.color = new Color(0, 0, 0, 255);
        }
        for (int i = 0; i < wheel.costumeT2List.Count; i++)
        {
            wheel.costumeT2List[i].isUnlocked = false;
            wheel.costumeT2List[i].filteredIn = false;
            wheel.costumeT2List[i].scrollCostumeImage.color = new Color(0, 0, 0, 255);
        }
        for (int i = 0; i < wheel.costumeT3List.Count; i++)
        {
            wheel.costumeT3List[i].isUnlocked = false;
            wheel.costumeT3List[i].filteredIn = false;
            wheel.costumeT3List[i].scrollCostumeImage.color = new Color(0, 0, 0, 255);
        }
    }

    //Logout to Main Menu
    public void LogoutButton()
    {
        StartCoroutine(idleGame.ExitAndSaveGame());
    }


    public void Save()
    {
        SaveGame.Save<float>("bgSound", bgMusicSlider.value);
        SaveGame.Save<float>("sfxSound", sfxSlider.value);
    }

    public void Load()
    {
        bgMusicSlider.value = SaveGame.Load<float>("bgSound", 0.5f);
        sfxSlider.value = SaveGame.Load<float>("sfxSound", 0.5f);
        if (idleGame.activateConfirmation)
        {
            confirmationToggle.isOn = false;
        }
        else confirmationToggle.isOn = true;

        UpdateSounds();
    }
}
