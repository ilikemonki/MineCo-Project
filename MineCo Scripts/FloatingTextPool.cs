using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextPool : MonoBehaviour
{
    public FloatingText floatingTextPrefab;
    public List<FloatingText> floatingTextPool;
    public GameObject poolParent;
    // Start is called before the first frame update
    void Start()
    {
        SpawnMoreFT(20);
    }
    //Miner storing ore
    public void SpawnFloatingText(Miners m, string image, string oreAmount, bool hasGem, bool doubleOre)
    {
        for (int i = 0; i < floatingTextPool.Count; i++)
        {
            if (!floatingTextPool[i].gameObject.activeSelf)
            {
                floatingTextPool[i].transform.position = new Vector3(m.transform.position.x, m.transform.position.y + 10, m.transform.position.z);
                SetFTText(floatingTextPool[i], image, oreAmount, hasGem, doubleOre);
                floatingTextPool[i].gameObject.SetActive(true);
                floatingTextPool[i].Spawn();
                return;
            }
            else if (i == floatingTextPool.Count - 5)
            {
                SpawnMoreFT(10);
                i = 0;
            }
        }
    }

    //Clicking ore
    public void SpawnFloatingText(GameObject clickPos, string image, string oreAmount, bool hasGem)
    {
        for (int i = 0; i < floatingTextPool.Count; i++)
        {
            if (!floatingTextPool[i].gameObject.activeSelf)
            {
                floatingTextPool[i].transform.position = new Vector3(clickPos.transform.position.x + Random.Range(-5, 6), clickPos.transform.position.y + Random.Range(-10, 16), clickPos.transform.position.z);
                SetFTText(floatingTextPool[i], image, oreAmount, hasGem, false);
                floatingTextPool[i].gameObject.SetActive(true);
                floatingTextPool[i].Spawn();
                return;
            }
            else if (i == floatingTextPool.Count - 5)
            {
                SpawnMoreFT(10);
                i = 0;
            }
        }
    }

    public void SpawnMoreFT(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var ft = Instantiate(floatingTextPrefab, poolParent.transform);
            ft.gameObject.SetActive(false);
            floatingTextPool.Add(ft);
        }
    }


    public void SetFTText(FloatingText ft, string image, string oreValue, bool spawnGem, bool doubleOre)
    {
        switch (image)
        {
            case "copper":
                if (doubleOre)
                {
                    if (spawnGem)
                    {
                        ft.oreText.text = $"<color=yellow>{"+" + oreValue}</color>" + "<sprite=0>\n" + "+1<sprite=5>";
                    }
                    else ft.oreText.text = $"<color=yellow>{"+" + oreValue}</color>" + "<sprite=0>";
                }
                else
                {
                    if (spawnGem)
                    {
                        ft.oreText.text = "+" + oreValue + "<sprite=0>\n" + "+1<sprite=5>";
                    }
                    else ft.oreText.text = "+" + oreValue + "<sprite=0>";
                }
                break;
            case "iron":
                if (doubleOre)
                {
                    if (spawnGem)
                    {
                        ft.oreText.text = $"<color=yellow>{"+" + oreValue}</color>" + "<sprite=1>\n" + "+1<sprite=5>";
                    }
                    else ft.oreText.text = $"<color=yellow>{"+" + oreValue}</color>" + "<sprite=1>";
                }
                else
                {
                    if (spawnGem)
                    {
                        ft.oreText.text = "+" + oreValue + "<sprite=1>\n" + "+1<sprite=5>";
                    }
                    else ft.oreText.text = "+" + oreValue + "<sprite=1>";
                }
                break;
            case "gold":
                if (doubleOre)
                {
                    if (spawnGem)
                    {
                        ft.oreText.text = $"<color=yellow>{"+" + oreValue}</color>" + "<sprite=2>\n" + "+1<sprite=5>";
                    }
                    else ft.oreText.text = $"<color=yellow>{"+" + oreValue}</color>" + "<sprite=2>";
                }
                else
                {
                    if (spawnGem)
                    {
                        ft.oreText.text = "+" + oreValue + "<sprite=2>\n" + "+1<sprite=5>";
                    }
                    else ft.oreText.text = "+" + oreValue + "<sprite=2>";
                }
                break;
            case "diamond":
                if (doubleOre)
                {
                    if (spawnGem)
                    {
                        ft.oreText.text = $"<color=yellow>{"+" + oreValue}</color>" + "<sprite=3>\n" + "+1<sprite=5>";
                    }
                    else ft.oreText.text = $"<color=yellow>{"+" + oreValue}</color>" + "<sprite=3>";
                }
                else
                {
                    if (spawnGem)
                    {
                        ft.oreText.text = "+" + oreValue + "<sprite=3>\n" + "+1<sprite=5>";
                    }
                    else ft.oreText.text = "+" + oreValue + "<sprite=3>";
                }
                break;
            case "ironGold":
                if (doubleOre)
                {
                    if (spawnGem)
                    {
                        ft.oreText.text = $"<color=yellow>{"+" + oreValue}</color>" + "<sprite=6>\n" + "+1<sprite=5>";
                    }
                    else ft.oreText.text = $"<color=yellow>{"+" + oreValue}</color>" + "<sprite=6>";
                }
                else
                {
                    if (spawnGem)
                    {
                        ft.oreText.text = "+" + oreValue + "<sprite=6>\n" + "+1<sprite=5>";
                    }
                    else ft.oreText.text = "+" + oreValue + "<sprite=6>";
                }
                break;
            case "ironDiamond":
                if (doubleOre)
                {
                    if (spawnGem)
                    {
                        ft.oreText.text = $"<color=yellow>{"+" + oreValue}</color>" + "<sprite=7>\n" + "+1<sprite=5>";
                    }
                    else ft.oreText.text = $"<color=yellow>{"+" + oreValue}</color>" + "<sprite=7>";
                }
                else
                {
                    if (spawnGem)
                    {
                        ft.oreText.text = "+" + oreValue + "<sprite=7>\n" + "+1<sprite=5>";
                    }
                    else ft.oreText.text = "+" + oreValue + "<sprite=7>";
                }
                break;
            case "goldDiamond":
                if (doubleOre)
                {
                    if (spawnGem)
                    {
                        ft.oreText.text = $"<color=yellow>{"+" + oreValue}</color>" + "<sprite=8>\n" + "+1<sprite=5>";
                    }
                    else ft.oreText.text = $"<color=yellow>{"+" + oreValue}</color>" + "<sprite=8>";
                }
                else
                {
                    if (spawnGem)
                    {
                        ft.oreText.text = "+" + oreValue + "<sprite=8>\n" + "+1<sprite=5>";
                    }
                    else ft.oreText.text = "+" + oreValue + "<sprite=8>";
                }
                break;
            case "ironGoldDiamond":
                if (doubleOre)
                {
                    if (spawnGem)
                    {
                        ft.oreText.text = $"<color=yellow>{"+" + oreValue}</color>" + "<sprite=9>\n" + "+1<sprite=5>";
                    }
                    else ft.oreText.text = $"<color=yellow>{"+" + oreValue}</color>" + "<sprite=9>";
                }
                else
                {
                    if (spawnGem)
                    {
                        ft.oreText.text = "+" + oreValue + "<sprite=9>\n" + "+1<sprite=5>";
                    }
                    else ft.oreText.text = "+" + oreValue + "<sprite=9>";
                }
                break;
            case "coin":
                if (doubleOre)
                {
                    if (spawnGem)
                    {
                        ft.oreText.text = $"<color=yellow>{"+" + oreValue}</color>" + "<sprite=4>\n" + "+1<sprite=5>";
                    }
                    else ft.oreText.text = $"<color=yellow>{"+" + oreValue}</color>" + "<sprite=4>";
                }
                else
                {
                    if (spawnGem)
                    {
                        ft.oreText.text = "+" + oreValue + "<sprite=4>\n" + "+1<sprite=5>";
                    }
                    else ft.oreText.text = "+" + oreValue + "<sprite=4>";
                }
                break;
            default:
                break;
        }
    }

}
