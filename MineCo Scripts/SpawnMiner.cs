using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpawnMiner : MonoBehaviour
{
    public float randomXPos;
    public float yPos;

    public void SpawnToHousing(Miners miner, Inventory inv, bool activateSpawn)
    {
        if (miner.isBoosted)
        {
            miner.boostTime = 0;
            miner.speed = miner.boostTempSpeed;
            miner.isBoosted = false;
            miner.speedBoostIcon.SetActive(false);
        }
        miner.SetInventory(inv);
        //If mine is snow terrain and miner has no cold trait
        if (inv.terrainType != null)
        {
            if (inv.terrainType.snowTerrain && !miner.terrainTraits.Contains("Cold-Resist"))
            {
                miner.IsCold(true);
            }
            else
            {
                miner.IsCold(false);
            }
            //If mine is poison terrain and miner has no poison trait
            if (inv.terrainType.poisonTerrain && !miner.terrainTraits.Contains("Poison-Resist"))
            {
                miner.IsPoisoned(true);
            }
            else
            {
                miner.IsPoisoned(false);
            }
            //If mine is lava terrain and miner has no heat trait
            if (inv.terrainType.lavaTerrain && !miner.terrainTraits.Contains("Heat-Resist"))
            {
                miner.IsHeated(true);
            }
            else
            {
                miner.IsHeated(false);
            }
        }
        miner.transform.SetParent(inv.spawnPoint.transform);  //set miner as child to mine
        randomXPos = ((float)UnityEngine.Random.Range(0, 4) + (float)Math.Round(UnityEngine.Random.value, 2)) * -1; //-0~4
        yPos = randomXPos;
        if (miner.specialTraits.Contains("Huge"))
        {
            miner.transform.localPosition = new Vector3(0, randomXPos + 4.5f, yPos);
        }
        else if (miner.specialTraits.Contains("Tiny"))
        {
            miner.transform.localPosition = new Vector3(0, randomXPos - 4.5f, yPos);
        }
        else
        {
            miner.transform.localPosition = new Vector3(0, randomXPos, yPos);
        }
        if (inv.terrainType.inventoryHouse)
        {
            miner.moveRight = true;
            miner.spriteRend.flipX = false;
        }
        else
        {
            miner.moveRight = false;
            miner.spriteRend.flipX = true;
        }
        miner.gameObject.SetActive(activateSpawn);
    }

}
