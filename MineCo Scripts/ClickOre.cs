using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClickOre : MonoBehaviour
{
    public IdleGame idleGame;
    public FloatingTextPool ftPool;
    public List<GameObject> clickPositions;

    public void ClickCopper()
    {
        if (Math.Round(idleGame.clickPower * idleGame.clickMultiplier) >= 1000)
        {
            ftPool.SpawnFloatingText(clickPos: clickPositions[0], "copper", idleGame.OreConversion(Math.Round(idleGame.clickPower * idleGame.clickMultiplier)), false);
        }
        else
            ftPool.SpawnFloatingText(clickPos: clickPositions[0], "copper", Math.Round(idleGame.clickPower * idleGame.clickMultiplier).ToString(), false);
        idleGame.copper += Math.Round(idleGame.clickPower * idleGame.clickMultiplier);
        idleGame.UpdateOresText();
    }
    public void ClickIron()
    {
        if (Math.Round(idleGame.clickPower * idleGame.clickMultiplier) >= 1000)
        {
            ftPool.SpawnFloatingText(clickPos: clickPositions[1], "iron", idleGame.OreConversion(Math.Round(idleGame.clickPower * idleGame.clickMultiplier)), false);
        }
        else
            ftPool.SpawnFloatingText(clickPos: clickPositions[1], "iron", Math.Round(idleGame.clickPower * idleGame.clickMultiplier).ToString(), false);
        idleGame.iron += Math.Round(idleGame.clickPower * idleGame.clickMultiplier);
        idleGame.UpdateOresText();
    }
    public void ClickGold()
    {
        if (Math.Round(idleGame.clickPower * idleGame.clickMultiplier) >= 1000)
        {
            ftPool.SpawnFloatingText(clickPos: clickPositions[2], "gold", idleGame.OreConversion(Math.Round(idleGame.clickPower * idleGame.clickMultiplier)), false);
        }
        else
            ftPool.SpawnFloatingText(clickPos: clickPositions[2], "gold", Math.Round(idleGame.clickPower * idleGame.clickMultiplier).ToString(), false);
        idleGame.gold += Math.Round(idleGame.clickPower * idleGame.clickMultiplier);
        idleGame.UpdateOresText();
    }
    public void ClickDiamond()
    {
        if (Math.Round(idleGame.clickPower * idleGame.clickMultiplier) >= 1000)
        {
            ftPool.SpawnFloatingText(clickPos: clickPositions[3], "diamond", idleGame.OreConversion(Math.Round(idleGame.clickPower * idleGame.clickMultiplier)), false);
        }
        else
            ftPool.SpawnFloatingText(clickPos: clickPositions[3], "diamond", Math.Round(idleGame.clickPower * idleGame.clickMultiplier).ToString(), false);
        idleGame.diamond += Math.Round(idleGame.clickPower * idleGame.clickMultiplier);
        idleGame.UpdateOresText();
    }
    public void ClickIronGold()
    {
        if (Math.Round(idleGame.clickPower * idleGame.clickMultiplier) >= 1000)
        {
            ftPool.SpawnFloatingText(clickPos: clickPositions[4], "ironGold", idleGame.OreConversion(Math.Round(idleGame.clickPower * idleGame.clickMultiplier)), false);
        }
        else
            ftPool.SpawnFloatingText(clickPos: clickPositions[4], "ironGold", Math.Round(idleGame.clickPower * idleGame.clickMultiplier).ToString(), false);
        idleGame.iron += Math.Round(idleGame.clickPower * idleGame.clickMultiplier);
        idleGame.gold += Math.Round(idleGame.clickPower * idleGame.clickMultiplier);
        idleGame.UpdateOresText();
    }
    public void ClickIronDiamond()
    {
        if (Math.Round(idleGame.clickPower * idleGame.clickMultiplier) >= 1000)
        {
            ftPool.SpawnFloatingText(clickPos: clickPositions[5], "ironDiamond", idleGame.OreConversion(Math.Round(idleGame.clickPower * idleGame.clickMultiplier)), false);
        }
        else
            ftPool.SpawnFloatingText(clickPos: clickPositions[5], "ironDiamond", Math.Round(idleGame.clickPower * idleGame.clickMultiplier).ToString(), false);
        idleGame.iron += Math.Round(idleGame.clickPower * idleGame.clickMultiplier);
        idleGame.diamond += Math.Round(idleGame.clickPower * idleGame.clickMultiplier);
        idleGame.UpdateOresText();
    }
    public void ClickGoldDiamond()
    {
        if (Math.Round(idleGame.clickPower * idleGame.clickMultiplier) >= 1000)
        {
            ftPool.SpawnFloatingText(clickPos: clickPositions[6], "goldDiamond", idleGame.OreConversion(Math.Round(idleGame.clickPower * idleGame.clickMultiplier)), false);
        }
        else
            ftPool.SpawnFloatingText(clickPos: clickPositions[6], "goldDiamond", Math.Round(idleGame.clickPower * idleGame.clickMultiplier).ToString(), false);
        idleGame.gold += Math.Round(idleGame.clickPower * idleGame.clickMultiplier);
        idleGame.diamond += Math.Round(idleGame.clickPower * idleGame.clickMultiplier);
        idleGame.UpdateOresText();
    }
    public void ClickIronGoldDiamond()
    {
        if (Math.Round(idleGame.clickPower * idleGame.clickMultiplier) >= 1000)
        {
            ftPool.SpawnFloatingText(clickPos: clickPositions[7], "ironGoldDiamond", idleGame.OreConversion(Math.Round(idleGame.clickPower * idleGame.clickMultiplier)), false);
        }
        else
            ftPool.SpawnFloatingText(clickPos: clickPositions[7], "ironGoldDiamond", Math.Round(idleGame.clickPower * idleGame.clickMultiplier).ToString(), false);
        idleGame.iron += Math.Round(idleGame.clickPower * idleGame.clickMultiplier);
        idleGame.gold += Math.Round(idleGame.clickPower * idleGame.clickMultiplier);
        idleGame.diamond += Math.Round(idleGame.clickPower * idleGame.clickMultiplier);
        idleGame.UpdateOresText();
    }
}
