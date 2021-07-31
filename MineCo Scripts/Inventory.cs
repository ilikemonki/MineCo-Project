using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stores miners in inventory
public class Inventory : MonoBehaviour
{
    //When inventory is changed/updated. Use for triggering events.
    public delegate void onInventoryChange();
    //public onInventoryChange onInventoryAddCallBack;
    public InventoryUI invUI;
    public GameObject spawnPoint;
    public CurrentSelectedSlot currentSelectedSlot;

    public int maxSpace;
    public List<Miners> miners = new List<Miners>();
    public TerrainType terrainType;
    public IdleGame idleGame;


    public void Add(Miners miner)
    {
        if (miners.Count >= maxSpace)
        {
            return;
        }
        miners.Add(miner);
        if (!gameObject.name.Equals("HiringBoard"))
            idleGame.amountOfMinersWorking++;
        invUI.UpdateAddUI();
    }

    public void Remove(Miners miner)
    {
        miners.Remove(miner);
        idleGame.amountOfMinersWorking--;
        currentSelectedSlot.currentSlotSelected.ClearSlot();
    }

    public void ClearAndDestroyAll()
    {
        for (int i = 0; i < miners.Count; i++)
        {
            miners[i].DestroyMiner();
            idleGame.amountOfMinersWorking--;
        }
        miners.Clear();
    }

    public void LoadAddMiner(Miners miner)
    {
        if (miners.Count >= maxSpace)
        {
            return;
        }
        miners.Add(miner);
    }
}
