using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarryOres : MonoBehaviour
{
    public List<Sprite> oreSpritesList;
    public OreBeingCarried oreBeingCarriedPrefab;
    public List<OreBeingCarried> oresBeingCarriedPool;
    public List<GameObject> conveyorBeltOreSpawn;
    public GameObject orePoolParent;

    public void Start()
    {
        SpawnMoreOres(30);
    }

    //Assign ore to miner
    public OreBeingCarried GetOre(Miners miner, string ore)
    {
        for (int i = 0; i < oresBeingCarriedPool.Count; i++)
        {
            if (!oresBeingCarriedPool[i].gameObject.activeSelf && oresBeingCarriedPool[i].miner == null)
            {
                oresBeingCarriedPool[i].miner = miner;
                oresBeingCarriedPool[i].SetOreSprite(GetOreSprite(ore));
                if (miner.specialTraits.Contains("Huge"))
                {
                    oresBeingCarriedPool[i].gameObject.transform.position = new Vector3(miner.transform.position.x, miner.transform.position.y + 16, miner.transform.position.z);
                }
                else if (miner.specialTraits.Contains("Tiny"))
                {
                    oresBeingCarriedPool[i].gameObject.transform.position = new Vector3(miner.transform.position.x, miner.transform.position.y + 8, miner.transform.position.z);
                }
                else
                {
                    oresBeingCarriedPool[i].gameObject.transform.position = new Vector3(miner.transform.position.x, miner.transform.position.y + 12, miner.transform.position.z);
                }
                oresBeingCarriedPool[i].gameObject.SetActive(true);
                return oresBeingCarriedPool[i];
            }
            else if (i == oresBeingCarriedPool.Count - 5)
            {
                SpawnMoreOres(10);
                i = 0;
            }
        }
        return null;
    }

    public void PutOreOnConveyor(OreBeingCarried obc, string ore)
    {
        obc.miner = null;
        obc.isOnConveyor = true;
        switch (ore)
        {
            case "copper":
                obc.gameObject.transform.position = new Vector3(conveyorBeltOreSpawn[0].transform.position.x, conveyorBeltOreSpawn[0].transform.position.y, obc.transform.position.z);
                break;
            case "iron":
                obc.gameObject.transform.position = new Vector3(conveyorBeltOreSpawn[1].transform.position.x, conveyorBeltOreSpawn[1].transform.position.y, obc.transform.position.z);
                break;
            case "gold":
                obc.gameObject.transform.position = new Vector3(conveyorBeltOreSpawn[2].transform.position.x, conveyorBeltOreSpawn[2].transform.position.y, obc.transform.position.z);
                break;
            case "diamond":
                obc.gameObject.transform.position = new Vector3(conveyorBeltOreSpawn[3].transform.position.x, conveyorBeltOreSpawn[3].transform.position.y, obc.transform.position.z);
                break;
            case "ironGold":
                obc.gameObject.transform.position = new Vector3(conveyorBeltOreSpawn[4].transform.position.x, conveyorBeltOreSpawn[4].transform.position.y, obc.transform.position.z);
                break;
            case "ironDiamond":
                obc.gameObject.transform.position = new Vector3(conveyorBeltOreSpawn[5].transform.position.x, conveyorBeltOreSpawn[5].transform.position.y, obc.transform.position.z);
                break;
            case "goldDiamond":
                obc.gameObject.transform.position = new Vector3(conveyorBeltOreSpawn[6].transform.position.x, conveyorBeltOreSpawn[6].transform.position.y, obc.transform.position.z);
                break;
            case "ironGoldDiamond":
                obc.gameObject.transform.position = new Vector3(conveyorBeltOreSpawn[7].transform.position.x, conveyorBeltOreSpawn[7].transform.position.y, obc.transform.position.z);
                break;
            default:
                break;
        }
    }

    public void SpawnMoreOres(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var o = Instantiate(oreBeingCarriedPrefab, orePoolParent.transform);
            o.gameObject.SetActive(false);
            oresBeingCarriedPool.Add(o);
        }
    }

    public Sprite GetOreSprite(string ore)
    {
        switch (ore)
        {
            case "copper":
                return oreSpritesList[0];
            case "iron":
                return oreSpritesList[1];
            case "gold":
                return oreSpritesList[2];
            case "diamond":
                return oreSpritesList[3];
            case "ironGold":
                return oreSpritesList[4];
            case "ironDiamond":
                return oreSpritesList[5];
            case "goldDiamond":
                return oreSpritesList[6];
            case "ironGoldDiamond":
                return oreSpritesList[7];
            default:
                break;
        }
        return oreSpritesList[0];
    }
}
