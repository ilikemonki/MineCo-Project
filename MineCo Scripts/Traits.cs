using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class Traits : MonoBehaviour
{
    public Miners miner;
    public List<string> possibleGoodTraits = new List<string>{"Happy", "Fast", "Strong", "Fit", "Active", "Experienced", "Smart", "Dexterous", "Useful", "Mighty"};
    public List<string> possibleBadTraits = new List<string> {"Sad", "Slow", "Weak", "Scrawny", "Lazy", "Inexperienced", "Dumb", "Clumsy", "Useless", "Powerless"};
    public List<string> possibleTerrainTraits = new List<string> {"Cold-Resist", "Heat-Resist", "Poison-Resist"};
    public List<string> possibleSpecialTraits = new List<string> {"Demonic", "Angelic", "Huge", "Tiny"};

    public List<string> minerTraits;
    public int numberOfGoodTraits;
    public int numberOfBadTraits;
    public int numberOfTerrainTraits;
    public int numberOfSpecialTraits;
    public List<int> maxTraitsList = new List<int> { 0, 0, 0, 0 }; //bad,good,terrain,special
    public int pickedNumber;
    public List<int> goodTraitsNumberStorage;
    public List<int> badTraitsNumberStorage;
    public List<int> terrainTraitsNumberStorage;
    public List<int> specialTraitsNumberStorage;
    public IdleGame idleGame;

    public float randValue; //0.00-1.00
    public int maxTraitsCount;
    public List<double> statsMultiplier = new List<double> {1, 1, 1};   //0-speed, 1-ore, 2-exp

    //apply trait to miner
    //max stats: spd=+30%, ore=+90%, exp=+85%
    public void ApplyTraits(string chosenTrait)
    {
        switch (chosenTrait)
        {
            //Good traits
            case "Happy":
                statsMultiplier[0] += 0.05;
                statsMultiplier[1] += 0.2;
                statsMultiplier[2] += 0.2;
                break;
            case "Fast":
                statsMultiplier[0] += 0.10;
                break;
            case "Strong":
                statsMultiplier[1] += 0.2;
                break;
            case "Fit":
                statsMultiplier[0] += 0.05;
                statsMultiplier[1] += 0.1;
                break;
            case "Active":
                statsMultiplier[0] += 0.05;
                statsMultiplier[2] += 0.1;
                break;
            case "Experienced":
                statsMultiplier[2] += 0.3;
                break;
            case "Smart":
                statsMultiplier[2] += 0.2;
                break;
            case "Dexterous":
                statsMultiplier[0] += 0.05;
                break;
            case "Useful":
                statsMultiplier[1] += 0.15;
                statsMultiplier[2] += 0.15;
                break;
            case "Mighty":
                statsMultiplier[1] += 0.25;
                break;
            //Bad traits
            case "Sad":
                statsMultiplier[0] -= 0.05;
                statsMultiplier[1] -= 0.20;
                statsMultiplier[2] -= 0.20;
                break;
            case "Slow":
                statsMultiplier[0] -= 0.10;
                break;
            case "Weak":
                statsMultiplier[1] -= 0.20;
                break;
            case "Scrawny":
                statsMultiplier[0] -= 0.05;
                statsMultiplier[1] -= 0.10;
                break;
            case "Lazy":
                statsMultiplier[0] -= 0.05;
                statsMultiplier[2] -= 0.10;
                break;
            case "Inexperienced":
                statsMultiplier[2] -= 0.30;
                break;
            case "Dumb":
                statsMultiplier[2] -= 0.20;
                break;
            case "Clumsy":
                statsMultiplier[0] -= 0.05;
                break;
            case "Useless":
                statsMultiplier[1] -= 0.15;
                miner.exp -= .85;
                break;
            case "Powerless":
                statsMultiplier[1] -= 0.25;
                break;
            //Special Traits
            case "Demonic":
                statsMultiplier[1] += 2;
                statsMultiplier[2] += 1;
                break;
            case "Angelic":
                statsMultiplier[1] += 1;
                statsMultiplier[2] += 2;
                break;
            //Default. will not apply anything if trait doesn't exist / not listed.
            default:
                break;
        }
    }

    //Get the good/bad traits into minerTraits
    //max traits + 1 is the max traits you can have.
    public void GetRandomTraits()
    {
        //Get random number of max traits
        maxTraitsCount = UnityEngine.Random.Range(0, (int)idleGame.maxTraits + (int)idleGame.maxTraitsPlus + 1);
        if (maxTraitsCount == 0) return;
        //each trait gets a random number. These are max possible amount for that type of trait.
        for (int i = 0; i < 4; i++)
        {
            if (maxTraitsCount != 0)
            {
                if (maxTraitsCount > 10)
                {
                    maxTraitsList[i] = UnityEngine.Random.Range(0, 10 + 1); //Get random numbers for each traits
                    maxTraitsCount -= maxTraitsList[i];
                }
                else
                {
                    maxTraitsList[i] = UnityEngine.Random.Range(0, maxTraitsCount + 1); //Get random numbers for each traits
                    maxTraitsCount -= maxTraitsList[i];
                }
            }
            else break;
        }
        if (maxTraitsList[2] > 3) maxTraitsList[2] = 3;
        if (maxTraitsList[3] > 2) maxTraitsList[3] = 2;
        //Calculate the chances of gaining the traits.
        if (maxTraitsList[0] + maxTraitsList[1] + maxTraitsList[2] + maxTraitsList[3] > 0)
        {
            //get bad traits first
            for (int i = 0; i < maxTraitsList[0]; i++)
            {
                randValue = UnityEngine.Random.value;
                if (randValue <= (idleGame.badTraitsChance / 100))  //if rand is larger than trait chance, get traits.
                {
                    numberOfBadTraits++;
                }
            }
            //then get good traits
            for (int i = 0; i < maxTraitsList[1]; i++)
            {
                randValue = UnityEngine.Random.value;
                if (randValue <= (idleGame.goodTraitsChance / 100))  //if rand is less than trait chance, get traits.
                {
                    numberOfGoodTraits++;
                }
            }
            //then get terrain traits
            for (int i = 0; i < maxTraitsList[2]; i++)
            {
                randValue = UnityEngine.Random.value;
                if (randValue <= (idleGame.terrainTraitsChance / 100))  //if rand is less than trait chance, get traits.
                {
                    numberOfTerrainTraits++;
                }
            }
            //then special traits.
            if (idleGame.specialTraitAmount > 0)
            {
                for (int i = 0; i < maxTraitsList[3]; i++)
                {
                    randValue = UnityEngine.Random.value;
                    if (randValue <= 0.15)  //15% chance to get special trait.
                    {
                        numberOfSpecialTraits++;
                    }
                }
            }

            //Randomly pick a trait. Will not pick the same trait again.
            //Get random good traits
            for (int i = 0; i < numberOfGoodTraits; i++)
            {
                do
                {
                    pickedNumber = UnityEngine.Random.Range(0, possibleGoodTraits.Count);   //Get random trait
                }
                while (goodTraitsNumberStorage.Contains(pickedNumber)); //if trait is already picked, pick again.
                goodTraitsNumberStorage.Add(pickedNumber);  //add trait to storage.
            }
            for (int i = 0; i < numberOfGoodTraits; i++)
            {
                minerTraits.Add(possibleGoodTraits[goodTraitsNumberStorage[i]]);    //added to minerTraits to apply to miner
                miner.goodTraits.Add(possibleGoodTraits[goodTraitsNumberStorage[i]]);   //set the traits to miner.
            }
            //Get random bad traits
            for (int i = 0; i < numberOfBadTraits; i++)
            {
                do
                {
                    pickedNumber = UnityEngine.Random.Range(0, possibleBadTraits.Count);
                }
                while (badTraitsNumberStorage.Contains(pickedNumber));
                badTraitsNumberStorage.Add(pickedNumber);
            }
            for (int i = 0; i < numberOfBadTraits; i++)
            {

                minerTraits.Add(possibleBadTraits[badTraitsNumberStorage[i]]);
                miner.badTraits.Add(possibleBadTraits[badTraitsNumberStorage[i]]);
            }
            //Get random Terrain traits
            if (numberOfTerrainTraits == 3) //shortcut to add all.
            {
                for (int i = 0; i < possibleTerrainTraits.Count; i++)
                {
                    terrainTraitsNumberStorage.Add(i);
                }
            }
            else
            {
                for (int i = 0; i < numberOfTerrainTraits; i++)
                {
                    do
                    {
                        if (UnityEngine.Random.value <= 0.6)
                        {
                            pickedNumber = 0;
                            if (terrainTraitsNumberStorage.Contains(pickedNumber))
                            {
                                pickedNumber = UnityEngine.Random.Range(1, possibleTerrainTraits.Count);
                            }
                        }
                        else pickedNumber = UnityEngine.Random.Range(0, possibleTerrainTraits.Count);
                    }
                    while (terrainTraitsNumberStorage.Contains(pickedNumber));
                    terrainTraitsNumberStorage.Add(pickedNumber);
                }
            }
            for (int i = 0; i < numberOfTerrainTraits; i++)
            {

                minerTraits.Add(possibleTerrainTraits[terrainTraitsNumberStorage[i]]);
                miner.terrainTraits.Add(possibleTerrainTraits[terrainTraitsNumberStorage[i]]);
            }
            //Get random Special traits
            for (int i = 0; i < numberOfSpecialTraits; i++)
            {
                do
                {
                    if (i == 1) //if this is the second trait selection, choose from another type. Ex) Cannot have Angelic and Demonic chosen together.
                    {
                        if (pickedNumber == 0 || pickedNumber == 1) //if Angelic/Demonic, choose the other ones.
                        {
                            pickedNumber = UnityEngine.Random.Range(2, possibleSpecialTraits.Count);
                        }
                        else
                        {
                            pickedNumber = UnityEngine.Random.Range(0, 2);
                        }
                    }
                    else
                    {
                        //for 1 speciaal trait only, 60% to be tiny, huge.
                        if (UnityEngine.Random.value <= 0.6)
                            pickedNumber = UnityEngine.Random.Range(2, possibleSpecialTraits.Count);
                        else pickedNumber = UnityEngine.Random.Range(0, 2);
                    }

                }
                while (specialTraitsNumberStorage.Contains(pickedNumber));
                specialTraitsNumberStorage.Add(pickedNumber);
            }
            for (int i = 0; i < numberOfSpecialTraits; i++)
            {
                minerTraits.Add(possibleSpecialTraits[specialTraitsNumberStorage[i]]);
                miner.specialTraits.Add(possibleSpecialTraits[specialTraitsNumberStorage[i]]);
            }
        }
    }

    //Called when creating miner. Get randomTraits and apply them to miner.
    public void PopulateTraits(Miners m)
    {
        this.miner = m;
        GetRandomTraits();
        for (int i = 0; i < numberOfGoodTraits + numberOfBadTraits + numberOfSpecialTraits; i++)
        {
            ApplyTraits(minerTraits[i]);
        }
        miner.speed = Math.Round(miner.speed * statsMultiplier[0]);
        miner.orePower = Math.Round(miner.orePower * statsMultiplier[1]);
        miner.exp = Math.Round(miner.exp * statsMultiplier[2]);
        ResetVariables();
    }

    //reset variables for creating other miners.
    public void ResetVariables()
    {
        statsMultiplier[0] = 1;
        statsMultiplier[1] = 1;
        statsMultiplier[2] = 1;
        for (int i = 0; i < 4; i++)
        {
            maxTraitsList[i] = 0;
        }
        numberOfGoodTraits = 0;
        numberOfBadTraits = 0;
        numberOfTerrainTraits = 0;
        numberOfSpecialTraits = 0;
        minerTraits.Clear();
        goodTraitsNumberStorage.Clear();
        badTraitsNumberStorage.Clear();
        terrainTraitsNumberStorage.Clear();
        specialTraitsNumberStorage.Clear();
    }

    //Show popup text description of the trait
    public string DescriptionTraits(string chosenTrait)
    {
        switch (chosenTrait)
        {
            //Good traits
            case "Happy":
                return "+5% Speed\n+20% Ore\n+20% Exp";
            case "Fast":
                return "+10% Speed";
            case "Strong":
                return "+20% Ore";
            case "Fit":
                return "+5% Speed\n+10% Ore";
            case "Active":
                return "+5% Speed\n+10% Exp";
            case "Experienced":
                return "+30% Exp";
            case "Smart":
                return "+20% Exp";
            case "Dexterous":
                return "+5% Speed";
            case "Useful":
                return "+15% Ore\n+15% Exp";
            case "Mighty":
                return "+25% Ore";
            //Bad traits 
            case "Sad":
                return "-5% Speed\n-20% Ore\n-20% Exp";
            case "Slow":
                return "-10% Speed";
            case "Weak":
                return "-20% Ore";
            case "Scrawny":
                return "-5% Speed\n-10% Ore";
            case "Lazy":
                return "-5% Speed\n-10% Exp";
            case "Inexperienced":
                return "-30% Exp";
            case "Dumb":
                return "-20% Exp";
            case "Clumsy":
                return "-5% Speed";
            case "Useless":
                return "-15% Ore\n-15% Exp";
            case "Powerless":
                return "-25% Ore";
            //Terrain traits
            case "Cold-Resist":
                return "Can mine in snow mines\nwithout getting debuffed.";
            case "Heat-Resist":
                return "Can mine in lava mines\nwithout getting debuffed.";
            case "Poison-Resist":
                return "Can mine in poison mines\nwithout getting debuffed.";
            //Special Traits
            case "Demonic":
                return "Demon-born miner.\n+200% Ore\n+100% Exp";
            case "Angelic":
                return "Angel-born miner.\n+100% Ore\n+200% Exp";
            case "Huge":
                return "Bigger than your mom.\n(Cosmetic)";
            case "Tiny":
                return "Like a mouse.\n(Cosmetic)";
            //Default. will not apply anything if trait doesn't exist.
            default:
                return "Description not found.";
        }
    }

}
