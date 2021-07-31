using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using BayatGames.SaveGameFree;

public class InventoryUI : MonoBehaviour
{
    public string id;
    public Transform slotsParent;
    public Inventory inventory;
    public Text speedText;
    public Text oreText;
    public Text expText;
    public Text minerCostText;
    public Button traitPrefab;
    public GameObject traitContentParent;
    public List<Button> traitList;
    public DisplayTraitDescription displayTraitDescription;
    public IdleGame idleGame;
    public double tempMinerCost;
    public string tempMinerCostString;
    public bool doNotShowMinerCostText;
    
    public Color32 green = new Color32(150, 255, 150, 255);  //green for good traits
    public Color32 red = new Color32(255, 150, 150, 255);    //red for bad traits
    public Color32 yellow = new Color32(255, 255, 150, 255);    //yellow for terrain traits
    public Color32 orange = new Color32(255, 200, 100, 255);    //orange for special traits

    public InventorySlots[] slots;
    public LockedSlots[] lockedSlots;
    // Start is called before the first frame update
    public void Start()
    {
        //Add UpdateUI to CallBack. UpdateUI will be called when CallBack.Invoke().
        //inventory.onInventoryAddCallBack += UpdateAddUI;
    }

    public void Update()
    {
        if (minerCostText != null && doNotShowMinerCostText)
        {
            if (idleGame.coins >= tempMinerCost)
            { 
                minerCostText.text = $"<color=Green>{tempMinerCostString}</color>";
            }
            else minerCostText.text = $"<color=Red>{tempMinerCostString}</color>";
        }
    }

    //Everytime Inventory adds or removes, call this method.
    public void UpdateAddUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].isLocked && slots[i].miner == null)   //if slot is open and not occupied
            {
                slots[i].AddMiner(inventory.miners[inventory.miners.Count - 1]);
                return;
            }
        }
    }

    public void LoadAddMiners(Miners m, int slotPos)
    {
        slots[slotPos].AddMiner(m);
    }

    public void CloseUIButton()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    //show stats when user selects a miner.
    public void PopulateStats(Miners m)
    {
        if (minerCostText != null)  //For HiringBoard only. Show Cost of miner.
        {
            doNotShowMinerCostText = true;
            if (m.idleGame.coins >= m.minerCost)
            {
                minerCostText.text = $"<color=Green>{idleGame.OreConversion(m.minerCost)}</color>";
            }
            else minerCostText.text = $"<color=Red>{idleGame.OreConversion(m.minerCost)}</color>";
            tempMinerCost = m.minerCost;
            tempMinerCostString = idleGame.OreConversion(m.minerCost);
        }

        //Show red debuff in Stats
        // is cold, show speed
        if (m.wasCold)  
        {
            speedText.text = idleGame.OreConversion(m.fixedSpeed) + "\n" + $"<color=Red>{idleGame.OreConversion(m.speed)}</color>";
        }
        else
        {
            speedText.text = idleGame.OreConversion(m.fixedSpeed);
        }
        //is hot, show ore
        if (m.wasHeated)
        {
            oreText.text = idleGame.OreConversion(m.fixedOrePower) + "\n" + $"<color=Red>{idleGame.OreConversion(m.orePower)}</color>";
        }
        else if (m.inventory.terrainType.inventoryHouse && idleGame.sellOrePower > 1) //at MineCo. calculate sellMoreOre upgrade
        {
            oreText.text = idleGame.OreConversion(m.fixedOrePower) + "\n" + $"<color=Green>{idleGame.OreConversion(Math.Round(m.orePower * idleGame.sellOrePower))}</color>";
        }
        else
        {
            oreText.text = idleGame.OreConversion(m.fixedOrePower);
        }
        //is poisoned, show exp
        if (m.wasPoisoned)
        {
            expText.text = idleGame.OreConversion(m.fixedExp) + "\n" + $"<color=Red>{idleGame.OreConversion(m.exp)}</color>";
        }
        else
        {
            expText.text = idleGame.OreConversion(m.fixedExp);
        }

    }

    public void ClearStats()
    {
        speedText.text = "";
        oreText.text = "";
        expText.text = "";
        if (minerCostText != null)
        {
            doNotShowMinerCostText = false;
            minerCostText.text = "";
        }
        if (traitList.Count > 0)
        {
            foreach (Button b in traitList)
            {
                Destroy(b.gameObject);
            }
        }
        traitList.Clear();
    }


    //Instantiate trait
    public void PopulateTraits(List<string> goodtraits, List<string> badtraits, List<string> terraintraits, List<string> specialtraits)
    {
        if (traitList.Count > 0)
        {
            foreach (Button b in traitList)
            {
                Destroy(b.gameObject);
            }
        }
        traitList.Clear();

        for (int i = 0; i < specialtraits.Count; i++)
        {
            Button t = Instantiate(traitPrefab, traitContentParent.transform);
            t.GetComponentInChildren<Text>().text = specialtraits[i];
            t.GetComponent<Image>().color = orange;
            t.onClick.AddListener(displayTraitDescription.DisplayDescriptionButton);
            traitList.Add(t);
        }
        for (int i = 0; i < terraintraits.Count; i++)
        {
            Button t = Instantiate(traitPrefab, traitContentParent.transform);
            t.GetComponentInChildren<Text>().text = terraintraits[i];
            t.GetComponent<Image>().color = yellow;
            t.onClick.AddListener(displayTraitDescription.DisplayDescriptionButton);
            traitList.Add(t);
        }
        for (int i = 0; i < goodtraits.Count; i++)
        {
            Button t = Instantiate(traitPrefab, traitContentParent.transform);
            t.GetComponentInChildren<Text>().text = goodtraits[i];
            t.GetComponent<Image>().color = green;
            t.onClick.AddListener(displayTraitDescription.DisplayDescriptionButton);
            traitList.Add(t);
        }
        for (int i = 0; i < badtraits.Count; i++)
        {
            Button t = Instantiate(traitPrefab, traitContentParent.transform);
            t.GetComponentInChildren<Text>().text = badtraits[i];
            t.GetComponent<Image>().color = red;
            t.onClick.AddListener(displayTraitDescription.DisplayDescriptionButton);
            traitList.Add(t);
        }
    }

    public void Load(GameData.SlotsSaveData s)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!s.isLocked[i])    //is unlocked
            {
                if (i != 0)
                {
                    //Load slots
                    if (lockedSlots[i - 1].gemCostText != null) 
                        lockedSlots[i - 1].OpenGemSlot();
                    else lockedSlots[i - 1].OpenCoinSlot();
                }
            }
        }
    }
}
