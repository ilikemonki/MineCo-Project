using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Miners : MonoBehaviour
{
    public FloatingTextPool ftPool;
    public IdleGame idleGame;
    public GameObject speedBoostIcon;
    public bool moveRight;

    //Stats
    public double speed, orePower, exp;
    public double fixedSpeed, fixedOrePower, fixedExp;
    public double terrainSpeed;
    public double terrainOrePower;
    public double terrainExp;
    public List<string> goodTraits;
    public List<string> badTraits;
    public List<string> terrainTraits;
    public List<string> specialTraits;
    public double minerCost;

    public SpriteRenderer spriteRend;
    public Sprite sprite;
    public Inventory inventory;
    public CarryOres carryOres;
    public OreBeingCarried oreBeingCarried;
    public CoinPopup coinPopup;
    //Terrain affected.
    public bool wasCold, wasHeated, wasPoisoned;
    //Selling Ores
    public double oreBeingSoldAmount;
    public bool copperBeingCarried, ironBeingCarried, goldBeingCarried, diamondBeingCarried;

    public bool isBoosted;
    public double boostTime;
    public double boostTempSpeed;
    public double randDoubleOreValue;

    public void OnDisable()
    {
        if (oreBeingCarried != null)
        {
            oreBeingCarried.gameObject.SetActive(false);
            oreBeingCarried = null;
            copperBeingCarried = false;
            ironBeingCarried = false;
            goldBeingCarried = false;
            diamondBeingCarried = false;
            oreBeingSoldAmount = 0;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (moveRight)
        {
            transform.Translate(1 * Time.deltaTime * (float)speed, 0, 0);
        }
        else
        {
            transform.Translate(-1 * Time.deltaTime * (float)speed, 0, 0);
        }
        

        if (isBoosted)
        {
            boostTime += Time.deltaTime;
            if (boostTime >= 5)
            {
                boostTime = 0;
                speed = boostTempSpeed;
                isBoosted = false;
                speedBoostIcon.SetActive(false);
            }
        }
        
    }

    //When miner hits the store ore tag. Spawn floating text.
    public void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("MineCopper"))
        {
            MineOre("copper");
        }
        else if (trigger.gameObject.CompareTag("MineIron"))
        {
            MineOre("iron");
        }
        else if (trigger.gameObject.CompareTag("MineGold"))
        {
            MineOre("gold");
        }
        else if (trigger.gameObject.CompareTag("MineDiamond"))
        {
            MineOre("diamond");
        }
        else if (trigger.gameObject.CompareTag("MineIronGold"))
        {
            MineOre("ironGold");
        }
        else if (trigger.gameObject.CompareTag("MineIronDiamond"))
        {
            MineOre("ironDiamond");
        }
        else if (trigger.gameObject.CompareTag("MineGoldDiamond"))
        {
            MineOre("goldDiamond");
        }
        else if (trigger.gameObject.CompareTag("MineIronGoldDiamond"))
        {
            MineOre("ironGoldDiamond");
        }

        if (trigger.gameObject.CompareTag("StoreCopperOre") 
            || trigger.gameObject.CompareTag("StoreDoubleCopperOre")
            || trigger.gameObject.CompareTag("StoreTripleCopperOre")
            || trigger.gameObject.CompareTag("StoreQuadCopperOre")
            || trigger.gameObject.CompareTag("StorePentaCopperOre")
            || trigger.gameObject.CompareTag("StoreIronOre") 
            || trigger.gameObject.CompareTag("StoreGoldOre")
            || trigger.gameObject.CompareTag("StoreDiamondOre") 
            || trigger.gameObject.CompareTag("StoreDoubleCopperOre") 
            || trigger.gameObject.CompareTag("StoreIronGoldOre")
            || trigger.gameObject.CompareTag("StoreIronDiamondOre") 
            || trigger.gameObject.CompareTag("StoreGoldDiamondOre") 
            || trigger.gameObject.CompareTag("StoreIronGoldDiamondOre"))
        {
            //Gain exp
            idleGame.GainExp(exp);
            moveRight = false;
            spriteRend.flipX = true;

            if (trigger.gameObject.CompareTag("StoreCopperOre"))
            {
                idleGame.copper += StoreOre(orePower, "copper", true);
                StoreOreConveyor("copper");
            }
            else if (trigger.gameObject.CompareTag("StoreDoubleCopperOre"))
            {
                idleGame.copper += (StoreOre(Math.Round(orePower * 1.5), "copper", true));
                StoreOreConveyor("copper");
            }
            else if (trigger.gameObject.CompareTag("StoreTripleCopperOre"))
            {
                idleGame.copper += (StoreOre(orePower * 2, "copper", true));
                StoreOreConveyor("copper");
            }
            else if (trigger.gameObject.CompareTag("StoreQuadCopperOre"))
            {
                idleGame.copper += (StoreOre(orePower * 3, "copper", true));
                StoreOreConveyor("copper");
            }
            else if (trigger.gameObject.CompareTag("StorePentaCopperOre"))
            {
                idleGame.copper += (StoreOre(orePower * 4, "copper", true) );
                StoreOreConveyor("copper");
            }
            else if (trigger.gameObject.CompareTag("StoreIronOre"))
            {
                idleGame.iron += StoreOre(orePower, "iron", true);
                StoreOreConveyor("iron");
            }
            else if (trigger.gameObject.CompareTag("StoreGoldOre"))
            {
                idleGame.gold += StoreOre(orePower, "gold", true);
                StoreOreConveyor("gold");
            }
            else if (trigger.gameObject.CompareTag("StoreDiamondOre"))
            {
                idleGame.diamond += StoreOre(orePower, "diamond", true);
                StoreOreConveyor("diamond");
            }
            else if (trigger.gameObject.CompareTag("StoreIronGoldOre"))
            {
                idleGame.iron += StoreOre(orePower, "ironGold", false);
                idleGame.gold += StoreOre(orePower, "ironGold", true);
                StoreOreConveyor("ironGold");
            }
            else if (trigger.gameObject.CompareTag("StoreIronDiamondOre"))
            {
                idleGame.iron += StoreOre(orePower, "ironDiamond", false);
                idleGame.diamond += StoreOre(orePower, "ironDiamond", true);
                StoreOreConveyor("ironDiamond");
            }
            else if (trigger.gameObject.CompareTag("StoreGoldDiamondOre"))
            {
                idleGame.diamond += StoreOre(orePower, "goldDiamond", false);
                idleGame.gold += StoreOre(orePower, "goldDiamond", true);
                StoreOreConveyor("goldDiamond");
            }
            else if (trigger.gameObject.CompareTag("StoreIronGoldDiamondOre"))
            {
                idleGame.iron += StoreOre(orePower, "ironGoldDiamond", false);
                idleGame.gold += StoreOre(orePower, "ironGoldDiamond", false);
                idleGame.diamond += StoreOre(orePower, "ironGoldDiamond", true);
                StoreOreConveyor("ironGoldDiamond");
            }
            idleGame.UpdateOresText();
        }
        //At Sell Ore Pile. Miner will pick up ore to sell at the store.
        if (trigger.gameObject.CompareTag("SellOrePile"))
        {
            if (!diamondBeingCarried || !goldBeingCarried || !ironBeingCarried || !copperBeingCarried)
            {
                var overOrePower = Math.Round(orePower * idleGame.sellOrePower);    //Get new orePower with sellMoreOre upgrade.
                if (overOrePower < idleGame.sellingOre.diamondToSell)
                {
                    idleGame.sellingOre.diamondToSell -= overOrePower;
                    oreBeingSoldAmount = overOrePower;
                    diamondBeingCarried = true;
                    idleGame.sellingOre.diamondToSellText.text = idleGame.OreConversion(idleGame.sellingOre.diamondToSell);
                    MineOre("diamond");
                }
                else if (overOrePower >= idleGame.sellingOre.diamondToSell && idleGame.sellingOre.diamondToSell != 0)
                {
                    oreBeingSoldAmount = idleGame.sellingOre.diamondToSell;
                    idleGame.sellingOre.diamondToSell = 0;
                    diamondBeingCarried = true;
                    idleGame.sellingOre.diamondToSellText.text = idleGame.OreConversion(idleGame.sellingOre.diamondToSell);
                    idleGame.sellingOre.diamondToSellImage.gameObject.SetActive(false);
                    MineOre("diamond");
                }
                else if (overOrePower < idleGame.sellingOre.goldToSell)
                {
                    idleGame.sellingOre.goldToSell -= overOrePower;
                    oreBeingSoldAmount = overOrePower;
                    goldBeingCarried = true;
                    idleGame.sellingOre.goldToSellText.text = idleGame.OreConversion(idleGame.sellingOre.goldToSell);
                    MineOre("gold");
                }
                else if (overOrePower >= idleGame.sellingOre.goldToSell && idleGame.sellingOre.goldToSell != 0)
                {
                    oreBeingSoldAmount = idleGame.sellingOre.goldToSell;
                    idleGame.sellingOre.goldToSell = 0;
                    goldBeingCarried = true;
                    idleGame.sellingOre.goldToSellText.text = idleGame.OreConversion(idleGame.sellingOre.goldToSell);
                    idleGame.sellingOre.goldToSellImage.gameObject.SetActive(false);
                    MineOre("gold");
                }
                else if (overOrePower < idleGame.sellingOre.ironToSell)
                {
                    idleGame.sellingOre.ironToSell -= overOrePower;
                    oreBeingSoldAmount = overOrePower;
                    ironBeingCarried = true;
                    idleGame.sellingOre.ironToSellText.text = idleGame.OreConversion(idleGame.sellingOre.ironToSell);
                    MineOre("iron");
                }
                else if (overOrePower >= idleGame.sellingOre.ironToSell && idleGame.sellingOre.ironToSell != 0)
                {
                    oreBeingSoldAmount = idleGame.sellingOre.ironToSell;
                    idleGame.sellingOre.ironToSell = 0;
                    ironBeingCarried = true;
                    idleGame.sellingOre.ironToSellText.text = idleGame.OreConversion(idleGame.sellingOre.ironToSell);
                    idleGame.sellingOre.ironToSellImage.gameObject.SetActive(false);
                    MineOre("iron");
                }
                else if (overOrePower < idleGame.sellingOre.copperToSell)
                {
                    idleGame.sellingOre.copperToSell -= overOrePower;
                    oreBeingSoldAmount = overOrePower;
                    copperBeingCarried = true;
                    idleGame.sellingOre.copperToSellText.text = idleGame.OreConversion(idleGame.sellingOre.copperToSell);
                    MineOre("copper");
                }
                else if (overOrePower >= idleGame.sellingOre.copperToSell && idleGame.sellingOre.copperToSell != 0)
                {
                    oreBeingSoldAmount = idleGame.sellingOre.copperToSell;
                    idleGame.sellingOre.copperToSell = 0;
                    copperBeingCarried = true;
                    idleGame.sellingOre.copperToSellText.text = idleGame.OreConversion(idleGame.sellingOre.copperToSell);
                    idleGame.sellingOre.copperToSellImage.gameObject.SetActive(false);
                    MineOre("copper");
                }
            }
            moveRight = false;
            spriteRend.flipX = true;
        }
        else if (trigger.gameObject.CompareTag("SellOreStore")) //Sell ores and get coins.
        {
            if (diamondBeingCarried)
            {
                SpawnFloatingText(idleGame.CoinConversionTwoDecimal(oreBeingSoldAmount * idleGame.diamondPrice), "coin", false, false);
                idleGame.coins += oreBeingSoldAmount * idleGame.diamondPrice;
                idleGame.totalCoins += oreBeingSoldAmount * idleGame.diamondPrice;
                idleGame.totalDiamondSold += oreBeingSoldAmount;
                diamondBeingCarried = false;
                //set carriedOre to inactive and pop up coins.
                oreBeingCarried.gameObject.SetActive(false);
                coinPopup.SpawnInCoin();
            }
            else if (goldBeingCarried)
            {
                SpawnFloatingText(idleGame.CoinConversionTwoDecimal(oreBeingSoldAmount * idleGame.goldPrice), "coin", false, false);
                idleGame.coins += oreBeingSoldAmount * idleGame.goldPrice;
                idleGame.totalCoins += oreBeingSoldAmount * idleGame.goldPrice;
                idleGame.totalGoldSold += oreBeingSoldAmount;
                goldBeingCarried = false;
                oreBeingCarried.gameObject.SetActive(false);
                coinPopup.SpawnInCoin();
            }
            else if (ironBeingCarried)
            {
                SpawnFloatingText(idleGame.CoinConversionTwoDecimal(oreBeingSoldAmount * idleGame.ironPrice), "coin", false, false);
                idleGame.coins += oreBeingSoldAmount * idleGame.ironPrice;
                idleGame.totalCoins += oreBeingSoldAmount * idleGame.ironPrice;
                idleGame.totalIronSold += oreBeingSoldAmount;
                ironBeingCarried = false;
                oreBeingCarried.gameObject.SetActive(false);
                coinPopup.SpawnInCoin();
            }
            else if (copperBeingCarried)
            {
                SpawnFloatingText(idleGame.CoinConversionTwoDecimal(oreBeingSoldAmount * idleGame.copperPrice), "coin", false, false);
                idleGame.coins += oreBeingSoldAmount * idleGame.copperPrice;
                idleGame.totalCoins += oreBeingSoldAmount * idleGame.copperPrice;
                idleGame.totalCopperSold += oreBeingSoldAmount;
                copperBeingCarried = false;
                oreBeingCarried.gameObject.SetActive(false);
                coinPopup.SpawnInCoin();
            }
            idleGame.UpdateCurrencyText();
            oreBeingSoldAmount = 0;
            moveRight = true;
            spriteRend.flipX = false;
        }
        if (trigger.gameObject.CompareTag("InventoryDoor")) //Go back to house if no more ores to sell.
        {
            if (oreBeingSoldAmount == 0 && !idleGame.sellingOre.IsSellingOres() )
            {
                if (!diamondBeingCarried || !goldBeingCarried || !ironBeingCarried || !copperBeingCarried)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        //spamming the sell btn will make the ore being carried to be disappearing when other sellers go back into house. FIx this.
    }

    //When user clicks on miner.
    public void OnMouseDown()
    {
        Boosted();
    }

    public void Boosted()
    {
        if (gameObject.activeSelf)
        {
            if (isBoosted)
            {
                boostTime = 0;
            }
            else
            {
                speedBoostIcon.SetActive(true);
                boostTempSpeed = speed;
                if (boostTempSpeed < 65)
                {
                    speed = Math.Round(speed * 1.25);
                    if (speed > 65) speed = 65;   //set max speed to 65.
                }
                isBoosted = true;
            }
        }
    }

    //In snow terrain, set speed to half.
    public void IsCold(bool coldTerrain)
    {
        if (coldTerrain && !wasCold)   //sent from non-snow to snow terrain
        {
            this.terrainSpeed = Math.Round(fixedSpeed / 2);
            if (terrainSpeed <= 0)
            {
                terrainSpeed = 1;
            }
            this.speed = terrainSpeed;
            wasCold = true;
        }
        else if (!coldTerrain && wasCold)  //sent from snow to non-snow terrain
        {
            this.speed = fixedSpeed;
            wasCold = false;
        }
    }
    //In poison terrain, set exp to half.
    public void IsPoisoned(bool poisonedTerrain)
    {
        if (poisonedTerrain && !wasPoisoned)   //sent from non-poison to poison terrain
        {
            this.terrainExp = Math.Round(fixedExp / 2);
            if (terrainExp <= 0)
            {
                terrainExp = 1;
            }
            this.exp = terrainExp;
            wasPoisoned = true;
        }
        else if (!poisonedTerrain && wasPoisoned)  //sent from poison to non-poison terrain
        {
            this.exp = fixedExp;
            wasPoisoned = false;
        }
    }
    //In lava terrain, set ore to half.
    public void IsHeated(bool heatedTerrain)
    {
        if (heatedTerrain && !wasHeated)   //sent from non-lava to lava terrain
        {
            this.terrainOrePower = Math.Round(fixedOrePower / 2);
            if (terrainOrePower <= 0)
            {
                terrainOrePower = 1;
            }
            this.orePower = terrainOrePower;
            wasHeated = true;
        }
        else if (!heatedTerrain && wasHeated)  //sent from lava to non-lava terrain
        {
            this.orePower = fixedOrePower;
            wasHeated = false;
        }
        else
        {
            return;
        }
    }

    //checks for doubleOre, spawn floating text, and return the orePower.
    public double StoreOre(double oreAmount, string oreType, bool showFloatingText)
    {
        randDoubleOreValue = UnityEngine.Random.value;
        if (showFloatingText)
        {
            if (randDoubleOreValue <= (idleGame.doubleOreChance / 100))
            {
                SpawnFloatingText(idleGame.OreConversion(oreAmount * 2), oreType, GemChance(), true);
                return oreAmount * 2;
            }
            SpawnFloatingText(idleGame.OreConversion(oreAmount), oreType, GemChance(), false);
            return oreAmount;
        }
        else
        {
            if (randDoubleOreValue <= (idleGame.doubleOreChance / 100))
            {
                return oreAmount * 2;
            }
            return oreAmount;
        }
    }

    public bool GemChance()
    {
        if (idleGame.gemChance > 0)
        {
            if (UnityEngine.Random.value <= (idleGame.gemChance / 100))
            {
                idleGame.gems++;
                idleGame.UpdateCurrencyText();
                return true;
            }
        }
        return false;
    }

    public void SpawnFloatingText(string amount, string oreType, bool spawnGem, bool doubleOre)
    {
        ftPool.SpawnFloatingText(this, oreType, amount, spawnGem, doubleOre);
    }

    public void MineOre(string ore)
    {
        moveRight = true;
        spriteRend.flipX = false;
        oreBeingCarried = carryOres.GetOre(this, ore);   // Carry ore visual
    }

    public void StoreOreConveyor(string ore)
    {
        if (oreBeingCarried != null)
            carryOres.PutOreOnConveyor(oreBeingCarried, ore);
    }

    public void SetScale(float x, float y)
    {
        this.transform.localScale = new Vector2(x, y);
    }
    public void Initialize(IdleGame ig, CarryOres co, CoinPopup cp, FloatingTextPool ftp)
    {
        this.idleGame = ig;
        this.carryOres = co;
        this.coinPopup = cp;
        this.ftPool = ftp;
    }

    public void SetInventory(Inventory inv)
    {
        this.inventory = inv;
    }

    public void DestroyMiner()
    {
        Destroy(gameObject);
    }


}
