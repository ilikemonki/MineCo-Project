using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PopulateHiringMiners : MonoBehaviour
{
    //Stats
    public double speed;
    public double orePower;
    public double exp;

    public IdleGame idleGame;
    public Inventory hiringInventory;
    public InventoryUI hiringUI;
    public Miners minerPrefab;
    public CurrentSelectedSlot cSS;
    public MinerSprites minerSprites;
    public CarryOres carryOres;
    public CoinPopup coinPopup;
    public FloatingTextPool ftp;

    public SpawnMiner spawnMiner;
    public Traits traits;
    public GameObject hiringBoardSpawnParent;

    public Button hiringboard;
    public OpenInventoryButton oib;
    public PopupText popupText;
    public bool showAd;
    public Button rerollBtn;
    public int amountToCreate;


    public void OnDisable()
    {
        popupText.HidePopup();
    }

    //Instantiate miner and set stats/traits.
    //Re-stat exisisting miners and create new ones.
    public void Populate()
    {
        if (idleGame.adMob.rewardedAd != null)
        {
            if (!idleGame.adMob.rewardedAd.IsLoaded())
            {
                rerollBtn.interactable = false;
                if (idleGame.ads)
                    idleGame.adMob.RequestRewardedAd();
            }
        }
        //Clear all slots
        for (int i = 0; i < hiringUI.slots.Length; i++)
        {
            if (hiringUI.slots[i].miner != null)
            {
                hiringUI.slots[i].ClearSlot();
            }
        }
        //Re-stat existing miners.
        for (int i = 0; i < hiringInventory.miners.Count; i++)
        {
            hiringInventory.miners[i].gameObject.SetActive(false);
            //Set stats
            hiringInventory.miners[i].speed = RandomizeSpeed();
            hiringInventory.miners[i].orePower = RandomizeOre();
            hiringInventory.miners[i].exp = RandomizeExp();
            //Set traits
            hiringInventory.miners[i].badTraits.Clear();
            hiringInventory.miners[i].goodTraits.Clear();
            hiringInventory.miners[i].terrainTraits.Clear();
            hiringInventory.miners[i].specialTraits.Clear();
            hiringInventory.miners[i].SetScale(1.0f, 1.0f);
            if (idleGame.maxTraits + idleGame.maxTraitsPlus > 0 || minerSprites.costumeMinerSprites.Count > 0)
            {
                traits.PopulateTraits(hiringInventory.miners[i]);
            }
            //Setup sprite and animator, and scale
            GetMinerSprite(hiringInventory.miners[i]);

            if (hiringInventory.miners[i].speed <= 0) hiringInventory.miners[i].speed = 1;  //if stats is 0, set to one.
            if (hiringInventory.miners[i].orePower <= 0) hiringInventory.miners[i].orePower = 1;
            if (hiringInventory.miners[i].exp <= 0) hiringInventory.miners[i].exp = 1;
            //Set fixed stats
            hiringInventory.miners[i].fixedSpeed = hiringInventory.miners[i].speed;
            hiringInventory.miners[i].fixedOrePower = hiringInventory.miners[i].orePower;
            hiringInventory.miners[i].fixedExp = hiringInventory.miners[i].exp;
            //Miner cost
            hiringInventory.miners[i].minerCost = 1;   //Base cost
            hiringInventory.miners[i].minerCost = RandomizeCost(hiringInventory.miners[i]);
            //Set miner to slot
            for (int j = 0; j < hiringUI.slots.Length; j++)
            {
                if (!hiringUI.slots[j].isLocked && hiringUI.slots[j].miner == null)   //if slot is open and not occupied
                {
                    hiringUI.slots[j].AddMiner(hiringInventory.miners[i]);
                    break;
                }
            }

        }
        //instantiate miners if there are open slots
        if (hiringInventory.maxSpace > hiringInventory.miners.Count)
        {
            amountToCreate = hiringInventory.maxSpace - hiringInventory.miners.Count;
            for (int i = 0; i < amountToCreate; i++)
            {
                var m = Instantiate(minerPrefab, hiringBoardSpawnParent.transform);
                m.gameObject.SetActive(false);
                //Set stats
                m.speed = RandomizeSpeed();
                m.orePower = RandomizeOre();
                m.exp = RandomizeExp();
                //Set traits
                if (idleGame.maxTraits + idleGame.maxTraitsPlus > 0 || minerSprites.costumeMinerSprites.Count > 0)
                {
                    traits.PopulateTraits(m);
                }

                //Setup sprite and animator, and scale
                GetMinerSprite(m);

                if (m.speed <= 0) m.speed = 1;  //if stats is 0, set to one.
                if (m.orePower <= 0) m.orePower = 1;
                if (m.exp <= 0) m.exp = 1;
                //set fixed stats
                m.fixedSpeed = m.speed;
                m.fixedOrePower = m.orePower;
                m.fixedExp = m.exp;
                //Miner cost
                m.minerCost = 1;   //Base cost
                //Calculated Cost
                m.minerCost = RandomizeCost(m);
                m.Initialize(idleGame, carryOres, coinPopup, ftp);
                m.SetInventory(hiringInventory);
                hiringInventory.Add(m);
            }
        }
        if (idleGame.adMob.rewardedAd != null)
        {
            if (idleGame.adMob.rewardedAd.IsLoaded())
            {
                rerollBtn.interactable = true;
            }
            else
            {
                rerollBtn.interactable = false;
                if (idleGame.ads)
                    idleGame.adMob.RequestRewardedAd();
            }
        }
    }

    public double RandomizeSpeed()
    {
        if (UnityEngine.Random.value <= .40) //40% chance for good roll.
        {
            return idleGame.RoundCostConversion(UnityEngine.Random.Range(
                (int)Math.Round((10 + idleGame.flatSpeed + idleGame.speedChance * 1.3)), 
                (int)Math.Round((15 + idleGame.flatSpeed) + (idleGame.speedChance * 2.5))));  //max speed w/o traits = 50. w/ traits max = 65.
        }
        else
        {
            return idleGame.RoundCostConversion(UnityEngine.Random.Range(
                (int)Math.Round((8 + idleGame.flatSpeed) + (idleGame.speedChance * 0.8)), 
                (int)Math.Round((12 + idleGame.flatSpeed) + (idleGame.speedChance * 1.5))));
        }
    }

    public double RandomizeOre()
    {
        if (UnityEngine.Random.value <= .40) //40% chance for good roll.
        {
            return idleGame.RoundCostConversion(UnityEngine.Random.Range(
                (int)Math.Round((4 + idleGame.flatOre + idleGame.playerLevel * 0.7) * (idleGame.oreChance * 0.8)), 
                (int)Math.Round((6 + idleGame.flatOre + idleGame.playerLevel) * idleGame.oreChance)));
        }
        else
        {
            return idleGame.RoundCostConversion(UnityEngine.Random.Range(
                (int)Math.Round((3 + idleGame.flatOre + (idleGame.playerLevel * 0.5)) * (idleGame.oreChance * 0.5)), 
                (int)Math.Round((5 + idleGame.flatOre + (idleGame.playerLevel * 0.7)) * (idleGame.oreChance * 0.8))));
        }
    }

    public double RandomizeExp()
    {
        if (UnityEngine.Random.value <= .40) //40% chance for good roll.
        {
            return idleGame.RoundCostConversion(UnityEngine.Random.Range(
                (int)Math.Round((3 + idleGame.flatExp + (idleGame.playerLevel * 0.3)) * (idleGame.expChance * 0.8)), 
                (int)Math.Round((4 + idleGame.flatExp + (idleGame.playerLevel * 0.3)) * idleGame.expChance)));
        }
        else
        {
            return idleGame.RoundCostConversion(UnityEngine.Random.Range(
                (int)Math.Round((2 + idleGame.flatExp + (idleGame.playerLevel * 0.25)) * (idleGame.expChance * 0.5)), 
                (int)Math.Round((3 + idleGame.flatExp + (idleGame.playerLevel * 0.25)) * (idleGame.expChance * 0.8))));
        }
    }

    public double RandomizeCost(Miners m)
    {
        if (m.terrainTraits.Count > 0)
        {
            m.minerCost += 10 * m.terrainTraits.Count;
        }
        if (m.specialTraits.Count > 0)
        {
            m.minerCost += 50 * m.specialTraits.Count;
        }
        if (m.speed >= 60) m.minerCost += 100;
        else if (m.speed >= 50) m.minerCost += 70;
        else if (m.speed >= 40) m.minerCost += 40;
        else if (m.speed >= 30) m.minerCost += 20;
        else if (m.speed >= 20) m.minerCost += 5;
        //FlatCost * (multiplier).
        return idleGame.RoundCostConversion(Math.Round(((m.minerCost) + (m.speed * 1.3) * 0.3) * (((m.orePower * 1.3) + (m.exp * 2.3)) * (1 + m.orePower / 500))));
    }

    public void SearchButton()
    {
        cSS.currentSlotSelected = null;
        cSS.invUI.ClearStats();
        oib.OpenCloseHiringBoardButton();
    }

    // Send miner to inventory or mine, which ever one is opened.
    public void HireButton()
    {
        if (idleGame.coins >= cSS.invUI.tempMinerCost && cSS.currentSlotSelected != null)
        {
            //Hire miner
            if (cSS.invToSendTo.miners.Count < cSS.invToSendTo.maxSpace)
            {
                cSS.invToSendTo.Add(cSS.currentSlotSelected.miner);   //add miner to inv/mine
                RecordAchievements(cSS.currentSlotSelected.miner);
                if (cSS.dropDownText.text.Equals("Mine Co"))
                {
                    //send to inv
                    if (idleGame.sellingOre.IsSellingOres()) //if ores are being sold, spawn miner
                    {
                        spawnMiner.SpawnToHousing(cSS.currentSlotSelected.miner, cSS.invToSendTo, true);
                    }
                    else spawnMiner.SpawnToHousing(cSS.currentSlotSelected.miner, cSS.invToSendTo, false);
                }
                else
                {
                    //send to mine
                    spawnMiner.SpawnToHousing(cSS.currentSlotSelected.miner, cSS.invToSendTo, true);
                }
                idleGame.amountOfMinersWorking++;
                hiringInventory.Remove(cSS.currentSlotSelected.miner);    //remove miner from hiring board
                cSS.currentSlotSelected = null;     //clear selected miner
                cSS.invUI.ClearStats(); //clear displayed stats
                idleGame.coins -= cSS.invUI.tempMinerCost;
                idleGame.UpdateCurrencyText();
            }
            else if (cSS.invToSendTo.miners.Count >= cSS.invToSendTo.maxSpace)
            {
                popupText.SetText("Inventory is full.");
                popupText.ShowPopup();
            }
        }
        else if (idleGame.coins < cSS.invUI.tempMinerCost)
        {
            popupText.SetText("Not enough coins.");
            popupText.ShowPopup();
        }
    }
    //Record variables for achievements
    public void RecordAchievements(Miners m)
    {
        //Record highest miner stats
        if(idleGame.highestSpeedOnMiner < m.speed)
        {
            idleGame.highestSpeedOnMiner = m.speed;
        }
        if (idleGame.highestOreOnMiner < m.orePower)
        {
            idleGame.highestOreOnMiner = m.orePower;
        }
        if (idleGame.highestExpOnMiner < m.exp)
        {
            idleGame.highestExpOnMiner = m.exp;
        }
        if (idleGame.goodtraitsAmountOnMiner < m.goodTraits.Count)
        {
            idleGame.goodtraitsAmountOnMiner = m.goodTraits.Count;
        }
        if (idleGame.badtraitsAmountOnMiner < m.badTraits.Count)
        {
            idleGame.badtraitsAmountOnMiner = m.badTraits.Count;
        }
        if (idleGame.terraintraitsAmountOnMiner < m.terrainTraits.Count)
        {
            idleGame.terraintraitsAmountOnMiner = m.terrainTraits.Count;
        }

        idleGame.totalMinersHired++;    //increase total miners hired
    }

    public void RerollButton()
    {
        rerollBtn.interactable = false;
        if (idleGame.ads)
        {
            showAd = true;
            idleGame.adMob.ShowRewardedAd();
        }
        else
        {
            RerollPopulate();
        }
    }

    public void RerollPopulate() //used for reroll
    {
        cSS.currentSlotSelected = null;
        cSS.invUI.ClearStats();
        cSS.slotSelectedImage.gameObject.SetActive(false);
        Populate();
        rerollBtn.interactable = false;
        popupText.SetText("Rerolling miners");
        popupText.ShowPopup();
    }

    public void GetMinerSprite(Miners m)
    {
        m.gameObject.GetComponent<SpriteRenderer>().sprite = minerSprites.GetRandomSprite(m);
        m.gameObject.GetComponent<Animator>().runtimeAnimatorController = minerSprites.GetAnimatorController();
        m.spriteRend = m.gameObject.GetComponent<SpriteRenderer>();
        m.sprite = m.gameObject.GetComponent<SpriteRenderer>().sprite;
        if (m.specialTraits.Count > 0)
        {
            if (m.specialTraits.Contains("Huge"))
            {
                m.SetScale(1.5f, 1.5f);
            }
            else if (m.specialTraits.Contains("Tiny"))
            {
                m.SetScale(0.5f, 0.5f);
            }
        }
    }

    //Start game/reset with two random miners. One in Mine Co and one in mine1.
    public void StartResetPopulate(Inventory invToSendTo, bool setActive)
    {
        var m = Instantiate(minerPrefab, hiringBoardSpawnParent.transform);
        m.gameObject.SetActive(false);
        //Set stats
        m.speed = RandomizeSpeed();
        m.orePower = RandomizeOre();
        m.exp = RandomizeExp();
        //Set traits
        if (idleGame.maxTraits + idleGame.maxTraitsPlus > 0 || minerSprites.costumeMinerSprites.Count > 0)
        {
            traits.PopulateTraits(m);
        }

        //Setup sprite and animator, and scale
        GetMinerSprite(m);
        if (m.speed <= 0) m.speed = 1;  //if stats is 0, set to one.
        if (m.orePower <= 0) m.orePower = 1;
        if (m.exp <= 0) m.exp = 1;
        //set fixed stats
        m.fixedSpeed = m.speed;
        m.fixedOrePower = m.orePower;
        m.fixedExp = m.exp;

        m.Initialize(idleGame, carryOres, coinPopup, ftp);
        m.SetInventory(invToSendTo);
        invToSendTo.Add(m);
        spawnMiner.SpawnToHousing(m, invToSendTo, setActive);
    }

    public Miners LoadMiners(GameData.MinerData s, Inventory invToSendTo)
    {
        if (s.oreBeingSoldAmount > 0)   //Return ores being sold back to the sell pile.
        {
            if (s.copperBeingCarried)
                idleGame.sellingOre.copperToSell += s.oreBeingSoldAmount;
            else if (s.ironBeingCarried)
                idleGame.sellingOre.ironToSell += s.oreBeingSoldAmount;
            else if (s.goldBeingCarried)
                idleGame.sellingOre.goldToSell += s.oreBeingSoldAmount;
            else
                idleGame.sellingOre.diamondToSell += s.oreBeingSoldAmount;
        }
        var m = Instantiate(minerPrefab, hiringBoardSpawnParent.transform);
        m.gameObject.SetActive(false);
        //Set stats
        m.speed = s.fixedSpeed;
        m.orePower = s.fixedOrePower;
        m.exp = s.fixedExp;
        m.fixedSpeed = s.fixedSpeed;
        m.fixedOrePower = s.fixedOrePower;
        m.fixedExp = s.fixedExp;
        m.goodTraits = s.goodTraits;
        m.badTraits = s.badTraits;
        m.terrainTraits = s.terrainTraits;
        m.specialTraits = s.specialTraits;
        m.minerCost = s.minerCost;
        //Get sprite.
        bool found = false;
        for (int i = 0; i < minerSprites.normalMinerSprites.Count; i++)
        {
            if (minerSprites.normalMinerSprites[i].name.Equals(s.spriteName))
            {
                m.gameObject.GetComponent<SpriteRenderer>().sprite = minerSprites.normalMinerSprites[i];
                m.gameObject.GetComponent<Animator>().runtimeAnimatorController = minerSprites.normalMinerAnimatorController[i];
                found = true;
                break;
            }
        }
        for (int i = 0; i < minerSprites.unlockedCostumeMinerSprites.Count; i++)
        {
            if (found) break;
            if (minerSprites.unlockedCostumeMinerSprites[i].name.Equals(s.spriteName))
            {
                m.gameObject.GetComponent<SpriteRenderer>().sprite = minerSprites.unlockedCostumeMinerSprites[i];
                m.gameObject.GetComponent<Animator>().runtimeAnimatorController = minerSprites.unlockedCostumeMinerAnimatorController[i];
                found = true;
                break;
            }
        }
        for (int i = 0; i < minerSprites.specialMinerSprites.Count; i++)
        {
            if (found) break;
            if (minerSprites.specialMinerSprites[i].name.Equals(s.spriteName))
            {
                m.gameObject.GetComponent<SpriteRenderer>().sprite = minerSprites.specialMinerSprites[i];
                m.gameObject.GetComponent<Animator>().runtimeAnimatorController = minerSprites.specialMinerAnimatorController[i];
                break;
            }
        }
        if (!found)
        {
            m.gameObject.GetComponent<SpriteRenderer>().sprite = minerSprites.normalMinerSprites[UnityEngine.Random.Range(0, minerSprites.normalMinerSprites.Count)];
            m.gameObject.GetComponent<Animator>().runtimeAnimatorController = minerSprites.normalMinerAnimatorController[UnityEngine.Random.Range(0, minerSprites.normalMinerSprites.Count)];
        }
        m.spriteRend = m.gameObject.GetComponent<SpriteRenderer>();
        m.sprite = m.gameObject.GetComponent<SpriteRenderer>().sprite;
        if (s.minerScale != 1)
        {
            m.SetScale(s.minerScale, s.minerScale);
        }

        m.Initialize(idleGame, carryOres, coinPopup, ftp);
        m.SetInventory(invToSendTo);
        invToSendTo.LoadAddMiner(m);
        return m;
    }
}
